using AutoMapper;
using Core.Constants;
using Core.DTOS;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Task_Management_System_API.Helpers;
using Task_Management_System_API.Services.Interfaces;

namespace Task_Management_System_API.Services.Implementations
{
    public class AuthService:IAuthService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IMapper mapper;
        private readonly IOptionsMonitor<JWT> JWTConfigs;

        public AuthService(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IMapper mapper,
            IOptionsMonitor<JWT> JWTConfigs)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.JWTConfigs = JWTConfigs;
        }


        public async Task<AuthResultDTO> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {

            if (await userManager.FindByEmailAsync(userRegisterDTO.Email) is not null)
                return new AuthResultDTO()
                {
                    Success = false,
                    Messages = new List<string> { "Email is already Registered!" }
                };
            if (await userManager.FindByNameAsync(userRegisterDTO.UserName) is not null)
                return new AuthResultDTO()
                {
                    Success = false,
                    Messages = new List<string> { "User Name is already Registered!" }
                };

            // create user
           var user= mapper.Map<AppUser>(userRegisterDTO);
           var result=await userManager.CreateAsync(user,userRegisterDTO.Password);
            if (!result.Succeeded)
                return new AuthResultDTO()
                {
                    Success = false,
                    Messages = result.Errors.Select(e => e.Description).ToList()
                };

              // add role to user
              await userManager.AddToRoleAsync(user,Roles.UserRole);


            // generate token
            var token = await GenerateJwtTokenAsync(user);
            return new AuthResultDTO()
            {
                Success = true,
                ExpiresOn = token.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

        }
        public async Task<AuthResultDTO> LoginAsync(UserLoginDTO UserDTO)
        {
            var user = await userManager.FindByEmailAsync(UserDTO.Email);
            if (user == null)
                return new AuthResultDTO
                {
                    Success = false,
                    Messages = new List<string> { "Email or Password is incorrect" }
                };

            var result = await userManager.CheckPasswordAsync(user, UserDTO.Password);
            if (!result)
                return new AuthResultDTO
                {
                    Success = false,
                    Messages = new List<string> { "Email or Password is incorrect" }
                };

            var token = await GenerateJwtTokenAsync(user);
            return new AuthResultDTO()
            {
                Success = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo
            };


        }
        private async Task<JwtSecurityToken> GenerateJwtTokenAsync(AppUser appUser)
        {

            var userClaims = await userManager.GetClaimsAsync(appUser);
            var roles = await userManager.GetRolesAsync(appUser);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim("uid", appUser.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTConfigs.CurrentValue.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: JWTConfigs.CurrentValue.Issuer,
                audience: JWTConfigs.CurrentValue.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(JWTConfigs.CurrentValue.ExpireAfterInMinute),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;


          
        }


    }

   
}
