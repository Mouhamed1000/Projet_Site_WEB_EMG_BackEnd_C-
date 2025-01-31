public class LoginModel
{
    //Définition des propriétés du formulaire de Connexion
    private String Email;
    private String Password;

    //Il y'a un champ rôle car tous nos utilisateurs n'ont pas les mêmes droits
    private string Profil;

    //Définition des getters et setters
    public String _email
    {
        get { return Email; }
        set { Email = value; }
    }

    public String _password
    {
        get { return Password; }
        set { Password = value; }
    }

    public String _profil
    {
        get { return Profil; }
        set { Profil = value; }
    }
}