using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApi.Models
{
    public class CreateSchoolFormModel
    {
        [Required]
        [StringLength(32, MinimumLength = 3)]
        public string? Name { get; set; }
    }
}
