using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? Role { get; set; }

        public Person Person { get; set; }

        public int PersonId { get; set; }

        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
