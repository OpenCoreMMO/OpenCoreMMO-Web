using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OCM.Infrastructure.Interfaces;

public interface IBaseRepositoryNeo<TEntity> where TEntity : class
{
    Task Insert(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(TEntity entity);
    Task<IList<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(int id);
    Task<TEntity> GetAsync(long id);
    Task<int> CountAllAsync(Expression<Func<TEntity, bool>> filter);
}