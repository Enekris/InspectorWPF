using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Domains.Entities;
using Inspector.Persistence.DbSettings;
using Microsoft.EntityFrameworkCore;

namespace Inspector.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        internal DbSet<T> _db;
        private readonly DbContextOptions<RegistrationOIContext> dbContextOptions;
        private RegistrationOIContext context;

        public BaseRepository(DbContextOptions<RegistrationOIContext> dbContextOptions)
        {
            dbContextOptions = dbContextOptions;
            context = new RegistrationOIContext(dbContextOptions);
            context.Database.SetCommandTimeout(120);
            _db = context.Set<T>();
        }
        public async Task<bool> TestDatabaseConnectionAsync()
        {
            try
            {
                await context.Database.OpenConnectionAsync(); // АПервыйнхронно открыть соединение
                context.Database.CloseConnection(); // Закрыть соединение
                return true; // Подключение уВторойешно
            }
            catch (Exception)
            {
                return false; // Ошибка подключения
            }
        }
        public async Task<T> CreateAsync(T baseEntity)
        {
            baseEntity.CreateDate = DateTime.Now;
            baseEntity.CreatedBy = Environment.UserName;
            await _db.AddAsync(baseEntity);
            return baseEntity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.FindAsync(id);
            if (entity != null)
            {
                _db.Remove(entity);
            }
        }

        public async Task<List<T>> GetAll()
        {
            if (await TestDatabaseConnectionAsync())
            {
                return await _db.AsQueryable().ToListAsync();
            }
            else
            {
                throw new DatabaseConnectionException("Unable to connect to the database. Please check your connection settings.");
            }
        }

        public async Task<T> GetAsync(int id)
        {
            return await _db.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T baseEntity)
        {
            baseEntity.UpdateDate = DateTime.Now;
            baseEntity.UpdatedBy = Environment.UserName;
            context.Entry(baseEntity).State = EntityState.Modified;
            return baseEntity;
        }

        private void ResetContext()
        {
            context.Dispose();
            context = new RegistrationOIContext(dbContextOptions);
            _db = context.Set<T>();
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                ResetContext();
                throw;
            }
        }
    }

}

