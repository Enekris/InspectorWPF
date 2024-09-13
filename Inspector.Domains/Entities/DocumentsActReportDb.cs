namespace Inspector.Domains.Entities
{
    public class DocumentsActReportDb : DocumentsDb
    {
        public ICollection<CabinetsDb> CabinetsDb { get; set; } = [];

        public DocumentsActReportDb()
        {

        }

    }
}
