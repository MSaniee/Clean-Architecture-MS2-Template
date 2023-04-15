using $ext_safeprojectname$.Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace $ext_safeprojectname$.Common.Validations.Attributes
{
    public class UserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string strErrorMessage = Memos.InvalidUserNameEntered;

            try
            {
                if (!Regex.IsMatch(value.ToString().TrimEnd().TrimStart(), @"[A-Za-z]{1,}[A-Za-z0-9]{4,}"))
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
