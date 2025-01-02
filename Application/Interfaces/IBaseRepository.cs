using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBaseRepository<TEntity>  where TEntity : BaseEntity
    {
        TEntity? GetById<TKey>(TKey id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllTracking();
        int Add(params TEntity[] entities);
        int Update(params TEntity[] entities);
        int Delete<TKey>(params TKey[] ids);
        int Delete(params TEntity[] entities);
        //int SoftDelete(params TEntity[] entities);
        void Detach(params TEntity[] entities);
        bool Existed(Expression<Func<TEntity, bool>> predicate);
    }
}
