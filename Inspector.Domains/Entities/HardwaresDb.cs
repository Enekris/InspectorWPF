namespace Inspector.Domains.Entities
{
    public class HardwaresDb : BaseEntity
    {
        public string SerialNumber { get; set; }
        public int NameId { get; set; }
        public string? Model { get; set; }
        public int? CabinetId { get; set; }
        public int? OvtId { get; set; }
        public bool UsageInfo { get; set; }
        public int? DocLocationFirstId { get; set; }
        public int? DocLocationSecondId { get; set; }
        public int? DocLocationThirdId { get; set; }
        public string? Appointment { get; set; }
        public string? Note { get; set; }

        public OVTsDb OVTDb { get; set; }
        public HardwareFilterNameDb FilterDb { get; set; }
        public DocumentsFirstDb DocumentFirstDb { get; set; }
        public DocumentsSecondDb DocumentSecondDb { get; set; }
        public DocumentsThirdDb DocumentThirdDb { get; set; }
        public CabinetsDb CabinetDb { get; set; }

        public HardwaresDb()
        {

        }


    }
}
