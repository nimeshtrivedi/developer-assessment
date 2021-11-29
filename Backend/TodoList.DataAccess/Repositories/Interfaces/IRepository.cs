using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TodoList.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // async Task< IEnumerable<T>>

        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T> Add(T entity);
        Task Update(T entity);

 
    }
}
