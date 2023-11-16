using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BLL.Commands.Clients
{
    public class UpdateClientCommand : IUpdateEntityCommand<ClientDataModel>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? ParentFullName { get; set; }
        public string PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? SocialNetworks { get; set; }
        public int? FilialId { get; set; }
    }
}
