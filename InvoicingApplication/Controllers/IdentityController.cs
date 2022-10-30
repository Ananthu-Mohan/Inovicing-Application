using InvoicingApplication.Data;
using InvoicingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace InvoicingApplication.Controllers
{
    public class IdentityController : Controller
    {
        private readonly LoginDbContext _db;

        public IdentityController(LoginDbContext db)
        {
            _db = db;
        }

        //GET

        public IActionResult LoginPage()
        {
            return View();
        }

        //POST
        [HttpPost,ActionName("LoginPage")]
        [ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult LoginPage(LoginModelClass retrievedFromUILoginPage)
        {
            
            IEnumerable<LoginModelClass> userDetailsFromDB = _db.Users;
            var userFromDb = _db.Users.FirstOrDefault(user => user.Email == retrievedFromUILoginPage.Email);
            if (userFromDb == null)
            {
                TempData["UserNotFound"] = "Not able to find the given user";
                return RedirectToAction("LoginPage", "Identity");
            }
            foreach (var userDetails in userDetailsFromDB)
            {
                if(userDetails.Email == retrievedFromUILoginPage.Email)
                {
                    if(userDetails.Password == retrievedFromUILoginPage.Password)
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        TempData["WrongPassword"] = "Password mismatch occurred , Please try again.";
                        return RedirectToAction("LoginPage", "Identity");
                    }
                }
            }
            if (!ModelState.IsValid)
            {
                TempData["UserNotFound"] = "Not able to find the given user";
                return RedirectToAction("LoginPage", "Identity");
            }
            return View();
        }
    }
}
