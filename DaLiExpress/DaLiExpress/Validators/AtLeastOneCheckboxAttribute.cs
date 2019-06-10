using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DaLiExpress.Validators
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AtLeastOneCheckboxAttribute : ValidationAttribute
    {
        private string[] _checkboxNames;

        public AtLeastOneCheckboxAttribute(params string[] checkboxNames)
        {
            this._checkboxNames = checkboxNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfos = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => this._checkboxNames.Contains(x.Name));

            var values = propertyInfos.Select(x => x.GetGetMethod().Invoke(value, null));
            if (values.Any(x => Convert.ToBoolean(x)))
                return ValidationResult.Success;
            else
            {
                this.ErrorMessage = "At least one checkbox must be selected";
                return new ValidationResult(this.ErrorMessage);
            }
        }
    }
}