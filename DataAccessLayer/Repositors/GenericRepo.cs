using DataAccessLayer.Data;
using DataAccessLayer.Repositors.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositors
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddNewOne(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOne(int ID)
        {
            var entry = await _context.Set<T>().FindAsync(ID);
            if(entry is null)
            {
                throw new ArgumentNullException("Invalid ID , Please Enter Valid ID");
            }
            _context.Set<T>().Remove(entry);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllEntries(string[]? includes = null)
        {
            IQueryable<T> RelatedEntities = _context.Set<T>();
            if(includes is not null)
            {
                foreach (var include in includes)
                    RelatedEntities = RelatedEntities.Include(include);
            }
            return await RelatedEntities.ToListAsync();
        }

        public async Task<T> UpdateOne(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
