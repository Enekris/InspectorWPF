namespace Inspector.Models
{
    public class DocumentsThirdWpf : DocumentsWpf
    {

        public ICollection<HardwaresWpf> HardwaresWpf { get; set; } = [];

        public DocumentsThirdWpf()
        {

        }

    }
}
