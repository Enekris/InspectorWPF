using System.ComponentModel.DataAnnotations.Schema;

namespace Inspector.Models
{
    public class CabinetsWpf
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string? ResponsibleExp { get; set; }
        public string? ResponsibleTZI { get; set; }
        public string? Persons { get; set; }
        public int? SertificateId { get; set; }
        public int? RaspExpId { get; set; }
        public int? ActReportId { get; set; }
        public string? Note { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; }


        public SertificatesWpf SertificateWpf { get; set; }
        public DocumentsRaspOVVWpf DocumentRaspOVVWpf { get; set; }
        public DocumentsActReportWpf DocumentActReportWpf { get; set; }
        [NotMapped]
        public string? Treetittle { get; set; }

        public ICollection<HardwaresWpf> HardwaresWpf { get; set; } = [];


        [NotMapped]
        public string NumberWithNote
        {
            get
            {
                if (Note == "" || Note == null)
                {
                    return string.Format("{0}", Number);
                }
                else
                {
                    return string.Format("{0}\n{1}", Number, Note);
                }

            }


        }
        public CabinetsWpf()
        {

        }
    }
}
