using EMG_MED1000_BACKEND.Entities;
using EMG_MED1000_BACKEND.Models;
using Microsoft.EntityFrameworkCore;

public class VoitureService
{
    private readonly VoitureContext _context;

    //Constructeur pour initialiser notre objet _context d'accès à la base de données
    public VoitureService (VoitureContext context)
    {
        _context = context;
    }

    //Méthode pour créer une voiture
    public async Task<Voiture> CreateVoiture(Voiture voiture)
    {
        //Ajout de l'entité voiture au _context
        _context.Voitures.Add(voiture);

        //Sauvegarde de l'entité dans la base de données
        await _context.SaveChangesAsync();

        return voiture;
    }

    //Méthode pour supprimer une voiture
    public async Task DeleteVoiture(int id)
    {
        //Pour supprimer une voiture, on aura besoin de son id pour le retirer de la base de données
        //Ici, on vérifie si l'id fournie de la voiture existe rééllement dans la table de la base de données 
        var voiture = await _context.Voitures.FindAsync(id);
        if (voiture == null)
            throw new KeyNotFoundException("Voiture introuvable");

        //Suppression de l'entité voiture au _context
        _context.Voitures.Remove(voiture);

        //Sauvegarde des changements dans la base de données
        await _context.SaveChangesAsync();
    }

    //Méthode pour obtenir toutes les voitures
    public async Task<List<Voiture>> GetAllVoituresAsync()
    {
        return await _context.Voitures.ToListAsync();
    }
}

