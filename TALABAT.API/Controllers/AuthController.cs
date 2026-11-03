using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Services;
using TALABAT.API.DTO_s;
using TALABAT.API.Errors;
using TALABAT.API.Extensions;

namespace TALABAT.API.Controllers
{

    public class AuthController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly SignInManager<AppUser> _signInManger;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(UserManager<AppUser> userManger,
            SignInManager<AppUser> signInManger,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManger = userManger;
            _signInManger = signInManger;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            if (CheckEmailExists(dto.Email).Result.Value)
                return BadRequest(new ApiResponse(400, "This Email Is Already Exist"));
            var user = new AppUser()
            {
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                UserName = dto.Email.Split('@')[0],
                PhoneNumber = dto.PhoneNumber
            };

            var Result = await _userManger.CreateAsync(user, dto.Password);

            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user, _userManger)
            };
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var user = await _userManger.FindByEmailAsync(dto.Email);

            if (user is null) return Unauthorized(new ApiResponse(401));


            var Result = await _signInManger.CheckPasswordSignInAsync(user, dto.Password, false);

            if (!Result.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user, _userManger)
            });
        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrenntUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManger.FindByEmailAsync(Email);

            var ReturnedUser = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user, _userManger),
            };
            return Ok(ReturnedUser);
        }
        [Authorize]
        [HttpGet("GetAddress")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {

            var user = await _userManger.FindUserWithAddressAsync(User);

            var MappedAddress = _mapper.Map<Address, AddressDto>(user.Address);

            return Ok(MappedAddress);
        }

        [Authorize]
        [HttpPut("UpdateAddres")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto UpdatedAddress)
        {
            var user = await _userManger.FindUserWithAddressAsync(User);

            if (user is null) return Unauthorized(new ApiResponse(401));

            var address = _mapper.Map<AddressDto, Address>(UpdatedAddress);

            address.Id = user.Address.Id;

            user.Address = address;

            var Result = await _userManger.UpdateAsync(user);

            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(UpdatedAddress);

        }

        [HttpGet("IsEmailExisted")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email) 
        {
            return await _userManger.FindByEmailAsync(email) is not null;
        }


    }


}
