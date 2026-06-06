using MedicalERP.Application.DTOs.Auth;
using MedicalERP.Domain.Constants;
using MedicalERP.Domain.Entities;
using MedicalERP.Infrastructure.Data;
using MedicalERP.Infrastructure.Services;
using MedicalERP.Tests.Common;
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

    // Constructor
    public AuthServiceTests()
    {
        _context = TestDbFactory.Create();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    { "Jwt:Key", "ThisIsAVeryLongSecretKeyForTesting123456" },
                    { "Jwt:Issuer", "MedicalERP" },
                    { "Jwt:Audience", "MedicalERPUsers" },
                    { "Jwt:DurationInMinutes", "60" }
                })
            .Build();

        _jwtService = new JwtService(configuration);

        _authService = new AuthService(
            _context,
            _jwtService);
    }

    // Helper Method
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
        // paste Step 6 here
    }

    // ==========================
    // TEST #2
    // ==========================

    [Fact]
    public async Task RegisterAsync_Should_Throw_When_User_Already_Exists()
    {
        // paste Step 7 here
    }

    // ==========================
    // TEST #3
    // ==========================

    [Fact]
    public async Task LoginAsync_Should_Return_Token_When_Credentials_Are_Valid()
    {
        // paste Step 8 here
    }

    // ==========================
    // TEST #4
    // ==========================

    [Fact]
    public async Task LoginAsync_Should_Throw_When_User_Not_Found()
    {
        // paste Step 9 here
    }

    // ==========================
    // TEST #5
    // ==========================

    [Fact]
    public async Task LoginAsync_Should_Throw_When_Password_Is_Wrong()
    {
        // paste Step 10 here
    }

    // ==========================
    // TEST #6
    // ==========================

    [Fact]
    public async Task RegisterAsync_Should_Save_Hashed_Password()
    {
        // paste Step 11 here
    }
}