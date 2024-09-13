using Inspector.Application.Contracts.Logic.Services.Cabinets.Models;
using Inspector.Application.Contracts.Logic.Services.Documents.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspector.Application.Contracts.Logic.Services.DocumentsActReport.Models
{
    public class DocumentsActReportDto : DocumentsDto
    {

        public ICollection<CabinetsDto> CabinetsDto { get; set; } = [];

        [NotMapped]
        public int PercentRemaining { get; set; }
        public DocumentsActReportDto()
        {

        }

    }
}
