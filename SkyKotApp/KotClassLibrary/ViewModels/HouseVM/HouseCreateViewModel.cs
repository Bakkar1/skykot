using KotClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.ViewModels.HouseVM
{
    public class HouseCreateViewModel : House
    {
        public List<HouseSpecification>HouseSpecificationsList { get; set; }
        public List<HouseExpense> HouseExpensesList { get; set; }
    }
}
