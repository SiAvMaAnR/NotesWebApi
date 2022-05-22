using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Notes.Domain.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
