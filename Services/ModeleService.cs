using EMG_MED1000_BACKEND.Entities;
using EMG_MED1000_BACKEND.Models;
using Microsoft.EntityFrameworkCore;

public class ModeleService
{
    private readonly VoitureContext _context;

    //Constructeur pour initialiser notre objet _context d'accès à la base de données
    public ModeleService(VoitureContext context)
    {
        _context = context;
    }

    //Méthode pour créer un modele
    public async Task<Modele> CreateModele(String _NomModele, DateTime _AnneeModele, int marqueId)
    {
        //On utilise le constructeur Modele(...) pour initialiser notre objet modele
        var modele = new Modele (_NomModele, _AnneeModele, marqueId);

        //AJout de l'entité modele fournie au _context
        _context.Modeles.Add(modele);

        //Sauvegarde de l'entité dans la base de données
        await _context.SaveChangesAsync();

        return modele;
    }

    //Méthode pour modifier un modele
    public async Task<Modele> UpdateModele(int id, Modele updatedModele)
    {
        //On vérifie l'id renseigné au niveau de notre table de la base de données
        var modele = await _context.Modeles.FindAsync(id);
        //On s'assure que l'id existe dans notre table de la base de données 
        if (modele == null)
        {
            return null;
        }

        modele.nomModele = updatedModele.nomModele;
        modele.anneeModele = updatedModele.anneeModele;
        modele.MarqId = updatedModele.MarqId;

        //Ensuite on sauvegarde les changements
        await _context.SaveChangesAsync();
        return modele;
    }

    //Méthode pour supprimer un modele
    public async Task<bool> DeleteModele(int id)
    {
        //Pour supprimer un modele, on a besoin de son id
        var modele = await _context.Modeles.FindAsync(id);
        if (modele == null)
            throw new KeyNotFoundException("Modele introuvable");

        //Suppression de l'entité modele au _context
        _context.Modeles.Remove(modele);

        //Sauvegarde des changements dans la base de données
        await _context.SaveChangesAsync();

        return true;
    }

    //Methode pour obtenir la liste des modeles associes a une marque
    //Pour cela, on aura besoin de la cle etrangere, donc l'id de la marque
    public async Task<List<Modele>> GetAllModelesAsync(int marqueId)
    {
        return await _context.Modeles
                                    . Where(m => m.MarqId == marqueId)
                                    .ToListAsync();
    }
}