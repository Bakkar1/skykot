using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KotClassLibrary.CustomModelValidation
{
    public class ValidateDateFuture : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var dtm = DateTime.Now;
            var lst = new List<ModelValidationResult>();

            if (DateTime.TryParse(context.Model.ToString(), out dtm))
            {

                if (dtm < DateTime.Now)
                {
                    lst.Add(new ModelValidationResult("", "Date cannot be in the past"));
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
