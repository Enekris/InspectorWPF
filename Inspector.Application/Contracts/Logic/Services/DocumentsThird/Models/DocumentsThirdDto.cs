using Inspector.Application.Contracts.Logic.Services.Documents.Models;
using Inspector.Application.Contracts.Logic.Services.Hardwares.Models;

namespace Inspector.Application.Contracts.Logic.Services.DocumentsThird.Models
{
    public class DocumentsThirdDto : DocumentsDto
    {

        public ICollection<HardwaresDto> HardwaresDto { get; set; } = [];

        public DocumentsThirdDto()
        {

        }

    }
}
