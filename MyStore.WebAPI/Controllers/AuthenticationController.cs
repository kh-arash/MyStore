using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyStore.Database;
using MyStore.Database.Models.Authentication.Login;
using MyStore.Database.Models.Authentication.SignUp;
using MyStore.Database.Models.User;
using MyStore.Service.Models;
using MyStore.Service.Services.Email;
using MyStore.Service.Services.User;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Response = MyStore.WebAPI.Models.Response;

namespace MyStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public AuthenticationController(UserManager<ApplicationUser> userManager, IEmailService emailService, IUserService userService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel registerUserModel)
        {
            var result = await _userService.CreateAsync(registerUserModel);
            if (result.IsSuccess && result.Result != null)
            {
                await _userService.AssignRoleAsync(registerUserModel.Roles, result.Result.User);

                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { result.Result.Token, email = registerUserModel.Email }, Request.Scheme);

                var message = new Message(new string[] { registerUserModel.Email! }, "Confirmation email link", confirmationLink!);
                var responseMsg = _emailService.SendEmail(message);
                return StatusCode(StatusCodes.Status200OK,
                        new Response { IsSuccess = true, Message = $"{result.Message} {responseMsg}" });

            }

            return StatusCode(StatusCodes.Status500InternalServerError,
                  new Response { Message = result.Message, IsSuccess = false });
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                      new Response { Status = "Success", Message = "Email Verified Successfully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response { Status = "Error", Message = "This User does not exist!" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _userService.LoginAsync(model);
            if (result != null && result.IsSuccess)
            {
                return Ok(result);
            }

            return StatusCode(StatusCodes.Status404NotFound,
                new Response { Status = "Success", Message = $"Invalid user" });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetPasswordLink = Url.Action(nameof(ResetPassword), "Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Reset Password", resetPasswordLink!);
                await _emailService.SendEmail(message);

                return StatusCode(StatusCodes.Status200OK, new Response { IsSuccess = true, Status = "Success", Message = $"Reset password request is sent to {user.Email}. Please open your email and click on the link" });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { IsSuccess = false, Status = "Error", Message = "Could not send the link to your email, please try again." });
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if(!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }

                return StatusCode(StatusCodes.Status200OK, new Response { IsSuccess = true, Status = "Success", Message = $"Password has been changed" });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { IsSuccess = false, Status = "Error", Message = "Could not send the link to your email, please try again." });
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(LoginResponse tokens)
        {
            var jwt = await _userService.RenewAccessTokenAsync(tokens);
            if (jwt.IsSuccess)
            {
                return Ok(jwt);
            }
            return StatusCode(StatusCodes.Status404NotFound,
                new Response { Status = "Success", Message = $"Invalid Code" });
        }
    }
}
