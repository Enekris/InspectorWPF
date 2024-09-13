using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.Sertificates;
using Inspector.Application.Contracts.Logic.Services.Volumes;
using Inspector.Application.Contracts.Logic.Services.Volumes.Models;
using Inspector.Models;
using Inspector.Services;
using Inspector.ViewModels.Windows.VolumesTree;
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
    public partial class GridVolumesViewModel : DependencyObject, INotifyPropertyChanged
    {
        internal readonly IVolumesService _volumesService;
        internal readonly IMapper _mapper;
        private List<VolumesWpf> deleteItems = [];

        private readonly VolumesWpf _selectedItem;
        public static readonly DependencyProperty ObservableListProperty = DependencyProperty.Register("SelectedItemsDatagrid",
    typeof(ObservableCollection<VolumesWpf>), typeof(GridVolumesPage), new PropertyMetadata(default(ObservableCollection<VolumesWpf>)));
        public ObservableCollection<VolumesWpf> SelectedItemsDatagrid { get { return (ObservableCollection<VolumesWpf>)GetValue(ObservableListProperty); } set { SetValue(ObservableListProperty, value); } }

        public string ErrorMessage { get; set; }
        public IAsyncCommand SaveCommand { get; private set; }
        public IAsyncCommand RestoreCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public IAsyncCommand ShowTreeCommand { get; private set; }
        public ICommand ExportToWordCommand { get; private set; }
        private ObservableCollection<VolumesWpf> dbCollection { get; set; }
        public ObservableCollection<VolumesWpf> DBCollection
        {
            get { return dbCollection; }
            set
            {
                dbCollection = value;
                OnPropertyChanged(nameof(DBCollection));
            }
        }

        private ObservableCollection<VolumesWpf> originalDbCollection { get; set; }
        public bool IsSelected
        {
            get { return _selectedItem.IsSelected; }
            set
            {
                _selectedItem.IsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        public int Id
        {
            get { return _selectedItem.Id; }
        }

        public string InvNumber
        {
            get { return _selectedItem.InvNumber; }
            set
            {
                _selectedItem.InvNumber = value;
                OnPropertyChanged("InvNumber");
            }
        }
        public string CaseNumber
        {
            get { return _selectedItem.CaseNumber; }
            set
            {
                _selectedItem.CaseNumber = value;
                OnPropertyChanged("CaseNumber");
            }
        }
        public int VolumeNumber
        {
            get { return _selectedItem.VolumeNumber; }
            set
            {
                _selectedItem.VolumeNumber = value;
                OnPropertyChanged("VolumeNumber");
            }
        }
        public string CaseYear
        {
            get { return _selectedItem.CaseYear; }
            set
            {
                _selectedItem.CaseYear = value;
                OnPropertyChanged("SCaseYear");
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


        public GridVolumesViewModel(VolumesWpf vol, IVolumesService volumesService, IMapper mapper,
        ISertificatesService sertificatesService)
        {
            _selectedItem = vol;
            _volumesService = volumesService;
            _mapper = mapper;

            DBCollection = [];
            RefreshDataAsync();
            SaveCommand = new AsyncCommand(SaveDataAsync);
            RestoreCommand = new AsyncCommand(RefreshDataAsync);
            AddCommand = new RelayCommand(AddData);
            DeleteCommand = new RelayCommand(DeleteData);
            ShowTreeCommand = new AsyncCommand(() => ShowTreeAsync(volumesService, mapper, sertificatesService));
            ExportToWordCommand = new RelayCommand(DataToWord);
            SelectedItemsDatagrid = [];
        }

        public static async System.Threading.Tasks.Task ShowTreeAsync(IVolumesService volumesService, IMapper mapper,
       ISertificatesService sertificatesService)
        {
            var treeViewModel = new TreeVolumesViewModel(volumesService, sertificatesService, mapper);
            await treeViewModel.LoadDataAsync();
            var errorMessageWindow = new Views.TreeViewPages.TreeViewPageVolumes
            {
                DataContext = treeViewModel,
            };

            errorMessageWindow.Show();

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

            var volumes = await _volumesService.GetAll();

            foreach (var item in volumes)
            {
                var mappedItem = _mapper.Map<VolumesWpf>(item);
                DBCollection.Add(mappedItem);
                originalDbCollection.Add(mappedItem);
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
                            await _volumesService.DeleteAsync(_mapper.Map<VolumesDto>(item));
                        }
                    }
                    deleteItems.Clear();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex, "Ошибка при сохранении данных ");

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
                        await _volumesService.UpdateAsync(_mapper.Map<VolumesDto>(item));
                    }
                    if (item.Id == 0)
                    {
                        await _volumesService.CreateAsync(_mapper.Map<VolumesDto>(item));
                    }

                }
                await _volumesService.SaveChangesAsync();
                await RefreshDataAsync();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex, "Ошибка при сохранении данных ");

            }
        }

        private void AddData()
        {
            var item = new VolumesWpf
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
            var selected = new List<VolumesWpf>(SelectedItemsDatagrid.Where(i => i.Id != 0));
            var header = HeaderSelectedItems("Id,Инв. номер,Год дела,Номер дела,Номер тома,На уничтожение,Уничтожено");
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
                table.Cell(i, 2).Range.Text = selected[j].InvNumber?.ToString();
                table.Cell(i, 3).Range.Text = selected[j].CaseYear.ToString();
                table.Cell(i, 4).Range.Text = selected[j].CaseNumber.ToString();
                table.Cell(i, 5).Range.Text = selected[j].VolumeNumber.ToString();
                table.Cell(i, 6).Range.Text = selected[j].ForDestruction ? "+" : "";
                table.Cell(i, 7).Range.Text = selected[j].DestructionMark ? "+" : "";
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
