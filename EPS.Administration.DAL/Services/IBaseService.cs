﻿using EPS.Administration.DAL.Context;
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
        List<TEntity> Get(Expression<Func<TEntity, bool>> func);
        List<TEntity> Get(string query);
        TEntity GetSingle(Expression<Func<TEntity, bool>> func);
        int Count();
        void AddOrUpdate(TEntity entity);
        void Delete(int id, int revision);
        void UpdateEntity(TEntity entity);
        List<TEntity> GetLatest();
        void Save();
    }
}
