using Notes.Domain.Models;
using Notes.DTOs.Service.Account.Edit;
using Notes.DTOs.Service.Account.Info;
using Notes.DTOs.Service.Account.Login;
using Notes.DTOs.Service.Account.Register;

namespace Notes.Interfaces
{
    public interface IAccountService:IBaseService
    {
        Task<InfoResponse> GetInfoAccountAsync(InfoRequest request);
        Task<LoginResponse> LoginAccountAsync(LoginRequest request);
        Task<RegisterResponse> RegisterAccountAsync(RegisterRequest request);
        Task<EditResponse> EditAccountAsync(EditRequest request);
    }
}
