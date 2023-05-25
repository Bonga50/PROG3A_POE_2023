using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG3A_POE.Models;
using System.Security.Claims;

namespace PROG3A_POE.Controllers
{
    public class UserController : BaseController
    {
      
        public List<User> Users = new List<User>() {
            new User("Aatorox","Emp1","Pass1","Employee"),
            new User("Dris","Farm1", "Pass1", "Farmer"), };
        // GET: UserController
        public ActionResult Login()
        {
            return View();
        }


        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> LoginUserAsync()
        {
            string UserID = Request.Form["txtuserID"].ToString();
            string UserPass = Request.Form["txtPassword"].ToString();

            User model = new User();
            // Perform authentication and validation logic
            if (ModelState.IsValid)
            {
                // Check the username and password 
                bool isAuthenticated = model.checkUser(UserID, UserPass, Users);
                model = model.getUser(UserID, UserPass, Users);


                if (isAuthenticated)
                {
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, model.User_Roles)
                     // Add additional claims as needed (e.g., roles)
                    };
                    TempData["User_Role"] = model.User_Roles;
                    TempData["Username"] = model.Username;
                    var identity = new ClaimsIdentity(claims, "SessionAuthentication");
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("SessionAuthentication", principal);

                    // Authentication successful
                    // Redirect to another view
                    if (model.User_Roles.Equals("Employee"))
                    {
                        return RedirectToAction("AllFarmers", "Farmer");
                    }
                    else
                    {
                        return RedirectToAction("AllProducts", "Product");
                    }
                }
                else
                {
                    // Authentication failed
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            // If there are any validation errors or authentication failed, return the login view
            return View("Login");
        }
        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
