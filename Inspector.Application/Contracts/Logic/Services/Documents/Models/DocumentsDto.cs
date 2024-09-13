using Inspector.Application.Contracts.Logic.Services.Invertories.Models;
using Inspector.Application.Contracts.Logic.Services.Volumes.Models;

namespace Inspector.Application.Contracts.Logic.Services.Documents.Models
{
    public class DocumentsDto
    {
        //что уникально помечаю
        public int Id { get; set; }
        public string InvNumber { get; set; }
        public DateTime? Data { get; set; }
        public string? Name { get; set; }
        public int? VolumeId { get; set; }
        public int? InventoryId { get; set; }
        public int? Page { get; set; }
        public bool ForDestruction { get; set; }
        public bool DestructionMark { get; set; }
        public string? Note { get; set; }
        public VolumesDto VolumeDto { get; set; }
        public InvertoriesDto InventoryDto { get; set; }


        public DocumentsDto()
        {

        }
    }
}

