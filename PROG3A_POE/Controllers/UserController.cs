﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG3A_POE.Data;
using PROG3A_POE.Models;
using System.Security.Claims;

namespace PROG3A_POE.Controllers
{
    public class UserController : BaseController
    {
        private string con;
        private IConfiguration _config;
        ApplicationDBContext dbhelper;

        public UserController(IConfiguration configuration)
        {
            dbhelper = new ApplicationDBContext(configuration);
        }


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
            try
            {

           
            string UserID = Request.Form["txtuserID"].ToString();
            string UserPass = Request.Form["txtPassword"].ToString();

            User model = new User();
            // Perform authentication and validation logic
            if (ModelState.IsValid)
            {
                // Check the username and password 
                model = dbhelper.getUser(UserID, UserPass);


                if (model != null)
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
            }
            catch (Exception)
            {

                return View("Login");
            }
            return View("Login");

            // If there are any validation errors or authentication failed, return the login view


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
