using DapperCRUD.Models;
using DapperCRUD.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DapperCRUD
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _config;

        public EmployeeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            using var unitOfWork = new UnitOfWork(_config.GetConnectionString("DB_CONNECTION"));
            var list = unitOfWork.EmployeeRepository.All();
            return View(list);
        }

        public IActionResult Details(int id)
        {
            using var unitOfWork = new UnitOfWork(_config.GetConnectionString("DB_CONNECTION"));
            var employee = unitOfWork.EmployeeRepository.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,firstName,lastName,age,email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = new UnitOfWork(_config.GetConnectionString("DB_CONNECTION")))
                {
                    unitOfWork.EmployeeRepository.Add(employee);
                    unitOfWork.Complete();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            using var unitOfWork = new UnitOfWork(_config.GetConnectionString("DB_CONNECTION"));
            var employee = unitOfWork.EmployeeRepository.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,firstName,lastName,age,email")] Employee employee)
        {
            if (id != employee.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var unitOfWork = new UnitOfWork(_config.GetConnectionString("DB_CONNECTION")))
                {
                    unitOfWork.EmployeeRepository.Update(employee);
                    unitOfWork.Complete();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            using var unitOfWork = new UnitOfWork(_config.GetConnectionString("DB_CONNECTION"));
            var employee = unitOfWork.EmployeeRepository.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (var unitOfWork = new UnitOfWork(_config.GetConnectionString("DB_CONNECTION")))
            {
                unitOfWork.EmployeeRepository.Remove(id);
                unitOfWork.Complete();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}