using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.DbManagers
{
    public interface IDbManager<T> where T : IDbEntity
    {
        public Task<IEnumerable<T>> LoadData<U>(string procedureName, U parameters);

        public Task<IEnumerable<T>> LoadData<TFirst, TSecond, U>(string sql, Func<TFirst, TSecond, T> map, U parameters, string splitOn = "Id");
        public Task ExecuteProcedure<U>(string procedureName, U parameters);
        public Task SaveMany(IEnumerable<T> entities);
    }
}
