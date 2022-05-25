using System;
using System.IO;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp4.Context;

public class SqliteConnection : BaseConnection
{
    protected string DatabaseFileName { get; }

    public SqliteConnection(string databaseFileName) : base(ProviderName.SQLite, $"Data Source={databaseFileName}", Schema.Value)
    {
        DatabaseFileName = databaseFileName;
    }

    public void ApplyMigrations()
    {
        var databaseFileInfo = new FileInfo(DatabaseFileName);
            
        if (databaseFileInfo.Exists == false)
        {
            using var _ = databaseFileInfo.Create();
        }
            
        var services = new ServiceCollection();

        services.AddFluentMigratorCore()
                .ConfigureRunner(runnerBuilder =>
                {
                    runnerBuilder.AddSQLite();
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