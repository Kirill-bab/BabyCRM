using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Commands;
using DAL;
using DAL.Repositories;

namespace BLL.Managers
{
    public abstract class EntityManager<T, TCreateCommand, TUpdateCommand> 
        where T : IDbEntity
        where TCreateCommand : ICreateEntityCommand<T>
        where TUpdateCommand : IUpdateEntityCommand<T>
    {
        protected readonly IRepository<T> Repository;

        public EntityManager(IRepository<T> repository)
        {
            Repository = repository;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await Repository.Get();
        }

        public virtual async Task<T?> Get(int id)
        {
            return await Repository.GetOne(id);
        }

        public virtual async Task Add(TCreateCommand command)
        {
            await Repository.Save(command);
        }

        public virtual async Task AddMany(IEnumerable<T> entities)
        {
            await Repository.SaveMany(entities);
        }

        public virtual async Task Update(TUpdateCommand command)
        {
            await Repository.Update(command);
        }

        public virtual async Task Delete(int id)
        {
            await Repository.Delete(id);
        }
    }
}
