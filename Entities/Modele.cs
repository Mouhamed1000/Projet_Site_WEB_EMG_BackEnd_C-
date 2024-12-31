namespace EMG_MED1000_BACKEND.Entities
{
    public class Modele
    {
        private int ModeleId { get; set; }
        private String NomModele { get; set; }
        private DateTime AnneeModele { get; set; }

        //Pour une marque donnée d'une voiture, il doit être disponible ses modèles
        //Donc dans l'entité Modele, il y'aura l'id de la marque qui est une clé étrangère
        private int MarqueId { get; set; }

        //Propriété de navigation 
        private Marque Marque { get; set; }

    }
}