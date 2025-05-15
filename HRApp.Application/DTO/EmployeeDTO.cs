using System.ComponentModel.DataAnnotations;

namespace HRApp.Application.DTO;

public class EmployeeDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(32)]
    [Display(Name = "EmployeeID")]
    public string EmployeeID { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [Display(Name = "Birth Place")]
    public string BirthPlace { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Birth Date")]
    public DateTime BirthDate { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    [Display(Name = "Basic Salary")]
    public double BasicSalary { get; set; }

    [Required]
    public string Gender { get; set; } = null!;

    [Required]
    [Display(Name = "Marital Status")]
    public string MaritalStatus { get; set; } = null!;
}