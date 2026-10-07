using Common.Aplication;
using Common.Aplication.SecurityUtil;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.JwtUtil;
using Shop.Api.ViewModels.Auth;
using Shop.Application.User.AddToken;
using Shop.Application.User.Register;
using Shop.Application.User.RemoveToken;
using Shop.Domain.UserAgg.Enums;
using Shop.Presentation.Facade.User;
using Shop.Query.User.DTOs;
using System;
using System.Threading.Tasks;
using UAParser;

namespace Shop.Api.Controllers
{
    [EnableRateLimiting("ApiPolicy")]
    public class AuthController : ApiController
    {
        private readonly IUserFacade _userFacade;
        private readonly JwtConfig _jwtConfig;

        public AuthController(IUserFacade userFacade, JwtConfig jwtConfig)
        {
            _userFacade = userFacade;
            _jwtConfig = jwtConfig;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<LoginResultDto>>> Register(
        [FromBody] RegisterViewModel register)
        {
            var registerCommand = new RegisterUserCommand(
                Email: register.Email,
                PhoneNumber: register.PhoneNumber,
                Password: register.Password,
                ConfirmPassword: register.ConfirmPassword,
                Gender: Gender.None
            );

            var registerResult = await _userFacade.Register(registerCommand);
            if (registerResult.Status != OperationResultStatus.Success)
                return BadRequest(registerResult.Message);

            var user = await _userFacade.GetUserByPhoneNumber(register.PhoneNumber);
            if (user == null)
                return BadRequest("User not found.");

            var loginResult = await AddTokenAndGenerateJwt(user);
            return Ok(loginResult);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<LoginResultDto>>> Login(
            [FromBody] LoginViewModel loginViewModel)
        {
            var user = await _userFacade.GetUserByPhoneNumber(loginViewModel.PhoneNumber);
            if (user == null)
                return BadRequest("کاربری با مشخصات وارد شده یافت نشد");

            if (!Sha256Hasher.IsCompare(user.Password, loginViewModel.Password))
                return BadRequest("کاربری با مشخصات وارد شده یافت نشد");

            if (!user.IsActive)
                return BadRequest("حساب کاربری شما غیرفعال است");

            var loginResult = await AddTokenAndGenerateJwt(user);
            return Ok(loginResult);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<LoginResultDto>>> RefreshToken(
        [FromBody] RefreshTokenViewModel viewModel) 
        {
            var hashRefreshToken = Sha256Hasher.Hash(viewModel.RefreshToken);
            var token = await _userFacade.GetUserTokenByRefreshToken(hashRefreshToken);

            if (token == null)
                return NotFound();

            if (token.RefreshTokenExpireDate < DateTime.Now)
                return BadRequest("زمان رفرش توکن به پایان رسیده است");

            var user = await _userFacade.GetUserById(token.UserId);
            if (user == null)
                return BadRequest("User not found.");

            await _userFacade.RemoveToken(new RemoveUserTokenCommand(token.UserId, token.Id));

            var loginResult = await AddTokenAndGenerateJwt(user);
            return Ok(loginResult);
        }

        [HttpDelete("logout")]
        [Authorize]
        public async Task<ActionResult<ApiResult>> Logout()
        {
            var jwtToken = HttpContext.Request.Headers["Authorization"]
                .ToString()
                .Replace("Bearer ", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Trim();

            if (string.IsNullOrWhiteSpace(jwtToken))
                return BadRequest("Token not found.");

            var hashJwtToken = Sha256Hasher.Hash(jwtToken);
            var token = await _userFacade.GetUserTokenByJwtToken(hashJwtToken);

            if (token == null)
                return NotFound();

            await _userFacade.RemoveToken(new RemoveUserTokenCommand(token.UserId, token.Id));
            return Ok(new ApiResult { IsSuccess = true });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<ApiResult<UserDto>>> GetCurrentUser()
        {
            var userId = UserId;

            var user = await _userFacade.GetUserById(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        private async Task<LoginResultDto> AddTokenAndGenerateJwt(UserDto user)
        {
            var uaParser = Parser.GetDefault();
            var header = HttpContext.Request.Headers["user-agent"].ToString();
            var device = "Unknown";

            if (!string.IsNullOrWhiteSpace(header))
            {
                var info = uaParser.Parse(header);
                device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";
            }

            var token = JwtTokenBuilder.BuildToken(user, _jwtConfig);
            var refreshToken = Guid.NewGuid().ToString();

            var hashJwt = Sha256Hasher.Hash(token);
            var hashRefreshToken = Sha256Hasher.Hash(refreshToken);

            await _userFacade.AddToken(new AddUserTokenCommand(
                user.Id,
                hashJwt,
                hashRefreshToken,
                DateTime.Now.AddDays(_jwtConfig.TokenExpireDays),
                DateTime.Now.AddDays(_jwtConfig.RefreshTokenExpireDays),
                device
            ));

            return new LoginResultDto
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }
    }
}