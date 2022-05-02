using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
namespace WebAPIGerenciadorDeClientes.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(long id);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> whereClause = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool asNoTracking = false);

        IEnumerable<TEntity> Get(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool asNoTracking = false);

        IEnumerable<T> Get<T>(
            Expression<Func<TEntity, T>> mapping,
            Expression<Func<T, bool>> whereClause = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool asNoTracking = false);

        bool Any(Expression<Func<TEntity, bool>> anyClause = null);

        #region Sync CRUD methods

        void Add(TEntity obj);
        void AddRange(IEnumerable<TEntity> objs);
        void Update(TEntity obj);
        void Remove(long id);
        void Delete(TEntity entityToDelete);
        void RemoveAndSave(TEntity entityToDelete);
        void RemoveRangeAndSave(IEnumerable<TEntity> entityToDelete);
        TEntity AddAndSave(TEntity obj, bool saveWithNestedProperties = true);
        IEnumerable<TEntity> AddRangeAndSave(IEnumerable<TEntity> objs, bool saveWithNestedProperties = true);
        TEntity UpdateAndSave(TEntity obj, bool saveWithNestedProperties = true);
        IEnumerable<TEntity> UpdateRangeAndSave(IEnumerable<TEntity> objs, bool saveWithNestedProperties = true);
        IEnumerable<TEntity> RemoveRangeAndSave(IEnumerable<TEntity> entities, bool saveWithNestedProperties = true);

        #endregion

        #region Async CRUD methods

        Task SaveAsync();
        Task<TEntity> AddAndSaveAsync(TEntity obj, bool saveWithNestedProperties = true);
        Task<IEnumerable<TEntity>> AddRangeAndSaveAsync(IEnumerable<TEntity> objs, bool saveWithNestedProperties = true);
        Task<TEntity> UpdateAndSaveAsync(TEntity obj, bool saveWithNestedProperties = true);
        Task<IEnumerable<TEntity>> UpdateRangeAndSaveAsync(IEnumerable<TEntity> objs, bool saveWithNestedProperties = true);

        #endregion
    }
}
