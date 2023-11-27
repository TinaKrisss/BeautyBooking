using Microsoft.AspNetCore.Mvc;

namespace BeautyBooking.Controllers
{
    public class FreeTimeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
