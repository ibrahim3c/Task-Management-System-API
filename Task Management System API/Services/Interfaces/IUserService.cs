using Core.DTOS;

namespace Task_Management_System_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResultDTO<IEnumerable<GetUserDTO>>> GetAllUsersAsync();
        Task<ResultDTO<GetUserDTO>> GetUserByIdAsync(string userID);
        Task<ResultDTO<GetUserDTO>> GetUserByEmailAsync(string email);
        Task<ResultDTO<IEnumerable<GetRoleDTO>>> GetRolesOfUserAsync(string userId);
        Task<ResultDTO<IEnumerable<string>>> GetRolesNameOfUserAsync(string userId);
        Task<ResultDTO<IEnumerable<GetProjectDTO>>> GetAllUserProjectsAsync(string userId);
        Task<ResultDTO<IEnumerable<GetAttachmentDTO>>> GetAllAttachmentsOfUserAsync(string userId);
        Task<ResultDTO<string>> LockUnLock(string id);
        Task<ResultDTO<GetUserDTO>> AddUserAsync(UserRegisterDTO userDTO);
        Task<ResultDTO<GetUserDTO>> UpdateUserAsync(UpdateUserDTO userDTO);
        Task<ResultDTO<GetUserDTO>> DeleteUserAsync(string userID);
        Task<ResultDTO<GetUserDTO>> DeleteUserByEmailAsync(string email);
        Task<ResultDTO<ManageRolesDTO>> GetRolesForManagingAsync(string userId);
        Task<ResultDTO<ManageRolesDTO>> ManageUserRolesAsync(ManageRolesDTO manageRolesDTO);

    }
}
