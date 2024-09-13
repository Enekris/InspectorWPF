using System.ComponentModel.DataAnnotations.Schema;

namespace Inspector.Models
{
    public class DocumentsActReportWpf : DocumentsWpf
    {

        public ICollection<CabinetsWpf> CabinetsWpf { get; set; } = [];

        [NotMapped]
        public int PercentRemaining { get; set; }
        public DocumentsActReportWpf()
        {

        }

    }
}
