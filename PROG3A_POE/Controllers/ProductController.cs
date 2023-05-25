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

        public List<Product> ProductList;
        //public List<Product> Products = new List<Product>() { new Product("001","Meat","Protien", DateTime.Parse("12/30/2015"),4),
        //new Product("002","Chicken","Protien", DateTime.Parse("12/30/2014"),4)};
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
            return View();
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
