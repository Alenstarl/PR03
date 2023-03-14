using AspNetCoreWebApi.Models;
using AspNetCoreWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace AspNetCoreWebApi.Controllers
{
    [Route("api/v1/school")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolCrudService _schoolCrudService;

        public SchoolController(SchoolCrudService schoolCrudService)
        {
            _schoolCrudService = schoolCrudService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var schools = await _schoolCrudService.GetAsync();

            return Ok(schools);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateSchoolFormModel newSchool)
        {
            if (!ModelState.IsValid)
            {
                // Will return HTTP status code 400, which indicates user input errors due validation etc.
                return ValidationProblem(ModelState);
            }

            var newId = await _schoolCrudService.CreateAsync(newSchool);

            return Ok(newId);
        }

        [HttpPut("{schoolId}")]
        public async Task<ActionResult> Update([FromRoute] int schoolId, UpdateSchoolFormModel newSchoolData)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var isSuccess = await _schoolCrudService.UpdateAsync(schoolId, newSchoolData);

            if (!isSuccess)
            {
                ModelState.AddModelError("schoolId", "The school data was not found.");
                return ValidationProblem(ModelState);
            }

            return Ok($"School ID {schoolId} has been successfully updated.");
        }

        [HttpDelete("{schoolId}")]
        public async Task<ActionResult> Delete([FromRoute] int schoolId)
        {
            var isSuccess = await _schoolCrudService.DeleteAsync(schoolId);

            if (!isSuccess)
            {
                ModelState.AddModelError("schoolId", "The school data was not found.");
                return ValidationProblem(ModelState);
            }

            return Ok($"School ID {schoolId} has been successfully deleted.");
        }

        [Obsolete("Raw query version.")]
        [HttpGet("raw")]
        public ActionResult GetObsolete()
        {
            var schools = new List<SchoolViewModel>();
            var connString = "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=turbo_bootcamp;";
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var selectCommand = conn.CreateCommand())
                {
                    var query = @"
SELECT school_id,
    name,
    established_at
FROM schools";
                    selectCommand.CommandText = query;

                    // Use reader to SELECT data.
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var school = new SchoolViewModel
                            {
                                // Get the school_id, which column index in the SELECT query is 0.
                                SchoolId = reader.GetInt32(0),
                                SchoolName = reader.GetString(1),
                                EstablishedAt = reader.GetDateTime(2)
                            };

                            schools.Add(school);
                        }
                    }
                }
                
            }

            return Ok(schools);
        }
    
        [HttpGet("students")]
        public async Task<ActionResult> GetStudents()
        {
            var students = await _studentCrudService.GetStudentsAsync();
            var studentViewModels = students.Select(s => new StudentViewModel
            {
                StudentId = s.StudentId,
                StudentName = s.StudentName,
                Nickname = s.Nickname,
                PhoneNumber = s.PhoneNumber,
                JoinedAt = s.JoinedAt,
                SchoolName = s.School.Name
            });

            return Ok(studentViewModels);
        }
    }
}
