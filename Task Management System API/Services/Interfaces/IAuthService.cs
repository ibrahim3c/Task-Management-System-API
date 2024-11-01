using Core.DTOS;

namespace Task_Management_System_API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDTO> RegisterAsync(UserRegisterDTO userRegisterDTO);
        Task<AuthResultDTO> LoginAsync(UserLoginDTO UserDTO);
    }
}
