using EPS.Administration.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Administration.DAL.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        DeviceContext context { get; }
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> func);
        TEntity GetSingle(Expression<Func<TEntity, bool>> func);
        void AddOrUpdate(TEntity entity);
        void Delete(int id, int revision);
        void UpdateEntity(TEntity entity);
        void Save();
    }
}
