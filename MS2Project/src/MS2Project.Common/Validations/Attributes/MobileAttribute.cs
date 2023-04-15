using $ext_safeprojectname$.Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace $ext_safeprojectname$.Common.Validations.Attributes
{
    public class MobileAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string strErrorMessage = Memos.InvalidPhoneNumberEntered;

            try
            {
                if (!Regex.IsMatch(value.ToString().TrimEnd().TrimStart(), @"(0|\+98)?([ ]|,|-|[()]){0,2}9[1|2|3|4]([ ]|,|-|[()]){0,2}(?:[0-9]([ ]|,|-|[()]){0,2}){8}"))
                {
                    return new ValidationResult(strErrorMessage);
                }
                else return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult(strErrorMessage);
            }
        }
    }
}
