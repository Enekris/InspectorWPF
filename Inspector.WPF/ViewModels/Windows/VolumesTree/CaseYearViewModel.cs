using System.Collections.ObjectModel;

namespace Inspector.ViewModels.Windows.VolumesTree
{
    public class CaseYearViewModel
    {
        public int CaseYearForSort { get; }
        public string CaseYear { get; }
        public bool DestructionMark { get; set; } = false;
        public bool ForDestruction { get; set; } = false;
        public ObservableCollection<CaseNumberViewModel> CaseNumbersCollection { get; } = new ObservableCollection<CaseNumberViewModel>();

        public CaseYearViewModel(string caseYear)
        {
            if (int.TryParse(caseYear, out int parsedYear))
            {
                CaseYearForSort = parsedYear;
            }
            else
            {
                CaseYearForSort = 0;
            }

            CaseYear = "Год " + caseYear;
        }
    }
}
