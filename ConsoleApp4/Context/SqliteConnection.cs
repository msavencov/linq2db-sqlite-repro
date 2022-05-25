using LinqToDB;

namespace ConsoleApp4.Context
{
    public class SqliteConnection : BaseConnection
    {
        public SqliteConnection(string connectionString) : base(ProviderName.SQLite, connectionString, Schema.Value)
        {
            
        }
    }
}