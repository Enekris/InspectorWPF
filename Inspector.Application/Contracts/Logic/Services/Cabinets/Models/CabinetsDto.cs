using Inspector.Application.Contracts.Logic.Services.DocumentsActReport.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV.Models;
using Inspector.Application.Contracts.Logic.Services.Hardwares.Models;
using Inspector.Application.Contracts.Logic.Services.Sertificates.Models;

namespace Inspector.Application.Contracts.Logic.Services.Cabinets.Models
{
    public class CabinetsDto
    {
        public int Id { get; set; }
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



        public SertificatesDto SertificateDto { get; set; }
        public DocumentsRaspOVVDto DocumentRaspOVVDto { get; set; }
        public DocumentsActReportDto DocumentActReportDto { get; set; }


        public ICollection<HardwaresDto> HardwaresDto { get; set; } = [];

        public CabinetsDto()
        {

        }
    }
}
