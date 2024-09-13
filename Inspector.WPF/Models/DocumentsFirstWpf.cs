namespace Inspector.Models
{
    public class DocumentsFirstWpf : DocumentsWpf
    {

        public ICollection<HardwaresWpf> HardwaresWpf { get; set; } = [];

        public DocumentsFirstWpf()
        {

        }

    }
}
