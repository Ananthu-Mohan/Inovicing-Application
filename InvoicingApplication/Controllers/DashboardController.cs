using InvoicingApplication.Data;
using InvoicingApplication.Models;
using InvoicingApplication.Utilis;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingApplication.Controllers
{
    public class DashboardController : Controller
    {
        private readonly LoginDbContext _db;
        SystemUtils systemUtils = new SystemUtils();
        public DashboardController(LoginDbContext db)
        {

            _db = db;
            
        }
        public void RetrieveUserInfo()
        {
            try
            {
                ViewBag.Email = HttpContext.Session.GetString("Email");
                string temp = ViewBag.Email;
                int id = 1;
                int atTheRateIndexValue = temp.IndexOf('@');
                string userName = temp.Substring(0, atTheRateIndexValue);
                ViewBag.userName = userName;
                var UsersInfo = _db.Users.ToList();
                if (UsersInfo == null)
                {
                    id = 1;
                }
                foreach (var users in UsersInfo)
                {
                    if (users.Email == ViewBag.Email)
                    {
                        id = users.Id;
                    }
                }
                ViewBag.Id = id;
            }
            catch (Exception ex)
            {
                ViewBag.Email = "Error-User";
                ViewBag.userName = "Error-User";
                ViewBag.Id = 0;
            }
        }

        public IActionResult DashboardPage()
        {
            RetrieveUserInfo();
            if(ViewBag.Id == 0)
            {
                return NotFound();
            }
            return View();
        }
        public IActionResult ContactUs()
        {
            RetrieveUserInfo();
            return View();
        }
    }
}
