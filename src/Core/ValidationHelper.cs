using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Core
{
    public class ValidationHelper
    {

        public static bool ValidateObject(object model, out string errorMessages)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results);

            if (isValid)
            {
                errorMessages = string.Empty;
                return true;
            }
            else
            {
                errorMessages = string.Join(", ", results.Select(a => a.ErrorMessage));
                return false;
            }
        }
    }
}
