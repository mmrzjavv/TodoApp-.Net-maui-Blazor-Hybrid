using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoApp.Data
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task<int> AddAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(T entity);

        Task<int> CountAsync();

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null, string? orderByColumn = null, bool descending = false);

        //void BeginTransaction();

        //Task CommitTransactionAsync();

        //void RollbackTransaction();
    }
}
