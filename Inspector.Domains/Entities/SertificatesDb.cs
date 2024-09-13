namespace Inspector.Domains.Entities
{
    public class SertificatesDb : BaseEntity
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public DateTime DataFirst { get; set; }
        public DateTime DataEnd { get; set; }
        public string Organisation { get; set; }
        public int? VolumeId { get; set; }
        public int? InventoryId { get; set; }
        public int? Page { get; set; }
        public bool ForDestruction { get; set; } = false;
        public bool DestructionMark { get; set; } = false;
        public string? Note { get; set; }
        public ICollection<CabinetsDb> CabinetsDb { get; set; } = [];
        public OVTsDb OVTDb { get; set; }
        public VolumesDb VolumeDb { get; set; }
        public InvertoriesDb InventoryDb { get; set; }
        public SertificatesDb()
        {

        }


    }
}
