using AutoMapper;
using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Application.Contracts.Logic.Services.Volumes;
using Inspector.Application.Contracts.Logic.Services.Volumes.Models;
using Inspector.Domains.Entities;

namespace Inspector.Logic.Services
{
    public class VolumesService : IVolumesService
    {
        internal readonly IVolumesRepository _volumesRepository;
        internal readonly IMapper _mapper;

        public VolumesService(IVolumesRepository volumesRepository, IMapper mapper)
        {
            _volumesRepository = volumesRepository;
            _mapper = mapper;
        }
        public async Task<VolumesDto> CreateAsync(VolumesDto volDto)
        {
            var volDb = _mapper.Map<VolumesDb>(volDto);
            return _mapper.Map<VolumesDto>(await _volumesRepository.CreateAsync(volDb));
        }

        public async Task DeleteAsync(VolumesDto volDto)
        {
            await _volumesRepository.DeleteAsync(volDto.Id);
        }

        public async Task<List<VolumesDto>> GetAll()
        {
            var list = _mapper.Map<List<VolumesDto>>(await _volumesRepository.GetAll());
            return list;
        }

        public async Task<List<VolumesDto>> GetAllIncludeForVolumesTree()
        {
            var list = _mapper.Map<List<VolumesDto>>(await _volumesRepository.GetAllIncludeForVolumesTree());
            return list;
        }

        public async Task<VolumesDto> GetAsync(int Id)
        {
            return _mapper.Map<VolumesDto>(await _volumesRepository.GetAsync(Id));
        }

        public async Task SaveChangesAsync()
        {
            await _volumesRepository.SaveChangesAsync();
        }

        public async Task<VolumesDto> UpdateAsync(VolumesDto cabDto)
        {
            var cabDb = await _volumesRepository.GetAsync(cabDto.Id);

            var dbProperties = typeof(VolumesDb).GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            bool hasChanges = false;

            foreach (var property in typeof(VolumesDto).GetProperties())
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

            return _mapper.Map<VolumesDto>(await _volumesRepository.UpdateAsync(cabDb));
        }
    }
}
