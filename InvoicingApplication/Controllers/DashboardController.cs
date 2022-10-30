using Microsoft.AspNetCore.Mvc;

namespace InvoicingApplication.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
