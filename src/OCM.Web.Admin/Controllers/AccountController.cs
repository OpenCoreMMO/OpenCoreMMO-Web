using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OCM.Infrastructure.Interfaces;

namespace OCM.Web.Admin.Controllers;

// AccountController.cs
[ApiController]
[Route("[controller]")]
public class AccountController(IAccountRepository accountRepository) : Controller
{
    [HttpGet("Login")]
    public async Task<IActionResult> Login(string email, int accountId)
    {
        var account = await accountRepository.GetById(accountId);
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, email),
            new(ClaimTypes.NameIdentifier, accountId.ToString()),
            new(ClaimTypes.Name, account.AccountName),
        };

        var claimsIdentity = new ClaimsIdentity(claims, "CustomAuth");
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        };

        await HttpContext.SignInAsync(
            "CustomAuth",
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
            
        return Redirect("/"); // Redirect back to Blazor app
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("CustomAuth");
        return Redirect("/login");
    }
}