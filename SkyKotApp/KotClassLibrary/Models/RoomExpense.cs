using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KotClassLibrary.Models
{
    public class RoomExpense
    {
        public int RoomExpenseId { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int ExpenceId { get; set; }
        public Expence Expence { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
