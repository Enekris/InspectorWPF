namespace Inspector.Domains.Entities
{
    public class DocumentsThirdDb : DocumentsDb
    {
        public ICollection<HardwaresDb> HardwaresDb { get; set; } = [];

        public DocumentsThirdDb()
        {

        }

    }
}
