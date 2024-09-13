using AutoMapper;
using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Application.Contracts.Logic.Services.Hardwares;
using Inspector.Application.Contracts.Logic.Services.Hardwares.Models;
using Inspector.Domains.Entities;

namespace Inspector.Logic.Services
{
    public class HardwaresService : IHardwaresService
    {
        internal readonly IHardwareRepository _hardwareRepository;
        internal readonly IMapper _mapper;

        public HardwaresService(IHardwareRepository hardwareRepository, IMapper mapper)
        {
            _hardwareRepository = hardwareRepository;
            _mapper = mapper;
        }
        public async Task<HardwaresDto> CreateAsync(HardwaresDto hardDto)
        {
            var hardDb = _mapper.Map<HardwaresDb>(hardDto);
            return _mapper.Map<HardwaresDto>(await _hardwareRepository.CreateAsync(hardDb));
        }

        public async Task DeleteAsync(HardwaresDto hardDto)
        {
            await _hardwareRepository.DeleteAsync(hardDto.Id);
        }

        public async Task<List<HardwaresDto>> GetAll()
        {
            var list = _mapper.Map<List<HardwaresDto>>(await _hardwareRepository.GetAll());
            return list;
        }

        public async Task<List<HardwaresDto>> GetAllIncludeForCabinetTree()
        {
            var list = _mapper.Map<List<HardwaresDto>>(await _hardwareRepository.GetAllIncludeForCabinetTree());
            return list;
        }

        public async Task<HardwaresDto> GetAsync(int Id)
        {
            return _mapper.Map<HardwaresDto>(await _hardwareRepository.GetAsync(Id));
        }

        public async Task SaveChangesAsync()
        {
            await _hardwareRepository.SaveChangesAsync();
        }

        public async Task<HardwaresDto> UpdateAsync(HardwaresDto cabDto)
        {
            var cabDb = await _hardwareRepository.GetAsync(cabDto.Id);

            var dbProperties = typeof(HardwaresDb).GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            bool hasChanges = false;

            foreach (var property in typeof(HardwaresDto).GetProperties())
            {
                if (property.Name == "Id")
                {
                    continue;
                }

                var newValue = property.GetValue(cabDto);

                if (newValue is string str)
                {
                    newValue = string.IsNullOrEmpty(str) ? null : newValue;
                }
                else if (newValue is int integer)
                {
                    newValue = integer == 0 ? null : newValue;
                }

                if (dbProperties.TryGetValue(property.Name, out var dbProperty))
                {
                    var currentValue = dbProperty.GetValue(cabDb);

                    if (!Equals(newValue, currentValue))
                    {
                        hasChanges = true;
                        dbProperty.SetValue(cabDb, newValue);
                    }
                }
            }

            if (!hasChanges)
            {
                return cabDto;
            }

            return _mapper.Map<HardwaresDto>(await _hardwareRepository.UpdateAsync(cabDb));
        }
    }
}
