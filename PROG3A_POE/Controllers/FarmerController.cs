using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using PROG3A_POE.Data;
using PROG3A_POE.Models;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml.Linq;

namespace PROG3A_POE.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    public class FarmerController : BaseController
    {
        private string con;
        private IConfiguration _config;
        ApplicationDBContext dbhelper;

        public FarmerController(IConfiguration configuration)
        {
            dbhelper = new ApplicationDBContext(configuration);
        }

        public List<User> FarmerList;
       
        // GET: FarmerController
        public ActionResult AllFarmers()
        {
            string selectedRole = TempData["SelectedRole"] as string;

            if (string.IsNullOrEmpty(selectedRole))
            {
                selectedRole = "Farmer"; // Set the default role here
                TempData["SelectedRole"] = selectedRole; // Store the selected role in TempData
            }


            FarmerList = dbhelper.GetAllDistinctUsers(selectedRole);
            return View(FarmerList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllFarmers(IFormCollection form)
        {
            try
            {
                string selectedRole = form["role"];
                TempData["SelectedRole"] = selectedRole;
                FarmerList = dbhelper.GetAllDistinctUsers(selectedRole);
                return View(FarmerList);
            }
            catch
            {
                return View();
            }
        }

        
      
        // GET: FarmerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FarmerController/Details/5
        public ActionResult Details(string id)
        {
            
            return View(dbhelper.getAUser(id));
        }

        // GET: FarmerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FarmerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string FarmerId = collection["txtUsername"];
                string FarmerName = collection["txtFirstName"];
                string FarmerPassword = collection["txtPassword"];
                string UserRole = collection["role"];
                User ClientUser = new User(FarmerName, FarmerId, FarmerPassword, UserRole);
                dbhelper.AddUser(ClientUser);
                return RedirectToAction("AllFarmers");
            }
            catch
            {
                return View();
            }
        }


        // GET: FarmerController/Edit/5
        public ActionResult ViewProducts(string id)
        {
            if (FarmerId != id) { FarmerId = id; }
            ViewBag.Id = id;

            if (TempData["MyData"] == null)
            {
                TempData["MyData"] = dbhelper.GetProducts(FarmerId);
            }
            

            return View(TempData["MyData"]);
        }

       

        // GET: FarmerController/Edit/5
        public ActionResult Edit(string id)
        {
            

            
            return View(dbhelper.getAUser(id));
        }

        // POST: FarmerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                string FarmerId = collection["txtUsername"];
                string FarmerName = collection["txtFirstName"];
                string FarmerPassword = collection["txtPassword"];
                string UserRole = collection["role"];
                User Farmer = new User(FarmerName, FarmerId, FarmerPassword, UserRole);
                dbhelper.UpdateUser(Farmer,id);
                return RedirectToAction("AllFarmers");
            }
            catch
            {
                return View();
            }
        }

        // GET: FarmerController/Delete/5
        public ActionResult Delete(string id)
        {

            return View(dbhelper.getAUser(id));
        }

        // POST: FarmerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                dbhelper.deleteUserProducts(id);
                dbhelper.DeleteUser(id);
                
                return RedirectToAction("AllFarmers");
            }
            catch
            {
                return View();
            }
        }


        // GET: FarmerController/CreateProduct/5
        public ActionResult CreateProduct(string id)
        {
            
            ViewBag.Id = id;
            return View();
        }

        // POST: FarmerController/CreateProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(string id, IFormCollection collection)
        {
            try
            {

                string productName = collection["txtProductName"];
                string productType = collection["txtproductType"];
                int productQuantity = Int32.Parse(collection["txtQuantity"]);
                
                Product prod = new Product(productName, productType, DateTime.Now, productQuantity);
                dbhelper.createProduct(prod, id);
                
                return RedirectToAction("ViewProducts", new { id = id });

                
            }
            catch
            {
                return View();
            }
        }

        // GET: FarmerController/EditProduct/5
        public ActionResult EditProduct(int id)
        {
            
            ViewBag.Id = id;
            return View(dbhelper.GetDistinctProduct(id));
        }

        // POST: FarmerController/EditProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(int id, IFormCollection collection)
        {
            try
            {

                string productName = collection["txtProductName"];
                string productType = collection["txtproductType"];
                DateTime date = DateTime.Parse(collection["txtProductDate"]);
                int productQuantity = Int32.Parse(collection["txtQuantity"]);

                Product prod = new Product(id, productName, productType, date, productQuantity);
                dbhelper.updateProduct(prod);

                return RedirectToAction("ViewProducts",new{ id = FarmerId });


            }
            catch
            {
                return View();
            }
        }

        // GET: FarmerController/DetailsProduct/5
        public ActionResult DetailsProduct(int id)
        {

            ViewBag.Id = id;
            return View(dbhelper.GetDistinctProduct(id));
        }
        public ActionResult DeleteProduct(int id)
        {

            ViewBag.Id = id;
            return View(dbhelper.GetDistinctProduct(id));
        }

        // POST: FarmerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id, IFormCollection collection)
        {
            try
            {
                dbhelper.deleteProduct(id);
                

                return RedirectToAction("ViewProducts", new { id = FarmerId });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sort(int id, IFormCollection collection)
        {
            List<Product> products = new List<Product>();
            try
            {
                string productType = collection["txtproductType"];
                DateTime ? startDate = DateTime.Parse(collection["txtstartDate"]);
                DateTime ? endDate = DateTime.Parse(collection["txtendDate"]);
                if (productType != null) { 
                    products =
                    dbhelper.SortByProductType(dbhelper.GetProducts(FarmerId), productType);


                }
                if (startDate != null && endDate != null)
                {
                    products = dbhelper.SortByDateRange(products, startDate, endDate);
                }

                else { products = dbhelper.GetProducts(FarmerId); }

                TempData["MyData"] = products;

                return View("ViewProducts");
            }
            catch
            {
                return View("ViewProducts");
            }
        }
    }
}
