using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KotClassLibrary.Models;

public class HouseExpense
{
    public int HouseExpenseId { get; set; }
    public int HouseId { get; set; }
    public House House { get; set; }
    public int ExpenceId { get; set; }
    public Expence Expence { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int Value { get; set; }
}
