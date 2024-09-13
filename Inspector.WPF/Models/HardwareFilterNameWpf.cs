using System.ComponentModel.DataAnnotations.Schema;

namespace Inspector.Models
{
    public class HardwareFilterNameWpf
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<HardwaresWpf> HardwaresWpf { get; set; } = [];
        [NotMapped]
        public bool IsSelected { get; set; }
        public HardwareFilterNameWpf()
        {

        }
    }
}
