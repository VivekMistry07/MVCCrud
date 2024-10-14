using CRUDEF.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRUDEF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolContext _schoolContext;

        public HomeController(ILogger<HomeController> logger, SchoolContext schoolContext)
        {
            _logger = logger;
            _schoolContext = schoolContext;
        }

        public IActionResult Index()
        {
            return View(_schoolContext.Teacher);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _schoolContext.Teacher.Add(teacher);
                _schoolContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else 
            {
                return View();
            }

        }

        public IActionResult Update(int id)
        {
            return View(_schoolContext.Teacher.Where(a => a.Id == id).FirstOrDefault());
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult Update_Post(Teacher teacher)
        {
            _schoolContext.Teacher.Update(teacher);
            _schoolContext.SaveChanges();
            return RedirectToAction("Index");
        }

		[HttpPost]
        [ActionName("Delete")]
		public IActionResult Delete(int id)
		{
			var teacher = _schoolContext.Teacher.Where(a => a.Id == id).FirstOrDefault();
			_schoolContext.Teacher.Remove(teacher);
			_schoolContext.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
