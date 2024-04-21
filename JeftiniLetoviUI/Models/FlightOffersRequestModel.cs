using JeftiniLetoviUI.Models.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace JeftiniLetoviUI.Models
{
    public class FlightOffersRequestModel
    {
        [Required(ErrorMessage = "Code of departin airport is required!")]
        [StringLength(3, ErrorMessage = "Identifier too long (3 character limit).")]
        [RegularExpression(@"^[A-Z]*$", ErrorMessage = "Only uppercase allowed.")]
        public string? DepartingCode { get; set; }

        [Required(ErrorMessage = "Code of arriving airport is required!")]
        [StringLength(3, ErrorMessage = "Identifier too long (3 character limit).")]
        [RegularExpression(@"^[A-Z]*$", ErrorMessage = "Only uppercase allowed.")]
        public string? ArrivalCode { get; set; }

        [Required(ErrorMessage = "Date of departing is required!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DepartureDateValidation("ReturningDate", ErrorMessage = "Date is older than today or greater than the return date.")]
        public DateOnly DepartureDate { get; set; }

        [Required(ErrorMessage = "Date of returning is required!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateOnly ReturningDate { get; set; }

        [Range(1, 100, ErrorMessage = "Number of passengers must be between 1 and 100.")]
        [Required(ErrorMessage = "Number of passengers is required!")]
        public int NumberOfPassengers { get; set; }

        [Required(ErrorMessage = "Currency code is required!")]
        [StringLength(3, ErrorMessage = "Identifier too long (3 character limit).")]
        [RegularExpression(@"^[A-Z]*$", ErrorMessage = "Only uppercase allowed.")]
        public string? Currency { get; set; }
    }
}
