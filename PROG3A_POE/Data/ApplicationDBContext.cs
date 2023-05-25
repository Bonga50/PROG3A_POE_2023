
using Microsoft.EntityFrameworkCore;
using PROG3A_POE.Models;
using System.Data;
using System.Data.SqlClient;

namespace PROG3A_POE.Data
{
    public class ApplicationDBContext 
    {
       
        private string con;
        private IConfiguration _config;
        public ApplicationDBContext(IConfiguration configuration)
        {
            _config = configuration;
            con = _config.GetConnectionString("dbConnect");
        }
        //methed t get list of farmers 
        public List<User> GetAllDistinctUsers(string UserRole) {
        
            List<User> listFarmers = new List<User>();
            SqlConnection myconnection = new SqlConnection(con);
            SqlDataAdapter cmdSelect = new SqlDataAdapter($"select * from ApplicationUser where UserRole = '{UserRole}'", myconnection);
            DataTable dt = new DataTable();
            DataRow dr;

            myconnection.Open();
            cmdSelect.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    listFarmers.Add(new User(
                        (string)dr["FirstName"],
                        (string)dr["UserId"], 
                        (string)dr["Password"],
                        (string)dr["UserRole"]));
                }
            }


            return listFarmers;
        
        }
        // adding a new User
        public void AddUser(User newUser) {
            using (SqlConnection myconnection = new SqlConnection(con))
            {
                SqlCommand sqlCommand = new SqlCommand($"insert into ApplicationUser values ('{newUser.Username}','{newUser.Name}','{newUser.Password}','{newUser.User_Roles}');", myconnection);
                myconnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
        //read User
        public User getUser(User CertainUser) {
            User myUser = new User();

            SqlConnection myconnection = new SqlConnection(con);
            SqlDataAdapter cmdSelect = new SqlDataAdapter($"select * from ApplicationUser where UserId = '{CertainUser.Username}'", myconnection);
            DataTable dt = new DataTable();
            DataRow dr;

            myconnection.Open();
            cmdSelect.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    myUser= new User(
                        (string)dr["FirstName"],
                        (string)dr["UserId"],
                        (string)dr["Password"],
                        (string)dr["UserRole"] );
                }
            }

            return myUser;
        }
        //Update User
        public void UpdateUser(User CertainUser) {

            using (SqlConnection myconnection = new SqlConnection(con))
            {
                SqlCommand sqlCommand = new SqlCommand($"UPDATE ApplicationUser SET FirstName = '{CertainUser.Name}', Password = '{CertainUser.Password}', UserRole = '{CertainUser.User_Roles}' WHERE UserId = '{CertainUser.Username}';", myconnection);
                myconnection.Open();
                sqlCommand.ExecuteNonQuery();
            }

        }

        //Delete User
        public void DeleteUser(string UserId) {
            using (SqlConnection myconnection = new SqlConnection(con))
            {
                SqlCommand sqlCommand = new SqlCommand($"DELETE FROM ApplicationUser WHERE UserId = '{UserId}';", myconnection);
                myconnection.Open();
                sqlCommand.ExecuteNonQuery();
            }

        }
        //Create Product

        public void createProduct(Product newProduct) {
            using (SqlConnection myconnection = new SqlConnection(con))
            {
                SqlCommand sqlCommand = new SqlCommand($"INSERT INTO Products ( ProductName, ProductType, ProductDate, Quantity) " +
                    $"VALUES ( '{newProduct.ProductName}', '{newProduct.ProductType}', '{newProduct.ProductDate}', {newProduct.Quantity});", myconnection);
                myconnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        //update product
        public void updateProduct(Product CertainProduct) {
            using (SqlConnection myconnection = new SqlConnection(con))
            {
                SqlCommand sqlCommand = new SqlCommand($"UPDATE Products SET ProductName = '{CertainProduct.ProductName}',ProductType = '{CertainProduct.ProductType}', ProductDate = '{CertainProduct.ProductDate}', quantity = {CertainProduct.Quantity}   WHERE ProductID = {CertainProduct.ProductID};", myconnection);
                myconnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
        //delete product
        public void deleteProduct(Product CertainProduct)
        {
            using (SqlConnection myconnection = new SqlConnection(con))
            {
                SqlCommand sqlCommand = new SqlCommand($"DELETE FROM Products WHERE ProductID = {CertainProduct.ProductID};", myconnection);
                myconnection.Open();
                sqlCommand.ExecuteNonQuery();
            }

        }
        //getting a list of products for a specific farmer
        public List<Product> GetProducts(string UserId) {
        List<Product> products = new List<Product>();
            SqlConnection myconnection = new SqlConnection(con);
            SqlDataAdapter cmdSelect = new SqlDataAdapter($"select * from Products where FarmerId ='{UserId}';", myconnection);
            DataTable dt = new DataTable();
            DataRow dr;

            myconnection.Open();
            cmdSelect.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    products.Add(new Product(
                        Convert.ToInt32(dr["ProductID"]),
                        (string)dr["ProductName"],
                        (string)dr["ProductType"],
                        (DateTime)dr["ProductDate"],
                        Convert.ToInt32(dr["quantity"])));
                }
            }


            return products;
        }


    }
}
