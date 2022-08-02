using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "appdbpsr.database.windows.net";
        private static string db_user = "psr";
        private static string db_password = "siva@83108917";
        private static string db_database = "appdb";


        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            //_builder.ConnectionString = db_source;
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;

            return new SqlConnection(_builder.ConnectionString);

        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();

            List<Product> products = new List<Product>();

            string statement = "SELECT ProductID,ProductName,Quantity FROM Products";

            conn.Open();

            SqlCommand command = new SqlCommand(statement, conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    products.Add(_product);
                }
            }
            conn.Close();
            return products;
           
        }

    }
}
