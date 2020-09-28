using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseProject.Web.Interfaces;
using BaseProject.Web.Models;

namespace BaseProject.Web.Repository
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;
        private readonly ISpecificationEvaluator<T> _specificationEvaluator;

        public EfRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _specificationEvaluator = new SpecificationEvaluator<T>();
        }

        public EfRepository(ApplicationDbContext dbContext, ISpecificationEvaluator<T> specificationEvaluator)
        {
            _dbContext = dbContext;
            _specificationEvaluator = specificationEvaluator;
        }

        public async Task<T> GetByIdAsync<TId>(TId id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetBySpecAsync(ISpecification<T> specification)
        {
            return (await ListAsync(specification)).FirstOrDefault();
        }

        public async Task<IReadOnlyList<TResult>> ListAsyncSelect<TResult>(ISpecification<T, TResult> specification)
        {
            if (specification is null) throw new ArgumentNullException("Specification is required");
            if (specification.Selector is null) throw new Exception("Specification must have Selector defined.");

            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.CountAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> FirstAsync(ISpecification<T> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.FirstAsync();
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return _specificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }

        private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
        {
            return _specificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), specification);
        }
    }
}