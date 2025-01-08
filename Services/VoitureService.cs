using EMG_MED1000_BACKEND.Entities;
using EMG_MED1000_BACKEND.Models;

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
}

