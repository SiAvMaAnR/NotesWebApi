
using System.Security.Claims;

namespace Notes.DTOs.Service.Account.Edit
{
    public class EditRequest
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
