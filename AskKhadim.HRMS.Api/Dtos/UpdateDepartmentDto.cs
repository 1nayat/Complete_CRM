using System.ComponentModel.DataAnnotations;

namespace AskKhadim.HRMS.Api.Dtos
{
    public class UpdateDepartmentDto
    {
        [Required]
        public string DepartmentName { get; set; } = null!;

        public string? Description { get; set; }
        public Guid? ParentDepartmentId { get; set; }
        public string? CostCenter { get; set; }
        public string? Location { get; set; }
    }

}
