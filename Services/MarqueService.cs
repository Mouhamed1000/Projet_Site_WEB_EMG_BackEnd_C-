using EMG_MED1000_BACKEND.Entities;
using EMG_MED1000_BACKEND.Models;
using Microsoft.EntityFrameworkCore;

public class MarqueService
{
    private readonly VoitureContext _context;

    //Constructeur pour initialiser notre objet _context d'accès à la base de données
    public MarqueService(VoitureContext context)
    {
        _context = context;
    }

    //Méthode pour créer une marque
    public async Task<Marque> CreateMarque(String _NomMarque, List<Modele> _Modeles)
    {
        // On verifie si une marque avec ce nom existe déjà
        var existingMarque = await _context.Marques
                                .FirstOrDefaultAsync(m => m.NomMarq == _NomMarque);

        if (existingMarque != null)
        {
            throw new Exception("Une marque avec ce nom existe déjà.");
        }

        //On Vérifie l'unicité des modèles associés
        foreach (var modele in _Modeles)
        {
            var existingModele = await _context.Modeles
                .FirstOrDefaultAsync(m => m.nomModele == modele.nomModele);

            if (existingModele != null)
            {
                throw new Exception($"Un modèle avec le nom {modele.nomModele} existe déjà.");
            }
        }

        //Création d'un objet Marque sans utiliser le constructeur
        var marque = new Marque()
        {
            NomMarq = _NomMarque,
            ListModele = _Modeles ?? new List<Modele>()
        };

        //Ajout de l'entité marque au _context
        _context.Marques.Add(marque);

        //Sauvegarde de l'entité dans la base de données
        await _context.SaveChangesAsync();

        return marque;
    }

    //Méthode pour modifier une marque
    public async Task<bool> UpdateMarque(int id, String _NomMarque, List<Modele> _Modeles)
    {
        //Une fois que l'id est renseigné, on recherche l'id dans la table de la base de données
        var marque = await _context.Marques.FindAsync(id);
        if (marque == null)
        {
            return false;
        }

        //Si la marque existe, on met à jour les propriétés
        marque.NomMarq = _NomMarque;
        marque.ListModele = _Modeles;

        //Ici, on met à jour la table à travers le _context
        _context.Marques.Update(marque);

        //Ensuite, on sauvegarde les changements
        await _context.SaveChangesAsync();

        return true;
    }


    //Méthode pour supprimer une marque
    public async Task<bool> DeleteMarque(int id)
    {
        //Pour supprimer une marque, on aura besoin de son id
        var marque = await _context.Marques.FindAsync(id);
        if (marque == null)
            throw new KeyNotFoundException("Marque introuvable");

        //Suppression de l'entité marque au _context
        _context.Marques.Remove(marque);

        //Sauvegarde des changements dans la base de données
        await _context.SaveChangesAsync();

        return true;
    }

    //Méthode pour obtenir toutes les marques
    public async Task<List<Marque>> GetAllMarquesAsync()
    {
        return await _context.Marques.ToListAsync();
    }

    //Méthode pour obtenir la marque par Id
    public async Task<Marque> GetMarqueById(int id)
    {
        return await _context.Marques
            .Include(m => m.ListModele) 
            .FirstOrDefaultAsync(m => m.MarqId == id);
    }
   
}