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
using Notes.DTOs.Notes.DeleteNote;
using Notes.DTOs.Notes.GetNote;
using Notes.DTOs.Notes.GetNotesList;
using Notes.DTOs.Notes.UpdateNote;
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
                if (User == null)
                    return NotFound(new
                    {
                        status = TStatusCodes.Not_Found,
                        text = "User not found!"
                    });

                var result = await service.GetNotesListAsync(new GetNotesListRequest(pageNumber, pageSize, sort));

                if (result.Notes == null)
                    return NotFound(new
                    {
                        status = TStatusCodes.Not_Found,
                        text = "Notes not found!"
                    });

                return Ok(new
                {
                    data = new
                    {
                        notes = result.Notes,
                        pageNumber = result.PageNumber,
                        pageSize = result.PageSize,
                        totalNotes = result.TotalNotes,
                        totalPages = result.TotalPages
                    },
                    status = TStatusCodes.OK,
                    text = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCodes.Bad_Request,
                    text = "Failed to get notes!"
                });
            }
        }


        [HttpGet("{id:int}"), Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await service.GetNoteAsync(new GetNoteRequest(id));

                if (User == null)
                    return NotFound(new
                    {
                        status = TStatusCodes.Not_Found,
                        text = "User not found!"
                    });

                if (result.Note == null)
                    return NotFound(new
                    {
                        status = TStatusCodes.Not_Found,
                        text = "Note not found!"
                    });

                return Ok(new
                {
                    data = new
                    {
                        note = result.Note
                    },
                    status = TStatusCodes.OK,
                    text = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCodes.Bad_Request,
                    text = "Failed to get notes!"
                });
            }
        }


        [HttpPost, Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Post([FromBody] AddNoteDto noteDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        status = TStatusCodes.Bad_Request,
                        text = "Incorrect data!"
                    });

                var result = await service.AddNoteAsync(new AddNoteRequest()
                {
                    Title = noteDto.Title,
                    Description = noteDto.Description,
                    IsDone = noteDto.IsDone,
                });

                if (result.Note == null)
                    return NotFound(new
                    {
                        status = TStatusCodes.Not_Found,
                        text = "Note not found!"
                    });


                return Ok(new
                {
                    data = new
                    {
                        note = result.Note
                    },
                    status = TStatusCodes.OK,
                    text = "Success!",
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCodes.Bad_Request,
                    text = "Unable to recognize note!"
                });
            }
        }


        [HttpDelete, Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                var result = await service.DeleteNoteAsync(new DeleteNoteRequest(id));

                if (User == null)
                    return NotFound(new
                    {
                        status = TStatusCodes.Not_Found,
                        text = "User not found!"
                    });

                if (!result.IsDeleted)
                    return BadRequest(new
                    {
                        status = TStatusCodes.Bad_Request,
                        text = "Failed to delete note!"
                    });

                return Ok(new
                {
                    status = TStatusCodes.OK,
                    text = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCodes.Bad_Request,
                    text = "Failed to delete note!"
                });
            }
        }


        [HttpPut, Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Put([FromBody] UpdateNoteDto noteDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        status = TStatusCodes.Bad_Request,
                        text = "Incorrect data!"
                    });

                if (User == null)
                    return NotFound(new
                    {
                        status = TStatusCodes.Not_Found,
                        text = "User not found!"
                    });

                var result = await service.UpdateNoteAsync(new UpdateNoteRequest()
                {
                    Id = noteDto.Id,
                    Title = noteDto.Title,
                    Description = noteDto.Description,
                    IsDone = noteDto.IsDone,
                });

                if (!result.IsUpdate)
                    return NotFound(new
                    {
                        status = TStatusCodes.Not_Found,
                        text = "Note not found!",
                    });

                return Ok(new
                {
                    status = TStatusCodes.OK,
                    text = "Success!",
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCodes.Bad_Request,
                    text = "Failed to update note!"
                });
            }
        }
    }
}
