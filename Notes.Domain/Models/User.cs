using Notes.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Notes.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }

        public string Role { get; set; }

        public Person Person { get; set; }

        public int PersonId { get; set; }

        [JsonIgnore]
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
