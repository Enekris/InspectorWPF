using System.ComponentModel.DataAnnotations.Schema;

namespace Inspector.Models
{

    public class SertificatesWpf
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Number { get; set; }

        public DateTime DataFirst { get; set; }

        public DateTime DataEnd { get; set; }

        public string Organisation { get; set; }

        public int? VolumeId { get; set; }
        public int? InventoryId { get; set; }
        public int? Page { get; set; }
        public bool ForDestruction { get; set; }
        public bool DestructionMark { get; set; }
        public string? Note { get; set; }

        public ICollection<CabinetsWpf> CabinetsWpf { get; set; } = [];
        public OVTsWpf OVTWpf { get; set; }
        public VolumesWpf VolumeWpf { get; set; }
        public InvertoriesWpf InventoryWpf { get; set; }

        [NotMapped]
        public string InvNumberWithName
        {
            get
            {
                if (Number == "Пусто")
                {
                    return string.Format(Number);
                }
                else
                {
                    return string.Format("{0}\n{1}", Number, Name);
                }

            }

        }
        [NotMapped]
        public bool IsSelected { get; set; }
        [NotMapped]
        public int PercentRemaining { get; set; }
        public SertificatesWpf()
        {

        }


    }
}
