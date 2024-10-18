﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        ValueTask AddAsync(T entity);


        void Delete(T entity);



        IQueryable<T> GetAll();


        ValueTask<T?> GetByIdAsync(int id);


        void Update(T entity);


        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        
    }
}
