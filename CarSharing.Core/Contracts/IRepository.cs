using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharing.Contracts
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetByExpression(Func<T, bool> expression);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
