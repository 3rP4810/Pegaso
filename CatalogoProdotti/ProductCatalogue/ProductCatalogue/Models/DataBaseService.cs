using Microsoft.Data.SqlClient;

namespace ProductCatalogue.Models
{
    public class DataBaseService
    {
        readonly string _connection;
        public DataBaseService() => _connection = "Server=127.0.0.1;Database=Catalogue;User=admin;Password=admin123;TrustServerCertificate=True;";
        public SqlConnection GetConnection() { return new SqlConnection(_connection); }
    }
}
