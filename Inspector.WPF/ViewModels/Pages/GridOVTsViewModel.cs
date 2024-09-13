using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV;
using Inspector.Application.Contracts.Logic.Services.OVTs;
using Inspector.Application.Contracts.Logic.Services.OVTs.Models;
using Inspector.Application.Contracts.Logic.Services.Sertificates;
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
    public partial class GridOVTsViewModel : DependencyObject, INotifyPropertyChanged
    {
        internal readonly IOVTsService _oVTsService;
        internal readonly ISertificatesService _sertificatesService;
        internal readonly IDocumentsRaspOVVService _documentsRaspOVVService;
        internal readonly IMapper _mapper;
        private readonly OVTsWpf _selectedItem;
        public static readonly DependencyProperty ObservableListProperty = DependencyProperty.Register("SelectedItemsDatagrid",
    typeof(ObservableCollection<OVTsWpf>), typeof(GridOVTsPage), new PropertyMetadata(default(ObservableCollection<OVTsWpf>)));
        public ObservableCollection<OVTsWpf> SelectedItemsDatagrid { get { return (ObservableCollection<OVTsWpf>)GetValue(ObservableListProperty); } set { SetValue(ObservableListProperty, value); } }
        private List<OVTsWpf> deleteItems = [];

        public string ErrorMessage { get; set; }
        public IAsyncCommand SaveCommand { get; private set; }
        public IAsyncCommand RestoreCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand EditDataCommand { get; private set; }
        public ICommand ExportToWordCommand { get; private set; }

        public List<ItemForCombobox> getDocumentRaspOVVDb { get; set; }
        public List<ItemForCombobox> getSertificateDb { get; set; }
        private ObservableCollection<OVTsWpf> dbCollection { get; set; }
        public ObservableCollection<OVTsWpf> DBCollection
        {
            get { return dbCollection; }
            set
            {
                dbCollection = value;
                OnPropertyChanged(nameof(DBCollection));
            }
        }

        private ObservableCollection<OVTsWpf> originalDbCollection { get; set; }
        private ObservableCollection<OVTsWpf> editDbCollection { get; set; }

        public ObservableCollection<OVTsWpf> EditDbCollection
        {
            get { return editDbCollection; }
            set
            {
                editDbCollection = value;
                OnPropertyChanged(nameof(EditDbCollection));
            }
        }

        private Views.EditWindows.EditOVTPage editGridOvtPage { get; set; }
        public Views.EditWindows.EditOVTPage? EditGridOVTPage
        {
            get { return editGridOvtPage; }
            set
            {
                editGridOvtPage = value;
                OnPropertyChanged(nameof(editGridOvtPage));
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

        public string Name
        {
            get { return _selectedItem.Name; }
            set
            {
                _selectedItem.Name = value;
                OnPropertyChanged("Name");
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
        public string? ResponsibleExp
        {
            get { return _selectedItem.ResponsibleExp; }
            set
            {
                _selectedItem.ResponsibleExp = value;
                OnPropertyChanged("ResponsibleExp");
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
        public string? AdminSec
        {
            get { return _selectedItem.AdminSec; }
            set
            {
                _selectedItem.AdminSec = value;
                OnPropertyChanged("AdminSec");
            }
        }
        public string? AdminSys
        {
            get { return _selectedItem.AdminSys; }
            set
            {
                _selectedItem.AdminSys = value;
                OnPropertyChanged("AdminSys");
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


        public GridOVTsViewModel(OVTsWpf ovt, IOVTsService oVTsService, ISertificatesService sertificatesServic,
       IDocumentsRaspOVVService documentsRaspOVVService, IMapper mapper)
        {
            _selectedItem = ovt;

            _oVTsService = oVTsService;
            _sertificatesService = sertificatesServic;
            _documentsRaspOVVService = documentsRaspOVVService;
            _mapper = mapper;

            DBCollection = [];
            RefreshDataAsync();
            SaveCommand = new AsyncCommand(SaveDataAsync);
            RestoreCommand = new AsyncCommand(RefreshDataAsync);
            AddCommand = new RelayCommand(AddData);
            DeleteCommand = new RelayCommand(DeleteData);
            EditDataCommand = new RelayCommand(EditData);
            SelectedItemsDatagrid = [];
            ExportToWordCommand = new RelayCommand(DataToWord);

        }
        public void EditData()
        {

            EditDbCollection = SelectedItemsDatagrid;
            EditGridOVTPage = new Views.EditWindows.EditOVTPage();
            RefreshEditDataCommandContext();
            EditGridOVTPage.Show();
        }

        public void RefreshEditDataCommandContext()
        {
            EditGridOVTPage.DataContext = this;
        }
        public void DeleteData()
        {
            deleteItems = SelectedItemsDatagrid.ToList();

            foreach (var item in deleteItems)
            {
                DBCollection.Remove(item);
                if (EditGridOVTPage is not null)
                {
                    editDbCollection.Remove(item);
                }
            }

        }
        public async System.Threading.Tasks.Task RefreshDataAsync()
        {
            DBCollection?.Clear();
            deleteItems?.Clear();
            getDocumentRaspOVVDb?.Clear();
            getSertificateDb?.Clear();
            originalDbCollection = [];

            var serList = _mapper.Map<List<SertificatesWpf>>(await _sertificatesService.GetAll());
            var raspList = _mapper.Map<List<DocumentsRaspOVVWpf>>(await _documentsRaspOVVService.GetAll());
            getDocumentRaspOVVDb = ItemForComboboxService.FillInvNumberWithNameCollection(
     raspList,
     item => item.Id,
     item => item.InvNumberWithName
 );

            getSertificateDb = ItemForComboboxService.FillInvNumberWithNameCollection(
                 serList,
                 item => item.Id,
                 item => item.InvNumberWithName
             );

            foreach (var item in _mapper.Map<List<OVTsWpf>>(await _oVTsService.GetAll()))
            {
                if (item.SertificateId != null)
                {
                    item.SertificateWpf = serList.Find(p => p.Id == item.SertificateId);
                }
                if (item.RaspExpId != null)
                {
                    item.DocumentRaspOVVWpf = raspList.Find(p => p.Id == item.RaspExpId);
                }
                DBCollection.Add(item);
                originalDbCollection.Add(item);

            }
            SelectedItemsDatagrid?.Clear();
            if (EditGridOVTPage != null)
            {
                EditGridOVTPage.Close();
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
                            await _oVTsService.DeleteAsync(_mapper.Map<OVTsDto>(item));
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
                        await _oVTsService.UpdateAsync(_mapper.Map<OVTsDto>(item));
                    }
                    if (item.Id == 0)
                    {
                        await _oVTsService.CreateAsync(_mapper.Map<OVTsDto>(item));
                    }

                }
                await _oVTsService.SaveChangesAsync();
                await RefreshDataAsync();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex, "Ошибка при сохранении данных ");

            }
        }

        private void AddData()
        {
            var item = new OVTsWpf
            {
            };

            DBCollection.Add(item);
            if (EditGridOVTPage is not null)
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
            var selected = new List<OVTsWpf>(SelectedItemsDatagrid.Where(i => i.Id != 0));
            var header = HeaderSelectedItems("Id,Наименование,Аттестат,Отв. за эксплуатацию,Отв. за ТЗИ,Администратор безоп. Инф.,Администратор Системный,Расп. о вводе");
            var rows = selected.Count + 1;
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
                table.Cell(i, 2).Range.Text = selected[j].Name.ToString();
                table.Cell(i, 3).Range.Text = selected[j].SertificateWpf?.InvNumberWithName?.ToString();
                table.Cell(i, 4).Range.Text = selected[j].ResponsibleExp?.ToString();
                table.Cell(i, 5).Range.Text = selected[j].ResponsibleTZI?.ToString();
                table.Cell(i, 6).Range.Text = selected[j].AdminSec?.ToString();
                table.Cell(i, 7).Range.Text = selected[j].AdminSys?.ToString();
                table.Cell(i, 8).Range.Text = selected[j].DocumentRaspOVVWpf?.InvNumberWithName?.ToString();

            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
