using Inspector.Application.Contracts.Logic.Services.Cabinets.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsFirst.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsSecond.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsThird.Models;
using Inspector.Application.Contracts.Logic.Services.HardwareFilterName.Models;
using Inspector.Application.Contracts.Logic.Services.OVTs.Models;

namespace Inspector.Application.Contracts.Logic.Services.Hardwares.Models
{
    public class HardwaresDto
    {
        public int Id { get; set; }
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

        public OVTsDto OVTDto { get; set; }
        public HardwareFilterNameDto FilterDto { get; set; }
        public DocumentsFirstDto DocumentFirstDto { get; set; }
        public DocumentsSecondDto DocumentSecondDto { get; set; }
        public DocumentsThirdDto documentThirdDto { get; set; }
        public CabinetsDto CabinetDto { get; set; }


        public HardwaresDto()
        {

        }


    }
}
