using AutoMapper;
using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Application.Contracts.Logic.Services.Cabinets;
using Inspector.Application.Contracts.Logic.Services.Cabinets.Models;
using Inspector.Domains.Entities;

namespace Inspector.Logic.Services
{
    public class CabinetService : ICabinetService
    {
        internal readonly ICabinetsRepository _cabinetsRepository;
        internal readonly IMapper _mapper;

        public CabinetService(ICabinetsRepository cabinetsRepository, IMapper mapper)
        {
            _cabinetsRepository = cabinetsRepository;
            _mapper = mapper;
        }

        public async Task<CabinetsDto> CreateAsync(CabinetsDto cabDto)
        {
            var cabDb = _mapper.Map<CabinetsDb>(cabDto);
            return _mapper.Map<CabinetsDto>(await _cabinetsRepository.CreateAsync(cabDb));
        }

        public async Task DeleteAsync(CabinetsDto cabDto)
        {
            await _cabinetsRepository.DeleteAsync(cabDto.Id);
        }

        public async Task<List<CabinetsDto>> GetAll()
        {
            var list = _mapper.Map<List<CabinetsDto>>(await _cabinetsRepository.GetAll());
            return list;
        }

        public async Task<List<CabinetsDto>> GetAllIncludeForCabinetTree()
        {
            var list = _mapper.Map<List<CabinetsDto>>(await _cabinetsRepository.GetAllIncludeForCabinetTree());
            return list;
        }

        public async Task<CabinetsDto> GetAsync(int Id)
        {
            return _mapper.Map<CabinetsDto>(await _cabinetsRepository.GetAsync(Id));
        }

        public async Task SaveChangesAsync()
        {
            await _cabinetsRepository.SaveChangesAsync();
        }

        public async Task<CabinetsDto> UpdateAsync(CabinetsDto cabDto)
        {
            var cabDb = await _cabinetsRepository.GetAsync(cabDto.Id);

            var dbProperties = typeof(CabinetsDb).GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            bool hasChanges = false;

            foreach (var property in typeof(CabinetsDto).GetProperties())
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

            return _mapper.Map<CabinetsDto>(await _cabinetsRepository.UpdateAsync(cabDb));
        }
    }
}
