using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROG3A_POE.Data;
using PROG3A_POE.Models;

namespace PROG3A_POE.Controllers
{
    [Authorize(Policy = "FarmerOnly")]
    public class ProductController : BaseController
    {
        private string con;
        private IConfiguration _config;
        ApplicationDBContext dbhelper;

        public ProductController(IConfiguration configuration)
        {
            dbhelper = new ApplicationDBContext(configuration);
        }

       
        // GET: ProductController
        public ActionResult AllProducts()
        {
            
            return View(dbhelper.GetProducts(Username));
        }
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View(dbhelper.GetDistinctProduct(id));
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string productName = collection["txtProductName"];
                string productType = collection["txtproductType"];
                int productQuantity = Int32.Parse(collection["txtQuantity"]);
                
                Product prod = new Product(productName, productType,DateTime.Now, productQuantity);
                dbhelper.createProduct(prod,Username);
                return RedirectToAction("AllProducts");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dbhelper.GetDistinctProduct( id));
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                string productName = collection["txtProductName"];
                string productType = collection["txtproductType"];
                DateTime date = DateTime.Parse(collection["txtProductDate"]);
                int productQuantity = Int32.Parse(collection["txtQuantity"]);

                Product prod = new Product(id,productName, productType, date, productQuantity);
                dbhelper.updateProduct(prod);
                return RedirectToAction("AllProducts");
                
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dbhelper.GetDistinctProduct( id));
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                dbhelper.deleteProduct(id);
                return RedirectToAction("AllProducts");
            }
            catch
            {
                return View();
            }
        }
    }
}
