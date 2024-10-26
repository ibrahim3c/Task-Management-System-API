using AutoMapper;
using Core.DTOS;
using Core.IRepositoreis.UOW;
using Core.Services.Interfaces;
using MyShop.Services.Interfaces;
using static Core.Constants.GeneralConsts;

namespace Core.Services.Implementations
{
    public class TaskService:ITaskService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IFileService fileService;

        public TaskService(IUnitOfWork unitOfWork,IMapper mapper,IFileService fileService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.fileService = fileService;
        }

       
        // get
        public async Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetAllTasksAsync()
        {
            //List<GetTaskDTO> TasksDTO = mapper.Map<List<GetTaskDTO>>((await unitOfWork.TaskRepository.GetAllAsync()));
            var TasksDTO = (await unitOfWork.TaskRepository.GetAllAsync(new string[] {"Project"})).Select(t=>new GetTaskDTO
            {
                ProjectTaskId = t.ProjectTaskId,
                CreatedDate = t.CreatedDate, 
                Description = t.Description,
                ProjectName=t.Project.Name,
                Status = Enum.GetName<ProjectTaskStatus>(t.Status),
                Title=t.Title
            }).ToList();

            // Check if the list is empty
            if (!TasksDTO.Any())
            {
                return new ResultDTO<IEnumerable<GetTaskDTO>>
                {
                    Success = false,
                    Messages = new List<string> { "No Tasks found" }
                };
            }

            return new ResultDTO<IEnumerable<GetTaskDTO>>
            {
                Data = TasksDTO,
                Success = true,
            };
            
        }

        public async Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetProjectTasksAsync(int projectID)
        {
            var TasksDTO = (await unitOfWork.TaskRepository.FindAllAsync(t=>t.ProjectId==projectID,new string[] { "Project" })).Select(t => new GetTaskDTO
            {
                ProjectTaskId = t.ProjectTaskId,
                CreatedDate = t.CreatedDate,
                Description = t.Description,
                ProjectName = t.Project.Name,
                Status = Enum.GetName<ProjectTaskStatus>(t.Status),
                Title = t.Title
            }).ToList();

            // Check if the list is empty
            if (!TasksDTO.Any())
            {
                return new ResultDTO<IEnumerable<GetTaskDTO>>
                {
                    Success = false,
                    Messages = new List<string> { "No Tasks found" }
                };
            }

            return new ResultDTO<IEnumerable<GetTaskDTO>>
            {
                Data = TasksDTO,
                Success = true,
            };

        }

        public async Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetAllTasksOfStatus(ProjectTaskStatus status)
        {
            var TasksDTO = (await unitOfWork.TaskRepository.FindAllAsync(t => t.Status==status, new string[] { "Project" })).Select(t => new GetTaskDTO
            {
                ProjectTaskId = t.ProjectTaskId,
                CreatedDate = t.CreatedDate,
                Description = t.Description,
                ProjectName = t.Project.Name,
                Status = Enum.GetName<ProjectTaskStatus>(t.Status),
                Title = t.Title
            }).ToList();

            // Check if the list is empty
            if (!TasksDTO.Any())
            {
                return new ResultDTO<IEnumerable<GetTaskDTO>>
                {
                    Success = false,
                    Messages = new List<string> { "No Tasks found" }
                };
            }

            return new ResultDTO<IEnumerable<GetTaskDTO>>
            {
                Data = TasksDTO,
                Success = true,
            };

        }


        public async Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetAllTasksOfStatus(string status)
        {
            // if status is not in TaskStatus
            if (!Enum.TryParse<ProjectTaskStatus>(status, out var taskStatus))
                return new ResultDTO<IEnumerable<GetTaskDTO>>
                {
                    Messages = new List<string> { $"InValid Status. Valid Satus are {string.Join(" , ", Enum.GetNames(typeof(ProjectTaskStatus)))}" },
                    Success = false,
                };

            var TasksDTO = (await unitOfWork.TaskRepository.FindAllAsync(t => t.Status == taskStatus, new string[] { "Project" })).Select(t => new GetTaskDTO
            {
                ProjectTaskId = t.ProjectTaskId,
                CreatedDate = t.CreatedDate,
                Description = t.Description,
                ProjectName = t.Project.Name,
                Status = Enum.GetName<ProjectTaskStatus>(t.Status),
                Title = t.Title
            }).ToList();

            // Check if the list is empty
            if (!TasksDTO.Any())
            {
                return new ResultDTO<IEnumerable<GetTaskDTO>>
                {
                    Success = false,
                    Messages = new List<string> { "No Tasks found" }
                };
            }

            return new ResultDTO<IEnumerable<GetTaskDTO>>
            {
                Data = TasksDTO,
                Success = true,
            };

        }

        public async Task<ResultDTO<IEnumerable<GetTaskDTO>>> GetProjectTasksOfStatusAsync(int projectID,ProjectTaskStatus taskStatus)
        {
            var TasksDTO = (await unitOfWork.TaskRepository.FindAllAsync(t => t.ProjectId == projectID && t.Status==taskStatus, new string[] { "Project" })).Select(t => new GetTaskDTO
            {
                ProjectTaskId = t.ProjectTaskId,
                CreatedDate = t.CreatedDate,
                Description = t.Description,
                ProjectName = t.Project.Name,
                Status = Enum.GetName<ProjectTaskStatus>(t.Status),
                Title = t.Title
            }).ToList();

            // Check if the list is empty
            if (!TasksDTO.Any())
            {
                return new ResultDTO<IEnumerable<GetTaskDTO>>
                {
                    Success = false,
                    Messages = new List<string> { "No Tasks found" }
                };
            }

            return new ResultDTO<IEnumerable<GetTaskDTO>>
            {
                Data = TasksDTO,
                Success = true,
            };

        }

        //Don't forget to check it
        public async Task<ResultDTO<IEnumerable<GetAttachmentDTO>>> GetAllAttachmentsOfTask(int taskID)
        {
            var task = await unitOfWork.TaskRepository.FindAsync(t => t.ProjectTaskId == taskID, new string[] { "Attachments" });
            if (task == null)
                return new ResultDTO<IEnumerable<GetAttachmentDTO>>
                {
                    Success = false,
                    Messages = new List<string> { "No Task Found" }

                };

            if (!task.Attachments.Any())
            {
                return new ResultDTO<IEnumerable<GetAttachmentDTO>>
                {
                    Success = false,
                    Messages = new List<string> { "No Attachment for this Task" }
                };
            }
            var Attachments = task.Attachments.Select(a => new GetAttachmentDTO
            {
                AttachmentId = a.AttachmentId,
                File = fileService.GetFileAsIFormFileAsync(a.FilePath).Result,
                FileName = a.FileName,
                TaskName = a.Task.Title
            });

            return new ResultDTO<IEnumerable<GetAttachmentDTO>>
            {
                Data = Attachments,
                Success = true
            };
        }


        //



    }
}
