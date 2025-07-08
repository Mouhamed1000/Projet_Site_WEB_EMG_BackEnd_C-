using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;

public class AuthenticationService
{
    //ApplicationUser fichier qu'on a créé pour personnalisé les champs de Identity et hérite de Identity
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    
    //Création d'une propriété pour les rôles
    private readonly RoleManager<IdentityRole> roleManager;

    private readonly IConfiguration configuration;

    //Definition du constructeur
    public AuthenticationService(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, RoleManager<IdentityRole> _roleManager, IConfiguration _configuration)
    {
        userManager = _userManager;
        signInManager = _signInManager;
        roleManager = _roleManager;
        configuration = _configuration;
    }

    //Méthode pour générer le token JWT
    private async Task<string> GenerateJwtToken(ApplicationUser user, IList<string> roles)
    {
        // On récupère les valeurs du Jwt depuis les variables d'environnement
        var jwtSettings = configuration.GetSection("JwtSettings");

        var key = Convert.ToBase64String(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
        var key2 = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
    
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("id", user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };
    
        // Ajouter les rôles comme claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
    
        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key2), SecurityAlgorithms.HmacSha256)
        );
    
        return new JwtSecurityTokenHandler().WriteToken(token);
}



    //Methode pour inscrire un utilisateur en associant le jeton JWT
    public async Task<object> RegisterUser(string _firstName, string _lastName, string _email, string _password)
    {
        // Vérifier si un utilisateur avec cet email existe déjà
        var existingUser = await userManager.FindByEmailAsync(_email);
        if (existingUser != null)
        {
            return new { message = "Cet email est déjà utilisé." };
        }

        // Créer un nouvel utilisateur
        var user = new ApplicationUser
        {
            firstName = _firstName,
            lastName = _lastName,
            UserName = _email,
            Email = _email
        };

        var result = await userManager.CreateAsync(user, _password);
        if (!result.Succeeded)
        {
            var errorMessages = result.Errors.Select(e => e.Description).ToArray();
            return new JsonResult(new { message = "Erreur lors de la création du compte", errors = errorMessages });
        }

        // Ajouter un rôle par défaut ici, "Personnel"
        string defaultRole = "Personnel"; 

        if (!await roleManager.RoleExistsAsync(defaultRole))
        {
            await roleManager.CreateAsync(new IdentityRole(defaultRole));
        }

        await userManager.AddToRoleAsync(user, defaultRole);

        // Récupérer les rôles attribués
        var roles = await userManager.GetRolesAsync(user);

        // Générer le token JWT
        var token = await GenerateJwtToken(user, roles);

        return new { message = "Utilisateur créé avec succès", Token = token };
    }


    // Méthode pour authentifier un utilisateur en lui associant un jeton Jwt
    public async Task<object> LoginUser(string email, string password, string profil)
    {
        //On verifie l'email
        var user = await userManager.FindByEmailAsync(email);
        if (user == null || !await userManager.CheckPasswordAsync(user, password))
            return new JsonResult(new { message = "Email ou mot de passe incorrect" });

        //On récupère les rôles de l'utilisateur
        var roles = await userManager.GetRolesAsync(user);

        //On Vérifie si l'utilisateur a le bon rôle en fonction du profil
        bool isAdmin = roles.Contains("Admin");
        bool isPersonnel = roles.Contains("Personnel");

        if (profil == "admin" && !isAdmin)
        {
            return new JsonResult(new { message = "L'utilisateur n'est pas administrateur" });// L'utilisateur n'est pas administrateur
        }

        if (profil == "personnel" && !isPersonnel)
        {
            return new JsonResult(new { message = "L'utilisateur n'est pas personnel" });// L'utilisateur n'est pas personnel
        }

        //On Génére un token JWT avec les rôles inclus
        var token = await GenerateJwtToken(user, roles);
        return new { Token = token , Roles = roles };
    }

    internal async Task RegisterUser(object email)
    {
        throw new NotImplementedException();
    }

    //Méthode pour créer un administrateur en lui associant un jeton JWT
    public async Task<object> RegisterAdmin(string _firstName, string _lastName, string _email, string _password)
    {
        // Vérifier si un utilisateur avec cet email existe déjà avanr de poursuivre
        var existingUser = await userManager.FindByEmailAsync(_email);
        if (existingUser != null)
        {
            return new { message = "Cet email est déjà utilisé." };
        }

        //On Vérifie après s'il existe déjà un administrateur
        var admins = await userManager.GetUsersInRoleAsync("Admin");

        if (admins.Count > 0)
        {
            return new { message = "Un compte administrateur existe déjà."};
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
            return new { message = "Erreur lors de la création du compte", errors = result.Errors }; 
        }

        //On va assigner le rôle "Admin" à cet utilisateur si l'opération se termine bien
        await userManager.AddToRoleAsync(adminUser, "Admin");

         // Récupérer les rôles attribués
        var roles = await userManager.GetRolesAsync(adminUser);
    
        //On crée un token pour l'associer à cet utilisateur admin
        var token = await GenerateJwtToken(adminUser, roles);
    
        return new { message = "Compte administrateur créé avec succès", Token = token };
    }
}