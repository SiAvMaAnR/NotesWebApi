using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notes.Domain.Enums;
using Notes.Domain.Models;
using Notes.DTOs;
using Notes.DTOs.Notes.AddNote;
using Notes.DTOs.Notes.Controller;
using Notes.DTOs.Notes.GetListNote;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastucture.Interfaces;
using Notes.Interfaces;
using System.Security.Claims;

namespace Notes.Api.Presentation.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService service;
        private readonly EFContext context;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public NoteController(INoteService service, EFContext context, ILogger<NoteController> logger, IConfiguration configuration)
        {
            this.service = service;
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
        }


        [HttpGet, Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Get(int pageNumber, int pageSize, string sort = "asc_date")
        {
            try
            {
                var result = await service.GetListNoteAsync(new GetListNoteRequest(pageNumber, pageSize, sort));

                return Ok(new
                {
                    data = new
                    {
                        Notes = result.Notes,
                        PageNumber = result.PageNumber,
                        PageSize = result.PageSize,
                        TotalNotes = result.TotalNotes,
                        TotalPages = result.TotalPages
                    },
                    status = result.StatusCode,
                    text = result.Response
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCodes.Bad_Request,
                    text = "Failed to get users!"
                });
            }
        }


        [HttpPost, Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Post(NoteDto noteDto)
        {
            try
            {
                var result = await service.AddNoteAsync(new AddNoteRequest()
                {
                    Title = noteDto.Title,
                    Description = noteDto.Description,
                    IsDone = noteDto.IsDone,
                    User = User
                });

                if (result.Note == null)
                    return BadRequest(new
                    {
                        status = TStatusCodes.Bad_Request,
                        text = result.Response
                    });


                return Ok(new
                {
                    status = TStatusCodes.OK,
                    text = result.Response,
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCodes.Bad_Request,
                    text = "Unable to recognize user!"
                });
            }
        }
    }
}
