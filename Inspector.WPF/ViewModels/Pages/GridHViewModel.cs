using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.Cabinets;
using Inspector.Application.Contracts.Logic.Services.DocumentsFirst;
using Inspector.Application.Contracts.Logic.Services.DocumentsSecond;
using Inspector.Application.Contracts.Logic.Services.DocumentsThird;
using Inspector.Application.Contracts.Logic.Services.HardwareFilterName;
using Inspector.Application.Contracts.Logic.Services.Hardwares;
using Inspector.Application.Contracts.Logic.Services.Hardwares.Models;
using Inspector.Application.Contracts.Logic.Services.OVTs;
using Inspector.Models;
using Inspector.Services;
using Inspector.Views.EditWindows;
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
    public partial class GridHViewModel : DependencyObject, INotifyPropertyChanged
    {
        internal readonly IHardwaresService _hardwaresService;
        internal readonly IHardwareFilterNameService _hardwareFilterNameService;
        internal readonly IOVTsService _oVTsService;
        internal readonly IDocumentsFirstService _DocumentsFirstService;
        internal readonly IDocumentsSecondService _DocumentsSecondService;
        internal readonly IDocumentsThirdService _DocumentsThirdService;
        internal readonly ICabinetService _cabinetService;

        internal readonly IMapper _mapper;
        private readonly HardwaresWpf _selectedItem;


        public static readonly DependencyProperty ObservableListProperty = DependencyProperty.Register("SelectedItemsDatagrid",
           typeof(ObservableCollection<HardwaresWpf>), typeof(GridHPage), new PropertyMetadata(default(ObservableCollection<HardwaresWpf>)));
        public ObservableCollection<HardwaresWpf> SelectedItemsDatagrid { get { return (ObservableCollection<HardwaresWpf>)GetValue(ObservableListProperty); } set { SetValue(ObservableListProperty, value); } }


        private List<HardwaresWpf> deleteItems;

        public List<ItemForCombobox> getFilter { get; set; }

        public List<ItemForCombobox> getOVTsDB { get; set; }

        public List<ItemForCombobox> getDocumentsFirstDB { get; set; }

        public List<ItemForCombobox> getDocumentsSecondDB { get; set; }

        public List<ItemForCombobox> getDocumentsThirdDB { get; set; }

        public List<ItemForCombobox> getCabinetsDB { get; set; }

        public string ErrorMessage { get; set; }
        public IAsyncCommand SaveCommand { get; private set; }
        public IAsyncCommand RestoreCommand { get; private set; }
        public ICommand RestoreEditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }

        public ICommand EditDataCommand { get; private set; }
        public ICommand ExportToWordCommand { get; private set; }



        private ObservableCollection<HardwaresWpf> dbCollection { get; set; }

        public ObservableCollection<HardwaresWpf> DBCollection
        {
            get { return dbCollection; }
            set
            {
                dbCollection = value;
                OnPropertyChanged("DBCollection");
            }
        }

        private ObservableCollection<HardwaresWpf> editDbCollection { get; set; }

        public ObservableCollection<HardwaresWpf> EditDbCollection
        {
            get { return editDbCollection; }
            set
            {
                editDbCollection = value;
                OnPropertyChanged(nameof(EditDbCollection));
            }
        }

        private EditGridHPage editGridHPage { get; set; }
        public EditGridHPage? EditGridHPage
        {
            get { return editGridHPage; }
            set
            {
                editGridHPage = value;
                OnPropertyChanged(nameof(editGridHPage));
            }
        }

        private ObservableCollection<HardwaresWpf> originalDbCollection { get; set; }
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

        public string SerialNumber
        {
            get { return _selectedItem.SerialNumber; }
            set
            {
                _selectedItem.SerialNumber = value;
                OnPropertyChanged("SerialNumber");
            }
        }
        public int NameId
        {
            get { return _selectedItem.NameId; }
            set
            {
                _selectedItem.NameId = value;
                OnPropertyChanged("NameId");
            }
        }
        public string? Model
        {
            get { return _selectedItem.Model; }
            set
            {
                _selectedItem.Model = value;
                OnPropertyChanged("Model");
            }
        }
        public int? CabinetId
        {
            get { return _selectedItem.CabinetId; }
            set
            {
                _selectedItem.CabinetId = value;
                OnPropertyChanged("CabinetId");
            }
        }
        public int? OvtId
        {
            get { return _selectedItem.OvtId; }
            set
            {
                _selectedItem.OvtId = value;
                OnPropertyChanged("OvtId");
            }
        }

        public bool UsageInfo
        {
            get { return _selectedItem.UsageInfo; }
            set
            {
                _selectedItem.UsageInfo = value;
                OnPropertyChanged("UsageInfo");
            }
        }
        public int? DocLocationFirstId
        {
            get { return _selectedItem.DocLocationFirstId; }
            set
            {
                _selectedItem.DocLocationFirstId = value;
                OnPropertyChanged("DocLocationFirstId");
            }
        }
        public int? DocLocationSecondId
        {
            get { return _selectedItem.DocLocationSecondId; }
            set
            {
                _selectedItem.DocLocationSecondId = value;
                OnPropertyChanged("DocLocationSecondId");
            }
        }
        public int? DocLocationThirdId
        {
            get { return _selectedItem.DocLocationThirdId; }
            set
            {
                _selectedItem.DocLocationThirdId = value;
                OnPropertyChanged("DocLocationThirdId");
            }
        }
        public string? Appointment
        {
            get { return _selectedItem.Appointment; }
            set
            {
                _selectedItem.Appointment = value;
                OnPropertyChanged("Appointment");
            }
        }
        public string? Note
        {
            get { return _selectedItem.Note; }
            set
            {
                _selectedItem.Note = value;
                OnPropertyChanged("Note");
            }
        }

        public GridHViewModel(HardwaresWpf h, IHardwaresService hardwaresService,
            IHardwareFilterNameService hardwareFilterNameService, IOVTsService oVTsService,
            IDocumentsFirstService DocumentsFirstService, IDocumentsSecondService DocumentsSecondService,
            IDocumentsThirdService DocumentsThirdService, ICabinetService cabinetService, IMapper mapper)
        {
            _selectedItem = h;

            _hardwaresService = hardwaresService;
            _hardwareFilterNameService = hardwareFilterNameService;
            _oVTsService = oVTsService;
            _DocumentsFirstService = DocumentsFirstService;
            _DocumentsSecondService = DocumentsSecondService;
            _DocumentsThirdService = DocumentsThirdService;
            _cabinetService = cabinetService;

            _mapper = mapper;

            DBCollection = [];
            RefreshDataAsync();
            SaveCommand = new AsyncCommand(SaveDataAsync);
            RestoreCommand = new AsyncCommand(RefreshDataAsync);
            AddCommand = new RelayCommand(AddData);
            DeleteCommand = new RelayCommand(DeleteData);
            EditDataCommand = new RelayCommand(EditData);
            ExportToWordCommand = new RelayCommand(DataToWord);
            SelectedItemsDatagrid = [];
        }

        public void EditData()
        {
            EditDbCollection = SelectedItemsDatagrid;
            EditGridHPage = new EditGridHPage();
            RefreshEditDataCommandContext();
            EditGridHPage.Show();
        }

        public void RefreshEditDataCommandContext()
        {
            EditGridHPage.DataContext = this;
        }

        public void DeleteData()
        {
            deleteItems = SelectedItemsDatagrid.ToList();

            foreach (var item in deleteItems)
            {
                DBCollection.Remove(item);
                if (EditGridHPage is not null)
                {
                    editDbCollection.Remove(item);
                }
            }

        }

        public async System.Threading.Tasks.Task RefreshDataAsync()
        {
            DBCollection?.Clear();
            deleteItems?.Clear();
            getFilter?.Clear();
            getOVTsDB?.Clear();
            getDocumentsFirstDB?.Clear();
            getDocumentsThirdDB?.Clear();
            getDocumentsSecondDB?.Clear();
            getCabinetsDB?.Clear();

            originalDbCollection = [];

            var getFilterList = _mapper.Map<List<HardwareFilterNameWpf>>(await _hardwareFilterNameService.GetAll());
            var getOVTsDBList = _mapper.Map<List<OVTsWpf>>(await _oVTsService.GetAll());
            var getDocumentsFirstDBList = _mapper.Map<List<DocumentsFirstWpf>>(await _DocumentsFirstService.GetAll());
            var getDocumentsSecondDBList = _mapper.Map<List<DocumentsSecondWpf>>(await _DocumentsSecondService.GetAll());
            var getDocumentsThirdDBList = _mapper.Map<List<DocumentsThirdWpf>>(await _DocumentsThirdService.GetAll());
            var getCabinetsDBList = _mapper.Map<List<CabinetsWpf>>(await _cabinetService.GetAll());

            getFilter = ItemForComboboxService.FillNameCollection(
    getFilterList,
    item => item.Id,
    item => item.Name
);
            getOVTsDB = ItemForComboboxService.FillNameCollection(
    getOVTsDBList,
    item => item.Id,
    item => item.Name
);
            getDocumentsFirstDB = ItemForComboboxService.FillInvNumberWithNameCollection(
    getDocumentsFirstDBList,
    item => item.Id,
    item => item.InvNumberWithName
);
            getDocumentsSecondDB = ItemForComboboxService.FillInvNumberWithNameCollection(
    getDocumentsSecondDBList,
    item => item.Id,
    item => item.InvNumberWithName

);
            getDocumentsThirdDB = ItemForComboboxService.FillInvNumberWithNameCollection(
getDocumentsThirdDBList,
item => item.Id,
item => item.InvNumberWithName
);

            getCabinetsDB = ItemForComboboxService.FillNumberWithNoteCollection(
    getCabinetsDBList,
    item => item.Id,
    item => item.NumberWithNote
);

            foreach (var item in _mapper.Map<List<HardwaresWpf>>(await _hardwaresService.GetAll()))
            {
                if (item.NameId != null)
                {
                    item.FilterWpf = getFilterList.Find(p => p.Id == item.NameId);
                }
                if (item.OvtId != null)
                {
                    item.OVTWpf = getOVTsDBList.Find(p => p.Id == item.OvtId);
                }
                if (item.DocLocationFirstId != null)
                {
                    item.DocumentFirstWpf = getDocumentsFirstDBList.Find(p => p.Id == item.DocLocationFirstId);
                }
                if (item.DocLocationSecondId != null)
                {
                    item.DocumentSecondWpf = getDocumentsSecondDBList.Find(p => p.Id == item.DocLocationSecondId);
                }
                if (item.DocLocationThirdId != null)
                {
                    item.documentThirdWpf = getDocumentsThirdDBList.Find(p => p.Id == item.DocLocationThirdId);
                }
                if (item.CabinetId != null)
                {
                    item.CabinetWpf = getCabinetsDBList.Find(p => p.Id == item.CabinetId);
                }

                DBCollection.Add(item);
                originalDbCollection.Add(item);
            }


            SelectedItemsDatagrid?.Clear();
            if (EditGridHPage != null)
            {
                EditGridHPage.Close();
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
                            await _hardwaresService.DeleteAsync(_mapper.Map<HardwaresDto>(item));
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
                        await _hardwaresService.UpdateAsync(_mapper.Map<HardwaresDto>(item));
                    }
                    if (item.Id == 0)
                    {
                        await _hardwaresService.CreateAsync(_mapper.Map<HardwaresDto>(item));
                    }

                }
                await _hardwaresService.SaveChangesAsync();
                await RefreshDataAsync();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex, "Ошибка при сохранении данных ");

            }

        }

        private void AddData()
        {
            var hardware = new HardwaresWpf();

            if (EditGridHPage is not null)
            {
                EditDbCollection.Add(hardware);
                DBCollection.Add(hardware);
            }

        }
        private static string[] HeaderSelectedItems(string header)
        {
            string[] values = header.Split([',']);
            return values;
        }
        private void DataToWord()
        {
            var selected = new List<HardwaresWpf>(SelectedItemsDatagrid.Where(i => i.Id != 0));
            var header = HeaderSelectedItems("Id,Наименование,Модель,Серийный номер,Назначение,Номер кабинета,Наименование ОВТ,Номер документа Первый,Номер документа Второй,Номер документа Третий,Актуальность,Примечание");
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
                table.Cell(i, 2).Range.Text = selected[j].FilterWpf.Name.ToString();
                table.Cell(i, 3).Range.Text = selected[j].Model?.ToString();
                table.Cell(i, 4).Range.Text = selected[j].SerialNumber.ToString();
                table.Cell(i, 5).Range.Text = selected[j].Appointment?.ToString();
                table.Cell(i, 6).Range.Text = selected[j].CabinetWpf?.Number?.ToString();
                table.Cell(i, 7).Range.Text = selected[j].OVTWpf?.Name.ToString();
                table.Cell(i, 8).Range.Text = selected[j].DocumentFirstWpf?.InvNumberWithName?.ToString();
                table.Cell(i, 9).Range.Text = selected[j].DocumentSecondWpf?.InvNumberWithName?.ToString();
                table.Cell(i, 10).Range.Text = selected[j].documentThirdWpf?.InvNumberWithName?.ToString();
                table.Cell(i, 11).Range.Text = selected[j].UsageInfo ? "+" : "";
                table.Cell(i, 12).Range.Text = selected[j].Note?.ToString();
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
