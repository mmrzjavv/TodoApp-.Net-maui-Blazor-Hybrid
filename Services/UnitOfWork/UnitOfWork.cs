using Services.Entities;
using Services.Repository;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Data;

namespace Services.UnitOfWork
{
    public class UnitOfWork
    {
        private readonly SQLiteAsyncConnection _database;

        public IGenericRepository<Todo> TodoRepository { get; }
        public IGenericRepository<Category> CategoryRepository { get; }

        public UnitOfWork(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            TodoRepository = new GenericRepository<Todo>(_database);
            CategoryRepository = new GenericRepository<Category>(_database);
        }
    }
}
