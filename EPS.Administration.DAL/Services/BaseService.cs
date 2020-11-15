using EPS.Administration.DAL.Context;
using EPS.Administration.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPS.Administration.DAL.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IRevisionableEntity
    {
        public DeviceContext context { get; private set; }
        private DbSet<TEntity> _dbEntity;
        IQueryable<TEntity> _cachedEntities;

        private static object lockObj = new object();

        public BaseService(DeviceContext context)
        {
            this.context = context;
            _dbEntity = this.context.Set<TEntity>();
            _cachedEntities = Get().AsQueryable<TEntity>();
        }

        public void AddOrUpdate(TEntity entity)
        {
            var item = GetSingle(x => x.Id == entity.Id);

            if (item == null)
            {
                entity.Revision = 1;
                Add(entity);
            }
            else
            {
                entity.Id = item.Id;
                entity.Revision = item.Revision;
                entity.BaseId = item.BaseId;

                if (!IsChanged(entity, item))
                {
                    return;
                }

                entity.Id = 0;
                entity.Revision = item.Revision + 1;
                entity.BaseId = item.BaseId == 0 ? item.Id : item.BaseId;
                Add(entity);
            }
            UpdateEntity(entity);
        }

        private bool IsChanged(dynamic first, dynamic second)
        {
            var a = JsonConvert.SerializeObject(first);
            var b = JsonConvert.SerializeObject(second);

            return a != b;
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
            return func == null ? _cachedEntities.ToList() : _cachedEntities.Where(func).ToList();
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> func)
        {
            var entity = _cachedEntities.Where(func).OrderByDescending(x => x.Revision).FirstOrDefault();

            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        public void Save()
        {
            lock (lockObj)
            {
                context.SaveChanges();
            }
            _cachedEntities = Get().AsQueryable<TEntity>();
        }

        public void UpdateEntity(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        private void Add(TEntity entity)
        {
            _dbEntity.Add(entity);
        }

        public DeviceContext GetContext()
        {
            return context;
        }
    }
}
