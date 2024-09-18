﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.Base.Interface;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Contract.Repository.Pagination;
using Petalaka.Account.Repository.Base;

namespace Petalaka.Account.Repository.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseEntity
{
    private readonly PetalakaDbContext _dbContext;
    private readonly DbSet<T> _dbSet;
    
    public GenericRepository(PetalakaDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public T Find(Expression<Func<T, bool>> predicate) => _dbSet.FirstOrDefault(predicate);
    public T FindUndeleted(Expression<Func<T, bool>> predicate) =>
        _dbSet.Where(entity => entity.DeletedTime == null)
            .Where(predicate)
            .FirstOrDefault();
    
    public async Task<T> FindUndeletedAsync(Expression<Func<T, bool>> predicate) =>
        await _dbSet.Where(entity => entity.DeletedTime == null)
            .Where(predicate)
            .FirstOrDefaultAsync();
    
    public IQueryable<T> AsQueryable() => _dbSet.AsQueryable();
    public IQueryable<T> AsQueryablePredicate(Expression<Func<T, bool>> predicate) =>
        _dbSet.Where(predicate)
            .AsQueryable();
    public IQueryable<T> AsQueryableUndeletedPredicate(Expression<Func<T, bool>> predicate) =>
        _dbSet.Where(entity => entity.DeletedTime == null)
            .Where(predicate)
            .AsQueryable();


    public async Task<IEnumerable<T>> FindAll() => await _dbSet.ToListAsync();
    
    public async Task<IEnumerable<T>> FindAllPredicate(Expression<Func<T, bool>> predicate) =>
        await _dbSet.Where(predicate)
            .ToListAsync();
    
    public async Task<IEnumerable<T>> FindAllUndeletedPredicate(Expression<Func<T, bool>> predicate) =>
        await _dbSet.Where(entity => entity.DeletedTime == null)
            .Where(predicate)
            .ToListAsync();
    public async Task<IEnumerable<T>> FindAllUndeleted() =>
        await _dbSet.Where(entity => entity.DeletedTime == null)
            .ToListAsync();
    public void Insert(T entity) => _dbSet.Add(entity);
    public async Task InsertAsync(T entity) => await _dbSet.AddAsync(entity);
    public void SaveChanges() => _dbContext.SaveChanges();
    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    public void Update(T entity) => _dbContext.Entry(entity).State = EntityState.Modified;
    public void Delete(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        entity.DeletedTime = DateTime.UtcNow;
    }
    public void DeletePermanent(T entity) => _dbContext.Entry(entity).State = EntityState.Deleted;

    public async Task<PaginationResponse<T>> GetPagination(int pageIndex, int pageSize)
    {
        int totalRecords = await _dbSet.CountAsync(p => p.DeletedTime == null);
        var data = await _dbSet.Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Where(p => p.DeletedTime == null)
            .ToListAsync();
        return new PaginationResponse<T>(data, pageIndex, pageSize, totalRecords);
    }
}