using Microsoft.Data.Sqlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApp.Data;

namespace Services.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly SQLiteAsyncConnection _database; 
        private SqliteTransaction _transaction;

        public GenericRepository(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<T>().Wait(); 
        }

        // CRUD Operations (Asynchronous)
        public async Task<List<T>> GetAllAsync()
        {
            return await _database.Table<T>().ToListAsync(); 
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _database.FindAsync<T>(id); 
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _database.Table<T>().Where(predicate).ToListAsync(); 
        }

        public async Task<int> AddAsync(T entity)
        {
            return await _database.InsertAsync(entity); 
        }

        public async Task<int> UpdateAsync(T entity)
        {
            return await _database.UpdateAsync(entity); 
        }

        public async Task<int> DeleteAsync(T entity)
        {
            return await _database.DeleteAsync(entity); 
        }

        public async Task<List<T>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null, string? orderByColumn = null, bool descending = false)
        {
            var query = _database.Table<T>();

            // Applying filter if predicate exists
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            // Skipping records for pagination
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(); // عملیات ناهمگام
        }
        public async Task<int> CountAsync()
        {
            return await _database.Table<T>().CountAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            var count = await _database.Table<T>().Where(predicate).CountAsync();
            return count > 0;
        }

        //public async Task BeginTransactionAsync()
        //{
        //    
        //}

        //public async Task CommitTransactionAsync()
        //{
        //   
        //}

        //public async Task RollbackTransactionAsync()
        //{
        //    
        //}



    }
}
