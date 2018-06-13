namespace AutofacGenericAPI.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task PostTaskAsync(T t);
        Task DeleteTaskAsync(int id);
        Task PutTaskAsync(T t);
        void Dispose();
    }
}