using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Managers
{
    public interface IEntityManager<T>
    {
        public string ProcedurePrefix { get; }
        public Task<IEnumerable<T>> GetAll();
        public Task<T?> Get(int id);
        public Task<IEnumerable<T>> GetByAuthor(string author);
    }
}
