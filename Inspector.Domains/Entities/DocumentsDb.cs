namespace Inspector.Domains.Entities
{
    public class DocumentsDb : BaseEntity
    {

        public string InvNumber { get; set; }
        public DateTime? Data { get; set; }
        public string? Name { get; set; }
        public int? VolumeId { get; set; }
        public int? InventoryId { get; set; }
        public int? Page { get; set; }
        public bool ForDestruction { get; set; } = false;
        public bool DestructionMark { get; set; } = false;
        public string? Note { get; set; }
        public VolumesDb VolumeDb { get; set; }
        public InvertoriesDb InventoryDb { get; set; }

        public DocumentsDb()
        {

        }
    }
}

