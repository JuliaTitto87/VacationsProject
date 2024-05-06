
using Microsoft.EntityFrameworkCore;
using Vacations_BLL.Contracts;
using Vacations_DAL;
using Vacations_DomainModel.Models.Department;
using Vacations_DomainModel.Models.Vacation;

namespace Vacations_BLL.Services
{
    public class VacationService : IVacationService
    {
        private readonly IUnitOfWork _unitOfWork;


        public VacationService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<string> CreatePartOfVacation(PartOfVacation partOfVacation, string userId)
        {
            string errorMessage = "";
            if (partOfVacation.DateStart >= partOfVacation.DateEnd)
            {
                errorMessage = "Конец отпуска должен быть позже начала";
            }
            var vacationRepo = _unitOfWork.GetRepository<Vacation>();
            var employeeRepo = _unitOfWork.GetRepository<Employee>();
            Employee employee = employeeRepo.AsReadOnlyQueryable().Include(v => v.Vacations).FirstOrDefault(p => p.UserId == userId);
            Vacation vacation = vacationRepo.AsReadOnlyQueryable().Include(d => d.PartsOfVacation).FirstOrDefault(p => p.EmployeeId == employee.Id);
            int VacationDuration = 0;

            foreach (PartOfVacation p in vacation.PartsOfVacation)
            {
                VacationDuration += p.DurationOfVacationPart;
                if (partOfVacation.DateStart >= p.DateStart && partOfVacation.DateStart <= p.DateEnd ||
                partOfVacation.DateEnd >= p.DateStart && partOfVacation.DateEnd <= p.DateEnd ||
                    partOfVacation.DateStart <= p.DateStart && partOfVacation.DateEnd >= p.DateEnd)
                {
                    errorMessage = "На эти даты отпуск уже введен";
                }
            }
            if (VacationDuration + partOfVacation.DurationOfVacationPart > (employee.CurrentDurationOfVocation))
            {
                errorMessage = "Невозможно добавить отпуск такой продолжительности";
            }

            if (errorMessage == "")
            {
                var repPartOfVacation = _unitOfWork.GetRepository<PartOfVacation>();
                repPartOfVacation.Create(partOfVacation);

                await _unitOfWork.SaveChangesAsync();
            }
            return errorMessage;
        }

        public async Task DeletePartOfVacation(int id)
        {
            {
                var partOfVacationrepo = _unitOfWork.GetRepository<PartOfVacation>();
                PartOfVacation partOfVacation = partOfVacationrepo.AsReadOnlyQueryable().FirstOrDefault(p => p.Id == id);
                partOfVacationrepo.Delete(partOfVacation);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<Vacation> GetVacation(string userId)
        {
            var vacationRepo = _unitOfWork.GetRepository<Vacation>();
            var employeeRepo = _unitOfWork.GetRepository<Employee>();
            Employee employee = employeeRepo.AsReadOnlyQueryable().Include(v => v.Vacations).FirstOrDefault(p => p.UserId == userId);
            Vacation vacation = vacationRepo.AsReadOnlyQueryable().Include(d => d.PartsOfVacation).FirstOrDefault(p => p.EmployeeId == employee.Id);
            if (vacation == null)
            {
                vacation = new Vacation
                {
                    Employee = employee,
                    EmployeeId = employee.Id,
                    Year = DateTime.Now.Year
                };
                vacationRepo.Create(vacation);
                employee.Vacations.Add(vacation);
                employeeRepo.InsertOrUpdate(_employee => _employee.Id == employee.Id, employee);
                await _unitOfWork.SaveChangesAsync();
            }

            return vacation;
        }
    }
}

