using FluentMigrator;

namespace ConsoleApp4.Context.Migrations
{
    [Migration(2205250957)]
    public class _2205250957 : AutoReversingMigration 
    {
        public override void Up()
        {
            Create.Table("TestTable")
                  .WithColumn("Id").AsGuid().PrimaryKey()
                  .WithColumn("Name").AsString(100)
                  .WithColumn("ExternalId").AsString(50).NotNullable()
                  .WithColumn("ExternalSource").AsString(50).NotNullable();
            
            Create.UniqueConstraint("UQ_TestTable_ExternalId")
                  .OnTable("TestTable")
                  .Columns(new[] {"ExternalId", "ExternalSource"});
        }
    }
}