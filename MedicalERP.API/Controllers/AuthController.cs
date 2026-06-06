using Microsoft.AspNetCore.Mvc;
using MedicalERP.Application.DTOs.Auth;
using MedicalERP.Application.Interfaces;

namespace MedicalERP.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _authService.RegisterAsync(request);
            _logger.LogInformation("User registered successfully");
            return Ok(new
            {
                success = true,
                message = "User registered successfully",
                data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while registering user");
            return BadRequest(new
            {
                success = false,
                message = ex.Message
            });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _authService.LoginAsync(request);
            return Ok(new
            {
                success = true,
                message = "Login successful",
                data = result
            });
        }
        catch (Exception ex)
        {
            return Unauthorized(new
            {
                success = false,
                message = ex.Message
            });
        }
    }
}