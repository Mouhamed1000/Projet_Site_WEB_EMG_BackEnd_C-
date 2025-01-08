using EMG_MED1000_BACKEND.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMG_MED1000_BACKEND.Models
{
    //Création d'une classe VoitureContext héritant de DbContext
    //Cette classe servira à l'interaction avec la base de données en utilisant EF Core
    public class VoitureContext : DbContext
    {
        private readonly IConfiguration _configuration;

        //Constructeur
        public VoitureContext(DbContextOptions<VoitureContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        //entitées
        public DbSet<Voiture> Voitures;
        public DbSet<Marque> Marques;
        public DbSet<Modele> Modeles;

        //Implémentation de la méthode OnConfiguring (...) permettant l'import des configs de notre base de données
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }

            base.OnConfiguring(optionsBuilder);
        }
    } 
}