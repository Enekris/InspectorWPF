namespace Inspector.Models
{
    public class DocumentsSecondWpf : DocumentsWpf
    {
        public ICollection<HardwaresWpf> HardwaresWpf { get; set; } = [];

        public DocumentsSecondWpf()
        {

        }

    }
}
