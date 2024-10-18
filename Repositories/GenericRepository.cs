using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {   
        private readonly DbSet<T> dbSet;
        protected AppDbContext context;

        public GenericRepository(AppDbContext context) 
        {
            this.context = context;
            dbSet  = context.Set<T>();
        }

        public async ValueTask AddAsync(T entity)
        {
            await dbSet.AddAsync(entity); ;

        }

        public void Delete(T entity)=>dbSet.Remove(entity);
        

        public IQueryable<T> GetAll() => dbSet.AsQueryable().AsNoTracking(); 
        

        public ValueTask<T?> GetByIdAsync(int id) => dbSet.FindAsync(id);
      

        public void Update(T entity) => dbSet.Update(entity);
       

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => dbSet.Where(predicate).AsNoTracking();
       
    }
}
