using System;
using System.Data.Common;
using ConsoleApp4.Context.Models;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using LinqToDB.Mapping;

namespace ConsoleApp4.Context
{
    public class BaseConnection : DataConnection
    {
        #region .ctor

        public BaseConnection()
        {
        }

        public BaseConnection(MappingSchema mappingSchema) : base(mappingSchema)
        {
        }

        public BaseConnection(string configurationString, MappingSchema mappingSchema) : base(configurationString,
            mappingSchema)
        {
        }

        public BaseConnection(string configurationString) : base(configurationString)
        {
        }

        public BaseConnection(string providerName, string connectionString, MappingSchema mappingSchema) : base(
            providerName, connectionString, mappingSchema)
        {
        }

        public BaseConnection(string providerName, string connectionString) : base(providerName, connectionString)
        {
        }

        public BaseConnection(IDataProvider dataProvider, string connectionString, MappingSchema mappingSchema) : base(
            dataProvider, connectionString, mappingSchema)
        {
        }

        public BaseConnection(IDataProvider dataProvider, string connectionString) : base(dataProvider,
            connectionString)
        {
        }

        public BaseConnection(IDataProvider dataProvider, Func<DbConnection> connectionFactory,
            MappingSchema mappingSchema) : base(dataProvider, connectionFactory, mappingSchema)
        {
        }

        public BaseConnection(IDataProvider dataProvider, Func<DbConnection> connectionFactory) : base(dataProvider,
            connectionFactory)
        {
        }

        public BaseConnection(IDataProvider dataProvider, DbConnection connection, MappingSchema mappingSchema) : base(
            dataProvider, connection, mappingSchema)
        {
        }

        public BaseConnection(IDataProvider dataProvider, DbConnection connection) : base(dataProvider, connection)
        {
        }

        public BaseConnection(IDataProvider dataProvider, DbConnection connection, bool disposeConnection) : base(
            dataProvider, connection, disposeConnection)
        {
        }

        public BaseConnection(IDataProvider dataProvider, DbTransaction transaction, MappingSchema mappingSchema) :
            base(dataProvider, transaction, mappingSchema)
        {
        }

        public BaseConnection(IDataProvider dataProvider, DbTransaction transaction) : base(dataProvider, transaction)
        {
        }

        public BaseConnection(LinqToDBConnectionOptions options) : base(options)
        {
        }

        #endregion

        #region Mapping

        protected static readonly Lazy<MappingSchema> Schema = new Lazy<MappingSchema>(SchemaFactory);

        private static MappingSchema SchemaFactory()
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
}