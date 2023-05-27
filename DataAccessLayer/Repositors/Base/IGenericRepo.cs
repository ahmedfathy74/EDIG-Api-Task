using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositors.Base
{
    public interface IGenericRepo<T> where T : class
    {
        Task<T> GetByID(int id);
        Task<IEnumerable<T>> GetAllEntries(string[]? includes = null);
        Task AddNewOne(T entity);
        Task<T> UpdateOne(T entity);
        Task DeleteOne(int ID);
    }
}
