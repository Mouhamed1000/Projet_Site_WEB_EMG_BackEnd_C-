using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    //Definition des proprietes FirstName, LastName
    private String FirstName;
    private String LastName;

    //Déclaration des getters et setters
    public String firstName
    {
        get { return FirstName; }
        set { FirstName = value; }
    }

    public String lastName
    {
        get { return LastName; }
        set { LastName = value; }
    }

}
