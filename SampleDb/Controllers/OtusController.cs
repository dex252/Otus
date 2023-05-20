using Microsoft.AspNetCore.Mvc;
using SampleDb.Models.Entity.Otus;
using SampleDb.Repositories.Db;
using SampleDb.ViewModels;

namespace SampleDb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OtusController : ControllerBase
    {
        private IDbRepository DbRepository { get; }

        public OtusController(IDbRepository dbRepository)
        {
            DbRepository = dbRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllData()
        {
            var otus = new Otus();

            otus.Courses = new List<Course>(await DbRepository.GetAllAsync<Course>());
            otus.Lessons = new List<Lesson>(await DbRepository.GetAllAsync<Lesson>());
            otus.Users = new List<User>(await DbRepository.GetAllAsync<User>());

            return Ok(otus);
        }

        [HttpPost]
        [Route("user")]
        public async Task<IActionResult> AddUser([FromBody]User user)
        {
            user.Id = Guid.NewGuid();
            var isSuccess = await DbRepository.AddAsync(user);

            if (!isSuccess)
            {
                return BadRequest();
            }

            return Ok(user.Id);
        }

        [HttpPost]
        [Route("course")]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            course.Id = Guid.NewGuid();
            var isSuccess = await DbRepository.AddAsync(course);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return Ok(course.Id);
        }

        [HttpPost]
        [Route("lesson")]
        public async Task<IActionResult> AddLesson([FromBody] Lesson lesson)
        {
            lesson.Id = Guid.NewGuid();
            var isSuccess = await DbRepository.AddAsync(lesson);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return Ok(lesson.Id);
        }

        [HttpPost]
        public async Task<IActionResult> SubscribeUserOnCource(Guid userId, Guid courceId)
        {
            var isSuccess = await DbRepository.SubscribeAsync<User, Course>(userId, courceId);
            return Ok(isSuccess);
        }

    }
}
