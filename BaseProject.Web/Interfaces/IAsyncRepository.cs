using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Web.Interfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task DeleteRangeAsync(IEnumerable<T> entities);

        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdAsync<TId>(TId id);

        Task<T> GetBySpecAsync(ISpecification<T> specification);

        Task<IReadOnlyList<T>> ListAllAsync();

        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);

        Task<IReadOnlyList<TResult>> ListAsyncSelect<TResult>(ISpecification<T, TResult> specification);

        Task<int> CountAsync(ISpecification<T> specification);
    }
}