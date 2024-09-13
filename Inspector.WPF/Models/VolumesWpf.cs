using System.ComponentModel.DataAnnotations.Schema;

namespace Inspector.Models
{
    public class VolumesWpf
    {
        public int Id { get; set; }
        public string? InvNumber { get; set; }
        public string CaseNumber { get; set; }
        public int VolumeNumber { get; set; }
        public string CaseYear { get; set; }
        public bool ForDestruction { get; set; } = false;
        public bool DestructionMark { get; set; } = false;
        public string? Note { get; set; }
        public ICollection<DocumentsRaspOVVWpf> DocumentRaspOVVWpf { get; set; } = [];
        public ICollection<DocumentsActReportWpf> DocumentActReportWpf { get; set; } = [];
        public ICollection<DocumentsThirdWpf> documentThirdWpf { get; set; } = [];
        public ICollection<DocumentsFirstWpf> DocumentFirstWpf { get; set; } = [];
        public ICollection<DocumentsSecondWpf> DocumentSecondWpf { get; set; } = [];
        public ICollection<SertificatesWpf> SertificatesWpf { get; set; } = [];
        public ICollection<DocumentsOthersWpf> DocumentsOthersWpf { get; set; } = [];

        [NotMapped]
        public string InvNumberWithName
        {
            get
            {
                if (InvNumber == "Пусто")
                {
                    return string.Format(InvNumber);
                }
                else if (InvNumber == "" || InvNumber == null)
                {
                    return string.Format("{0} д{1} т{2}", CaseYear, CaseNumber, VolumeNumber);
                }
                else
                {
                    return string.Format("{0}\n{1} д{2} т{3}", InvNumber, CaseYear, CaseNumber, VolumeNumber);
                }
            }

        }
        [NotMapped]
        public bool IsSelected { get; set; }
        public VolumesWpf()
        {

        }
    }
}
