using MedicalERP.Application.DTOs.Auth;
using MedicalERP.Domain.Entities;
using MedicalERP.Infrastructure.Data;
using MedicalERP.Infrastructure.Services;
using MedicalERP.Tests.Common;
using Moq;
using Xunit;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace MedicalERP.Tests.Auth;

public class AuthServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly JwtService _jwtService;
    private readonly AuthService _authService;
    private readonly Mock<IConfiguration> _configMock;

    public AuthServiceTests()
    {
        _context = TestDbFactory.Create();

        // =========================
        // MOCK CONFIGURATION (FIX)
        // =========================
        _configMock = new Mock<IConfiguration>();

        _configMock.Setup(x => x["Jwt:Key"])
            .Returns("ThisIsAVeryLongSecretKeyForTesting123456");

        _configMock.Setup(x => x["Jwt:Issuer"])
            .Returns("MedicalERP");

        _configMock.Setup(x => x["Jwt:Audience"])
            .Returns("MedicalERPUsers");

        _configMock.Setup(x => x["Jwt:DurationInMinutes"])
            .Returns("60");

        // JwtService now uses mocked configuration
        _jwtService = new JwtService(_configMock.Object);

        _authService = new AuthService(
            _context,
            _jwtService
        );
    }

    // ==========================
    // HELPER METHOD
    // ==========================
    private static string HashPassword(string password)
    {
        using var sha = SHA256.Create();

        var bytes = sha.ComputeHash(
            Encoding.UTF8.GetBytes(password));

        return Convert.ToBase64String(bytes);
    }

    // ==========================
    // TEST #1
    // ==========================
    [Fact]
    public async Task RegisterAsync_Should_Create_User_When_Email_Does_Not_Exist()
    {
        // Arrange
        var dto = new RegisterRequest
        {
            Email = "test@test.com",
            Password = "123456",
            FullName = "Test User"
        };

        // Act
        var result = await _authService.RegisterAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test@test.com", result.Email);
    }

    // ==========================
    // TEST #2
    // ==========================
    [Fact]
    public async Task RegisterAsync_Should_Throw_When_User_Already_Exists()
    {
        // Arrange
        var dto = new RegisterRequest
        {
            Email = "duplicate@test.com",
            Password = "123456",
            FullName = "User"
        };

        await _authService.RegisterAsync(dto);

        // Act + Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _authService.RegisterAsync(dto));
    }

    // ==========================
    // TEST #3
    // ==========================
    [Fact]
    public async Task LoginAsync_Should_Return_Token_When_Credentials_Are_Valid()
    {
        // Arrange
        var registerDto = new RegisterRequest
        {
            Email = "login@test.com",
            Password = "123456",
            FullName = "Login User"
        };

        await _authService.RegisterAsync(registerDto);

        var loginDto = new LoginRequest
        {
            Email = "login@test.com",
            Password = "123456"
        };

        // Act
        var result = await _authService.LoginAsync(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrEmpty(result.Token));
    }

    // ==========================
    // TEST #4
    // ==========================
    [Fact]
    public async Task LoginAsync_Should_Throw_When_User_Not_Found()
    {
        var loginDto = new LoginRequest
        {
            Email = "notfound@test.com",
            Password = "123456"
        };

        await Assert.ThrowsAsync<Exception>(() =>
            _authService.LoginAsync(loginDto));
    }

    // ==========================
    // TEST #5
    // ==========================
    [Fact]
    public async Task LoginAsync_Should_Throw_When_Password_Is_Wrong()
    {
        // Arrange
        var registerDto = new RegisterRequest
        {
            Email = "wrongpass@test.com",
            Password = "123456",
            FullName = "User"
        };

        await _authService.RegisterAsync(registerDto);

        var loginDto = new LoginRequest
        {
            Email = "wrongpass@test.com",
            Password = "wrongpassword"
        };

        // Act + Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _authService.LoginAsync(loginDto));
    }

    // ==========================
    // TEST #6
    // ==========================
    [Fact]
    public async Task RegisterAsync_Should_Save_Hashed_Password()
    {
        // Arrange
        var dto = new RegisterRequest
        {
            Email = "hash@test.com",
            Password = "123456",
            FullName = "Hash User"
        };

        // Act
        await _authService.RegisterAsync(dto);

        var user = _context.Users.First(x => x.Email == "hash@test.com");

        // Assert
        Assert.NotEqual("123456", user.PasswordHash);
    }
}