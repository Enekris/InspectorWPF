using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.DocumentsActReport;
using Inspector.Application.Contracts.Logic.Services.DocumentsActReport.Models;
using Inspector.Application.Contracts.Logic.Services.Invertories;
using Inspector.Application.Contracts.Logic.Services.Volumes;
using Inspector.Models;
using Inspector.Services;
using Inspector.ViewModels.Windows;
using Inspector.Views.EditWindows;
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
    public partial class GridDocumentsActReportViewModel : DependencyObject, INotifyPropertyChanged
    {
        internal readonly IDocumentsActReportService _docService;
        internal readonly IInvertoriesService _inventoriesService;
        internal readonly IVolumesService _volumesService;
        internal readonly IMapper _mapper;
        private readonly DocumentsActReportWpf _selectedItem;
        public static readonly DependencyProperty ObservableListProperty = DependencyProperty.Register("SelectedItemsDatagrid",
    typeof(ObservableCollection<DocumentsActReportWpf>), typeof(GridDocumentsActReportPage), new PropertyMetadata(default(ObservableCollection<DocumentsActReportWpf>)));
        public ObservableCollection<DocumentsActReportWpf> SelectedItemsDatagrid { get { return (ObservableCollection<DocumentsActReportWpf>)GetValue(ObservableListProperty); } set { SetValue(ObservableListProperty, value); } }
        private List<DocumentsActReportWpf> deleteItems = [];

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

        private List<DocumentsActReportWpf> daysLeft = [];

        private ObservableCollection<DocumentsActReportWpf> dbCollection { get; set; }
        public ObservableCollection<DocumentsActReportWpf> DBCollection
        {
            get { return dbCollection; }
            set
            {
                dbCollection = value;
                OnPropertyChanged(nameof(DBCollection));
            }
        }

        private ObservableCollection<DocumentsActReportWpf> originalDbCollection { get; set; }
        public bool IsSelected
        {
            get { return _selectedItem.IsSelected; }
            set
            {
                _selectedItem.IsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        private ObservableCollection<DocumentsActReportWpf> editDbCollection { get; set; }

        public ObservableCollection<DocumentsActReportWpf> EditDbCollection
        {
            get { return editDbCollection; }
            set
            {
                editDbCollection = value;
                OnPropertyChanged(nameof(EditDbCollection));
            }
        }

        private EditGridDocPage editGridDocPage { get; set; }
        public EditGridDocPage? EditGridDocPage
        {
            get { return editGridDocPage; }
            set
            {
                editGridDocPage = value;
                OnPropertyChanged(nameof(EditGridDocPage));
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
        public DateTime? Data
        {
            get { return _selectedItem.Data; }
            set
            {
                _selectedItem.Data = value;
                OnPropertyChanged("Data");
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
                _selectedItem.Page = value;
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

        public GridDocumentsActReportViewModel(DocumentsActReportWpf doc, IDocumentsActReportService documentsActReportService,
            IInvertoriesService inventoriesService, IVolumesService volumesService, IMapper mapper)
        {
            _selectedItem = doc;
            _docService = documentsActReportService;
            _inventoriesService = inventoriesService;
            _volumesService = volumesService;
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
            EditGridDocPage = new EditGridDocPage();
            RefreshEditDataCommandContext();
            EditGridDocPage.Show();
        }

        public void RefreshEditDataCommandContext()
        {
            EditGridDocPage.DataContext = this;
        }
        public void DeleteData()
        {
            deleteItems = SelectedItemsDatagrid.ToList();

            foreach (var item in deleteItems)
            {
                DBCollection.Remove(item);
                if (EditGridDocPage is not null)
                {
                    editDbCollection.Remove(item);
                }
            }

        }
        public async System.Threading.Tasks.Task RefreshDataAsync()
        {
            DBCollection?.Clear();
            deleteItems?.Clear();
            daysLeft.Clear();
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

            foreach (var item in _mapper.Map<List<DocumentsActReportWpf>>(await _docService.GetAll()))// 
            {
                if (item.Data == null)
                {
                    item.PercentRemaining = 100;
                }
                else if (item.Data != null)
                {
                    DateTime endDate = (item.Data ?? DateTime.Now).AddYears(5); // Дата окончания срока годности через 5 лет от начала (или от текущей даты, если начало не указано)
                    double totalDays = (endDate - (item.Data ?? DateTime.Now)).TotalDays; // Общее количество дней между началом и окончанием срока годности
                    double remainingDays = (DateTime.Now - (item.Data ?? DateTime.Now)).TotalDays;  // Количество оставшихся дней до окончания срока годности
                    item.PercentRemaining = (int)(remainingDays / totalDays * 100);
                }
                else if (item.PercentRemaining < 0)
                {
                    item.PercentRemaining = 0;
                }
                else if (item.PercentRemaining > 100)
                {
                    item.PercentRemaining = 100;
                }
                if (item.VolumeId != null)
                {
                    item.VolumeWpf = volList.Find(p => p.Id == item.VolumeId);
                }
                if (item.InventoryId != null)
                {
                    item.InventoryWpf = invList.Find(p => p.Id == item.InventoryId);
                }

                DBCollection.Add(item);
                originalDbCollection.Add(item);
            }

            daysLeft = DBCollection.Where(s => s.Data <= DateTime.Now.AddYears(-5).AddMonths(3)).Where(s => s.DestructionMark == false).ToList();
            SelectedItemsDatagrid?.Clear();

            if (daysLeft.Count >= 1)
            {
                Warning();
            }

            if (EditGridDocPage != null)
            {
                EditGridDocPage.Close();
            }
            await SaveOriginalDataAsync();
        }
        public async System.Threading.Tasks.Task SaveOriginalDataAsync()
        {

            foreach (var item in await _docService.GetAll())
            {
                originalDbCollection.Add(_mapper.Map<DocumentsActReportWpf>(item));
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
                            await _docService.DeleteAsync(_mapper.Map<DocumentsActReportDto>(item));
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
                        await _docService.UpdateAsync(_mapper.Map<DocumentsActReportDto>(item));
                    }
                    if (item.Id == 0)
                    {
                        await _docService.CreateAsync(_mapper.Map<DocumentsActReportDto>(item));
                    }

                }
                await _docService.SaveChangesAsync();
                await RefreshDataAsync();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex, "Ошибка при сохранении данных ");

            }
        }

        private void AddData()
        {
            var item = new DocumentsActReportWpf
            {

            };

            DBCollection.Add(item);
            if (EditGridDocPage is not null)
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
            var selected = new List<DocumentsActReportWpf>(SelectedItemsDatagrid.Where(i => i.Id != 0));
            var header = HeaderSelectedItems("Id,Инв. номер,Дата,Название,Том,Инвент.арь,Страница,На уничтожение,Уничтожено");
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
                table.Cell(i, 2).Range.Text = selected[j].InvNumber.ToString();
                table.Cell(i, 3).Range.Text = selected[j].Data?.ToString();
                table.Cell(i, 4).Range.Text = selected[j].Name?.ToString();
                table.Cell(i, 5).Range.Text = selected[j].VolumeWpf?.InvNumberWithName?.ToString();
                table.Cell(i, 6).Range.Text = selected[j].InventoryWpf?.InvNumberWithName?.ToString();
                table.Cell(i, 7).Range.Text = selected[j].Page?.ToString();
                table.Cell(i, 8).Range.Text = selected[j].ForDestruction ? "+" : "";
                table.Cell(i, 9).Range.Text = selected[j].DestructionMark ? "+" : "";
            }

        }
        private void Warning()
        {

            var warningViewModel = new WarningViewModel<DocumentsActReportWpf>
            {
                WarningMessage1 = "Внимание! истекает срок действия акта",
                DBCollection = daysLeft
            };

            var warningMessageWindow = new WarningPageActR
            {
                DataContext = warningViewModel,
            };

            warningViewModel.CloseCommand = new RelayCommand(() =>
            {
                warningMessageWindow.Close();
            });

            warningMessageWindow.Show();


        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
