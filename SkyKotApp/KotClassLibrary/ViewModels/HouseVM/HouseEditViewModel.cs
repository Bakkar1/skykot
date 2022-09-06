using KotClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.ViewModels.HouseVM
{
    public class HouseEditViewModel : House
    {
        public HouseEditViewModel()
        {

        }
        public HouseEditViewModel(House house)
        {
            HouseId = house.HouseId;
            Name = house.Name;
            ZipCodeId = house.ZipCodeId;
            StreetName = house.StreetName;
            HouseNumber = house.HouseNumber;
            Description = house.Description;
            ContractRules = house.ContractRules;
        }
        public List<HouseSpecification> HouseSpecificationsList { get; set; }
        public List<HouseExpense> HouseExpensesList { get; set; }
        public SelectList ZipCodesSelectList { get; set; }

        
    }
}
