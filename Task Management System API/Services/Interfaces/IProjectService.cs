using Core.DTOS;
using Entities.Models;

namespace Core.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<GetProjectDTO>> GetAllProjectAsync();
        Task<GetProjectDTO> GetProjectByIdAsync(int projectId);
        Task<ResultDTO<GetProjectDTO>> AddProjectAsync(AddUpdateProjectDTO projectDTO);
        Task<ResultDTO<GetProjectDTO>> UpdateProjectAsync(int id, AddUpdateProjectDTO projectDTO);
        Task<ResultDTO<GetProjectDTO>> DeleteProjectAsync(int projectID);
        Task<ResultDTO<IEnumerable<GetAttachmentDTO>>> GetProjectAttachmentsAsync(int projectID);
    }
}
