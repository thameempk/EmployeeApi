using System.ComponentModel.DataAnnotations;

namespace ApiProject.Models.Dto
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        
        public int salary { get; set; }
    }
}
