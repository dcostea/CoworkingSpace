using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoworkingSpace.Repository
{
    public interface IRepository<T>
    {
        void Add(T item);
        IEnumerable<T> GetAll();
        T Find(int id);
        void Remove(T item);
        void Update(T item);

        Task AddAsync(T item);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindAsync(int id);
        Task RemoveAsync(T item);
        Task UpdateAsync(T item);
    }
}
