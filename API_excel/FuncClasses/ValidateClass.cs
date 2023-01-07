using API_excel.Models;
using System.ComponentModel.DataAnnotations;

namespace API_excel.FuncClasses
{
    public static class ValidateClass
    {
        public static bool CheckValidateModel(ValueRead val)
        {
            var context = new ValidationContext(val, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(val, context, results);

            return isValid;
        }
    }  
}
