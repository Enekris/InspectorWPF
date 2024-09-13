using AutoMapper;
using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Application.Contracts.Logic.Services.HardwareFilterName;
using Inspector.Application.Contracts.Logic.Services.HardwareFilterName.Models;
using Inspector.Domains.Entities;

namespace Inspector.Logic.Services
{
    public class HardwareFilterNameService : IHardwareFilterNameService
    {
        internal readonly IHardwareFilterNameRepository _hardwareFilterNameRepository;
        internal readonly IMapper _mapper;

        public HardwareFilterNameService(IHardwareFilterNameRepository hardwareFilterNameRepository, IMapper mapper)
        {
            _hardwareFilterNameRepository = hardwareFilterNameRepository;
            _mapper = mapper;
        }
        public async Task<HardwareFilterNameDto> CreateAsync(HardwareFilterNameDto hardDto)
        {
            var hardDb = _mapper.Map<HardwareFilterNameDb>(hardDto);
            return _mapper.Map<HardwareFilterNameDto>(await _hardwareFilterNameRepository.CreateAsync(hardDb));
        }

        public async Task DeleteAsync(HardwareFilterNameDto hardDto)
        {

            await _hardwareFilterNameRepository.DeleteAsync(hardDto.Id);
        }

        public async Task<List<HardwareFilterNameDto>> GetAll()
        {
            var list = _mapper.Map<List<HardwareFilterNameDto>>(await _hardwareFilterNameRepository.GetAll());
            return list;
        }

        public async Task<HardwareFilterNameDto> GetAsync(int Id)
        {
            return _mapper.Map<HardwareFilterNameDto>(await _hardwareFilterNameRepository.GetAsync(Id));
        }

        public async Task SaveChangesAsync()
        {
            await _hardwareFilterNameRepository.SaveChangesAsync();
        }

        public async Task<HardwareFilterNameDto> UpdateAsync(HardwareFilterNameDto cabDto)
        {
            var cabDb = await _hardwareFilterNameRepository.GetAsync(cabDto.Id);

            var dbProperties = typeof(HardwareFilterNameDb).GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            bool hasChanges = false;

            foreach (var property in typeof(HardwareFilterNameDto).GetProperties())
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

            return _mapper.Map<HardwareFilterNameDto>(await _hardwareFilterNameRepository.UpdateAsync(cabDb));
        }
    }
}
