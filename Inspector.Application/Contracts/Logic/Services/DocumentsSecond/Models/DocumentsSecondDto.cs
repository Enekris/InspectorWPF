using Inspector.Application.Contracts.Logic.Services.Documents.Models;
using Inspector.Application.Contracts.Logic.Services.Hardwares.Models;

namespace Inspector.Application.Contracts.Logic.Services.DocumentsSecond.Models
{
    public class DocumentsSecondDto : DocumentsDto
    {
        public ICollection<HardwaresDto> HardwaresDto { get; set; } = [];

        public DocumentsSecondDto()
        {

        }

    }
}
