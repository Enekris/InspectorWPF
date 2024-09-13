using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.HardwareFilterName;
using Inspector.Application.Contracts.Logic.Services.HardwareFilterName.Models;
using Inspector.Models;
using Inspector.Services;
using Inspector.Views.Pages;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Inspector.ViewModels.Pages
{
    public partial class GridHFNViewModel : DependencyObject, INotifyPropertyChanged
    {
        internal readonly IHardwareFilterNameService _hardwareFilter;
        internal readonly IMapper _mapper;
        private readonly HardwareFilterNameWpf _selectedItem;

        public static readonly DependencyProperty ObservableListProperty = DependencyProperty.Register("SelectedItemsDatagrid",
           typeof(ObservableCollection<HardwareFilterNameWpf>), typeof(GridHFNPage), new PropertyMetadata(default(ObservableCollection<HardwareFilterNameWpf>)));
        public ObservableCollection<HardwareFilterNameWpf> SelectedItemsDatagrid { get { return (ObservableCollection<HardwareFilterNameWpf>)GetValue(ObservableListProperty); } set { SetValue(ObservableListProperty, value); } }

        private List<HardwareFilterNameWpf> deleteItems;


        public string ErrorMessage { get; set; }
        public IAsyncCommand SaveCommand { get; private set; }
        public IAsyncCommand RestoreCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        private ObservableCollection<HardwareFilterNameWpf> dbCollection { get; set; }
        public ObservableCollection<HardwareFilterNameWpf> DBCollection
        {
            get { return dbCollection; }
            set
            {
                dbCollection = value;
                OnPropertyChanged(nameof(DBCollection));
            }
        }

        private ObservableCollection<HardwareFilterNameWpf> originalDbCollection { get; set; }
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

        public GridHFNViewModel(HardwareFilterNameWpf hf, IHardwareFilterNameService hardwareFilterNameService, IMapper mapper)
        {
            _selectedItem = hf;
            _hardwareFilter = hardwareFilterNameService;
            _mapper = mapper;
            DBCollection = [];
            RefreshDataAsync();
            SaveCommand = new AsyncCommand(SaveDataAsync);
            RestoreCommand = new AsyncCommand(RefreshDataAsync);
            AddCommand = new RelayCommand(AddData);
            DeleteCommand = new RelayCommand(DeleteData);
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

        public async Task RefreshDataAsync()
        {
            DBCollection?.Clear();
            deleteItems?.Clear();
            originalDbCollection = [];

            foreach (var item in await _hardwareFilter.GetAll())
            {
                var mapitem = _mapper.Map<HardwareFilterNameWpf>(item);
                DBCollection.Add(mapitem);
                originalDbCollection.Add(mapitem);
            }


        }

        private async Task DeleteDataFromDbAsync()
        {
            try
            {
                if (deleteItems != null && deleteItems.Count >= 0)
                {
                    foreach (var item in deleteItems)
                    {
                        if (item.Id != 0)
                        {
                            await _hardwareFilter.DeleteAsync(_mapper.Map<HardwareFilterNameDto>(item));
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
        private async Task SaveDataAsync()
        {
            try
            {
                await DeleteDataFromDbAsync();

                foreach (var item in DBCollection)
                {
                    if (item.Id != 0)
                    {
                        await _hardwareFilter.UpdateAsync(_mapper.Map<HardwareFilterNameDto>(item));

                    }
                    if (item.Id == 0)
                    {
                        await _hardwareFilter.CreateAsync(_mapper.Map<HardwareFilterNameDto>(item));
                    }

                }
                await _hardwareFilter.SaveChangesAsync();
                await RefreshDataAsync();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError(ex, "Ошибка при сохранении данных ");

            }
        }

        private void AddData()
        {
            var item = new HardwareFilterNameWpf
            {
            };
            DBCollection.Add(item);

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
