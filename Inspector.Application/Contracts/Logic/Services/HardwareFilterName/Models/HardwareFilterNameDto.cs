using Inspector.Application.Contracts.Logic.Services.Hardwares.Models;

namespace Inspector.Application.Contracts.Logic.Services.HardwareFilterName.Models
{
    public class HardwareFilterNameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<HardwaresDto> HardwaresDto { get; set; } = [];

        public HardwareFilterNameDto()
        {

        }
    }
}
