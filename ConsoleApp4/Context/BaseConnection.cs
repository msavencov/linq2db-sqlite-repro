using System;
using ConsoleApp4.Context.Models;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace ConsoleApp4.Context;

public class BaseConnection : DataConnection
{
    public BaseConnection(string providerName, string connectionString, MappingSchema mappingSchema) : base(providerName, connectionString, mappingSchema)
    {
    }
    
    #region Mapping

    protected static readonly Lazy<MappingSchema> Schema = new Lazy<MappingSchema>(SchemaFactory);
    
    protected static MappingSchema SchemaFactory()
    {
        var schema = new MappingSchema();
        var builder = schema.GetFluentMappingBuilder();

        builder.Entity<TestTable>()
               .HasTableName("TestTable")
               .HasPrimaryKey(t => t.Id)
               .Property(t => t.Name).HasLength(100)
               .Property(t => t.ExternalId.Id).HasColumnName("ExternalId").HasLength(50)
               .Property(t => t.ExternalId.Source).HasColumnName("ExternalSource").HasLength(50)
            ;

        return builder.MappingSchema;
    }

    #endregion
}