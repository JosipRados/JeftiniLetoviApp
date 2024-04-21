using System.ComponentModel.DataAnnotations;

namespace JeftiniLetoviUI.Models.CustomValidation
{
    public class DepartureDateValidation : ValidationAttribute
    {
        private readonly string _returnDate;

        public DepartureDateValidation(string returnDate)
        {
            _returnDate = returnDate;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var departureDate = (DateOnly)value;

            var property = validationContext.ObjectType.GetProperty(_returnDate);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var returnDate = (DateOnly)property.GetValue(validationContext.ObjectInstance);

            if (departureDate > returnDate || departureDate < DateOnly.FromDateTime(DateTime.Now))
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
