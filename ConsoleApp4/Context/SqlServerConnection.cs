using System;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp4.Context
{
    public class SqlServerConnection : BaseConnection
    {
        public SqlServerConnection(string connectionString) : base(ProviderName.SqlServer2019, connectionString, Schema.Value)
        {
            
        }

        public void ApplyMigrations()
        {
            var services = new ServiceCollection();

            services.AddFluentMigratorCore()
                    .ConfigureRunner(runnerBuilder =>
                    {
                        runnerBuilder.AddSqlServer2016();
                        runnerBuilder.ScanIn(typeof(Program).Assembly);
                        runnerBuilder.WithGlobalConnectionString(ConnectionString);
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