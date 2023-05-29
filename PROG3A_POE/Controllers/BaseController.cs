using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PROG3A_POE.Models;

namespace PROG3A_POE.Controllers
{
    public class BaseController : Controller
    {
        protected string User_Role
        {
            get => TempData["User_Role"] as string;
            set => TempData["User_Role"] = value;
        }

        protected string Username
        {
            get => TempData["Username"] as string;
            set => TempData["Username"] = value;
        }
        protected string FarmerId
        {
            get => TempData["FarmerId"] as string;
            set => TempData["FarmerId"] = value;
        }

        protected List<Product> MyData
        {
            get => TempData["MyData"] as List<Product>;
            set => TempData["MyData"] = value;
        }
    }
}
