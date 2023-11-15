using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repositories
{
    public interface IRepository<T> where T : IDbEntity
    {
        public Task<IEnumerable<T>> Get();
        //public Task<IEnumerable<T>> Get(List<Filter> filters);
        public Task<T> GetOne(int id);
        public Task Save<TCreateCommand>(TCreateCommand command);
        public Task SaveMany(IEnumerable<T> entities);
        public Task Update<TUpdateCommand>(TUpdateCommand command);
        public Task Delete(int id);
    }
}
