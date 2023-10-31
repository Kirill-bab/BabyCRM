using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbManagers
{
    public interface IDbManager<T> where T : IDbEntity
    {
        public Task<IEnumerable<T>> LoadData<U>(string procedureName, U parameters);
        public Task ExecuteProcedure<U>(string procedureName, U parameters);
    }
}
