using AutoMapper;
using Core.Constants;
using Core.DTOS;
using DAL.Repositories.UOW;
using Entities.Models;
using MyShop.Services.Implementations;
using static Core.Constants.GeneralConsts;

namespace Core.Services.Interfaces
{
    public interface ITaskService
    {

        // get
        Task<ResultDTO<GetTaskDTO>> GetTaskByIdAsync(int TaskId);
        Task<ResultDTO<GetAttachmentDTO>> GetAttachmentAsync(int attachmentID);
        Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetAllTasksAsync();
        Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetProjectTasksAsync(int projectID);
        Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetAllTasksOfStatus(ProjectTaskStatus status);
        Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetAllTasksOfStatus(string status);
        Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetProjectTasksOfStatusAsync(int projectID, ProjectTaskStatus taskStatus);
        Task<ResultDTO<IEnumerable<GetAttachmentDTO>>> GetAllAttachmentsOfTaskAsync(int taskID);

        // add

        Task<ResultDTO<GetAttachmentDTO>> AddAttachmentToTaskAsync(AddUpdateTaskAttachmentDTO AttachmentDTO);
        Task<ResultDTO<GetTaskDTO>> AddTaskWithoutAttachmentAsync(AddUpdateTaskDTOWithNoAttachment TaskDTO);
        Task<ResultDTO<GetTaskDTO>> AddTaskAsync(AddUpdateTaskDTO TaskDTO);


        // Delete
        Task<ResultDTO<GetAttachmentDTO>> DeleteAttachmentAsync(int AttachmentID);
        Task<ResultDTO<string>> DeleteAttachmentsOfTask(int taskID);
        Task<ResultDTO<GetTaskDTO>> DeleteTaskAsync(int taskID);
        Task<ResultDTO<string>> DeleteProjectTasksAsync(int projectID);


        //update

        Task<ResultDTO<GetAttachmentDTO>> UpdateAttachmentToTaskAsync(AddUpdateTaskAttachmentDTO AttachmentDTO);
        Task<ResultDTO<GetTaskDTO>> UpdateTaskAsync(AddUpdateTaskDTO taskDTO);
       
        }

    
}
