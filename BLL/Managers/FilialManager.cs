using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Commands.Filials;
using DAL.Repositories;

namespace BLL.Managers
{
    public class FilialManager : EntityManager<FilialDataModel, CreateFilialCommand, UpdateFilialCommand>
    {
        public FilialManager(IRepository<FilialDataModel> repository) : base(repository)
        {
        }
    }
}
