using Microsoft.AspNetCore.Identity;

public class AuthenticationService
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    //Definition du constructeur
    public AuthenticationService(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
    {
        userManager = _userManager;
        signInManager = _signInManager;
    }

    //Methode pour inscrire un utilisateur
    public async Task<IdentityResult> RegisterUser (string _firstName, string _lastName, string _email, string _password)
    {
        //Creation d'un utilisateur
        var user = new ApplicationUser
        {
            firstName = _firstName,
            lastName = _lastName,
            UserName = _email,
            Email = _email
        };

        return await userManager.CreateAsync(user, _password);
    }

    // Méthode pour authentifier un utilisateur
    public async Task<bool> LoginUser(string email, string password)
    {
        var result = await signInManager.PasswordSignInAsync(email, password, false, false);
        return result.Succeeded;
    }
}