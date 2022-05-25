using System;
using System.IO;
using ConsoleApp4.Context;
using ConsoleApp4.Context.Models;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = GetConnection("default");
            var data = new TestTable
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                ExternalId = new ExternalId
                {
                    Id = "1",
                    Source = "unknown"
                }
            };
            
            var created = connection.GetTable<TestTable>().InsertWithOutput(data);
        }

        private static BaseConnection GetConnection(string database)
        {
            var databaseFileName = $"{database}.sqlite";
            var databaseFileInfo = new FileInfo(databaseFileName);
            
            if (databaseFileInfo.Exists == false)
            {
                using var _ = databaseFileInfo.Create();
            }
            
            var connectionString = $"Data Source={databaseFileName}";
            
            ApplyMigration(databaseFileName, connectionString);
            
            return new SqliteConnection(connectionString);
        }

        private static void ApplyMigration(string databasePath, string connectionString)
        {
            var services = new ServiceCollection();

            services.AddFluentMigratorCore()
                    .ConfigureRunner(runnerBuilder =>
                    {
                        runnerBuilder.AddSQLite();
                        runnerBuilder.ScanIn(typeof(Program).Assembly);
                        runnerBuilder.WithGlobalConnectionString(connectionString);
                    })
                    .AddLogging(builder => { builder.AddFluentMigratorConsole(); })
                    .Configure<ProcessorOptions>(options =>
                    {
                        options.Timeout = TimeSpan.FromMinutes(3);
                    })
                    .Configure<TypeFilterOptions>(options =>
                    {
                        
                    });

            using (var provider = services.BuildServiceProvider())
            {
                using (var scope = provider.CreateScope())
                {
                    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                    runner.MigrateUp();
                }
            }
        }
    }
}