using AutoMapper;
using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Application.Contracts.Logic.Services.OVTs;
using Inspector.Application.Contracts.Logic.Services.OVTs.Models;
using Inspector.Domains.Entities;

namespace Inspector.Logic.Services
{
    public class OVTsService : IOVTsService
    {
        internal readonly IOVTsRepository _oVTsRepository;
        internal readonly IMapper _mapper;

        public OVTsService(IOVTsRepository oVTsRepository, IMapper mapper)
        {
            _oVTsRepository = oVTsRepository;
            _mapper = mapper;
        }
        public async Task<OVTsDto> CreateAsync(OVTsDto ovtDto)
        {
            var ovtDb = _mapper.Map<OVTsDb>(ovtDto);
            return _mapper.Map<OVTsDto>(await _oVTsRepository.CreateAsync(ovtDb));
        }

        public async Task DeleteAsync(OVTsDto ovtDto)
        {
            await _oVTsRepository.DeleteAsync(ovtDto.Id);
        }

        public async Task<List<OVTsDto>> GetAll()
        {
            var list = _mapper.Map<List<OVTsDto>>(await _oVTsRepository.GetAll());
            return list;
        }

        public async Task<OVTsDto> GetAsync(int Id)
        {
            return _mapper.Map<OVTsDto>(await _oVTsRepository.GetAsync(Id));
        }

        public async Task SaveChangesAsync()
        {
            await _oVTsRepository.SaveChangesAsync();
        }

        public async Task<OVTsDto> UpdateAsync(OVTsDto cabDto)
        {
            var cabDb = await _oVTsRepository.GetAsync(cabDto.Id);

            var dbProperties = typeof(OVTsDb).GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            bool hasChanges = false;

            foreach (var property in typeof(OVTsDto).GetProperties())
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

            return _mapper.Map<OVTsDto>(await _oVTsRepository.UpdateAsync(cabDb));
        }
    }
}
