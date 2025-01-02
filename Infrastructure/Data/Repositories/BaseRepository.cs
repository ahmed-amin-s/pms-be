using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class BaseRepository<TEntity, TDbContext> : IBaseRepository<TEntity>
    where TDbContext : PmsDbContext
    where TEntity : BaseEntity
    {
        private readonly TDbContext _context;
        private readonly ILogger<BaseRepository<TEntity, TDbContext>> _logger;
        public BaseRepository(TDbContext context, ILogger<BaseRepository<TEntity, TDbContext>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public TEntity? GetById<TKey>(TKey id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> GetAllTracking()
        {
            return _context.Set<TEntity>().AsTracking();
        }

        public int Add(params TEntity[] entities)
        {
            _context.AddRange(entities);
            return _context.SaveChanges();
        }

        public int Update(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            return _context.SaveChanges();
        }

        public int Delete(params TEntity[] entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);

            int affectedRows = _context.SaveChanges();
            return affectedRows;
        }

        public int Delete<TKey>(params TKey[] ids)
        {
            foreach (TKey id in ids)
            {
                var entity = GetById(id);
                if (entity != null)
                    _context.Set<TEntity>().Remove(entity);
            }

            int affectedRows = _context.SaveChanges();
            return affectedRows;
        }


        public void Detach(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
        }

        public bool Existed(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

    }

}
