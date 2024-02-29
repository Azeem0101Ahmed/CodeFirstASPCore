using CodeFirstASPCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CodeFirstASPCore.Controllers
{
    public class HomeController : Controller
    {


        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly StudentDBContext student;
        public HomeController(StudentDBContext student)
        {
            this.student = student;
        }

        public IActionResult Index()
        {  
            var stdData = student.Students.ToList();
            return View(stdData);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student std)
        {
            if (ModelState.IsValid)
            {
                await student.Students.AddAsync(std);
                await student.SaveChangesAsync();
                TempData["create"]=" Creation is Completed";
                return RedirectToAction("Index","Home");
            }
            return View(std);
        }
        public async Task<IActionResult> Details(int id)
        {
           
               var stdData=  await student.Students.FirstOrDefaultAsync(x=>x.Id==id);
                
            return View(stdData);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var stdData = await student.Students.FindAsync(id);

            return View(stdData);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id ,Student std)
        {
            if (id != std.Id)
            {
               return NotFound();
            }
            if (ModelState.IsValid)
            {
                student.Students.Update(std);
                await student.SaveChangesAsync();
                TempData["update"] = "Updation is Complate...";
                return RedirectToAction("Index", "Home");
            }
            return View(std);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var StdData = student.Students.FirstOrDefault(x=>x.Id==id);
            if(StdData==null || student.Students == null)
            {
                return NotFound();
            }
            return View(StdData);
        }
        [HttpPost , ActionName("Delete")]
        public async Task<IActionResult> DeleteConform(int id)
        {
            var stdData=student.Students.FirstOrDefault(y=>y.Id==id);
            if(stdData==null || student.Students == null)
            { 
                return NotFound();
            }
            student.Students.Remove(stdData);
            await student.SaveChangesAsync();
            TempData["delete"] = "Deletion is Completed";
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
