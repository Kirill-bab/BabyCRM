using DAL.DbManagers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Commands;
using DAL;
using DAL.Models;

namespace BLL.Managers
{
    public abstract class EntityManager<T, TCreateCommand, TUpdateCommand> 
        where T : IDbEntity
        where TCreateCommand : ICreateEntityCommand<T>
        where TUpdateCommand : IUpdateEntityCommand<T>
    {
        protected readonly IDbManager<T> _dbManager;

        public EntityManager(IDbManager<T> dbManager)
        {
            _dbManager = dbManager;
        }

        public abstract string ProcedurePrefix { get; }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbManager.LoadData($"{ProcedurePrefix}_GetAll", new { });
        }

        public virtual async Task<T?> Get(int id)
        {
            var result = await _dbManager.LoadData($"{ProcedurePrefix}_Get", new { Id = id });
            return result.FirstOrDefault();
        }

        public virtual async Task<IEnumerable<T>> GetByAuthor(string author)
        {
            return await _dbManager.LoadData($"{ProcedurePrefix}_GetByAuthor", new { Author = author });
        }

        public virtual async Task Add(TCreateCommand command)
        {
            await _dbManager.ExecuteProcedure($"{ProcedurePrefix}_Insert", command);
        }

        public virtual async Task AddMany(IEnumerable<T> entities)
        {
            await _dbManager.SaveMany(entities);
        }

        public virtual async Task Update(TUpdateCommand command)
        {
            await _dbManager.ExecuteProcedure($"{ProcedurePrefix}_Update", command);
        }

        public virtual async Task Delete(int id)
        {
            await _dbManager.ExecuteProcedure($"{ProcedurePrefix}_Delete", new { Id = id });
        }
    }
}
