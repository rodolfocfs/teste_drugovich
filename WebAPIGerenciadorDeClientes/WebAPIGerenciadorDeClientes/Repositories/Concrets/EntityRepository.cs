using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;
using WebAPIGerenciadorDeClientes.Models;

namespace WebAPIGerenciadorDeClientes.Repositories
{
    public class EntityRepository<TEntity> : EntityRepository<TEntity, ModelContext>
        where TEntity : class
    {
        public EntityRepository(ModelContext context) : base(context)
        {
        }
    }

    public class EntityRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected DbContext Context { get; }
        private readonly DbSet<TEntity> _dbSet;

        public EntityRepository(TContext context)
        {
            Context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual TEntity GetById(long id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> whereClause = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool asNoTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (asNoTracking)
                query = query.AsNoTracking();

            if (whereClause != null)
                query = query.Where(whereClause);

            if (include != null)
                query = include(query);

            if (orderBy != null)
                return orderBy(query);
            else
                return query;
        }

        public virtual IEnumerable<TEntity> Get(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool asNoTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (asNoTracking)
                query = query.AsNoTracking();

            if (filter != null)
                query = filter(query);

            if (include != null)
                query = include(query);

            if (orderBy != null)
                return orderBy(query);
            else
                return query;
        }

        public virtual IEnumerable<T> Get<T>(
            Expression<Func<TEntity, T>> mapping,
            Expression<Func<T, bool>> whereClause = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            bool asNoTracking = false)
        {
            var dbSet = _dbSet.AsQueryable();

            if (asNoTracking)
                dbSet = dbSet.AsNoTracking();

            IQueryable<T> query = dbSet.Select(mapping);

            if (whereClause != null)
                query = query.Where(whereClause);

            if (orderBy != null)
                return orderBy(query);
            else
                return query;
        }

        public bool Any(Expression<Func<TEntity, bool>> anyClause = null)
        {
            return _dbSet.Any(anyClause);
        }

        #region Sync CRUD methods

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Remove(long id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void RemoveAndSave(TEntity entityToDelete)
        {
            _dbSet.Remove(entityToDelete);
            Context.SaveChanges();
        }

        public virtual void RemoveRangeAndSave(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            Context.SaveChanges();
        }

        public virtual TEntity AddAndSave(TEntity entity, bool saveWithNestedProperties = true)
        {
            if (entity == null)
                return entity;

            if (!saveWithNestedProperties)
                DeleteNestedProperties(entity);

            var saved = _dbSet.Add(entity);
            Context.SaveChanges();

            return saved.Entity;
        }

        public virtual IEnumerable<TEntity> AddRangeAndSave(IEnumerable<TEntity> entities,
            bool saveWithNestedProperties = true)
        {
            if (entities == null || !entities.Any())
                return entities;

            if (!saveWithNestedProperties)
                DeleteNestedProperties(entities);

            _dbSet.AddRange(entities);
            Context.SaveChanges();

            return entities;
        }

        public virtual TEntity UpdateAndSave(TEntity entity, bool saveWithNestedProperties = true)
        {
            if (entity == null)
                return entity;

            if (!saveWithNestedProperties)
                DeleteNestedProperties(entity);

            var updated = _dbSet.Update(entity);

            Context.SaveChanges();

            return updated.Entity;
        }

        public virtual IEnumerable<TEntity> UpdateRangeAndSave(IEnumerable<TEntity> entities,
            bool saveWithNestedProperties = true)
        {
            if (entities == null || !entities.Any())
                return entities;

            if (!saveWithNestedProperties)
                DeleteNestedProperties(entities);

            _dbSet.UpdateRange(entities);
            Context.SaveChanges();

            return entities;
        }

        public virtual IEnumerable<TEntity> RemoveRangeAndSave(IEnumerable<TEntity> entities,
             bool saveWithNestedProperties = true)
        {
            if (entities == null || !entities.Any())
                return entities;

            if (!saveWithNestedProperties)
                DeleteNestedProperties(entities);

            _dbSet.RemoveRange(entities);
            Context.SaveChanges();

            return entities;
        }

        #endregion

        public virtual async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> AddAndSaveAsync(TEntity entity,
            bool saveWithNestedProperties = true)
        {
            if (entity == null)
                return entity;

            if (!saveWithNestedProperties)
                DeleteNestedProperties(entity);

            var saved = _dbSet.Add(entity);
            await Context.SaveChangesAsync();

            return saved.Entity;
        }

        public virtual async Task<IEnumerable<TEntity>> AddRangeAndSaveAsync(IEnumerable<TEntity> entities,
            bool saveWithNestedProperties = true)
        {
            if (entities == null || !entities.Any())
                return entities;

            if (!saveWithNestedProperties)
                DeleteNestedProperties(entities);

            _dbSet.AddRange(entities);
            await Context.SaveChangesAsync();
            return entities;
        }

        public virtual async Task<TEntity> UpdateAndSaveAsync(TEntity entity,
            bool saveWithNestedProperties = true)
        {
            if (entity == null)
                return entity;

            if (!saveWithNestedProperties)
                DeleteNestedProperties(entity);

            var updated = _dbSet.Update(entity);
            await Context.SaveChangesAsync();

            return updated.Entity;
        }

        public virtual async Task<IEnumerable<TEntity>> UpdateRangeAndSaveAsync(IEnumerable<TEntity> entities,
            bool saveWithNestedProperties = true)
        {
            if (entities == null || !entities.Any())
                return entities;

            if (!saveWithNestedProperties)
                DeleteNestedProperties(entities);

            _dbSet.UpdateRange(entities);
            await Context.SaveChangesAsync();

            return entities;
        }

        public TEntity DeleteNestedProperties(TEntity entity)
        {
            Type entityType = typeof(TEntity);
            List<PropertyInfo> virtualProperties = entityType.GetProperties()
                .Where(p => p.GetGetMethod().IsVirtual && !p.GetGetMethod().IsFinal && p.PropertyType.IsClass)
                .ToList();

            virtualProperties.ForEach(prop =>
            {
                entityType
                    .GetProperties()
                    .Where(x => x.Name == prop.Name).ToList()
                    .ForEach(y => y.SetValue(entity, null));
            });

            return entity;
        }

        public IEnumerable<TEntity> DeleteNestedProperties(IEnumerable<TEntity> entities)
        {
            Type entityType = typeof(TEntity);
            List<PropertyInfo> virtualProperties = entityType.GetProperties()
                .Where(p => p.GetGetMethod().IsVirtual && !p.GetGetMethod().IsFinal && p.PropertyType.IsClass)
                .ToList();

            virtualProperties.ForEach(prop =>
            {
                /* Returns 1 instance, unless a subclass hides a property with 'new' keyword */
                var propsInfo = entityType.GetProperties().Where(x => x.Name == prop.Name).ToList();

                foreach (var entity in entities)
                {
                    propsInfo.ForEach(propInfo => propInfo.SetValue(entity, null));
                }
            });

            return entities;
        }
    }
}
