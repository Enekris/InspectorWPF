using Inspector.Application.Contracts.Logic.Services.Cabinets.Models;
using Inspector.Application.Contracts.Logic.Services.Documents.Models;
using Inspector.Application.Contracts.Logic.Services.OVTs.Models;

namespace Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV.Models
{
    public class DocumentsRaspOVVDto : DocumentsDto
    {

        public ICollection<OVTsDto> OVTsDto { get; set; } = [];
        public ICollection<CabinetsDto> CabinetsDto { get; set; } = [];


        public DocumentsRaspOVVDto()
        {

        }

    }
}
