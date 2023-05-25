
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
        public List<User> GetAllFarmers() {
        
            List<User> listFarmers = new List<User>();
            SqlConnection myconnection = new SqlConnection(con);
            SqlDataAdapter cmdSelect = new SqlDataAdapter($"select * from ApplicationUser where UserRole = 'Farmer'", myconnection);
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
        // adding a new farmer to database
        public void AddFarmer(User Farmer) {
            using (SqlConnection myconnection = new SqlConnection(con))
            {
                SqlCommand sqlCommand = new SqlCommand($"insert into ApplicationUser values ('{Farmer.Username}','{Farmer.Name}','{Farmer.Password}','{Farmer.User_Roles}');", myconnection);
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
