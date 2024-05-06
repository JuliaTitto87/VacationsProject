using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacations_DomainModel.Models.Department;

namespace Vacations_DomainModel.Models.Vacation
{
    public class PartOfVacation : Entity<int>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать дату")]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать дату")]
        public DateTime DateEnd { get; set; }

        [Range(0, 365, ErrorMessage = "Продолжительность отпуска не может быть меньше 0 дней")]
        public int DurationOfVacationPart
        {
            get
            {
                if ((DateStart == DateTime.MinValue || DateEnd == DateTime.MinValue))
                {
                    return 0;
                }
                if ((DateStart > DateEnd ))
                {
                    return -1;
                }
                return (DateEnd - DateStart).Days;
            }
        }
        public Vacation? Vacation { get; set; }
        public int VacationId { get; set; }


    }
}
