﻿using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Notes.DTOs.Service.Notes.UpdateNote
{
    public class UpdateNoteRequest
    {
        public UpdateNoteRequest(int id, string title, string description, DateTime eventDate, bool isDone)
        {
            Id = id;
            Title = title;
            Description = description;
            EventDate = eventDate;
            IsDone = isDone;
        }
        public UpdateNoteRequest() { }

        [Required]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        public DateTime EventDate { get; set; }

        [StringLength(1000, MinimumLength = 0)]
        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}
