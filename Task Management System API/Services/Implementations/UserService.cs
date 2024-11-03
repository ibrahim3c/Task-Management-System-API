using AutoMapper;
using Core.DTOS;
using Core.IRepositoreis.UOW;
using Core.Models;
using Core.Services.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyShop.Services.Interfaces;
using Task_Management_System_API.Services.Interfaces;

namespace Task_Management_System_API.Services.Implementations
{
    public class UserService:IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProjectService projectService;
        private readonly IFileService fileService;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IRolesService rolesService;

        public UserService(UserManager<AppUser> userManager,
            IMapper mapper,
            IUnitOfWork unitOfWork
            ,IProjectService projectService
            ,IFileService fileService,
            RoleManager<AppRole> roleManager,
            IRolesService rolesService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.projectService = projectService;
            this.fileService = fileService;
            this.roleManager = roleManager;
            this.rolesService = rolesService;
        }

        // get
        public async Task<ResultDTO<IEnumerable<GetUserDTO>>> GetAllUsersAsync()
        {
            var users =await userManager.Users.ToListAsync();
            if(!users.Any())
                return ResultDTO<IEnumerable<GetUserDTO>>.Failure(["No Users Found"]);
            var usersDTO = mapper.Map<IEnumerable<GetUserDTO>>(users);

            return ResultDTO<IEnumerable<GetUserDTO>>.SuccessFully(Data: usersDTO, messages: ["Users Found"]);
        }

        public async Task<ResultDTO<GetUserDTO>>GetUserByIdAsync(string userID)
        {
            var user=await userManager.FindByIdAsync(userID);
            if(user==null)
                   return ResultDTO<GetUserDTO>.Failure(["No User Found"]);
            var userDTO = mapper.Map<GetUserDTO>(user);

            return ResultDTO<GetUserDTO>.SuccessFully(Data: userDTO, messages: ["Users Found"]);
        }

        public async Task<ResultDTO<IEnumerable<GetRoleDTO>>> GetRolesOfUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return ResultDTO<IEnumerable<GetRoleDTO>>.Failure(["No User Found"]);

            var rolesName = await userManager.GetRolesAsync(user);
            if (!rolesName.Any())
                return ResultDTO<IEnumerable<GetRoleDTO>>.Failure(["No Roles Found"]);

