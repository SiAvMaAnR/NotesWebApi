using Notes.Domain.Models;
using Notes.DTOs.Service.Account.Edit;
using Notes.DTOs.Service.Account.Login;
using Notes.DTOs.Service.Account.Register;

namespace Notes.Interfaces
{
    public interface IAccountService
    {
        User? User { get; }
        Task<LoginResponse> LoginAccountAsync(LoginRequest request);
        Task<RegisterResponse> RegisterAccountAsync(RegisterRequest request);
        Task<EditResponse> EditAccountAsync(EditRequest request);
    }
}
