using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notes.Domain.Enums;
using Notes.Domain.Models;
using Notes.Infrastructure.ApplicationContext;

namespace Notes.Api.Presentation.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly EFContext context;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public TaskController(EFContext context, ILogger<TaskController> logger, IConfiguration configuration)
        {
            this.context = context;
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

            //context.Persons.AddRange(person, person1);

            //User user = new User()
            //{
            //    Email = "samarkin20022002@gmail.com",
            //    PasswordHash = "Sosnova61S",
            //    Role = Roles.Admin.ToString(),
            //    Person = person
            //};


            //context.Users.AddRange(user);

            //Note note = new Note()
            //{
            //    Title = "Hello",
            //    IsDone = true,
            //    Description = "Text",
            //    CreateDate = DateTime.Now,
            //    User = user
            //};

            //context.Notes.AddRange(note);
            //context.SaveChanges();

            ////int id = eFContext.Notes.First().Id;

            ////var _user = eFContext.Users.First(x => x.Id == id);
            ////return _user.Id + _user.Person.Name;

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
