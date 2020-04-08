using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Employee.ViewModels.CustomValidation
{
    public class NameCustomValidationAttribute : ValidationAttribute, IClientModelValidator
    {
        public NameCustomValidationAttribute(string errorMessage) : base(errorMessage)
        {
            
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var PropertyInfo = validationContext.ObjectType.GetProperty("Name");
            //string PropertyValue = PropertyInfo.GetValue(validationContext.ObjectInstance, null).ToString();

                Regex Patter = new Regex(@"^[\p{M}\P{M}]{2,}$");
                if (!Patter.IsMatch(value.ToString()))
                {
                    return new ValidationResult(ErrorMessageString);
                }
            
            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
            string propertyName = "Name";
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-namecustomvalidation", ErrorMessageString);
            MergeAttribute(context.Attributes, "data-val-namecustomvalidation-propertyname", "#" + propertyName);
        }
        private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
