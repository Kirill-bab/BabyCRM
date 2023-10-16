using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDbEntity
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int DataVersion { get; set; }
    }
}
