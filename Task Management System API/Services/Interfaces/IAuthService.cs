using Core.DTOS;

namespace Task_Management_System_API.Services.Interfaces
{
    public interface IAuthService
    {
        // JWT
        Task<AuthResultDTO> RegisterAsync(UserRegisterDTO userRegisterDTO);
        Task<AuthResultDTO> LoginAsync(UserLoginDTO UserDTO);

        //JWT RefreshToken
        Task<AuthResultDTOForRefresh> LoginWithRefreshTokenAsync(UserLoginDTO UserDTO);
    }
}
