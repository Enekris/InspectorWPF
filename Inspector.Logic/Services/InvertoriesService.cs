using AutoMapper;
using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Application.Contracts.Logic.Services.Invertories;
using Inspector.Application.Contracts.Logic.Services.Invertories.Models;
using Inspector.Domains.Entities;

namespace Inspector.Logic.Services
{
    public class InvertoriesService : IInvertoriesService
    {
        internal readonly IInvertoriesRepository _inventoriesRepository;
        internal readonly IMapper _mapper;

        public InvertoriesService(IInvertoriesRepository inventoriesRepository, IMapper mapper)
        {
            _inventoriesRepository = inventoriesRepository;
            _mapper = mapper;
        }
        public async Task<InvertoriesDto> CreateAsync(InvertoriesDto invDto)
        {
            var invDb = _mapper.Map<InvertoriesDb>(invDto);
            return _mapper.Map<InvertoriesDto>(await _inventoriesRepository.CreateAsync(invDb));
        }

        public async Task DeleteAsync(InvertoriesDto invDto)
        {
            await _inventoriesRepository.DeleteAsync(invDto.Id);
        }

        public async Task<List<InvertoriesDto>> GetAll()
        {
            var list = _mapper.Map<List<InvertoriesDto>>(await _inventoriesRepository.GetAll());
            return list;
        }

        public async Task<InvertoriesDto> GetAsync(int Id)
        {
            return _mapper.Map<InvertoriesDto>(await _inventoriesRepository.GetAsync(Id));
        }

        public async Task SaveChangesAsync()
        {
            await _inventoriesRepository.SaveChangesAsync();
        }

        public async Task<InvertoriesDto> UpdateAsync(InvertoriesDto cabDto)
        {
            var cabDb = await _inventoriesRepository.GetAsync(cabDto.Id);

            var dbProperties = typeof(InvertoriesDb).GetProperties()
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            bool hasChanges = false;

            foreach (var property in typeof(InvertoriesDto).GetProperties())
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

            return _mapper.Map<InvertoriesDto>(await _inventoriesRepository.UpdateAsync(cabDb));
        }
    }
}
