﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KotClassLibrary.CustomModelValidation
{
    public class ValidateRenterEndDate : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var dtm = DateTime.Now;
            var lst = new List<ModelValidationResult>();

            if (DateTime.TryParse(context.Model.ToString(), out dtm))
            {

                if (dtm < DateTime.Now)
                {
                    lst.Add(new ModelValidationResult("", "Date End of Date cannot be in the past"));
                }
                else if (dtm < new DateTime(1900, 1, 1))
                {
                    lst.Add(new ModelValidationResult("", "Date Of Birth should not be before 1900"));
                }
            }
            else
            {
                lst.Add(new ModelValidationResult("", "Not a valid Date!"));
            }
            return lst;
        }
    }
}
