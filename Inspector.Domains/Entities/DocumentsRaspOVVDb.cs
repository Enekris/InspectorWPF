namespace Inspector.Domains.Entities
{
    public class DocumentsRaspOVVDb : DocumentsDb
    {
        public ICollection<OVTsDb> OVTsDb { get; set; } = [];
        public ICollection<CabinetsDb> CabinetsDb { get; set; } = [];

        public DocumentsRaspOVVDb()
        {

        }

    }
}
