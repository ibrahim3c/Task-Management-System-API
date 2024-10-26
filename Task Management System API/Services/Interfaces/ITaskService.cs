using Core.DTOS;
using static Core.Constants.GeneralConsts;

namespace Core.Services.Interfaces
{
    public interface ITaskService
    {
        Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetAllTasksAsync();
        Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetProjectTasksAsync(int projectID);
        Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetAllTasksOfStatus(ProjectTaskStatus status);
        Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetAllTasksOfStatus(string status);
        Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetProjectTasksOfStatusAsync(int projectID, ProjectTaskStatus taskStatus);
        Task<ResultDTO<IEnumerable<GetAttachmentDTO>>> GetAllAttachmentsOfTask(int taskID);
    }
}
