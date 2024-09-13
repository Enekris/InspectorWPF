namespace Inspector.Domains.Entities
{
    public class HardwareFilterNameDb : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<HardwaresDb> HardwaresDb { get; set; } = [];

        public HardwareFilterNameDb()
        {

        }
    }
}
