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
    public async Task<Marque> CreateMarque(Marque marque)
    {
        //Ajout de l'entité marque au _context
        _context.Marques.Add(marque);

        //Sauvegarde de l'entité dans la base de données
        await _context.SaveChangesAsync();

        return marque;
    }

    //Méthode pour supprimer une marque
    public async Task DeleteMarque(int id)
    {
        //Pour supprimer une marque, on aura besoin de son id
        var marque = await _context.Marques.FindAsync(id);
        if (marque == null)
            throw new KeyNotFoundException("Marque introuvable");

        //Suppression de l'entité marque au _context
        _context.Marques.Remove(marque);

        //Sauvegarde des changements dans la base de données
        await _context.SaveChangesAsync();
    }
   
}