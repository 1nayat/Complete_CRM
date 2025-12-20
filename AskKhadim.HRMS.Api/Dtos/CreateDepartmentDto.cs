using System.ComponentModel.DataAnnotations;

namespace AskKhadim.HRMS.Api.Dtos
{
    public class CreateDepartmentDto
    {
        [Required]
        public string DepartmentCode { get; set; } = null!;

        [Required]
        public string DepartmentName { get; set; } = null!;

        public string? Description { get; set; }
        public Guid? ParentDepartmentId { get; set; }
        public string? CostCenter { get; set; }
        public string? Location { get; set; }
    }

}
