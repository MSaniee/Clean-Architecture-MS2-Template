using $ext_safeprojectname$.Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace $ext_safeprojectname$.Common.Validations.Attributes
{
    public class NationalIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string strErrorMessage = Memos.InvalidNationalIdEntered;
            if (value == null)
            {
                return ValidationResult.Success;
            }
            if (value.ToString().Trim().Length != 10)
            {
                return new ValidationResult(strErrorMessage);
            }
            try
            {
                value = value.ToString().PadLeft(10, '0');

                if (!Regex.IsMatch(value.ToString(), @"^\d{10}$"))
                {
                    return new ValidationResult(strErrorMessage);
                }

                var check = Convert.ToInt32(value.ToString().Substring(9, 1));
                var sum = Enumerable.Range(0, 9)
                    .Select(x => Convert.ToInt32(value.ToString().Substring(x, 1)) * (10 - x))
                    .Sum() % 11;

                if (sum < 2 && check == sum || sum >= 2 && check + sum == 11)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(strErrorMessage);
                }
            }
            catch (Exception)
            {
                return new ValidationResult(strErrorMessage);
            }
        }
    }
}
