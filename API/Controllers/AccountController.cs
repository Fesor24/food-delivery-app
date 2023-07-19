using System.Net;
using System.Runtime.CompilerServices;
using API.Dtos;
using API.Extensions;
using API.Helpers;
using API.Response;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly ITokenService tokenService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        [HttpPost(Routes.Login)]
        public async Task<ApiResponse> Login([FromBody] LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if(user is null)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                return new ApiResponse
                {
                    ErrorMessage = "Unauthorized"
                };
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                return new ApiResponse
                {
                    ErrorMessage = "Unauthorized"
                };
            }

            UserDto userDto = new UserDto
            {
                Email = loginDto.Email,
                Token = tokenService.CreateToken(user)
            };

            return new ApiResponse
            {
                Result = userDto
            };

        }

        [HttpPost(Routes.Register)]
        public async Task<ApiResponse> Register([FromBody] RegisterDto registerDto)
        {
            if(await userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return new ApiResponse
                {
                    ErrorMessage = "Email in use"
                };
            }

            ApplicationUser user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return new ApiResponse
                {
                    ErrorMessage = "Unauthorized",
                    ErrorResult = result.Errors.Select(x => x.Description).ToList()
                };
            }

            UserDto userDto = new UserDto
            {
                Email = registerDto.Email,
                Token = tokenService.CreateToken(user)
            };

            return new ApiResponse
            {
                Result = userDto
            };
        }

        [HttpGet(Routes.GetCurrentUser)]
        [Authorize]
        public async Task<ApiResponse> GetCurrentUser()
        {
            var user = await userManager.GetUserFromClaimsPrincipalAsync(User);

            if(user is null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;

                return new ApiResponse
                {
                    ErrorMessage = "User not found"
                };
            }

            UserDto userDto = new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            };

            return new ApiResponse { Result = userDto };
        }

        [HttpGet(Routes.EmailExists)]
        public async Task<ApiResponse> EmailExists(string email)
        {
            return new ApiResponse
            {
                Result = await userManager.FindByEmailAsync(email) != null
            };
        }

    }
}
