public class RegisterModel
{
    //Définition des propriétés du formulaire d'inscription
    private String FirstName;
    private String LastName;
    private String Email;
    private String Password;

    //Ajout de la propriété profil pour l'inscription
    private String Profil;

    //Définition des getters et setters
    public String _firstName
    {
        get { return FirstName; }
        set { FirstName = value; }
    }

    public String _lastName
    {
        get { return LastName; }
        set { LastName = value; }
    }

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