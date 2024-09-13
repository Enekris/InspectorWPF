using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.Invertories;
using Inspector.Application.Contracts.Logic.Services.Invertories.Models;
using Inspector.Models;
using Inspector.Services;
using Inspector.Views.Pages;
using Microsoft.Office.Interop.Word;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Inspector.ViewModels.Pages
{
    public partial class GridInvertoriesViewModel : DependencyObject, INotifyPropertyChanged
    {
        internal readonly IInvertoriesService _inventoriesService;
        internal readonly IMapper _mapper;
        private readonly InvertoriesWpf _selectedItem;
        private List<InvertoriesWpf> deleteItems = [];
        public static readonly DependencyProperty ObservableListProperty = DependencyProperty.Register("SelectedItemsDatagrid",
   typeof(ObservableCollection<InvertoriesWpf>), typeof(GridInvertoriesPage), new PropertyMetadata(default(ObservableCollection<InvertoriesWpf>)));
        public ObservableCollection<InvertoriesWpf> SelectedItemsDatagrid { get { return (ObservableCollection<InvertoriesWpf>)GetValue(ObservableListProperty); } set { SetValue(ObservableListProperty, value); } }
        public string ErrorMessage { get; set; }
        public IAsyncCommand SaveCommand { get; private set; }
        public IAsyncCommand RestoreCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand ExportToWordCommand { get; private set; }

        private ObservableCollection<InvertoriesWpf> dbCollection { get; set; }
        public ObservableCollection<InvertoriesWpf> DBCollection
        {
            get { return dbCollection; }
            set
            {
                dbCollection = value;
                OnPropertyChanged(nameof(DBCollection));
            }
        }

        private ObservableCollection<InvertoriesWpf> originalDbCollection { get; set; }
        public int Id
        {
            get { return _selectedItem.Id; }
        }

        public string Number
        {
            get { return _selectedItem.Number; }
            set
            {
                _selectedItem.Number = value;
                OnPropertyChanged("Number");
            }
        }
        public string? Name
        {
            get { return _selectedItem.Name; }
            set
            {
                _selectedItem.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public bool DestructionMark
        {
            get { return _selectedItem.DestructionMark; }
            set
            {
                _selectedItem.DestructionMark = value;
                OnPropertyChanged("DestructionMark");
            }
        }
        public bool ForDestruction
        {
            get { return _selectedItem.ForDestruction; }
            set
            {
                _selectedItem.ForDestruction = value;
                OnPropertyChanged("ForDestruction");
            }
        }


        public GridInvertoriesViewModel(InvertoriesWpf inv, IInvertoriesService inventoriesService, IMapper mapper)
        {
            _selectedItem = inv;

            _inventoriesService = inventoriesService;
            _mapper = mapper;

            DBCollection = [];
            RefreshDataAsync();
            SaveCommand = new AsyncCommand(SaveDataAsync);
            RestoreCommand = new AsyncCommand(RefreshDataAsync);
            AddCommand = new RelayCommand(AddData);
            DeleteCommand = new RelayCommand(DeleteData);
            ExportToWordCommand = new RelayCommand(DataToWord);
            originalDbCollection = [];
            SelectedItemsDatagrid = [];

        }
        public void DeleteData()
        {
            deleteItems = SelectedItemsDatagrid.ToList();

            foreach (var item in deleteItems)
            {
                DBCollection.Remove(item);
            }

        }
        public async System.Threading.Tasks.Task RefreshDataAsync()
        {
            DBCollection?.Clear();
            deleteItems?.Clear();
            originalDbCollection = [];

            foreach (var item in _mapper.Map<List<InvertoriesWpf>>(await _inventoriesService.GetAll()))
            {

                DBCollection.Add(item);
                originalDbCollection.Add(item);

            }

        }


        private async System.Threading.Tasks.Task DeleteDataFromDbAsync()
        {
            try
            {
                if (deleteItems != null && deleteItems.Count >= 0)
                {
                    foreach (var item in deleteItems)
                    {
                        if (item.Id != 0)
                        {
                            await _inventoriesService.DeleteAsync(_mapper.Map<InvertoriesDto>(item));
                        }
                    }
                    deleteItems.Clear();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex, "Ошибка при удалении данных ");

            }
        }
        private async System.Threading.Tasks.Task SaveDataAsync()
        {
            try
            {
                await DeleteDataFromDbAsync();
                foreach (var item in DBCollection)
                {
                    if (item.Id != 0)
                    {
                        await _inventoriesService.UpdateAsync(_mapper.Map<InvertoriesDto>(item));
                    }
                    if (item.Id == 0)
                    {
                        await _inventoriesService.CreateAsync(_mapper.Map<InvertoriesDto>(item));
                    }

                }
                await _inventoriesService.SaveChangesAsync();
                await RefreshDataAsync();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex, "Ошибка при сохранении данных ");

            }
        }

        private void AddData()
        {
            var item = new InvertoriesWpf
            {
            };
            DBCollection.Add(item);

        }

        private static string[] HeaderSelectedItems(string header)
        {
            string[] values = header.Split([',']);
            return values;
        }

        private void DataToWord()
        {
            var selected = new List<InvertoriesWpf>(SelectedItemsDatagrid.Where(i => i.Id != 0));
            var header = HeaderSelectedItems("Id,Инв. Номер,Наименование,На уничтожение,Уничтожено");
            var rows = SelectedItemsDatagrid.Count + 1;
            var columns = header.Length;
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application
            {
                Visible = true
            };
            Document doc = app.Documents.Add();
            doc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;

            Table table = doc.Tables.Add(doc.Range(), rows, columns);
            table.Borders.Enable = 1;
            table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle; // Стиль границы внешнего контура
            table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle; // Стиль внутренних границ
            table.Borders.OutsideColor = WdColor.wdColorBlack; // Цвет внешних границ
            table.Borders.InsideColor = WdColor.wdColorBlack; // Цвет внутренних границ

            for (int i = 0; i < header.Length; i++)
            {
                table.Cell(1, i + 1).Range.Text = header[i];
            }

            for (int i = 2, j = 0; i <= rows; i++, j++)
            {
                table.Cell(i, 1).Range.Text = selected[j].Id.ToString();
                table.Cell(i, 2).Range.Text = selected[j].Number.ToString();
                table.Cell(i, 3).Range.Text = selected[j].Name?.ToString();
                table.Cell(i, 4).Range.Text = selected[j].ForDestruction ? "+" : "";
                table.Cell(i, 5).Range.Text = selected[j].DestructionMark ? "+" : "";

            }

        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
