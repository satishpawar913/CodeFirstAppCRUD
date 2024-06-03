using System.ComponentModel.DataAnnotations;

namespace CodeFirstApproachCRUD.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(50, ErrorMessage = "Department name must be up to 50 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Department name can only contain letters and spaces.")]
        public required string DeptName { get; set; }

        [Required(ErrorMessage = "Department head name is required.")]
        [StringLength(50, ErrorMessage = "Department head name must be up to 50 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Department head name can only contain letters and spaces.")]
        public required string DeptHead { get; set; }

        public ICollection<Employee> Employees { get; set; } = [];

    }
}
