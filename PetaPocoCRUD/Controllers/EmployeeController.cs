using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PetaPocoCRUD.Models;
using PetaPocoCRUD.Repositories;
using PetaPocoCRUD.UnitOfWorks;
using System.Linq;

namespace PetaPocoCRUD.Controllers
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
            var repo = new RepositoryBase<Employee>(unitOfWork);
            var list = repo.GetAll().ToList();
            return View(list);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using var unitOfWork = new UnitOfWork(_config.GetConnectionString("DB_CONNECTION"));
            var repository = new RepositoryBase<Employee>(unitOfWork);
            var employee = repository.Get(id);

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
                using var unitOfWork = new UnitOfWork(_config.GetConnectionString("DB_CONNECTION"));
                var repository = new RepositoryBase<Employee>(unitOfWork);
                var id = repository.Add<int>(employee);
                unitOfWork.Commit();

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using var unitOfWork = new UnitOfWork(_config.GetConnectionString("DB_CONNECTION"));
            var repository = new RepositoryBase<Employee>(unitOfWork);
            var employee = repository.Get(id);

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
                using var unitOfWork = new UnitOfWork(_config.GetConnectionString("DB_CONNECTION"));
                var repository = new RepositoryBase<Employee>(unitOfWork);
                repository.Modify(employee);
                unitOfWork.Commit();

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using var unitOfWork = new UnitOfWork(_config.GetConnectionString("DB_CONNECTION"));
            var repository = new RepositoryBase<Employee>(unitOfWork);
            var employee = repository.Get(id);

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
            using var unitOfWork = new UnitOfWork(_config.GetConnectionString("DB_CONNECTION"));
            var repository = new RepositoryBase<Employee>(unitOfWork);
            var employee = repository.Get(id);
            if (employee != null) repository.Remove(employee);
            unitOfWork.Commit();

            return RedirectToAction(nameof(Index));
        }
    }
}