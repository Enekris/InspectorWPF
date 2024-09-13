using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.Cabinets;
using Inspector.Application.Contracts.Logic.Services.Cabinets.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsActReport;
using Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV;
using Inspector.Application.Contracts.Logic.Services.Hardwares;
using Inspector.Application.Contracts.Logic.Services.Sertificates;
using Inspector.Models;
using Inspector.Services;
using Inspector.ViewModels.Windows.CabinetsTree;
using Inspector.Views.Pages;
using Microsoft.Office.Interop.Word;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Inspector.ViewModels.Pages
{
    public partial class GridCabinetViewModel : DependencyObject, INotifyPropertyChanged
    {
        private readonly CabinetsWpf _selectedItem;
        private List<CabinetsWpf> deleteItems = [];
        internal readonly ICabinetService _cabinetService;
        internal readonly ISertificatesService _sertificatesService;
        internal readonly IDocumentsRaspOVVService _documentsRaspOVVService;
        internal readonly IDocumentsActReportService _documentsActReportService;
        internal readonly IMapper _mapper;

        public static readonly DependencyProperty ObservableListProperty = DependencyProperty.Register("SelectedItemsDatagrid",
    typeof(ObservableCollection<CabinetsWpf>), typeof(GridCabinetPage), new PropertyMetadata(default(ObservableCollection<CabinetsWpf>)));
        public ObservableCollection<CabinetsWpf> SelectedItemsDatagrid { get { return (ObservableCollection<CabinetsWpf>)GetValue(ObservableListProperty); } set { SetValue(ObservableListProperty, value); } }
        public string ErrorMessage { get; set; }
        public IAsyncCommand SaveCommand { get; private set; }
        public IAsyncCommand RestoreCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public IAsyncCommand ShowTreeCommand { get; private set; }
        public ICommand EditDataCommand { get; private set; }
        public ICommand ExportToWordCommand { get; private set; }

        public List<ItemForCombobox> getSertificateDb { get; set; }

        public List<ItemForCombobox> getDocumentRaspOVVDb { get; set; }

        public List<ItemForCombobox> getDocumentActReportDb { get; set; }
        private ObservableCollection<CabinetsWpf> dbCollection { get; set; }
        public ObservableCollection<CabinetsWpf> DBCollection
        {
            get { return dbCollection; }
            set
            {
                dbCollection = value;
                OnPropertyChanged(nameof(DBCollection));
            }
        }
        private ObservableCollection<CabinetsWpf> originalDbCollection { get; set; }
        private ObservableCollection<CabinetsWpf> editDbCollection { get; set; }

        public ObservableCollection<CabinetsWpf> EditDbCollection
        {
            get { return editDbCollection; }
            set
            {
                editDbCollection = value;
                OnPropertyChanged(nameof(EditDbCollection));
            }
        }

        private Views.EditWindows.EditCabinetPage editGridCabinetPage { get; set; }
        public Views.EditWindows.EditCabinetPage? EditGridCabinetPage
        {
            get { return editGridCabinetPage; }
            set
            {
                editGridCabinetPage = value;
                OnPropertyChanged(nameof(editGridCabinetPage));
            }
        }

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

        public string Number
        {
            get { return _selectedItem.Number; }
            set
            {
                _selectedItem.Number = value;
                OnPropertyChanged("Number");
            }
        }
        public string Address
        {
            get { return _selectedItem.Address; }
            set
            {
                _selectedItem.Address = value;
                OnPropertyChanged("Address");
            }
        }
        public string Status
        {
            get { return _selectedItem.Status; }
            set
            {
                _selectedItem.Status = value;
                OnPropertyChanged("Status");
            }
        }
        public string? ResponsibleExp
        {
            get { return _selectedItem.ResponsibleExp; }
            set
            {
                _selectedItem.ResponsibleExp = value;
                OnPropertyChanged("SResponsibleExp");
            }
        }
        public string? ResponsibleTZI
        {
            get { return _selectedItem.ResponsibleTZI; }
            set
            {
                _selectedItem.ResponsibleTZI = value;
                OnPropertyChanged("ResponsibleTZI");
            }
        }
        public int? SertificateId
        {
            get { return _selectedItem.SertificateId; }
            set
            {
                _selectedItem.SertificateId = value;
                OnPropertyChanged("SertificateId");
            }
        }
        public int? RaspExpId
        {
            get { return _selectedItem.RaspExpId; }
            set
            {
                _selectedItem.RaspExpId = value;
                OnPropertyChanged("RaspExpId");
            }
        }
        public int? ActReportId
        {
            get { return _selectedItem.ActReportId; }
            set
            {
                _selectedItem.ActReportId = value;
                OnPropertyChanged("ActReportId");
            }
        }
        public string? Persons
        {
            get { return _selectedItem.Persons; }
            set
            {
                _selectedItem.Persons = value;
                OnPropertyChanged("Persons");
            }
        }

        public GridCabinetViewModel(CabinetsWpf hf, ICabinetService cabinetService, ISertificatesService sertificatesService,
       IDocumentsRaspOVVService documentsRaspOVVService,
       IDocumentsActReportService documentsActReportService, IMapper mapper, IHardwaresService hardwaresService)
        {
            _selectedItem = hf;
            _cabinetService = cabinetService;
            _sertificatesService = sertificatesService;
            _documentsRaspOVVService = documentsRaspOVVService;
            _documentsActReportService = documentsActReportService;
            _mapper = mapper;
            DBCollection = [];
            RefreshDataAsync();
            SaveCommand = new AsyncCommand(SaveDataAsync);
            RestoreCommand = new AsyncCommand(RefreshDataAsync);
            AddCommand = new RelayCommand(AddData);
            DeleteCommand = new RelayCommand(DeleteData);
            ShowTreeCommand = new AsyncCommand(() => ShowTreeAsync(cabinetService, hardwaresService, mapper));
            EditDataCommand = new RelayCommand(EditData);
            ExportToWordCommand = new RelayCommand(DataToWord);
            SelectedItemsDatagrid = [];
        }
        public void EditData()
        {
            EditDbCollection = SelectedItemsDatagrid;
            EditGridCabinetPage = new Views.EditWindows.EditCabinetPage();
            RefreshEditDataCommandContext();
            EditGridCabinetPage.Show();
        }

        public void RefreshEditDataCommandContext()
        {
            EditGridCabinetPage.DataContext = this;
        }
        public static async System.Threading.Tasks.Task ShowTreeAsync(ICabinetService cabinetService,
        IHardwaresService hardwaresService,
       IMapper mapper)
        {
            var treeViewModel = new TreeCabinetsViewModel(cabinetService, hardwaresService, mapper);

            await treeViewModel.LoadDataAsync();
            var treeWindow = new Views.TreeViewPages.TreeViewPageCabinets
            {
                DataContext = treeViewModel,
            };

            treeWindow.Show();


        }
        public void DeleteData()
        {
            deleteItems = SelectedItemsDatagrid.ToList();

            foreach (var item in deleteItems)
            {
                DBCollection.Remove(item);
                if (EditGridCabinetPage is not null)
                {
                    editDbCollection.Remove(item);
                }
            }

        }
        public async System.Threading.Tasks.Task RefreshDataAsync()
        {
            DBCollection?.Clear();
            deleteItems?.Clear();
            getSertificateDb?.Clear();
            getDocumentRaspOVVDb?.Clear();
            getDocumentActReportDb?.Clear();
            originalDbCollection = [];

            var SertificateList = _mapper.Map<List<SertificatesWpf>>(await _sertificatesService.GetAll());
            var DocumentRaspList = _mapper.Map<List<DocumentsRaspOVVWpf>>(await _documentsRaspOVVService.GetAll());
            var DocumentActReportList = _mapper.Map<List<DocumentsActReportWpf>>(await _documentsActReportService.GetAll());

            getSertificateDb = ItemForComboboxService.FillInvNumberWithNameCollection(
                SertificateList,
                item => item.Id,
                item => item.InvNumberWithName
            );

            getDocumentRaspOVVDb = ItemForComboboxService.FillInvNumberWithNameCollection(
                 DocumentRaspList,
                 item => item.Id,
                 item => item.InvNumberWithName
             );
            getDocumentActReportDb = ItemForComboboxService.FillInvNumberWithNameCollection(
     DocumentActReportList,
     item => item.Id,
     item => item.InvNumberWithName
 );

            foreach (var item in _mapper.Map<List<CabinetsWpf>>(await _cabinetService.GetAll()))
            {
                if (item.SertificateId != null)
                {
                    item.SertificateWpf = SertificateList.Find(p => p.Id == item.SertificateId);
                }
                if (item.RaspExpId != null)
                {
                    item.DocumentRaspOVVWpf = DocumentRaspList.Find(p => p.Id == item.RaspExpId);
                }
                if (item.ActReportId != null)
                {
                    item.DocumentActReportWpf = DocumentActReportList.Find(p => p.Id == item.ActReportId);
                }
                DBCollection.Add(item);
                originalDbCollection.Add(item);
            }
            SelectedItemsDatagrid?.Clear();
            if (EditGridCabinetPage != null)
            {
                EditGridCabinetPage.Close();
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
                            await _cabinetService.DeleteAsync(_mapper.Map<CabinetsDto>(item));
                        }
                    }
                    deleteItems.Clear();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex, "Ошибка при удалении данных");

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
                        await _cabinetService.UpdateAsync(_mapper.Map<CabinetsDto>(item));
                    }
                    if (item.Id == 0)
                    {
                        await _cabinetService.CreateAsync(_mapper.Map<CabinetsDto>(item));
                    }

                }
                await _cabinetService.SaveChangesAsync();
                await RefreshDataAsync();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex, "Ошибка при сохранении данных");

            }
        }

        private void AddData()
        {
            var item = new CabinetsWpf
            {
            };

            DBCollection.Add(item);
            if (EditGridCabinetPage is not null)
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
            var selected = new List<CabinetsWpf>(SelectedItemsDatagrid.Where(i => i.Id != 0));
            var header = HeaderSelectedItems("Id,Номер,Адрес,Тип помещения,Отв. лицо за экспл.,Отв. лицо за ТЗИ,Аттестат,Расп. о вводе,Акт обследования,Допущенные лица");
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
                table.Cell(i, 2).Range.Text = selected[j].Number?.ToString();
                table.Cell(i, 3).Range.Text = selected[j].Address?.ToString();
                table.Cell(i, 4).Range.Text = selected[j].Status?.ToString();
                table.Cell(i, 5).Range.Text = selected[j].ResponsibleExp?.ToString();
                table.Cell(i, 6).Range.Text = selected[j].ResponsibleExp?.ToString();
                table.Cell(i, 7).Range.Text = selected[j].SertificateWpf?.InvNumberWithName?.ToString();
                table.Cell(i, 8).Range.Text = selected[j].DocumentRaspOVVWpf?.InvNumberWithName?.ToString();
                table.Cell(i, 9).Range.Text = selected[j].DocumentActReportWpf?.InvNumberWithName?.ToString();
                table.Cell(i, 10).Range.Text = selected[j].Persons?.ToString();
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
