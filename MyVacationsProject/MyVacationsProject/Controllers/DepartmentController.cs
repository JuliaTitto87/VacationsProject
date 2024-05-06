using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyVacationsProject.Models;
using Vacations_BLL.Contracts;
using Vacations_DAL;
using Vacations_DomainModel.Models.Department;

namespace MyVacationsProject.Controllers
{
     public class DepartmentController : Controller
    {
        // GET: DepartmentController
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IUnitOfWork uow, IDepartmentService departmentService)
        {
            _unitOfWork = uow;
            _departmentService = departmentService;
        }
        public ActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);

        }

        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            try
            {
                await _departmentService.CreateDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _departmentService.DeleteDepartment(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

