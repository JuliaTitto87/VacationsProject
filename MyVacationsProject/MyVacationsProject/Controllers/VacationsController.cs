using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyVacationsProject.Models;
using System.Globalization;
using System.Reflection.Emit;
using Vacations_BLL.Contracts;
using Vacations_DAL;
using Vacations_DomainModel.Models.Department;
using Vacations_DomainModel.Models.Identity;
using Vacations_DomainModel.Models.Vacation;
using System.Linq;

namespace MyVacationsProject.Controllers
{
    public class VacationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IVacationService _vacationService;
        private readonly IEmailSendingService _emailSender;
        public VacationsController(IUnitOfWork uow, UserManager<User> userManager, IVacationService vacationService, IEmailSendingService emailSendingService)
        {
            _unitOfWork = uow;
            this._userManager = userManager;
            _vacationService = vacationService;
            _emailSender=emailSendingService;

        }

        public ActionResult Index(string month, int year)
        {

            string _userId = _userManager.GetUserId(User);
            var employeeRepo = _unitOfWork.GetRepository<Employee>();
            Employee employee = employeeRepo.AsReadOnlyQueryable().Include(d => d.Department).ToList<Employee>().FirstOrDefault(p => p.UserId == _userId);
                int _year = DateTime.Now.Year;

                string _month = DateTimeFormatInfo.CurrentInfo.MonthNames[DateTime.Now.Month - 1];

                IQueryable<Employee> employees = employeeRepo.AsReadOnlyQueryable().Include(d => d.Vacations).ThenInclude(d => d.PartsOfVacation).Where(d => d.DepartmentId == employee.DepartmentId);

                if (month != null && month != "")
                {
                    _month = month;
                }
                if (year != 0)
                {
                    _year = year;
                }
                VacationsViewModel vacationsViewModel = new VacationsViewModel
                {
                    Department = employee.Department,
                    EmployeeId = employee.Id,
                    Employees = employees,
                    Month = _month,
                    Year = _year

                };
            

            return View(vacationsViewModel);
        }

        // GET: VacationsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VacationsController/Create
        public async Task<ActionResult> Create(string _errorMessage, DateTime dateStart, DateTime dateEnd)
        {
            string userId = _userManager.GetUserId(this.User);
            var employeeRepo = _unitOfWork.GetRepository<Employee>();
            Employee employee = employeeRepo.AsReadOnlyQueryable().Include(v => v.Vacations).FirstOrDefault(p => p.UserId == userId);
                        
            Vacation vacation=await _vacationService.GetVacation(userId);
            ViewBag.NameOfEmployee = employee.FullName;
            ViewBag.DurationOfVocation = employee.CurrentDurationOfVocation;
            ViewBag.PartsOfVacation = vacation.PartsOfVacation;
            ViewBag.ErrorMessage = _errorMessage;

            PartOfVacation partOfVacation = new PartOfVacation { VacationId = vacation.Id, DateStart=dateStart, DateEnd=dateEnd };
            return View(partOfVacation);
        }

        // POST: VacationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePartOfVacation(PartOfVacation partOfVacation)
        {
            string errorMessage = "";
            try
            {
                if (ModelState.IsValid)
                {
                    string _userId = _userManager.GetUserId(User);

                    errorMessage = await _vacationService.CreatePartOfVacation(partOfVacation, _userId).ConfigureAwait(false);
                }
                return RedirectToAction("Create", new { _errorMessage = errorMessage });
            }
            catch
            {
                errorMessage = "Что-то идет не так";
                return RedirectToAction("Create", new { _errorMessage = errorMessage });
            }
        }

        // GET: VacationsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VacationsController/Edit/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePartOfVacation(int id)
        {
            try
            {
                await _vacationService.DeletePartOfVacation(id);

                return RedirectToAction("Create");
            }
            catch
            {
                return RedirectToAction("Create", new { _errorMessage = "Ошибка удаления" });
            }
        }
    }
}
