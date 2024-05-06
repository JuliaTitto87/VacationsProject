using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacations_DAL;
using Vacations_DomainModel.Models.Department;
using Vacations_DomainModel.Models.Identity;
using Vacations_DomainModel.Models.Vacation;

namespace Vacations_BLL.Contracts
{
    public interface IVacationService:IService
    {
        public Task<string> CreatePartOfVacation(PartOfVacation partOfVacation, string userId);

        public Task DeletePartOfVacation(int id);

        public Task<Vacation> GetVacation(string userId);

    }
}
