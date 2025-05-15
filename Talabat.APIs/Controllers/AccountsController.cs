using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> LogIn(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Tokin = await _tokenService.CreateTokinAsync(user, _userManager)
            });
        }


        [HttpPost("register")]

        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (CheckEmailExists(registerDTO.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new[] { " that Email is in use allready " } });

            var user = new AppUser()
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email.Split("@")[0],

            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Tokin = await _tokenService.CreateTokinAsync(user, _userManager)
            });
        }




        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Tokin = await _tokenService.CreateTokinAsync(user, _userManager)
            });
        }


        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AdressDTo>> UpdateUserAdress(AdressDTo adressDTo)
        {
            var UpdatedAdress = _mapper.Map<AdressDTo, Address>(adressDTo);

            var user = await _userManager.FindUserWithAddressByEmailAsync(User);

            user.Address = UpdatedAdress;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400, "An error occured during updatind Address at that user"));

            var ShowingAddress = _mapper.Map<Address, AdressDTo>(user.Address);

            return Ok(ShowingAddress);
        }


        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AdressDTo>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);

            var result = _mapper.Map<Address, AdressDTo>(user.Address);

            return Ok(result);
        }


        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

    }
}
