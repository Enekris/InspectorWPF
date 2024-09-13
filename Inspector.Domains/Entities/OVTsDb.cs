namespace Inspector.Domains.Entities
{
    public class OVTsDb : BaseEntity
    {
        public string Name { get; set; }
        public int? SertificateId { get; set; }
        public string? ResponsibleExp { get; set; }
        public string? ResponsibleTZI { get; set; }
        public string? AdminSec { get; set; }
        public string? AdminSys { get; set; }
        public int? RaspExpId { get; set; }
        public string? Note { get; set; }
        public DocumentsRaspOVVDb DocumentRaspOVVDb { get; set; }
        public SertificatesDb SertificateDb { get; set; }
        public ICollection<HardwaresDb> HardwaresDb { get; set; } = [];

        public OVTsDb()
        {

        }
    }
}
