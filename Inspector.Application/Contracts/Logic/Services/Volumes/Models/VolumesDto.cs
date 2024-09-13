using Inspector.Application.Contracts.Logic.Services.DocumentsActReport.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsFirst.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsOthers.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsSecond.Models;
using Inspector.Application.Contracts.Logic.Services.DocumentsThird.Models;
using Inspector.Application.Contracts.Logic.Services.Sertificates.Models;

namespace Inspector.Application.Contracts.Logic.Services.Volumes.Models
{
    public class VolumesDto
    {
        public int Id { get; set; }
        public string? InvNumber { get; set; }
        public string CaseNumber { get; set; }
        public int VolumeNumber { get; set; }
        public string CaseYear { get; set; }
        public bool ForDestruction { get; set; } = false;
        public bool DestructionMark { get; set; } = false;
        public string? Note { get; set; }
        public ICollection<DocumentsRaspOVVDto> DocumentRaspOVVDto { get; set; } = [];
        public ICollection<DocumentsActReportDto> DocumentActReportDto { get; set; } = [];
        public ICollection<DocumentsThirdDto> documentThirdDto { get; set; } = [];
        public ICollection<DocumentsFirstDto> DocumentFirstDto { get; set; } = [];
        public ICollection<DocumentsSecondDto> DocumentSecondDto { get; set; } = [];
        public ICollection<SertificatesDto> SertificatesDto { get; set; } = [];
        public ICollection<DocumentsOthersDto> DocumentsOthersDto { get; set; } = [];


        public VolumesDto()
        {

        }
    }
}
