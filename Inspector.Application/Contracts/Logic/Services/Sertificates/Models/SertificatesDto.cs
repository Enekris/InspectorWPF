using Inspector.Application.Contracts.Logic.Services.Cabinets.Models;
using Inspector.Application.Contracts.Logic.Services.Invertories.Models;
using Inspector.Application.Contracts.Logic.Services.OVTs.Models;
using Inspector.Application.Contracts.Logic.Services.Volumes.Models;

namespace Inspector.Application.Contracts.Logic.Services.Sertificates.Models
{

    public class SertificatesDto
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

        public ICollection<CabinetsDto> CabinetsDto { get; set; } = [];
        public OVTsDto OVTDto { get; set; }
        public VolumesDto VolumeDto { get; set; }
        public InvertoriesDto InventoryDto { get; set; }

        public SertificatesDto()
        {

        }


    }
}
