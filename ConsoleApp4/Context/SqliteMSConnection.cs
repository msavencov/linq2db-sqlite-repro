using LinqToDB;

namespace ConsoleApp4.Context
{
    public class SqliteMSConnection : BaseConnection
    {
        public SqliteMSConnection(string connectionString) : base(ProviderName.SQLiteMS, connectionString, Schema.Value)
        {
            
        }
    }
}