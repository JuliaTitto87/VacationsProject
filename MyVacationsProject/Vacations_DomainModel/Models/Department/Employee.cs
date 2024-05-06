using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacations_DomainModel.Models.Identity;
using Vacations_DomainModel.Models.Vacation;

namespace Vacations_DomainModel.Models.Department
{
    public class Employee : Entity<int>
    {
        public User? User { get; set; }
        public string? UserId { get; set; }
        [Required]



        public required int PersonnelNumber { get; set; }

        [MaxLength(100, ErrorMessage = "Максимальная  длина текста - 100 символов")]

        [Required(ErrorMessage = "Поле Должность не может быть пустым")]
        public required string EmployeesPosition { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Поле Имя не может быть пустым")]
        public required string FirstName { get; set; }


        [MaxLength(100)]
        [Required(ErrorMessage = "Поле Фамилия не может быть пустым")]
        public string? LastName { get; set; }

        public string FullName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LastName))
                {
                    return FirstName;
                }
                return $"{FirstName} {LastName}";
            }
        }

        [Range (0, 365, ErrorMessage ="Продолжительность отпуска не может быть меньше 0 и больше 365 дней")]
        public int CurrentDurationOfVocation {  get; set; }
        private List<Vacation.Vacation> vacations = new List<Vacation.Vacation>();
        public List<Vacation.Vacation> Vacations 
        {
            get { return vacations; } 
            set { vacations=value; }
        }
        [ForeignKey("Department")]
        public required int DepartmentId { get; set; }

        public Department Department { get; set; }

        public bool IsHeadOfDepartment { get; set; }

    }
}
