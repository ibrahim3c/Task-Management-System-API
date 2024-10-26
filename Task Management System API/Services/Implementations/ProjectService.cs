using AutoMapper;
using Core.DTOS;
using Core.IRepositoreis.UOW;
using Core.Services.Interfaces;
using Entities.Models;

namespace Core.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProjectService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ResultDTO<GetProjectDTO>> AddProjectAsync(AddUpdateProjectDTO projectDTO)
        {
            var project=mapper.Map<Project>(projectDTO);
            await unitOfWork.ProjectRepository.AddAsync(project);

            var result = unitOfWork.Complete();
            if (result < 1)
            {
                return new ResultDTO<GetProjectDTO>
                {
                    Success = false,
                    Messages = new List<string> { "Failed To Add This Project" }
                };

            }


            return new ResultDTO<GetProjectDTO>
            {
                Success = true,
                Messages = new List<string> { " Project Added Successfully" },
                Data = await GetProjectByIdAsync(project.ProjectId)
            };



        }

        public async Task<ResultDTO<GetProjectDTO>> DeleteProjectAsync(int projectID)
        {
           var existingProject= await unitOfWork.ProjectRepository.GetByIdAsync(projectID);
            if (existingProject is null)
                return new ResultDTO<GetProjectDTO>
                {
                    Success = false,
                    Messages = new List<string> { "Project Not Found" }
                };



            unitOfWork.ProjectRepository.Delete(existingProject);


            var result=unitOfWork.Complete();
            if (result < 1)
            {
                return new ResultDTO<GetProjectDTO>
                {
                    Success = false,
                    Messages = new List<string> { "Failed To Delete This Project" }
                };

            }

            return new ResultDTO<GetProjectDTO>
            {
                Success = true,
                Messages = new List<string> { " Project Deleted Successfully" },
                Data = await GetProjectByIdAsync(projectID)
            };
           
        }


        public async Task<IEnumerable<GetProjectDTO>> GetAllProjectAsync()
        {
            return mapper.Map<IEnumerable<GetProjectDTO>>(await unitOfWork.ProjectRepository.GetAllAsync()) ;
        }

        public async Task<GetProjectDTO>GetProjectByIdAsync(int projectId)
        {
            return mapper.Map<GetProjectDTO>(await unitOfWork.ProjectRepository
                .FindAsync(p => p.ProjectId == projectId, new string[] { "Tasks" })); // Use the correct navigation property name here
        }


        public async Task<ResultDTO<GetProjectDTO>> UpdateProjectAsync(int id,AddUpdateProjectDTO projectDTO)
        {
            var existingProject=await unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (existingProject is null)
                return new ResultDTO<GetProjectDTO>
                {
                    Success = false,
                    Messages = new List<string> { " No Project Found" }
                };


            existingProject.Name = projectDTO.Name;
            existingProject.Description = projectDTO.Description;
            existingProject.CreatedDate = projectDTO.CreatedDate;

            unitOfWork.ProjectRepository.Update(existingProject);
            var result = unitOfWork.Complete();
            if (result < 1)
            {
                return new ResultDTO<GetProjectDTO>
                {
                    Success = false,
                    Messages = new List<string> { "Failed to Update this Project" }
                };
            }
            return new ResultDTO<GetProjectDTO>
            {
                Success = true,
                Messages = new List<string> { "Project Updated Successfully" },
                Data=await GetProjectByIdAsync(id)

            };

            
        }

       
    }
}
