using System;
using LinqToDB.Mapping;

namespace ConsoleApp4.Context.Models
{
    public class TestTable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public ExternalId ExternalId { get; set; } = new();
    }
}