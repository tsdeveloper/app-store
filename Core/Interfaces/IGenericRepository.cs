using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;


namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync (Expression<Func<T, bool>> filter = null, 
            params Expression<Func<T, object>>[] includes);
        
        Task<IReadOnlyCollection<T>> GetTAllAsync (int page = 1, int pageTotal = 10, 
            Expression<Func<T, bool>> filter = null, 
            Expression<Func<IQueryable<T>,IOrderedQueryable<T>>> orderFilter = null, 
            params Expression<Func<T, object>>[] includes);
    }
}