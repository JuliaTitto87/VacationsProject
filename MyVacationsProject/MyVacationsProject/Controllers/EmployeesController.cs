using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyVacationsProject.Models;
using Vacations_BLL.Contracts;
using Vacations_DAL;
using Vacations_DomainModel.Models.Department;

namespace MyVacationsProject.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IUnitOfWork uow, IEmployeesService employeesService)
        {
            _unitOfWork = uow;
            _employeesService = employeesService;
        }

        // GET: EmployeesController
        public ActionResult Index()
        {
            List<Employee> employees = _employeesService.GetEmployees();
            return View(employees);
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeesController/Create
        public ActionResult Create(string userId)
        {
            var departmentRepo = _unitOfWork.GetRepository<Department>();
            IQueryable<Department> departments = departmentRepo.AsReadOnlyQueryable();
            SelectList _departments = new SelectList(departments, "Id", "Name");
            ViewBag.Departments = _departments;
            EmployeeViewModel employeeViewModel = new EmployeeViewModel { UserId = userId };
            return View(employeeViewModel);
        }


        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var repDepartment = _unitOfWork.GetRepository<Department>();
                IQueryable<Department> departments = repDepartment.AsReadOnlyQueryable();
                Department department = departments.FirstOrDefault(d => d.Id == employeeViewModel.DepartmentId);

                Employee employee = new Employee
                {
                    EmployeesPosition = employeeViewModel.EmployeesPosition,
                    DepartmentId = employeeViewModel.DepartmentId,
                    FirstName = employeeViewModel.FirstName,
                    PersonnelNumber = employeeViewModel.PersonnelNumber,
                    IsHeadOfDepartment = employeeViewModel.IsHeadOfDepartment,
                    LastName = employeeViewModel.LastName,
                    CurrentDurationOfVocation = employeeViewModel.CurrentDurationOfVocation,
                    Vacations = new List<Vacations_DomainModel.Models.Vacation.Vacation>(),
                    UserId = employeeViewModel.UserId
                };
                await _employeesService.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }

            {
                var departmentRepo = _unitOfWork.GetRepository<Department>();
                IQueryable<Department> departments = departmentRepo.AsReadOnlyQueryable();
                SelectList _departments = new SelectList(departments, "Id", "Name");
                ViewBag.Departments = _departments;
                return View(employeeViewModel);
            }
        }

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int id)
        {
            _unitOfWork.BeginTransaction();
            var departmentRepo = _unitOfWork.GetRepository<Department>();
            IQueryable<Department> departments = departmentRepo.AsReadOnlyQueryable();

            var employeeRepo = _unitOfWork.GetRepository<Employee>();
            IQueryable<Employee> employees = employeeRepo.AsReadOnlyQueryable().Include(d => d.Department);

            Employee employee = employees.Include(d => d.Department).FirstOrDefault(_employee => _employee.Id == id);

            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                Departments = new SelectList(departments, "Id", "Name"),
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                IsHeadOfDepartment = employee.IsHeadOfDepartment,
                CurrentDurationOfVocation = employee.CurrentDurationOfVocation,
                EmployeesPosition = employee.EmployeesPosition,
                PersonnelNumber = employee.PersonnelNumber
            };
            return View(employeeViewModel);
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee, int department)

        {
            var rep = _unitOfWork.GetRepository<Department>();
            IQueryable<Department> departments = rep.AsReadOnlyQueryable().Include(e => e.Employees);
            employee.DepartmentId = department;
            employee.Department = departments.FirstOrDefault(d => d.Id == department);
            if (ModelState.IsValid)
            {
                _employeesService.UpdateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }

            {
                EmployeeViewModel employeeViewModel = new EmployeeViewModel
                {
                    CurrentDurationOfVocation = employee.CurrentDurationOfVocation,
                    IsHeadOfDepartment = employee.IsHeadOfDepartment,
                    LastName = employee.LastName,
                    EmployeesPosition = employee.EmployeesPosition,
                    FirstName = employee.FirstName,
                    PersonnelNumber = employee.PersonnelNumber,
                    Departments = new SelectList(departments, "Id", "Name"),
                };
                return View(employeeViewModel);
            }
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
