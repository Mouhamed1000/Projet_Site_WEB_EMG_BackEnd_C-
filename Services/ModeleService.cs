using EMG_MED1000_BACKEND.Entities;
using EMG_MED1000_BACKEND.Models;

public class ModeleService
{
    private readonly VoitureContext _context;

    //Constructeur pour initialiser notre objet _context d'accès à la base de données
    public ModeleService(VoitureContext context)
    {
        _context = context;
    }

    //Méthode pour créer un modele
    public async Task<Modele> CreateModele(Modele modele)
    {
        //AJout de l'entité modele fournie au _context
        _context.Modeles.Add(modele);

        //Sauvegarde de l'entité dans la base de données
        await _context.SaveChangesAsync();

        return modele;
    }

    //Méthode pour supprimer un modele
    public async Task DeleteModele(int id)
    {
        //Pour supprimer un modele, on a besoin de son id
        var modele = await _context.Modeles.FindAsync(id);
        if (modele == null)
            throw new KeyNotFoundException("Modele introuvable");

        //Suppression de l'entité modele au _context
        _context.Modeles.Remove(modele);

        //Sauvegarde des changements dans la base de données
        await _context.SaveChangesAsync();
    }
}