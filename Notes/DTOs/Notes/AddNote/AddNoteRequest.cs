﻿using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Notes.DTOs.Notes.AddNote
{
    public class AddNoteRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(300, MinimumLength = 0)]
        public string Description { get; set; }

        [Required]
        public bool IsDone { get; set; }
    }
}
