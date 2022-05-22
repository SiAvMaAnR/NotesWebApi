
using System.Security.Claims;

namespace Notes.DTOs.Service.Account.Edit
{
    public class EditRequest
    {
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
    }
}
