using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using PROG3A_POE.Data;
using PROG3A_POE.Models;
using System.Data.SqlClient;
using System.Data.SqlTypes;

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
           
            FarmerList = dbhelper.GetAllFarmers();
            return View(FarmerList);
        }
        // GET: FarmerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FarmerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                string FarmerId = collection["txtstId"];
                string FarmerName = collection["txtFirstName"];
                string FarmerPassword = collection["txtSurname"];

                User Farmer = new User(FarmerName, FarmerId, FarmerPassword,"Farmer");
                dbhelper.AddFarmer(Farmer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: FarmerController/Edit/5
        public ActionResult ViewProducts(int id)
        {
            return View();
        }

        // POST: FarmerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewProducts(int id, IFormCollection collection)
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

        // GET: FarmerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FarmerController/Edit/5
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

        // GET: FarmerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FarmerController/Delete/5
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
