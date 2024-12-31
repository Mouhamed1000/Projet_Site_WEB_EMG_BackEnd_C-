namespace EMG_MED1000_BACKEND.Entities
{
    //Création d'une classe domaine Voiture
    public class Voiture
    {
        //Définition des propriétés relatives à Voiture
        private int VoitureId { get; set; }
        public StatutVoiture statut { get; set; }
        private String photo { get; set; }
        private String description { get; set; }
        private DateTime AnneeVoiture { get; set; }

        //Clés étrangères vers Marque et Modele
        private int MarqueId { get; set; }
        private int ModeleId { get; set; }

        //Une voiture possede ses marques et à ses marques est rattachées ses modèles
        //Propriétés de navigation
        private Marque marque { get; set; } 
        private Modele modele { get; set; }

    }

    public enum StatutVoiture
    {
        Disponible,
        Vendue,
    }
}