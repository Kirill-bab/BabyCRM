using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Commands.Filials
{
    public class CreateFilialCommand : ICreateEntityCommand<FilialDataModel>
    {
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? CreatedBy { get; set; }
    }
}
