namespace Inspector.Domains.Entities
{
    public class DocumentsSecondDb : DocumentsDb
    {
        public ICollection<HardwaresDb> HardwaresDb { get; set; } = [];

        public DocumentsSecondDb()
        {

        }

    }
}
