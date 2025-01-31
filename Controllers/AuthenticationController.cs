using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]

public class AuthenticationController : ControllerBase
{
    private readonly AuthenticationService _authService;

    public AuthenticationController(AuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var result = await _authService.RegisterUser(model._firstName, model._lastName, model._email, model._password);

        if (result is string errorMessage)
            return BadRequest(new { message = errorMessage });

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var success = await _authService.LoginUser(model._email, model._password, model._profil);

        if (success == null)
            return Unauthorized(new { message = "Email ou mot de passe incorrect"}); 

        return Ok(new { message = "Connexion réussie" });
           
    }

    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
    {
        var result = await _authService.RegisterAdmin(model._firstName, model._lastName, model._email, model._password);

        if (result is string errorMessage)
            return BadRequest(new { message = errorMessage });

        return Ok(result);
    }
}