            // if u want to get roles as object
            var roles = new List<AppRole>();
            foreach (var roleName in rolesName)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    roles.Add(role);
                }
            }

            var rolesDTO = mapper.Map<IEnumerable<GetRoleDTO>>(roles);


            return ResultDTO<IEnumerable<GetRoleDTO>>.SuccessFully(Data: rolesDTO, messages: ["Roles Found"]);
        }


       
        public async Task<ResultDTO<GetUserDTO>> GetUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return ResultDTO<GetUserDTO>.Failure(["No User Found"]);
            var userDTO = mapper.Map<GetUserDTO>(user);

            return ResultDTO<GetUserDTO>.SuccessFully(Data: userDTO, messages: ["Users Found"]);
        }

        public async Task<ResultDTO<IEnumerable<GetProjectDTO>>> GetAllUserProjectsAsync(string userId)
        {
            var userResult = await GetUserByIdAsync(userId);
            if (!userResult.Success)
                return ResultDTO<IEnumerable<GetProjectDTO>>.Failure(userResult.Messages);
            var userProjects = await unitOfWork.ProjectRepository.FindAllAsync(p => p.UserId == userId);
            if (!userProjects.Any())
                return ResultDTO<IEnumerable<GetProjectDTO>>.Failure(["No projects Found"]);

            var projects = mapper.Map<IEnumerable<GetProjectDTO>>(userProjects);
            return ResultDTO<IEnumerable<GetProjectDTO>>.SuccessFully(["Projects Found"], Data: projects);
        }
        public async Task<ResultDTO<IEnumerable<GetAttachmentDTO>>> GetAllAttachmentsOfUserAsync(string userId)
        {
            var userProjects = await unitOfWork.ProjectRepository.FindAllAsync(p => p.UserId == userId, ["Tasks.Attachments"]);
            var attachments=new List<GetAttachmentDTO>();
            foreach (var project in userProjects)
            {
                var attResult = await projectService.GetProjectAttachmentsAsync(project.ProjectId);
                if (attResult.Success)
                    attachments.AddRange(attResult.Data);
            }

            if (!attachments.Any())
                return ResultDTO<IEnumerable<GetAttachmentDTO>>.Failure(["No Attachments Found"]);

            return ResultDTO<IEnumerable<GetAttachmentDTO>>.SuccessFully(["Attachments Found"],Data: attachments);

        }

        // lock
        public async Task<ResultDTO<string>> LockUnLock(string id)
        {
            // Find the user by their Id
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return ResultDTO<string>.Failure(["No User Found"]);

            var message = "";
            // If user is not locked out, lock them for 1 year
            if (user.LockoutEnd == null || user.LockoutEnd < DateTime.UtcNow)
            {
                user.LockoutEnd = DateTime.UtcNow.AddYears(1);  // Lock the user
                message = "User Locked Ssuccessfully";
            }
            else
            {
                // Unlock the user by setting LockoutEnd to null
                user.LockoutEnd = null;
                message = "User UnLocked Ssuccessfully";

            }

            // Update the user in the database
            await userManager.UpdateAsync(user);

            return ResultDTO<string>.SuccessFully([message],null);
        }


        // add
        public async Task<ResultDTO<GetUserDTO>> AddUserAsync(UserRegisterDTO userDTO)
        {
            if (await userManager.FindByEmailAsync(userDTO.Email) is not null)
                return ResultDTO<GetUserDTO>.Failure(["Email is already Registered!"]);

            if (await userManager.FindByNameAsync(userDTO.UserName) is not null)
                return ResultDTO<GetUserDTO>.Failure(["User Name is already Registered!"]);

            // create user
            var user = mapper.Map<AppUser>(userDTO);
            var result = await userManager.CreateAsync(user, userDTO.Password);
            if (!result.Succeeded)
                return ResultDTO<GetUserDTO>.Failure(result.Errors.Select(e => e.Description).ToList());

            return ResultDTO<GetUserDTO>.SuccessFully(["User Added Successfully"], (await GetUserByEmailAsync(user.Email)).Data);

        }


        // update 
        public async Task<ResultDTO<GetUserDTO>> UpdateUserAsync(UpdateUserDTO userDTO)
        {
            var user=await userManager.FindByIdAsync(userDTO.UserId);
            if (user == null)
                return ResultDTO<GetUserDTO>.Failure(["No User Found"]);

            if(user.UserName!=userDTO.UserName)
                user.UserName=userDTO.UserName;

            if (user.Address != userDTO.Address)
                user.Address = userDTO.Address;

            if(user.PhoneNumber!= userDTO.PhoneNumber)
                user.PhoneNumber=userDTO.PhoneNumber;

            await userManager.UpdateAsync(user);

            return ResultDTO<GetUserDTO>.SuccessFully(["User Updated Successfully"], (await GetUserByEmailAsync(user.Email)).Data);


        }

        //TODO:ChangeEmail and ChangePassword

        // delete User
        public async Task<ResultDTO<GetUserDTO>> DeleteUserAsync(string userID)
        {

            var user = await userManager.FindByIdAsync(userID);

            if (user == null)
                return ResultDTO<GetUserDTO>.Failure(["No User Found"]);

            // delete all files that related to this user
            var userProjects = await unitOfWork.ProjectRepository.FindAllAsync(p => p.UserId == userID, ["Tasks.Attachments"]);
            var userAttachmentsFilePaths = userProjects
                                 .SelectMany(p => p.Tasks)                    // Flatten tasks within each project
                                 .SelectMany(t => t.Attachments)              // Flatten attachments within each task
                                 .Where(attachment => attachment.FilePath != null)  // Filter to ensure FilePath is not null
                                 .Select(attachment => attachment.FilePath)    // Select only the FilePath of each attachment
                                 .ToList();

            IdentityResult result= await userManager.DeleteAsync(user);


            if (!result.Succeeded)
            {
                return new ResultDTO<GetUserDTO>
                {
                    Success = false,
                    Messages = new List<string> { "Failed To Delete This User" }
                };

            }

            // delete all files that related to this user
            if(userAttachmentsFilePaths.Any())
            await fileService.DeleteAllFilesAsync(userAttachmentsFilePaths);

            return new ResultDTO<GetUserDTO>
            {
                Success = true,
                Messages = new List<string> { " User Deleted Successfully" },
                Data = (await GetUserByIdAsync(userID)).Data
            };
           
                
        }

        public async Task<ResultDTO<GetUserDTO>> DeleteUserByEmailAsync(string email)
        {
            var user=await GetUserByEmailAsync(email);
            if (user.Success)
            {
                return await DeleteUserAsync(user.Data.UserId);
            }
            return user;
              
        }


        // User Roles
        public async Task<ResultDTO<IEnumerable<string>>> GetRolesNameOfUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return ResultDTO<IEnumerable<string>>.Failure(["No User Found"]);

            var roles = await userManager.GetRolesAsync(user);
            if (!roles.Any())
                return ResultDTO<IEnumerable<string>>.Failure(["No Roles Found"]);

            return ResultDTO<IEnumerable<string>>.SuccessFully(Data: roles, messages: ["Roles Found"]);
        }


        public async Task<ResultDTO<ManageRolesDTO>> GetRolesForManagingAsync(string userId)
        {
            var user= await userManager.FindByIdAsync(userId);
            if (user == null)
                return ResultDTO<ManageRolesDTO>.Failure(["No User Found"]);

            var roles = await rolesService.GetAllRolesAsync();

            if (!roles.Success)
                return ResultDTO<ManageRolesDTO>.Failure(roles.Messages);

            var manageRoles = roles.Data.Select(r => new RolesDTO
            {
                RoleName = r.RoleName,
                IsSelected = userManager.IsInRoleAsync(user, r.RoleName).Result
            }).ToList();

            var UserRoles = new ManageRolesDTO
            {
                Roles = manageRoles,
                UserId = userId
            };
            return ResultDTO<ManageRolesDTO>.SuccessFully(["Roles Found "], UserRoles);

        }
        public async Task<ResultDTO<ManageRolesDTO>> ManageUserRolesAsync(ManageRolesDTO manageRolesDTO)
        {
            var user = await userManager.FindByIdAsync(manageRolesDTO.UserId);

            if (user == null)
                return ResultDTO<ManageRolesDTO>.Failure(["No User Found"]);

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in manageRolesDTO.Roles)
            {
                if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                    await userManager.RemoveFromRoleAsync(user, role.RoleName);

                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                    await userManager.AddToRoleAsync(user, role.RoleName);
            }

            return ResultDTO<ManageRolesDTO>.SuccessFully(["Roles of User Managed Successfully"],
                (await GetRolesForManagingAsync(manageRolesDTO.UserId)).Data);


        }






    }
}
