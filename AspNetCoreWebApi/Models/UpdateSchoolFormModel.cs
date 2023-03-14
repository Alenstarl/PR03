using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApi.Models
{
    /// <summary>
    /// Model class for binding the update school input form data.
    /// </summary>
    public class UpdateSchoolFormModel
    {
        [Required]
        [StringLength(32, MinimumLength = 3)]
        public string? Name { get; set; }

        [Required]
        public DateTime EstablishedAt { get; set; }
    }
}
