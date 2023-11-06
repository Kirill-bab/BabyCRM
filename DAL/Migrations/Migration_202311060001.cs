using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace DAL.Migrations
{
    [Migration(202311060001)]
    public class Migration_202311060001 : Migration
    {
        public override void Up()
        {
            Create.Schema("Client");
            Create.Schema("Filial");

            Create.Table("Filial").InSchema("Filial")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(30).NotNullable()
                .WithColumn("PhoneNumber").AsString(15).NotNullable()
                .WithColumn("Address").AsString(50).NotNullable()
                .WithColumn("CreatedDate").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsString(30).NotNullable()
                .WithColumn("DataVersion").AsInt32().NotNullable();

            Create.Table("Client").InSchema("Client")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString(30).NotNullable()
                .WithColumn("LastName").AsString(30).NotNullable()
                .WithColumn("Birthday").AsDateTime().Nullable()
                .WithColumn("ParentFullName").AsString(30).Nullable()
                .WithColumn("PhoneNumber").AsString(15).NotNullable()
                .WithColumn("EmailAddress").AsString(50).Nullable()
                .WithColumn("SocialNetworks").AsString(100).Nullable()
                .WithColumn("CreatedDate").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsString(30).NotNullable()
                .WithColumn("DataVersion").AsInt32().NotNullable()
                .WithColumn("FilialId").AsInt32().NotNullable();

                Create.ForeignKey("FK_Client_Filial_FilialId")
                .FromTable("Client").InSchema("Client").ForeignColumn("FilialId")
                .ToTable("Filial").InSchema("Filial").PrimaryColumn("Id");

                Create.Index("IX_tblClient_CreatedBy").OnTable("Client").InSchema("Client").OnColumn("CreatedBy")
                    .Ascending().WithOptions().NonClustered();
                Create.Index("IX_tblClient_FilialId").OnTable("Client").InSchema("Client").OnColumn("FilialId")
                    .Ascending().WithOptions().NonClustered();
        }

        public override void Down()
        {
            Delete.Index("IX_tblClient_CreatedBy").OnTable("Client").InSchema("Client");
            Delete.Index("IX_tblClient_FilialId").OnTable("Client").InSchema("Client");
            Delete.ForeignKey("FK_Client_Filial_FilialId").OnTable("Client").InSchema("Client");
            Delete.Table("Client").InSchema("Client");
            Delete.Table("Filial").InSchema("Filial");
            Delete.Schema("Client");
            Delete.Schema("Filial");
        }
    }
}
