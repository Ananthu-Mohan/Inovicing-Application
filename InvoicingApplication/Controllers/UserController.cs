using InvoicingApplication.Data;
using InvoicingApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly LoginDbContext _db;

        public UserController(LoginDbContext db)
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

        public IActionResult Index()
        {
            RetrieveUserInfo();
            IEnumerable<LoginModelClass> ObjUsersInList = _db.Users;
            return View(ObjUsersInList);
        }
        
        //GET
        public IActionResult Edit(int? id)
        {
            RetrieveUserInfo();
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var foundUser = _db.Users.Find(id);
            if (foundUser == null)
            {
                return NotFound();
            }
            return View(foundUser);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LoginModelClass UpdatedInfo)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Update(UpdatedInfo);
                _db.SaveChanges();
                TempData["UpdateSucess"] = "Updated successfully , Please re-login again";
                return RedirectToAction("LoginPage", "Identity");
            }
            return View(UpdatedInfo);
        }
        //GET
        public IActionResult CreateUser()
        {
            RetrieveUserInfo();
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(LoginModelClass UpdatedInfo)
        {

            if (ModelState.IsValid)
            {
                _db.Users.Add(UpdatedInfo);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult EditOtherUser(int? id)
        {
            RetrieveUserInfo();
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var foundUser = _db.Users.Find(id);
            if (foundUser == null)
            {
                return NotFound();
            }
            return View(foundUser);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOtherUser(LoginModelClass UpdatedInfo)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Update(UpdatedInfo);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(UpdatedInfo);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            RetrieveUserInfo();
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var UserFromDb = _db.Users.Find(id);
            if (UserFromDb == null)
            {
                return NotFound();
            }
            return View(UserFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var UserFromDb = _db.Users.Find(id);
            if (UserFromDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Users.Remove(UserFromDb);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(UserFromDb);
        }
    }
}
