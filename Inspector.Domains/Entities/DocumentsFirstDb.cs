namespace Inspector.Domains.Entities
{
    public class DocumentsFirstDb : DocumentsDb
    {
        public ICollection<HardwaresDb> HardwaresDb { get; set; } = [];

        public DocumentsFirstDb()
        {

        }

    }
}
