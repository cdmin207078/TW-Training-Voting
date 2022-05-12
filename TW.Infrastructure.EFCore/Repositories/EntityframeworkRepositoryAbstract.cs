using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TW.Infrastructure.Core.AbstractModelObjects;
using TW.Infrastructure.Core.Primitives;

namespace TW.Infrastructure.EFCore.Repository;

public abstract class EntityframeworkRepositoryAbstract
{
    protected readonly DbContext dbContext;

    protected EntityframeworkRepositoryAbstract(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    #region Add
    
    public void Add<TEntity>(TEntity entity) where TEntity : AbstractModelObject
    {
        entity.IsNullThrowException();
    
        dbContext.Set<TEntity>().Attach(entity);
    }
    
    public void Add<TEntity>(List<TEntity> entities) where TEntity : AbstractModelObject
    {
        entities.IsNullOrEmptyThrowException();
    
        dbContext.Set<TEntity>().AttachRange(entities);
    }
    
    #endregion

    #region Remove
    
    public void Remove<TEntity>(TEntity entity) where TEntity : AbstractModelObject
    {
        entity.IsNullThrowException();

        dbContext.Entry(entity).State = EntityState.Deleted;
    }
    
    public void Remove<TEntity>(List<TEntity> entities) where TEntity : AbstractModelObject
    {
        entities.IsNullOrEmptyThrowException();
    
        foreach (var entity in entities)
        {
            Remove(entity);
        }
    }
    
    #endregion

    #region Update

    public void Update<TEntity>(TEntity source, TEntity destination) where TEntity : AbstractModelObject
    {
        source.IsNullThrowException();
        destination.IsNullThrowException();

        // dbContext.Set<TEntity>().Attach(source).CurrentValues.SetValues(destination);
        dbContext.Entry(source).State = EntityState.Unchanged;
        dbContext.Entry(source).CurrentValues.SetValues(destination);
    }
    
    public void Update<TEntity>(List<TEntity> sources, List<TEntity> destination) where TEntity : AbstractModelObject
    {
        sources.IsNullThrowException();
        destination.IsNullThrowException();
        
        // // 1. attach
        // foreach (var dest in destination)
        // {
        //     if()
        // }
        // dbContext.AttachRange(destination);
        
        // 2. remove or update
        // sources.RemoveAll(s => !destination.Any(d => d.Equals(s)));
        // foreach (var source in sources)
        // {
        //     var dest = destination.FirstOrDefault(x => source.Equals(x));
        //     if (dest is null)
        //         dbContext.Entry(source).State = EntityState.Deleted;
        //     else
        //         dbContext.Entry(source).CurrentValues.SetValues(dest);
        // }
        
        // 1. remove
        // sources.RemoveAll(s => !destination.Any(d => d.Equals(s)));
        foreach (var source in sources)
        {
            if (!destination.Any(x => source.Equals(x)))
                dbContext.Entry(source).State = EntityState.Deleted;
        }
        
        // 2. add or update
        foreach (var dest in destination)
        {
            var source = sources.FirstOrDefault(x => x.Equals(dest));
        
            if (source is null)
            {
                dbContext.Entry(dest).State = EntityState.Added;
                // if (dbContext.Entry(dest).State is not EntityState.Added)
                // {
                //     dbContext.Set<TEntity>().Attach(dest);
                // }
            }
            else
            {
                dbContext.Entry(source).State = EntityState.Unchanged;
                dbContext.Entry(source).CurrentValues.SetValues(dest);
            }
        }
    }

    #endregion

    #region Query

    #region Any

    public Task<bool> FindAny<TEntity>(int id) where TEntity : AbstractModelObject<int> => FindAny<TEntity, int>(id);
    public Task<bool> FindAny<TEntity>(long id) where TEntity : AbstractModelObject<long> => FindAny<TEntity, long>(id);
    public Task<bool> FindAny<TEntity>(string id) where TEntity : AbstractModelObject<string> => FindAny<TEntity, string>(id);
    public Task<bool> FindAny<TEntity>(Id<int> id) where TEntity : AbstractModelObject<int> => FindAny<TEntity, int>(id);
    public Task<bool> FindAny<TEntity>(Id<long> id) where TEntity : AbstractModelObject<long> => FindAny<TEntity, long>(id);
    public Task<bool> FindAny<TEntity>(Id<string> id) where TEntity : AbstractModelObject<string> => FindAny<TEntity, string>(id);

    public Task<bool> FindAny<TEntity, TKey>(TKey id) where TEntity : AbstractModelObject<TKey>
        => FindAny<TEntity>(x => x.Id.Equals(id));

    public Task<bool> FindAny<TEntity, TKey>(Id<TKey> id) where TEntity : AbstractModelObject<TKey>
        => id.IsNotNull() ? FindAny<TEntity>(x => x.Id.Equals(id.Value)) : Task.FromResult(false);

    public Task<bool> FindAny<TEntity>(Expression<Func<TEntity, bool>> predicate = default) where TEntity : AbstractModelObject
        => predicate.IsNull() ? dbContext.Set<TEntity>().AnyAsync() : dbContext.Set<TEntity>().AnyAsync(predicate);

    #endregion

    #region Find

    public Task<TEntity> Find<TEntity>(int id) where TEntity : AbstractModelObject<int> => Find<TEntity, int>(id);
    public Task<TEntity> Find<TEntity>(long id) where TEntity : AbstractModelObject<long> => Find<TEntity, long>(id);
    public Task<TEntity> Find<TEntity>(string id) where TEntity : AbstractModelObject<string> => Find<TEntity, string>(id);
    public Task<TEntity> Find<TEntity>(Id<int> id) where TEntity : AbstractModelObject<int> => Find<TEntity, int>(id);
    public Task<TEntity> Find<TEntity>(Id<long> id) where TEntity : AbstractModelObject<long> => Find<TEntity, long>(id);
    public Task<TEntity> Find<TEntity>(Id<string> id) where TEntity : AbstractModelObject<string> => Find<TEntity, string>(id);

    public Task<TEntity> Find<TEntity, TKey>(TKey id) where TEntity : AbstractModelObject<TKey>
        => Find<TEntity>(x => x.Id.Equals(id));

    public Task<TEntity> Find<TEntity, TKey>(Id<TKey> id) where TEntity : AbstractModelObject<TKey>
        => id.IsNotNull() ? Find<TEntity>(x => x.Id.Equals(id.Value)) : Task.FromResult(default(TEntity));

    public Task<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate = default) where TEntity : AbstractModelObject
        => predicate.IsNull() ? dbContext.Set<TEntity>().FirstOrDefaultAsync() : dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

    #endregion

    #region Query

    public IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> predicate = default) where TEntity : AbstractModelObject
        => predicate.IsNull() ? dbContext.Set<TEntity>() : dbContext.Set<TEntity>().Where(predicate);

    public Task<List<TEntity>> QueryList<TEntity>(Expression<Func<TEntity, bool>> predicate = default, CancellationToken cancellationToken = default)
        where TEntity : AbstractModelObject
        => predicate.IsNull() ? dbContext.Set<TEntity>().ToListAsync(cancellationToken) : dbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);

    public async Task<(int, List<TEntity>)> QueryPagination<TEntity>(IQueryable<TEntity> query, int pageSize, int currentPage, CancellationToken cancellationToken = default)
    {
        var count = await query.CountAsync(cancellationToken);
        if (count == 0) return (count, null);

        var items = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return (count, items);
    }

    #endregion

    #endregion

    public Task<int> SaveChanges(CancellationToken cancellationToken = default)
        => dbContext.SaveChangesAsync(cancellationToken);
}