namespace Inspector.Models
{
    public class DocumentsRaspOVVWpf : DocumentsWpf
    {

        public ICollection<OVTsWpf> OVTsWpf { get; set; } = [];
        public ICollection<CabinetsWpf> CabinetsWpf { get; set; } = [];


        public DocumentsRaspOVVWpf()
        {

        }

    }
}
