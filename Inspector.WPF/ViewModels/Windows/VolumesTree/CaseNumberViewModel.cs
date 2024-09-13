using System.Collections.ObjectModel;

namespace Inspector.ViewModels.Windows.VolumesTree
{
    public class CaseNumberViewModel
    {
        public string CaseNumber { get; }

        public ObservableCollection<VolumeViewModel> VolumesCollection { get; }
        public bool DestructionMark { get; set; } = false;
        public bool ForDestruction { get; set; } = false;
        public CaseNumberViewModel(string caseNumber)
        {
            CaseNumber = "Дело " + caseNumber;
            VolumesCollection = [];

        }

    }
}
