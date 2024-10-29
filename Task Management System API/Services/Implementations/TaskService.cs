using AutoMapper;
using Core.Constants;
using Core.DTOS;
using Core.IRepositoreis.UOW;
using Core.Services.Interfaces;
using Entities.Models;
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
        //TODO:GetAllAttachmentOfProject
        public async Task<ResultDTO<GetTaskDTO>> GetTaskByIdAsync(int TaskId)
        {
            var task = await unitOfWork.TaskRepository.FindAsync(t=>t.ProjectTaskId== TaskId,new string[] { "Project" });
            if (task is null)
                return ResultDTO<GetTaskDTO>.Failure(new List<string> { "No Task Found" });
            var TaskDTO=mapper.Map<GetTaskDTO>(task);
            TaskDTO.ProjectName = task.Project.Name;
            return ResultDTO<GetTaskDTO>.SuccessFully(new List<string> { "Task Found " }, TaskDTO);
        }
        public async Task<ResultDTO<GetAttachmentDTO>> GetAttachmentAsync(int attachmentID)
        {
            var att = await unitOfWork.TaskAttachmentRepository.FindAsync(t => t.AttachmentId == attachmentID, new string[] { "Task" });
            if (att is null)
                return ResultDTO<GetAttachmentDTO>.Failure(new List<string> { "No Attachment Found" });
            var attachmentDTO = mapper.Map<GetAttachmentDTO>(att);
            attachmentDTO.TaskName = att.Task.Title;
            attachmentDTO.File=await fileService.GetFileAsIFormFileAsync(att.FileName);
            return ResultDTO<GetAttachmentDTO>.SuccessFully(new List<string> { "Task Attachment " }, attachmentDTO);
        }
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

        public async Task<ResultDTO<IEnumerable<GetAttachmentDTO>>> GetAllAttachmentsOfTaskAsync(int taskID)
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
                    Messages = new List<string> { "No Attachments for this Task" }
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


        //Add
        //TODO: Add MultipleAttachmentToTask ,AddMultipleTasksToProject
        public async Task<ResultDTO<GetAttachmentDTO>> AddAttachmentToTaskAsync(AddUpdateTaskAttachmentDTO AttachmentDTO)
        {
            var task = await unitOfWork.TaskRepository.GetByIdAsync(AttachmentDTO.ProjectTaskId);
            if (task == null)
                return ResultDTO<GetAttachmentDTO>.Failure(new List<string> { "Task Not Found" });



            var fileExtension = Path.GetExtension(AttachmentDTO.File.FileName).ToLowerInvariant();
            var FileSize = AttachmentDTO.File.Length;

            if (!FileSettings.AllowedExtensions.Contains(fileExtension))  
                return ResultDTO<GetAttachmentDTO>.Failure (new List<string> { "This Extenstion Not Allowed" });

            if (FileSettings.MaxFileSizeInBytes < FileSize)
                return ResultDTO<GetAttachmentDTO>.Failure(new List<string> { $"The Max size of file must be {FileSettings.MaxFileSizeInMB}" });

            var fileSrc = await fileService.UploadFileAsync(AttachmentDTO.File, FileSettings.ImagePath);
            if (string.IsNullOrEmpty(fileSrc))
                return ResultDTO<GetAttachmentDTO>.Failure(new List<string> { "Error in Uploading File" });


            var attachment=mapper.Map<TaskAttachment>(AttachmentDTO);
            attachment.FilePath = fileSrc;

            unitOfWork.TaskAttachmentRepository.Add(attachment);
           var result= unitOfWork.Complete();

            if (result < 1)
            {

               await fileService.DeleteFileAsync(fileSrc);
               return ResultDTO<GetAttachmentDTO>.Failure(new List<string> { "Failed to Add Attachment" });

            }

            var Data = new GetAttachmentDTO
            {
                AttachmentId = AttachmentDTO.AttachmentId,
                FileName = AttachmentDTO.FileName,
                File = AttachmentDTO.File,
                TaskName = task.Title
            };
            return ResultDTO<GetAttachmentDTO>.SuccessFully(new List<string> { "Attachment Added successfully" },Data);
         


        }
        public async Task<ResultDTO<GetTaskDTO>> AddTaskWithoutAttachmentAsync(AddUpdateTaskDTOWithNoAttachment TaskDTO)
        {

            var task = mapper.Map<ProjectTask>(TaskDTO);
          
            unitOfWork.TaskRepository.Add(task);
            var result = unitOfWork.Complete();


            if (result < 1)
            {
                return ResultDTO<GetTaskDTO>.Failure(new List<string> { "Failed to Add Task" });

            }

            return ResultDTO<GetTaskDTO>.SuccessFully(new List<string> { "Add Task Successfully" }, (await GetTaskByIdAsync(task.ProjectTaskId)).Data);


        }
        public async Task<ResultDTO<GetTaskDTO>> AddTaskAsync(AddUpdateTaskDTO TaskDTO)
        {
            // Create a transaction scope
            using var transaction = await unitOfWork.ProjectRepository.BeginTransactionAsync(); // Assuming your UnitOfWork has a method for transaction management
            try
            {

                var task = mapper.Map<ProjectTask>(TaskDTO);
                var attachmentDTO = new AddUpdateTaskAttachmentDTO
                {
                    File = TaskDTO.File,
                    FileName = TaskDTO.FileName,
                    ProjectTaskId = task.ProjectTaskId,
                };

                unitOfWork.TaskRepository.Add(task);
                var result = unitOfWork.Complete();


                if (result < 1)
                {
                    return ResultDTO<GetTaskDTO>.Failure(new List<string> { "Failed to Add Task" });

                }
                // add taskId before save attachment
                attachmentDTO.ProjectTaskId = task.ProjectTaskId;
                var attachmentResult = await AddAttachmentToTaskAsync(attachmentDTO);

                if (!attachmentResult.Success)
                {
                    await transaction.RollbackAsync();
                    return ResultDTO<GetTaskDTO>.Failure(attachmentResult.Messages);
                }
                // Commit transaction if both operations are successful
                await transaction.CommitAsync();
                return ResultDTO<GetTaskDTO>.SuccessFully(new List<string> {"Add Task Successfully" }, (await GetTaskByIdAsync(task.ProjectTaskId)).Data);


            }
            catch (Exception ex)
            {
                // Rollback transaction on exception
                await transaction.RollbackAsync();
                return ResultDTO<GetTaskDTO>.Failure(new List<string> { "An error occurred: " + ex.Message });
            }


        }


        //Delete
        public async Task<ResultDTO<GetAttachmentDTO>> DeleteAttachmentAsync(int AttachmentID)
        {
            var attachment = await unitOfWork.TaskAttachmentRepository.GetByIdAsync(AttachmentID);
            if (attachment == null)
                return ResultDTO<GetAttachmentDTO>.Failure(new List<string> { "No Attachmentfound" });

            unitOfWork.TaskAttachmentRepository.Delete(attachment);
            var result = unitOfWork.Complete();
            if (result < 1)
                return ResultDTO<GetAttachmentDTO>.Failure(new List<string> { "Failed to Delete Attachment" });

            await fileService.DeleteFileAsync(attachment.FilePath);

            return ResultDTO<GetAttachmentDTO>.SuccessFully(new List<string> { "Attachment Deleted Successfully" },(await GetAttachmentAsync(attachment.AttachmentId)).Data);
        }

        public async Task<ResultDTO<string>> DeleteAttachmentsOfTask(int taskID)
        {
            var task = await unitOfWork.TaskRepository.FindAsync(t=>t.ProjectTaskId== taskID, ["Attachments"]);
            var errorMsg = new List<string>();
            if (task == null)
                return ResultDTO<string>.Failure([ "Task Not Found" ]);

            unitOfWork.TaskAttachmentRepository.DeleteRange(task.Attachments);
            var result= unitOfWork.Complete();
            if (result < 1)
                return ResultDTO<string>.Failure(["Faild To Delete Attachments"]);

            foreach(var attachment in task.Attachments)
            {
               var AttResult= await DeleteAttachmentAsync(attachment.AttachmentId);
                if (!AttResult.Success)
                {
                    errorMsg.Add($"Failed To delete Attachment that have id ={attachment.AttachmentId}");
                }
            }

            if (errorMsg.Any())
                return ResultDTO<string>.Failure(errorMsg);

            return ResultDTO<string>.SuccessFully(["Attachments of this Task Deleted Successfully"],null);


        }
       
        public async Task<ResultDTO<GetTaskDTO>>DeleteTaskAsync(int taskID)
        {
            var task = await unitOfWork.TaskRepository.FindAsync(t => t.ProjectTaskId == taskID, ["Attachments"]);
            List<string> taskFileSrcs = task.Attachments
                                .Where(a=>a.FilePath is not null)
                                .Select(attachment => attachment.FilePath)
                                .ToList();

            if (task == null)
                return ResultDTO<GetTaskDTO>.Failure(["Task Not Found"]);

            unitOfWork.TaskRepository.Delete(task);
            var result = unitOfWork.Complete();
            if (result < 1)
                return ResultDTO<GetTaskDTO>.Failure(["Faild To Delete Task"]);
          
            // delete all Files that related to Task
            await fileService.DeleteAllFilesAsync(taskFileSrcs);

            return ResultDTO<GetTaskDTO>.SuccessFully(["Task Deleted Successfully"],(await GetTaskByIdAsync(taskID)).Data);

        }

        public async Task<ResultDTO<string>> DeleteProjectTasksAsync(int projectID)
        {
            var project = await unitOfWork.ProjectRepository.FindAsync(p => p.ProjectId == projectID, ["Tasks"]);
            var errorMsg = new List<string>();
            if (project is null)
                return new ResultDTO<string>
                {
                    Success = false,
                    Messages = new List<string> { "Project Not Found" }
                };

            foreach(var task in project.Tasks)
            {
              var AttResult=  await DeleteTaskAsync(task.ProjectTaskId);
                if (!AttResult.Success)
                {
                    errorMsg.Add($"Failed To delete task that have id ={task.ProjectTaskId}");
                }
            }
            if (errorMsg.Any())
                return ResultDTO<string>.Failure(errorMsg);

            return ResultDTO<string>.SuccessFully(["Tasks of this Project Deleted Successfully"], null);


        }


        // Update 
        public async Task<ResultDTO<GetAttachmentDTO>> UpdateAttachmentToTaskAsync(AddUpdateTaskAttachmentDTO AttachmentDTO)
        {
            // get attachment from db
            var attachment =await  unitOfWork.TaskAttachmentRepository.GetByIdAsync(AttachmentDTO.AttachmentId);
            // ensure data is correct
            var task = await unitOfWork.TaskRepository.GetByIdAsync(AttachmentDTO.ProjectTaskId);
            if (task == null)
                return ResultDTO<GetAttachmentDTO>.Failure(new List<string> { "Task Not Found" });
            if (attachment == null)
                return ResultDTO<GetAttachmentDTO>.Failure(new List<string> { "Attachment Not Found" });



            var fileExtension = Path.GetExtension(AttachmentDTO.File.FileName).ToLowerInvariant();
            var FileSize = AttachmentDTO.File.Length;

            if (!FileSettings.AllowedExtensions.Contains(fileExtension))
                return ResultDTO<GetAttachmentDTO>.Failure(new List<string> { "This Extenstion Not Allowed" });

            if (FileSettings.MaxFileSizeInBytes < FileSize)
                return ResultDTO<GetAttachmentDTO>.Failure(new List<string> { $"The Max size of file must be {FileSettings.MaxFileSizeInMB}" });

            // update this attachment
            var oldPath = attachment.FilePath;
            mapper.Map(AttachmentDTO, attachment); // This updates the existing task instance

            if (AttachmentDTO.FilePath == oldPath)
            {
                attachment.FilePath= oldPath;
              
            }
            else
            {
                var fileSrc = await fileService.UploadFileAsync(AttachmentDTO.File, FileSettings.ImagePath);
                if (string.IsNullOrEmpty(fileSrc))
                    return ResultDTO<GetAttachmentDTO>.Failure(new List<string> { "Error in Uploading File" });

                attachment.FilePath= fileSrc;

            }
            
         
            unitOfWork.TaskAttachmentRepository.Update(attachment);

            var result = unitOfWork.Complete();
            if (result < 1)
            {
                await fileService.DeleteFileAsync(attachment.FilePath);
                return ResultDTO<GetAttachmentDTO>.Failure(new List<string> { "Failed to update Attachment" });
            }

            await fileService.DeleteFileAsync(oldPath);
            return ResultDTO<GetAttachmentDTO>.SuccessFully(["Attachment Updated Successfully"], Data: (await GetAttachmentAsync(attachment.AttachmentId)).Data);


            
        }
        public async Task<ResultDTO<GetTaskDTO>> UpdateTaskAsync(AddUpdateTaskDTO taskDTO)
        {
            var task =await unitOfWork.TaskRepository.FindAsync(t => t.ProjectTaskId == taskDTO.ProjectTaskId, ["Attachments"]);
            if (task is null)
                return ResultDTO<GetTaskDTO>.Failure(["Task Not Found"]);

            //update data 
            mapper.Map(taskDTO, task); // This updates the existing task instance

            var attachmentDTO = new AddUpdateTaskAttachmentDTO
            {
                AttachmentId = taskDTO.AttachmentID ?? 0,
                File = taskDTO.File,
                FileName = taskDTO.FileName,
                ProjectTaskId = taskDTO.ProjectTaskId
            };

            unitOfWork.TaskRepository.Update(task);
            var result = unitOfWork.Complete();
            if (result < 1)
            {
                return ResultDTO<GetTaskDTO>.Failure(new List<string> { "Failed to update Task" });
            }

            var attResult= await UpdateAttachmentToTaskAsync(attachmentDTO);
            if (attResult.Success)
            {

                return ResultDTO<GetTaskDTO>.SuccessFully(["Task Updated Successfully"], (await GetTaskByIdAsync(task.ProjectTaskId)).Data);
            }

            else
            {
                // Return partial success with combined messages if attachment update fails
                var errorMessages = new List<string> { "Task Updated but its attachment could not be updated: " }
                    .Concat(attResult.Messages).ToList();

                return ResultDTO<GetTaskDTO>.Failure(errorMessages);

            }
        }


    }
}
