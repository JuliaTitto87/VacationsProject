using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacations_BLL.Contracts;
using Vacations_DAL;
using Vacations_DomainModel.Models.Department;
using Vacations_DomainModel.Models.Vacation;

namespace Vacations_BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task CreateDepartment(Department department)
        {
            var departmentRepo = _unitOfWork.GetRepository<Department>();
            departmentRepo.Create(department);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDepartment(int id)
        {
            var departmentRepo = _unitOfWork.GetRepository<Department>();
            Department department = departmentRepo.AsReadOnlyQueryable().FirstOrDefault(p => p.Id == id);
            departmentRepo.Delete(department);
            await _unitOfWork.SaveChangesAsync();
        }

        public List<Department> GetAllDepartments()
        {
            List<Department> departments = new List<Department>();
            var departmentsRepo = _unitOfWork.GetRepository<Department>();
            departments = departmentsRepo.AsReadOnlyQueryable()
                .Include(d => d.Employees) // does not fetch the settings without the include!
                .ToList();
            return departments;
        }

        public Task UpdateDepartment(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
