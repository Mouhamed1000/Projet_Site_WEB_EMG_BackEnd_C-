using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

public class AuthenticationService
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;
    
    //Création d'une propriété pour les rôles
    private readonly RoleManager<IdentityRole> roleManager;

    //Definition du constructeur
    public AuthenticationService(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, RoleManager<IdentityRole> _roleManager)
    {
        userManager = _userManager;
        signInManager = _signInManager;
        roleManager = _roleManager;
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
        //Pour connecter un utilisateur, ici on envoie l'email et le password
        var result = await signInManager.PasswordSignInAsync(email, password, false, false);
        return result.Succeeded;
    }

    internal async Task RegisterUser(object email)
    {
        throw new NotImplementedException();
    }

    //Méthode pour créer un administrateur
    public async Task<IdentityResult> RegisterAdmin(string _firstName, string _lastName, string _email, string _password)
    {
        //On Vérifie s'il existe déjà un administrateur
        var admins = await userManager.GetUsersInRoleAsync("Admin");

        if (admins.Count > 0)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Un compte administrateur existe déjà." });
        }

        //On vérifie si le rôle Admin existe
        //Si c'est pas le cas, on le crée
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        //Si un compte admin n'existe pas, alors on le crée un nouvel utilisateur Admin
         var adminUser = new ApplicationUser
        {
            firstName = _firstName,
            lastName = _lastName,
            UserName = _email,
            Email = _email
        };

        var result = await userManager.CreateAsync(adminUser, _password);

        if (!result.Succeeded)
        {
            return result; 
        }

        //On Assigner le rôle "Admin" à cet utilisateur
        await userManager.AddToRoleAsync(adminUser, "Admin");

        return IdentityResult.Success;    
    }
}