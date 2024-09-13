using System.ComponentModel.DataAnnotations.Schema;

namespace Inspector.Models
{
    public class InvertoriesWpf
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string? Name { get; set; }
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
                if (Number == "Пусто")
                {
                    return string.Format(Number);
                }
                else
                {
                    return string.Format("{0}\n{1}", Number, Name);
                }

            }

        }
        [NotMapped]
        public bool IsSelected { get; set; }
        public InvertoriesWpf()
        {

        }

    }
}
