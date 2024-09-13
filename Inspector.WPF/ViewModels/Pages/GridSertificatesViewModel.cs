using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.Invertories;
using Inspector.Application.Contracts.Logic.Services.Sertificates;
using Inspector.Application.Contracts.Logic.Services.Sertificates.Models;
using Inspector.Application.Contracts.Logic.Services.Volumes;
using Inspector.Models;
using Inspector.Services;
using Inspector.ViewModels.Windows;
using Inspector.Views.Pages;
using Inspector.Views.Warning;
using Microsoft.Office.Interop.Word;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace Inspector.ViewModels.Pages
{
    public partial class GridSertificatesViewModel : DependencyObject, INotifyPropertyChanged
    {
        internal readonly ISertificatesService _sertificatesService;
        internal readonly IInvertoriesService _inventoriesService;
        internal readonly IVolumesService _volumesService;
        internal readonly IMapper _mapper;
        private readonly SertificatesWpf _selectedItem;
        public static readonly DependencyProperty ObservableListProperty = DependencyProperty.Register("SelectedItemsDatagrid",
    typeof(ObservableCollection<SertificatesWpf>), typeof(GridSertificatesPage), new PropertyMetadata(default(ObservableCollection<SertificatesWpf>)));
        public ObservableCollection<SertificatesWpf> SelectedItemsDatagrid { get { return (ObservableCollection<SertificatesWpf>)GetValue(ObservableListProperty); } set { SetValue(ObservableListProperty, value); } }
        private List<SertificatesWpf> deleteItems = [];

        public string ErrorMessage { get; set; }
        public IAsyncCommand SaveCommand { get; private set; }
        public IAsyncCommand RestoreCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand WarningCommand { get; private set; }
        public ICommand EditDataCommand { get; private set; }
        public ICommand ExportToWordCommand { get; private set; }
        public List<ItemForCombobox> getVolumeDb { get; set; }
        public List<ItemForCombobox> getInventoryDb { get; set; }

        private List<SertificatesWpf> daysLeft = [];
        private List<SertificatesWpf> intervalDays = [];

        private ObservableCollection<SertificatesWpf> dbCollection { get; set; }
        public ObservableCollection<SertificatesWpf> DBCollection
        {
            get { return dbCollection; }
            set
            {
                dbCollection = value;
                OnPropertyChanged(nameof(DBCollection));
            }
        }

        private ObservableCollection<SertificatesWpf> originalDbCollection { get; set; }

        public bool IsSelected
        {
            get { return _selectedItem.IsSelected; }
            set
            {
                _selectedItem.IsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        private ObservableCollection<SertificatesWpf> editDbCollection { get; set; }

        public ObservableCollection<SertificatesWpf> EditDbCollection
        {
            get { return editDbCollection; }
            set
            {
                editDbCollection = value;
                OnPropertyChanged(nameof(EditDbCollection));
            }
        }

        private Views.EditWindows.EditSertificatesPage editGridSerPage { get; set; }
        public Views.EditWindows.EditSertificatesPage? EditGridSerPage
        {
            get { return editGridSerPage; }
            set
            {
                editGridSerPage = value;
                OnPropertyChanged(nameof(editGridSerPage));
            }
        }
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
        public string Name
        {
            get { return _selectedItem.Name; }
            set
            {
                _selectedItem.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public DateTime DataFirst
        {
            get { return _selectedItem.DataFirst; }
            set
            {
                _selectedItem.DataFirst = value;
                OnPropertyChanged("DataFirst");
            }
        }
        public DateTime DataEnd
        {
            get { return _selectedItem.DataEnd; }
            set
            {
                _selectedItem.DataEnd = value;
                OnPropertyChanged("DataEnd");
            }
        }
        public string Organisation
        {
            get { return _selectedItem.Organisation; }
            set
            {
                _selectedItem.Organisation = value;
                OnPropertyChanged("Organisation");
            }
        }
        public int? VolumeId
        {
            get { return _selectedItem.VolumeId; }
            set
            {
                _selectedItem.VolumeId = value;
                OnPropertyChanged("VolumeId");
            }
        }
        public int? InventoryId
        {
            get { return _selectedItem.InventoryId; }
            set
            {
                _selectedItem.InventoryId = value;
                OnPropertyChanged("InventoryId");
            }
        }
        public int? Page
        {
            get { return _selectedItem.Page; }
            set
            {
                _selectedItem.InventoryId = value;
                OnPropertyChanged("Page");
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
        public int PercentRemaining
        {
            get { return _selectedItem.PercentRemaining; }
            set
            {
                _selectedItem.PercentRemaining = value;
                OnPropertyChanged("PercentRemaining");
            }
        }

        public GridSertificatesViewModel(SertificatesWpf ser, ISertificatesService sertificatesService,
                 IInvertoriesService inventoriesService, IVolumesService volumesService, IMapper mapper)
        {
            _selectedItem = ser;
            _inventoriesService = inventoriesService;
            _volumesService = volumesService;
            _sertificatesService = sertificatesService;
            _mapper = mapper;

            DBCollection = [];
            RefreshDataAsync();
            SaveCommand = new AsyncCommand(SaveDataAsync);
            RestoreCommand = new AsyncCommand(RefreshDataAsync);
            AddCommand = new RelayCommand(AddData);
            DeleteCommand = new RelayCommand(DeleteData);
            EditDataCommand = new RelayCommand(EditData);
            SelectedItemsDatagrid = [];
            WarningCommand = new RelayCommand(Warning);
            ExportToWordCommand = new RelayCommand(DataToWord);

        }

        public void EditData()
        {
            EditDbCollection = SelectedItemsDatagrid;
            EditGridSerPage = new Views.EditWindows.EditSertificatesPage();
            RefreshEditDataCommandContext();
            EditGridSerPage.Show();
        }

        public void RefreshEditDataCommandContext()
        {
            EditGridSerPage.DataContext = this;
        }

        public void DeleteData()
        {
            deleteItems = SelectedItemsDatagrid.ToList();
            foreach (var item in deleteItems)
            {
                DBCollection.Remove(item);
                if (EditGridSerPage is not null)
                {
                    editDbCollection.Remove(item);
                }
            }

        }

        private void Warning()
        {

            var warningViewModel = new WarningViewModel<SertificatesWpf>();
            if (daysLeft.Count >= 1)
            {
                warningViewModel.WarningMessage1 = "Внимание! истекает срок действия аттестата";
                warningViewModel.DBCollection = daysLeft;
            }
            if (intervalDays.Count >= 1)
            {
                warningViewModel.WarningMessage2 = "Внимание! срок контроля";
                warningViewModel.DBCollection1 = intervalDays;
            }

            var warningMessageWindow = new WarningPageSer
            {
                DataContext = warningViewModel,
            };

            warningViewModel.CloseCommand = new RelayCommand(() =>
            {
                warningMessageWindow.Close();
            });

            warningMessageWindow.Show();


        }

        public async System.Threading.Tasks.Task RefreshDataAsync()
        {
            DBCollection?.Clear();
            deleteItems?.Clear();
            daysLeft.Clear();
            intervalDays.Clear();
            getInventoryDb?.Clear();
            getVolumeDb?.Clear();
            originalDbCollection = [];
            var invList = _mapper.Map<List<InvertoriesWpf>>(await _inventoriesService.GetAll());
            var volList = _mapper.Map<List<VolumesWpf>>(await _volumesService.GetAll());

            getInventoryDb = ItemForComboboxService.FillInvNumberWithNameCollection(
                invList,
                item => item.Id,
                item => item.InvNumberWithName
            );

            getVolumeDb = ItemForComboboxService.FillInvNumberWithNameCollection(
                 volList,
                 item => item.Id,
                 item => item.InvNumberWithName
             );


            foreach (var item in _mapper.Map<List<SertificatesWpf>>(await _sertificatesService.GetAll()))
            {
                if (item.VolumeId != null)
                {
                    item.VolumeWpf = volList.Find(p => p.Id == item.VolumeId);
                }
                if (item.InventoryId != null)
                {
                    item.InventoryWpf = invList.Find(p => p.Id == item.InventoryId);
                }

                DateTime endDate = item.DataEnd;
                double totalDays = (endDate - item.DataFirst).TotalDays; // Общая продолжительность в днях
                double remainingDays = (DateTime.Now - item.DataFirst).TotalDays; // Оставшаяся продолжительность в днях
                item.PercentRemaining = (int)(remainingDays / totalDays * 100);

                if (item.PercentRemaining < 0)
                {
                    item.PercentRemaining = 0;
                }
                else if (item.PercentRemaining > 100)
                {
                    item.PercentRemaining = 100;
                }

                DBCollection.Add(item);
                originalDbCollection.Add(item);

            }
            daysLeft = DBCollection.Where(s => s.DataEnd <= DateTime.Now.AddMonths(3)).Where(s => s.DestructionMark == false).ToList();
            intervalDays = DBCollection.Where(s => s.DataEnd <= DateTime.Now.AddYears(2).AddMonths(6) && s.DataEnd >= DateTime.Now.AddYears(2).AddMonths(3)).ToList();

            if (daysLeft.Count >= 1 || intervalDays.Count >= 1)
            {
                Warning();
            }

            SelectedItemsDatagrid?.Clear();
            if (EditGridSerPage != null)
            {
                EditGridSerPage.Close();
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
                            await _sertificatesService.DeleteAsync(_mapper.Map<SertificatesDto>(item));
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
                        await _sertificatesService.UpdateAsync(_mapper.Map<SertificatesDto>(item));
                    }
                    if (item.Id == 0)
                    {
                        await _sertificatesService.CreateAsync(_mapper.Map<SertificatesDto>(item));
                    }

                }
                await _sertificatesService.SaveChangesAsync();
                await RefreshDataAsync();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex, "Ошибка при сохранении данных ");

            }
        }

        private void AddData()
        {
            var item = new SertificatesWpf
            {
            };

            DBCollection.Add(item);
            if (EditGridSerPage is not null)
            {
                EditDbCollection.Add(item);
            }
        }

        private static string[] HeaderSelectedItems(string header)
        {
            string[] values = header.Split([',']);
            return values;
        }

        private void DataToWord()
        {
            var selected = new List<SertificatesWpf>(SelectedItemsDatagrid.Where(i => i.Id != 0));
            var header = HeaderSelectedItems("Id,Номер,Дата выдачи,Наименование,Срок действия,Кем выдан,Том,Инвент.арь,Страница,На уничтожение,Уничтожено");
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
                table.Cell(i, 3).Range.Text = selected[j].DataFirst.ToString();
                table.Cell(i, 4).Range.Text = selected[j].Name?.ToString();
                table.Cell(i, 5).Range.Text = selected[j].DataEnd.ToString();
                table.Cell(i, 6).Range.Text = selected[j].Organisation.ToString();
                table.Cell(i, 7).Range.Text = selected[j].VolumeWpf?.InvNumberWithName?.ToString();
                table.Cell(i, 8).Range.Text = selected[j].InventoryWpf?.InvNumberWithName?.ToString();
                table.Cell(i, 9).Range.Text = selected[j].Page?.ToString();
                table.Cell(i, 10).Range.Text = selected[j].ForDestruction ? "+" : "";
                table.Cell(i, 11).Range.Text = selected[j].DestructionMark ? "+" : "";
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
