using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.Cabinets;
using Inspector.Application.Contracts.Logic.Services.Hardwares;
using Inspector.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Inspector.ViewModels.Windows.CabinetsTree
{
    public class TreeCabinetsViewModel : INotifyPropertyChanged
    {
        internal readonly ICabinetService _cabinetService;
        internal readonly IHardwaresService _hardwaresService;
        internal readonly IMapper _mapper;

        private List<CabinetsWpf> cabinets;
        public List<CabinetsWpf> Cabinets
        {
            get { return cabinets; }
            set
            {
                cabinets = value;
                OnPropertyChanged(nameof(Cabinets));
            }
        }

        private ObservableCollection<HardwaresWpf> actualHInNotActualCab { get; set; }

        public ObservableCollection<HardwaresWpf> ActualHInNotActualCab
        {
            get { return actualHInNotActualCab; }
            set
            {
                actualHInNotActualCab = value;
                OnPropertyChanged(nameof(ActualHInNotActualCab));
            }
        }

        private List<HardwaresWpf> hardwares { get; set; }



        private ObservableCollection<CabinetsWpf> actualCabWithNotActualH { get; set; }
        public ObservableCollection<CabinetsWpf> ActualCabWithNotActualH
        {
            get { return actualCabWithNotActualH; }
            set
            {
                actualCabWithNotActualH = value;
                OnPropertyChanged(nameof(ActualCabWithNotActualH));
            }
        }

        private ObservableCollection<CabinetsWpf> actualSerInNotActualCab { get; set; }
        public ObservableCollection<CabinetsWpf> ActualSerInNotActualCab
        {
            get { return actualSerInNotActualCab; }
            set
            {
                actualSerInNotActualCab = value;
                OnPropertyChanged(nameof(ActualSerInNotActualCab));
            }
        }
        private ObservableCollection<CabinetsWpf> actualDocInNotActualCab { get; set; }
        public ObservableCollection<CabinetsWpf> ActualDocInNotActualCab
        {
            get { return actualDocInNotActualCab; }
            set
            {
                actualDocInNotActualCab = value;
                OnPropertyChanged(nameof(ActualDocInNotActualCab));
            }
        }



        private string treeViewMessage1;
        public string TreeViewMessage1
        {
            get { return treeViewMessage1; }
            set
            {
                treeViewMessage1 = value;
                OnPropertyChanged(nameof(TreeViewMessage1));
            }
        }
        private string treeViewMessage2;
        public string TreeViewMessage2
        {
            get { return treeViewMessage2; }
            set
            {
                treeViewMessage2 = value;
                OnPropertyChanged(nameof(TreeViewMessage2));
            }
        }
        private string treeViewMessage3;
        public string TreeViewMessage3
        {
            get { return treeViewMessage3; }
            set
            {
                treeViewMessage3 = value;
                OnPropertyChanged(nameof(TreeViewMessage3));
            }
        }

        public TreeCabinetsViewModel(ICabinetService cabinetService,
        IHardwaresService hardwaresService,
       IMapper mapper)
        {
            _cabinetService = cabinetService;
            _hardwaresService = hardwaresService;
            _mapper = mapper;
            //LoadDataAsync();
        }
        // Метод загрузки данных из базы данных
        private async Task LoadDataFromDatabaseAsync()
        {
            hardwares = _mapper.Map<List<HardwaresWpf>>(await _hardwaresService.GetAllIncludeForCabinetTree());
            Cabinets = _mapper.Map<List<CabinetsWpf>>(await _cabinetService.GetAllIncludeForCabinetTree());
            foreach (var item in Cabinets)
            {
                var adress = $"\t{item.Address}";
                var number = $"\t{item.Number}";
                var status = $"\t{item.Status}";
                item.Treetittle = $"Кабинет{adress}{status}{number}";
                if (item.HardwaresWpf != null)
                {
                    foreach (var hardware in item.HardwaresWpf)
                    {
                        var name = $"\t{hardware.FilterWpf.Name}";
                        var model = hardware.Model == null ? "" : $"\t{hardware.Model}";
                        var hnumber = $"\t{hardware.SerialNumber}";
                        hardware.Treetittle = $"ТС{name}{model}{number}";

                    }
                }
            }

            ActualHInNotActualCab = new ObservableCollection<HardwaresWpf>(hardwares.Where(h => h.UsageInfo == true && (h.CabinetWpf == null || h.CabinetWpf?.RaspExpId == null)));
            ActualCabWithNotActualH = new ObservableCollection<CabinetsWpf>(cabinets.Where(c => c.RaspExpId != null && (c.HardwaresWpf == null || c.HardwaresWpf.Where(h => h.UsageInfo == false).Any())));
            ActualSerInNotActualCab = new ObservableCollection<CabinetsWpf>(cabinets.Where(c => c.RaspExpId == null && c.SertificateWpf != null && (c.SertificateWpf.DestructionMark == false || c.SertificateWpf.ForDestruction == false)));
            ActualDocInNotActualCab = new ObservableCollection<CabinetsWpf>(cabinets.Where(c => c.RaspExpId == null && c.DocumentActReportWpf != null && (c.DocumentActReportWpf.DestructionMark == false || c.DocumentActReportWpf.ForDestruction == false)));

        }
        public async Task LoadDataAsync()
        {
            await LoadDataFromDatabaseAsync();

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
