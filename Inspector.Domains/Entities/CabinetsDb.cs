namespace Inspector.Domains.Entities
{
    public class CabinetsDb : BaseEntity
    {
        public string Number { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string? ResponsibleExp { get; set; }
        public string? ResponsibleTZI { get; set; }
        public string? Persons { get; set; }
        public int? SertificateId { get; set; }
        public int? RaspExpId { get; set; }
        public int? ActReportId { get; set; }
        public string? Note { get; set; }
        public SertificatesDb SertificateDb { get; set; }
        public DocumentsRaspOVVDb DocumentRaspOVVDb { get; set; }
        public DocumentsActReportDb DocumentActReportDb { get; set; }
        public ICollection<HardwaresDb> HardwaresDb { get; set; } = [];

        public CabinetsDb()
        {

        }
    }
}
