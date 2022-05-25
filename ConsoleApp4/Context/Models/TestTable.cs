using System;

namespace ConsoleApp4.Context.Models;

public class TestTable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
        
    public virtual ExternalId ExternalId { get; set; } = new();
}