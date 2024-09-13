using Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV.Models;
using Inspector.Application.Contracts.Logic.Services.Hardwares.Models;
using Inspector.Application.Contracts.Logic.Services.Sertificates.Models;

namespace Inspector.Application.Contracts.Logic.Services.OVTs.Models
{
    public class OVTsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SertificateId { get; set; }
        public string? ResponsibleExp { get; set; }
        public string? ResponsibleTZI { get; set; }
        public string? AdminSec { get; set; }
        public string? AdminSys { get; set; }
        public int? RaspExpId { get; set; }
        public string? Note { get; set; }


        public DocumentsRaspOVVDto DocumentRaspOVVDto { get; set; }
        public SertificatesDto SertificateDto { get; set; }
        public ICollection<HardwaresDto> HardwaresDto { get; set; } = [];


        public OVTsDto()
        {

        }
    }
}
