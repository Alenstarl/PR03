namespace AspNetCoreWebApi.Models
{
    public class SchoolViewModel
    {
        public int SchoolId { get; set; }

        public string SchoolName { get; set; } = string.Empty;

        public DateTime EstablishedAt { get; set; }
    }
}
