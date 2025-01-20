namespace EMG_MED1000_BACKEND.Entities
{
    //Création d'une classe domaine Voiture
    public class Voiture
    {
        //Définition des propriétés relatives à Voiture
        private int VoitureId { get; set; }
        private StatutVoiture statut { get; set; }
        private String photo { get; set; }
        private String description { get; set; }
        private DateTime AnneeVoiture { get; set; }

        //Clés étrangères vers Marque et Modele
        private int MarqueId { get; set; }

        //Une voiture possede ses marques et à ses marques est rattachées ses modèles
        //Propriétés de navigation
        public Marque marque { get; set; } 

        //Déclaration d'un constructeur de la classe pour y initaliser notre objet Voiture
        public Voiture(StatutVoiture _statut, String _photo, String _description, DateTime _AnneeVoiture, int _MarqueId)
        {
            statut = _statut;
            photo = _photo;
            description = _description;
            AnneeVoiture = _AnneeVoiture;
            MarqueId = _MarqueId;
        }

    }

    public enum StatutVoiture
    {
        Disponible,
        Vendue,
    }

}