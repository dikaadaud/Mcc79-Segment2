using System.ComponentModel.DataAnnotations;

namespace API.Ultilities.Handler
{
    public class ConfirmPasswordAttribute : Attribute
    {
        private string password;

        public ConfirmPasswordAttribute(string password)
        {
            this.password = password;
        }
        protected ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var passwordProperty = validationContext.ObjectType.GetProperty(password);

            if (passwordProperty != null)
            {
                var passwordValue = passwordProperty.GetValue(validationContext.ObjectInstance, null);
                if (passwordValue != null && passwordProperty != null && !value.Equals(passwordValue))
                {
                    return new ValidationResult("Error");
                }
            }
            return ValidationResult.Success;
        }
    }
}
