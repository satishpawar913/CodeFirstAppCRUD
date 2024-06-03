using System.ComponentModel.DataAnnotations;

namespace CodeFirstApproachCRUD.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee name is required.")]
        [StringLength(50, ErrorMessage = "Employee name must be up to 50 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Employee name can only contain letters and spaces.")]
        public required string Name { get; set; }

        [Required]
        [RegularExpression(@"^[^@\s]+@gmail\.com$", ErrorMessage = "Invalid Email Address. Email must be a valid Gmail address.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Postal code is required.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Postal code must be exactly 6 digits.")]
        public required string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country must be up to 50 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Country can only contain letters and spaces.")]
        public required string Country { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number. Format: XXXXXXXXXX")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public required string Gender { get; set; }

        public int? DeptId { get; set; }

        public required Department? Department { get; set; }
    }
}
