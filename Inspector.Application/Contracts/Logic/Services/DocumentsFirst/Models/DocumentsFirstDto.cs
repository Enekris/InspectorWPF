using Inspector.Application.Contracts.Logic.Services.Documents.Models;
using Inspector.Application.Contracts.Logic.Services.Hardwares.Models;

namespace Inspector.Application.Contracts.Logic.Services.DocumentsFirst.Models
{
    public class DocumentsFirstDto : DocumentsDto
    {

        public ICollection<HardwaresDto> HardwaresDto { get; set; } = [];

        public DocumentsFirstDto()
        {

        }

    }
}
