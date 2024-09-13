using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.Sertificates;
using Inspector.Application.Contracts.Logic.Services.Volumes;
using Inspector.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Inspector.ViewModels.Windows.VolumesTree
{
    public class TreeVolumesViewModel : INotifyPropertyChanged

    {

        internal readonly IVolumesService _volumesService;
        internal readonly ISertificatesService _sertificatesService;
        internal readonly IMapper _mapper;

        private ObservableCollection<CaseYearViewModel> caseYears;

        private List<VolumesWpf> volumes { get; set; }
        private List<SertificatesWpf> sertirificates { get; set; }

        private List<DocumentsWpf> documents { get; set; }

        private ObservableCollection<DocumentsWpf> errorDocuments { get; set; }
        private ObservableCollection<DocumentsWpf> interestedForDeleteDocuments { get; set; }

        private ObservableCollection<VolumesWpf> errorVolumes { get; set; }
        private ObservableCollection<VolumesWpf> interestedForDeleteVolumes { get; set; }

        private ObservableCollection<SertificatesWpf> errorSertificates { get; set; }
        private ObservableCollection<SertificatesWpf> interestedForDeleteSertificates { get; set; }

        public ObservableCollection<DocumentsWpf> ErrorDocuments
        {
            get { return errorDocuments; }
            set
            {
                errorDocuments = value;
                OnPropertyChanged(nameof(ErrorDocuments));
            }
        }
        public ObservableCollection<DocumentsWpf> InterestedForDeleteDocuments
        {
            get { return interestedForDeleteDocuments; }
            set
            {
                interestedForDeleteDocuments = value;
                OnPropertyChanged(nameof(InterestedForDeleteDocuments));
            }
        }

        public ObservableCollection<VolumesWpf> ErrorVolumes
        {
            get { return errorVolumes; }
            set
            {
                errorVolumes = value;
                OnPropertyChanged(nameof(ErrorVolumes));
            }
        }
        public ObservableCollection<VolumesWpf> InterestedForDeleteVolumes
        {
            get { return interestedForDeleteVolumes; }
            set
            {
                interestedForDeleteVolumes = value;
                OnPropertyChanged(nameof(InterestedForDeleteVolumes));
            }
        }

        public ObservableCollection<SertificatesWpf> ErrorSertificates
        {
            get { return errorSertificates; }
            set
            {
                errorSertificates = value;
                OnPropertyChanged(nameof(ErrorSertificates));
            }
        }
        public ObservableCollection<SertificatesWpf> InterestedForDeleteSertificates
        {
            get { return interestedForDeleteSertificates; }
            set
            {
                interestedForDeleteSertificates = value;
                OnPropertyChanged(nameof(InterestedForDeleteSertificates));
            }
        }

        public ObservableCollection<CaseYearViewModel> CaseYears
        {
            get { return caseYears; }
            set
            {
                caseYears = value;
                OnPropertyChanged(nameof(CaseYears));
            }
        }
        public TreeVolumesViewModel(IVolumesService volumesService,
ISertificatesService sertificatesService, IMapper mapper)
        {
            _volumesService = volumesService;
            _sertificatesService = sertificatesService;
            _mapper = mapper;
            //  LoadDataAsync();
        }

        private static List<T> ProcessDocumentItems<T>(ICollection<T> doc, string type) where T : DocumentsWpf
        {
            foreach (var item in doc)
            {
                var page = "\t";
                var number = $"\t{item.InvNumber}";
                var name = item.Name == null ? "" : $"\t{item.Name}";
                if (item.Page != null)
                {
                    if (item.Page / 10 < 1)
                    {
                        page = $"000{item.Page}\t";
                    }
                    else if (item.Page / 100 < 1)
                    {
                        page = $"00{item.Page}\t";
                    }
                    else if (item.Page / 1000 < 1)
                    {
                        page = $"0{item.Page}\t";
                    }
                    else if (item.Page / 10000 < 1)
                    {
                        page = $"{item.Page}\t";
                    }
                }

                item.InvNumberWithNameForTree = $"{page}{type}{number}{name}";
            }
            return doc.ToList();
        }
        private async Task LoadDataFromDatabaseAsync()
        {
            var volumesResult = await _volumesService.GetAllIncludeForVolumesTree();
            var sertirificatesResult = await _sertificatesService.GetAll();

            volumes = _mapper.Map<List<VolumesWpf>>(volumesResult);
            sertirificates = _mapper.Map<List<SertificatesWpf>>(sertirificatesResult);

            documents = [];
            foreach (var v in volumes)
            {
                documents.AddRange(ProcessDocumentItems(v.DocumentActReportWpf, "Акт").Cast<DocumentsWpf>());
                documents.AddRange(ProcessDocumentItems(v.DocumentsOthersWpf, "Прочий").Cast<DocumentsWpf>());
                documents.AddRange(ProcessDocumentItems(v.documentThirdWpf, "Третий.").Cast<DocumentsWpf>());
                documents.AddRange(ProcessDocumentItems(v.DocumentRaspOVVWpf, "Расп.").Cast<DocumentsWpf>());
                documents.AddRange(ProcessDocumentItems(v.DocumentFirstWpf, "Первый").Cast<DocumentsWpf>());
                documents.AddRange(ProcessDocumentItems(v.DocumentSecondWpf, "Второй").Cast<DocumentsWpf>());
            }

            var interestedForDeleteDocumentsList = volumes
    .Where(v => v.DestructionMark || v.ForDestruction)
    .SelectMany(v => new List<IEnumerable<DocumentsWpf>>
    {
        v.DocumentActReportWpf
            .Where(d => !d.DestructionMark &&
                        (d.CabinetsWpf.Count == 0 || d.CabinetsWpf.All(c => c.RaspExpId == null))),
        v.DocumentsOthersWpf
            .Where(d => !d.DestructionMark),
        v.documentThirdWpf
            .Where(d => !d.DestructionMark &&
                        (d.HardwaresWpf.Count == 0 || d.HardwaresWpf.All(c => !c.UsageInfo))),
        v.DocumentRaspOVVWpf
            .Where(d => !d.DestructionMark &&
                        (d.OVTsWpf.Count == 0 || d.OVTsWpf.All(c => c.RaspExpId == null)) &&
                        (d.CabinetsWpf.Count == 0 || d.CabinetsWpf.All(c => c.RaspExpId == null))),
        v.DocumentFirstWpf
            .Where(d => !d.DestructionMark &&
                        (d.HardwaresWpf.Count == 0 || d.HardwaresWpf.All(c => !c.UsageInfo))),
        v.DocumentSecondWpf
            .Where(d => !d.DestructionMark &&
                        (d.HardwaresWpf.Count == 0 || d.HardwaresWpf.All(c => !c.UsageInfo)))
    }.SelectMany(docs => docs))
    .Where(item => item.InventoryWpf == null ||
                    (item.InventoryWpf.DestructionMark || item.InventoryWpf.ForDestruction))
    .ToList();

            InterestedForDeleteDocuments = new ObservableCollection<DocumentsWpf>(interestedForDeleteDocumentsList);

            ErrorVolumes = new ObservableCollection<VolumesWpf>(volumes
                .Where(v => v.DestructionMark && v.ForDestruction));

            ErrorDocuments = new ObservableCollection<DocumentsWpf>(documents
                .Where(v => v.DestructionMark && v.ForDestruction));

            ErrorSertificates = new ObservableCollection<SertificatesWpf>(sertirificates
                .Where(v => v.DestructionMark && v.ForDestruction));
        }

        private static DocumentsWpf ProcessDocumentItems<T>(T doc, string type) where T : DocumentsWpf
        {
            var page = "\t";
            var number = $"\t{doc.InvNumber}";
            var name = doc.Name == null ? "" : $"\t{doc.Name}";

            if (doc.Page != null)
            {
                if (doc.Page / 10 < 1)
                {
                    page = $"000{doc.Page}\t";
                }
                else if (doc.Page / 100 < 1)
                {
                    page = $"00{doc.Page}\t";
                }
                else if (doc.Page / 1000 < 1)
                {
                    page = $"0{doc.Page}\t";
                }
                else if (doc.Page / 10000 < 1)
                {
                    page = $"{doc.Page}\t";
                }
            }

            doc.InvNumberWithNameForTree = $"{page}{type}{number}{name}";

            return doc;
        }

        public async System.Threading.Tasks.Task LoadDataAsync()
        {
            await LoadDataFromDatabaseAsync();
            InterestedForDeleteVolumes = [];
            CaseYears = [];
            List<int> volumesIdtoDestr = [];
            var caseYearGroups = volumes.GroupBy(v => v.CaseYear);
            foreach (var caseYearGroup in caseYearGroups)
            {
                var caseYearViewModel = new CaseYearViewModel(caseYearGroup.Key);

                var caseNumberGroups = caseYearGroup.GroupBy(v => v.CaseNumber);
                foreach (var caseNumberGroup in caseNumberGroups)
                {
                    var caseNumberViewModel = new CaseNumberViewModel(caseNumberGroup.Key);

                    foreach (var volume in caseNumberGroup)
                    {
                        var vdoc = documents.Where(d => d.VolumeId == volume.Id).ToList();
                        var vser = sertirificates.Where(d => d.VolumeId == volume.Id).ToList();
                        var volumeViewModel = new VolumeViewModel(volume.VolumeNumber, vdoc, vser, volume.DestructionMark, volume.ForDestruction);
                        caseNumberViewModel.VolumesCollection.Add(volumeViewModel);

                        List<DocumentsWpf> destrdoc = vdoc.Where(d => d.DestructionMark == true || d.ForDestruction == true).ToList(); //ищу доки в этом томе в которых метка стоит
                        List<SertificatesWpf> destrser = vser.Where(d => d.DestructionMark == true || d.ForDestruction == true).ToList(); //ищу аттестаты в этом томе в которых метка стоит

                        if (volumeViewModel.DestructionMark == false &&
    (
        (vser.Count == 0 && vdoc.Count != 0 && vdoc.Count == destrdoc.Count) ||
        (vdoc.Count == 0 && vser.Count != 0 && vser.Count == destrser.Count) ||
        (vser.Count != 0 && vdoc.Count != 0 && vdoc.Count == destrdoc.Count && vser.Count == destrser.Count)
    ))
                        {
                            InterestedForDeleteVolumes.Add(volume);
                        }
                    }

                    CheckCollectionCaseNumber(caseNumberViewModel);
                    caseYearViewModel.CaseNumbersCollection.Add(caseNumberViewModel);
                }

                CheckCollectionCaseYears(caseYearViewModel);
                CaseYears.Add(caseYearViewModel);
            }
            SortCaseYears();
        }
        public void SortCaseYears()
        {
            var sortedList = CaseYears.OrderBy(o => o.CaseYearForSort).ToList();

            var sortedObservableCollection = new ObservableCollection<CaseYearViewModel>(sortedList);

            CaseYears.Clear();
            foreach (var item in sortedObservableCollection)
            {
                CaseYears.Add(item);
            }
        }
        private static void CheckCollectionCaseNumber(CaseNumberViewModel model)
        {
            if (model.VolumesCollection.Count > 0 && model.VolumesCollection.All(v => v.DestructionMark == true))
            {
                model.DestructionMark = true;
            }

            if (model.VolumesCollection.Count > 0 && model.VolumesCollection.All(v => v.ForDestruction == true))
            {
                model.ForDestruction = true;
            }
        }

        private static void CheckCollectionCaseYears(CaseYearViewModel model)
        {
            if (model.CaseNumbersCollection.Count > 0 && model.CaseNumbersCollection.All(v => v.DestructionMark == true))
            {
                model.DestructionMark = true;
            }

            if (model.CaseNumbersCollection.Count > 0 && model.CaseNumbersCollection.All(v => v.ForDestruction == true))
            {
                model.ForDestruction = true;
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
