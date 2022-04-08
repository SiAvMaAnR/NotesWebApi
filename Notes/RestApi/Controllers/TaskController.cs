using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notes.Domain.Models;
using Notes.Infrastructure.ApplicationContext;

namespace Notes.Api.Presentation.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly EFContext eFContext;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public TaskController(EFContext eFContext, ILogger<TaskController> logger, IConfiguration configuration)
        {
            this.eFContext = eFContext;
            this.logger = logger;
            this.configuration = configuration;
        }


        [HttpGet]
        public string Get()
        {

            #region mock

            //Person person = new Person()
            //{
            //    Name = "Agent",
            //    Age = 18,
            //};

            //Person person1 = new Person()
            //{
            //    Name = "HHH",
            //    Age = 18,
            //};

            //eFContext.Persons.AddRange(person, person1);

            //User user = new User()
            //{
            //    Email = "samarkin20022002@gmail.com",
            //    Password = "Sosnova61S",
            //    Role = "Admin",
            //    Person = person
            //};


            //eFContext.Users.AddRange(user);

            //Note note = new Note()
            //{
            //    Title = "Hello",
            //    IsDone = true,
            //    Description = "Text",
            //    CreateDate = DateTime.Now,
            //    User = user
            //};

            //eFContext.Notes.AddRange(note);
            //eFContext.SaveChanges();

            //int id = eFContext.Notes.First().Id;

            //var _user = eFContext.Users.First(x => x.Id == id);
            //return _user.Id + _user.Person.Name;

            #endregion


            return "1";
        }


        [HttpPost]
        public async Task<IActionResult> Post()
        {

            return Ok(1);
        }


    }
}
