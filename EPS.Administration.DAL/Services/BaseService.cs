using EPS.Administration.DAL.Context;
using EPS.Administration.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPS.Administration.DAL.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IRevisionableEntity
    {
        private readonly DeviceContext _deviceContext;
        private DbSet<TEntity> _dbEntity;

        public BaseService(DeviceContext context)
        {
            _deviceContext = context;
            _dbEntity = _deviceContext.Set<TEntity>();
        }

        public void AddOrUpdate(TEntity entity)
        {
            var item = GetSingle(x => x.Id == entity.Id);

            if (item == null)
            {
                Add(entity);
            }else
            {
                entity.Id = 0;
                entity.Revision = item.Revision + 1;
                Add(entity);
            }
        }

        public void Delete(int entityKey, int revision)
        {
            //Find revision manually
            TEntity model = _dbEntity.Find(entityKey, revision);
            _dbEntity.Remove(model);
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbEntity.ToList();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> func)
        {
            return func == null ? _dbEntity.ToList() : _dbEntity.Where(func).ToList();
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> func)
        {
            return _dbEntity.Where(func).OrderByDescending(x=>x.Revision).FirstOrDefault();
        }

        public async Task Save()
        {
            await _deviceContext.SaveChangesAsync();
        }

        public void UpdateEntity(TEntity entity)
        {
            _deviceContext.Entry(entity).State = EntityState.Modified;
        }

        private void Add(TEntity entity)
        {
            _dbEntity.Add(entity);
        }
    }
}
