using Microsoft.EntityFrameworkCore;
using Vacations_BLL.Contracts;
using Vacations_DAL;
using Vacations_DomainModel.Models.Department;

namespace Vacations_BLL.Services
{
    internal class EmployeeService : IEmployeesService
    {
        private readonly IUnitOfWork _unitOfWork;


        public EmployeeService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task AddEmployee(Employee employee)
        {
            var employeeRepo = _unitOfWork.GetRepository<Employee>();
            employeeRepo.Create(employee);
            await _unitOfWork.SaveChangesAsync();

        }

        public Task DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            var employeeRepo = _unitOfWork.GetRepository<Employee>();
            employees = employeeRepo.AsReadOnlyQueryable().Include(d => d.Department).ToList<Employee>();
            return employees;
        }


        public async Task UpdateEmployee(Employee employee)
        {
            var employeeRepo = _unitOfWork.GetRepository<Employee>();
            employeeRepo.InsertOrUpdate(_employee => _employee.Id == employee.Id, employee);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
