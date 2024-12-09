using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace ProductCatalogue.Models
{
    public interface IProductService
    {
        IList<Product> Products { get; }
        public bool Add (Product product);
        public bool Remove(int Id);
        bool Update(Product product);
    }

    class ProductService : IProductService
    {
        readonly DataBaseService dataBaseService = new DataBaseService();
        private List<Product> _products = null;
        public ProductService()
        {
            _products = new List<Product>();
        }
        private List<Product> GetList()
        {
            var products = new List<Product>();

            using (var connection = dataBaseService.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Products", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                productId = (int)reader["ProductId"],
                                productName = reader["ProductName"].ToString(),
                                price = (decimal)reader["Price"],
                                quantity = (int)reader["Quantity"]
                            });
                        }
                    }
                }
            }
            return products;
        }

        public IList<Product> Products => GetList();

        public bool Remove(int Id)
        {
            bool res;
            using (var connection = dataBaseService.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"DELETE  FROM Products WHERE productId = {Id}", connection))
                {
                    if (command.ExecuteNonQuery() > 0)
                    {
                        res = true;
                    }
                    res = false;
                }
            }

            return res;
        }

        public bool Add(Product product)
        {
            bool res;
            using (var connection = dataBaseService.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"INSERT INTO [Catalogue].[dbo].[Products] ([ProductName], [Quantity], [Price]) VALUES (@ProductName, @Quantity, @Price)", connection))
                {
                    command.Parameters.AddWithValue("@ProductName", product.productName);
                    command.Parameters.AddWithValue("@Quantity", product.quantity);
                    command.Parameters.AddWithValue("@Price", product.price);
                    if (command.ExecuteNonQuery() > 0)
                    {
                        res = true;
                    }
                    res = false;
                }
            }
            return res;
        }

        public bool Update(Product product)
        {
            bool res;
            using (var connection = dataBaseService.GetConnection())
            {
                if(connection == null) { return false; }
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Products SET ProductName = @ProductName, Quantity = @Quantity, Price = @Price WHERE productId = @ProductId", connection))
                {
                    command.Parameters.AddWithValue("@ProductName", product.productName);
                    command.Parameters.AddWithValue("@Quantity", product.quantity);
                    command.Parameters.AddWithValue("@Price", product.price);
                    command.Parameters.AddWithValue("@ProductId", product.productId);
                    if (command.ExecuteNonQuery() > 0)
                    {
                        res = true;
                    }
                    res = false;
                }
            }
            return res;
        }

    }
}
