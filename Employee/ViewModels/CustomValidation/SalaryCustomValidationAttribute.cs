using Employee.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Poco.CustomValidation
{
    public class SalaryCustomValidationAttribute : ValidationAttribute, IClientModelValidator
    {
        private string otherPropertyName;
        public SalaryCustomValidationAttribute(string otherPropertyName, string errorMessage) : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-salarycustomvalidation", ErrorMessageString);
            MergeAttribute(context.Attributes, "data-val-salarycustomvalidation-otherpropertyname", "#" + otherPropertyName);
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


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ProvincePropertyInfo = validationContext.ObjectType.GetProperty(otherPropertyName);
            string SelectedProvinceIdPropertyValue = ProvincePropertyInfo.GetValue(validationContext.ObjectInstance, null).ToString();

            if(Int32.Parse(SelectedProvinceIdPropertyValue) == 2) // ProvinceId = 2 for Quebec
            {
                Regex Patter = new Regex(@"^[0-9]{2}[\s\,]?[0-9]{3}((\.[0-9]{2})|(\,[0-9]{2}))?$");
                if (!Patter.IsMatch(value.ToString()))
                {
                    return new ValidationResult(ErrorMessageString);
                }

            }
            else
            {
                Regex Patter = new Regex(@"^[0-9]{2}[\,]?[0-9]{3}(\.[0-9]{2})?$");
                if (!Patter.IsMatch(value.ToString()))
                {
                    return new ValidationResult(ErrorMessageString);
                }
            }
            return ValidationResult.Success;
        }
    }
    
}
