using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Domain.Enums;
using Notes.DTOs.Controller.Notes;
using Notes.DTOs.Service.Notes.AddNote;
using Notes.DTOs.Service.Notes.DeleteNote;
using Notes.DTOs.Service.Notes.GetNote;
using Notes.DTOs.Service.Notes.GetNotes;
using Notes.DTOs.Service.Notes.UpdateDoneNote;
using Notes.DTOs.Service.Notes.UpdateFavoriteNote;
using Notes.DTOs.Service.Notes.UpdateNote;
using Notes.Infrastructure.ApplicationContext;
using Notes.Interfaces;

namespace Notes.Api.Presentation.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService service;
        private readonly EFContext context;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public NotesController(INoteService service, EFContext context, ILogger<NotesController> logger, IConfiguration configuration)
        {
            this.service = service;
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
        }

        [HttpGet("Get"), Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetNotes([FromQuery] GetNotesDto noteDto)
        {
            try
            {
                if (User == null)
                {
                    return NotFound(new
                    {
                        message = "User not found!"
                    });
                }

                var result = await service.GetNotesAsync(new GetNotesRequest()
                {
                    PageNumber = noteDto.PageNumber,
                    PageSize = noteDto.PageSize,
                    Sort = noteDto.Sort,
                    OnlyFavorite = false
                });

                if (result.Notes == null)
                {
                    return NotFound(new
                    {
                        message = "Notes not found!"
                    });
                }

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
                    message = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to get notes!"
                });
            }
        }

        [HttpGet("Get/Favorite"), Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetFavoriteNotes([FromQuery] GetNotesDto noteDto)
        {
            try
            {
                if (User == null)
                {
                    return NotFound(new
                    {
                        message = "User not found!"
                    });
                }

                var result = await service.GetNotesAsync(new GetNotesRequest()
                {
                    PageNumber = noteDto.PageNumber,
                    PageSize = noteDto.PageSize,
                    Sort = noteDto.Sort,
                    OnlyFavorite = true
                });

                if (result.Notes == null)
                {
                    return NotFound(new
                    {
                        message = "Notes not found!"
                    });
                }

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
                    message = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to get notes!"
                });
            }
        }

        [HttpGet("Get/{id:int}"), Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetNote(int id)
        {
            try
            {
                var result = await service.GetNoteAsync(new GetNoteRequest(id));

                if (User == null)
                {
                    return NotFound(new
                    {
                        message = "User not found!"
                    });
                }

                if (result.Note == null)
                {
                    return NotFound(new
                    {
                        message = "Note not found!"
                    });
                }

                return Ok(new
                {
                    data = new { note = result.Note },
                    message = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to get notes!"
                });
            }
        }

        [HttpPost("Add"), Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> AddNote([FromBody] AddNoteDto noteDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Incorrect data!"
                    });
                }

                var result = await service.AddNoteAsync(new AddNoteRequest()
                {
                    Title = noteDto.Title,
                    Description = noteDto.Description,
                    IsDone = noteDto.IsDone,
                    EventDate = noteDto.EventDate
                });

                if (result.Note == null)
                {
                    return NotFound(new
                    {
                        message = "Note not found!"
                    });
                }

                return Ok(new
                {
                    data = new { note = result.Note },
                    message = "Success!",
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Unable to recognize note!"
                });
            }
        }

        [HttpDelete("Delete"), Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteNote([FromBody] int id)
        {
            try
            {
                var result = await service.DeleteNoteAsync(new DeleteNoteRequest(id));

                if (User == null)
                {
                    return NotFound(new
                    {
                        message = "User not found!"
                    });
                }

                if (!result.IsDeleted)
                {
                    return BadRequest(new
                    {
                        message = "Failed to delete note!"
                    });
                }

                return Ok(new
                {
                    message = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to delete note!"
                });
            }
        }

        [HttpPut("Update"), Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateNote([FromBody] UpdateNoteDto noteDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Incorrect data!"
                    });
                }

                if (User == null)
                {
                    return NotFound(new
                    {
                        message = "User not found!"
                    });
                }

                var result = await service.UpdateNoteAsync(new UpdateNoteRequest()
                {
                    Id = noteDto.Id,
                    Title = noteDto.Title,
                    Description = noteDto.Description,
                    IsDone = noteDto.IsDone,
                });

                if (!result.IsSuccess)
                {
                    return NotFound(new
                    {
                        message = "Note not found!",
                    });
                }

                return Ok(new
                {
                    message = "Success!",
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to update note!"
                });
            }
        }

        [HttpPut("Done"), Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DoneNote([FromBody] UpdateDoneNoteDto noteDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Incorrect data!"
                    });
                }

                if (User == null)
                {
                    return NotFound(new
                    {
                        message = "User not found!"
                    });
                }

                var result = await service.UpdateDoneNoteAsync(new UpdateDoneNoteRequest()
                {
                    Id = noteDto.Id,
                    IsDone = noteDto.IsDone,
                });

                if (!result.IsSuccess)
                {
                    return NotFound(new
                    {
                        message = "Note not found!",
                    });
                }

                return Ok(new
                {
                    message = "Success!",
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to update note!"
                });
            }
        }

        [HttpPut("Favorite"), Authorize(Roles ="Admin,User")]
        public async Task<IActionResult> FavoriteNote([FromBody] UpdateFavoriteNoteDto noteDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Incorrect data!"
                    });
                }

                if (User == null)
                {
                    return NotFound(new
                    {
                        message = "User not found!"
                    });
                }

                var result = await service.UpdateFavoriteNoteAsync(new UpdateFavoriteNoteRequest()
                {
                    Id = noteDto.Id,
                    IsFavorite = noteDto.IsFavorite,
                });

                if (!result.IsSuccess)
                {
                    return NotFound(new
                    {
                        message = "Note not found!",
                    });
                }

                return Ok(new
                {
                    message = "Success!",
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to update note!"
                });
            }
        }
    }
}
