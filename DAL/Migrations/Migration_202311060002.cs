using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Migrations
{
    [Migration(202311060002)]
    public class Migration_202311060002 : Migration
    {
        public override void Up()
        {
            Execute.Script(@"../DAL/Migrations/Scripts/ClientStoredProceduresUP.sql");
            Execute.Script(@"../DAL/Migrations/Scripts/FilialStoredProceduresUP.sql");
        }

        public override void Down()
        {
            Execute.Script(@"../DAL/Migrations/Scripts/ClientStoredProceduresDOWN.sql");
            Execute.Script(@"../DAL/Migrations/Scripts/FilialStoredProceduresDOWN.sql");
        }
    }
}
