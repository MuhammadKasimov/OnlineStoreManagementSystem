﻿using OnlineStoreManagementSystem.Domain.Commons;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Data.IRepositories
{
    public interface IGenericRepository<T> where T : Auditable
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string[] includes = null, bool isTracking = true);
        ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, bool isTracking = true, string[] includes = null);
        ValueTask<T> CreateAsync(T entity);
        ValueTask<bool> DeleteAsync(long id);
        T Update(T entity);
        ValueTask SaveChangesAsync();
    }
}
