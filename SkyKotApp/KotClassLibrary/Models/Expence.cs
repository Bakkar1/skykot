using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace KotClassLibrary.Models
{
    public class Expence
    {
        public int ExpenceId { get; set; }
        [Required]
        public string Description { get; set; }
        public ICollection<HouseExpense> HouseExpenses { get; set; }
    }
}
