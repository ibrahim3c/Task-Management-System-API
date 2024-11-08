using Core.DTOS;

namespace Task_Management_System_API.Services.Interfaces
{
    public interface IAuthService
    {
        // JWT
        Task<AuthResultDTO> RegisterAsync(UserRegisterDTO userRegisterDTO);
        Task<AuthResultDTO> LoginAsync(UserLoginDTO UserDTO);

        //JWT RefreshToken
        Task<AuthResultDTOForRefresh> RegisterWithRefreshTokenAsync(UserRegisterDTO userRegisterDTO);
        Task<AuthResultDTOForRefresh> LoginWithRefreshTokenAsync(UserLoginDTO UserDTO);
        Task<AuthResultDTOForRefresh> RefreshTokenAsync(string refreshToken);
        Task<bool> RevokeTokenAsync(string refreshToken);

        // additional
        Task<ResultDTO<string>> RegisterWithEmailVerification(UserRegisterDTO userRegisterDTO, string scheme, string host);
        Task<ResultDTO<string>> VerifyEmailAsync(string userId, string code);
        Task<AuthResultDTOForRefresh> LoginWithEmailVerificationAsync(UserLoginDTO UserDTO);
        Task<ResultDTO<string>> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO, string scheme, string host);
        Task<ResultDTO<string>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    }
}
