namespace AutofacGenericAPI.Repositories
{
    using System.Linq;
    using Services;
    using System.Threading.Tasks;
    using Model;
    using System.Data.Entity;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ProyectoGloboDBEntities _dbContext;

        private IDbService DbService { get; }

        private ProyectoGloboDBEntities DbContext => _dbContext ?? (_dbContext = DbService.Init());

        public GenericRepository(IDbService dbService)
        {
            DbService = dbService;
        }

        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public Task<T> GetByIdAsync(int id)
        {
            return DbContext.Set<T>().FindAsync(id);
        }

        public Task PostTaskAsync(T t)
        {
            DbContext.Set<T>().Add(t);
            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteTaskAsync(int id)
        {
            var deleteObject = DbContext.Set<T>().FindAsync(id).Result;

            if (deleteObject == null) return Task.CompletedTask;
            DbContext.Set<T>().Remove(deleteObject);
            return  _dbContext.SaveChangesAsync();

        }

        public Task PutTaskAsync(T t)
        {
            DbContext.Entry(t).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}