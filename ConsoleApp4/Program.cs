using System;
using System.Linq;
using ConsoleApp4.Context;
using ConsoleApp4.Context.Models;
using LinqToDB;

namespace ConsoleApp4;

class Program
{
    static void Main(string[] args)
    {
        var random = new Random();
        
        var row1 = new TestTable {Id = Guid.NewGuid(), Name = "John Doe", ExternalId = {Id = random.Next().ToString(), Source = "unknown"}};
        var row2 = new TestTable {Id = Guid.NewGuid(), Name = "John Doe", ExternalId = {Id = random.Next().ToString(), Source = "unknown"}};
            
        var connection = new SqliteConnection("default.sqlite");
        //var connection = new SqlServerConnection("Server=localhost;Database=test;Integrated Security=True;");
            
        connection.ApplyMigrations();
            
        var inserted = connection.Insert(row1);
        var actual = connection.GetTable<TestTable>().Take(10).ToList();
            
        try
        {
            var created = connection.GetTable<TestTable>().InsertWithOutput(row2);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}