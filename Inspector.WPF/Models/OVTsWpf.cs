using System.ComponentModel.DataAnnotations.Schema;

namespace Inspector.Models
{
    public class OVTsWpf
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SertificateId { get; set; }
        public string? ResponsibleExp { get; set; }
        public string? ResponsibleTZI { get; set; }
        public string? AdminSec { get; set; }
        public string? AdminSys { get; set; }
        public int? RaspExpId { get; set; }

        public string? Note { get; set; }
        public DocumentsRaspOVVWpf DocumentRaspOVVWpf { get; set; }
        public SertificatesWpf SertificateWpf { get; set; }
        public ICollection<HardwaresWpf> HardwaresWpf { get; set; } = [];

        [NotMapped]
        public bool IsSelected { get; set; }
        public OVTsWpf()
        {

        }
    }
}
