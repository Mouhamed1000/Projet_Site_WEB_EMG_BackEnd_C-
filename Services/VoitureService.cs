using EMG_MED1000_BACKEND.Entities;
using EMG_MED1000_BACKEND.Models;
using Microsoft.EntityFrameworkCore;

public class VoitureService
{
    private readonly VoitureContext _context;
    private readonly IWebHostEnvironment _environment;

    //Constructeur pour initialiser notre objet _context d'accès à la base de données
    //on nitialise également notre object d'accès aux ressources de notre environnement
    public VoitureService (VoitureContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    //Méthode pour gérer l'upload d'image de la voiture 
    public async Task<String> UploadImageAsync(IFormFile file)
    {
        //Une fois qu'un fichier est donné, on vérifie qu'il n'est pas vide
        if (file == null || file.Length == 0)
            return null;

        //On génère un nom unique pour l'image, de ce fait 2images n'auront pas le même nom
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        //On a un dossier pour stocker nos images
        //Ici, on prend le chemin du dossier
        var uploadPath = Path.Combine(_environment.WebRootPath, "images");

        //Ici, on vérifie que le dossier existe
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        //Ici, on enregistre l'image dans notre dossier
        var filePath = Path.Combine(uploadPath, fileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        //Après on retourne le chemin relatif de l'image pour notre base de données
        return $"/images/{fileName}";

    }

    //Méthode pour créer une voiture
    public async Task<Voiture> CreateVoiture(StatutVoiture _statut, String _photo, String _description, DateTime _AnneeVoiture, int _MarqueId)
    {
        //On utilise le constructeur Voiture(...) pour initialiser notre objet voiture
        var voiture = new Voiture(_statut, _photo, _description, _AnneeVoiture, _MarqueId);

        //Ajout de l'entité voiture au _context
        _context.Voitures.Add(voiture);

        //Sauvegarde de l'entité dans la base de données
        await _context.SaveChangesAsync();

        return voiture;
    }

    //Méthode pour supprimer une voiture
    public async Task<bool> DeleteVoiture(int id)
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

        return true;
    }

    //Méthode pour obtenir toutes les voitures
    public async Task<List<Voiture>> GetAllVoituresAsync()
    {
        return await _context.Voitures.ToListAsync();
    }
}

