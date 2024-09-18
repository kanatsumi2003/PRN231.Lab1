using System.Linq.Expressions;
using Petalaka.Account.Contract.Repository.Pagination;

namespace Petalaka.Account.Contract.Repository.Interface;

public interface IGenericRepository<T> where T : class
{
    T Find(Expression<Func<T, bool>> predicate);
    T FindUndeleted(Expression<Func<T, bool>> predicate);
    Task<T> FindUndeletedAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAll();
    IQueryable<T> AsQueryable();
    IQueryable<T> AsQueryablePredicate(Expression<Func<T, bool>> predicate);
    IQueryable<T> AsQueryableUndeletedPredicate(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAllPredicate(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAllUndeletedPredicate(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAllUndeleted();
    void Insert(T entity);
    Task InsertAsync(T entity);
    void SaveChanges();
    Task SaveChangesAsync();
    void Update(T entity);
    void Delete(T entity);
    Task<PaginationResponse<T>> GetPagination(int pageIndex, int pageSize);
    void DeletePermanent(T entity);
}