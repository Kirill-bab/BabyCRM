﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BLL.Commands
{
    public interface ICreateEntityCommand<T> where T : IDbEntity
    {
    }
}
