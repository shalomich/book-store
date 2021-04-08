using Microsoft.EntityFrameworkCore;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage.Extensions;

namespace Storage.Services
{
    public class ContextRepository<T> : IRepository<T> where T : Entity
    {
        private DbContext _context;
        public ContextRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            T entity = Select(id);

            if (entity == null)
                throw new ArgumentException();

            _context.Set<T>().Remove(entity);

            _context.SaveChanges();
        }

        public IEnumerable<T> Select()
        {
            return  _context.Set<T>().AsNoTracking().Include(entity => entity.Images).Include().ToList();
        }

        public T Select(int id)
        {
            return _context.Set<T>().AsNoTracking().Include(entity => entity.Images).Include().FirstOrDefault(entity => entity.Id == id);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
