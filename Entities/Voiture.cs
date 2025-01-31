namespace EMG_MED1000_BACKEND.Entities
{
    //Création d'une classe domaine Voiture
    public class Voiture
    {
        //Définition des propriétés relatives à Voiture
        private int VoitureId;
        private StatutVoiture statut;
        private String photo;
        private String description;
        private DateTime AnneeVoiture;

        //Clés étrangères vers Marque et Modele
        private int MarqueId;

        //Une voiture possede ses marques et à ses marques est rattachées ses modèles
        //Propriétés de navigation
        public Marque marque { get; set; } 

        //Déclaration d'un constructeur sans paramètres
        public Voiture() {}

        //Déclaration d'un constructeur de la classe pour y initaliser notre objet Voiture
        public Voiture(StatutVoiture _statut, String _photo, String _description, DateTime _AnneeVoiture, int _MarqueId)
        {
            statut = _statut;
            photo = _photo;
            description = _description;
            AnneeVoiture = _AnneeVoiture;
            MarqueId = _MarqueId;
        }

        //Déclaration des getters et setters
        public int VoitId
        {
            get { return VoitureId; }
            set { VoitureId = value; }
        }

        public StatutVoiture statutVoiture
        {
            get { return statut; }
            set { statut = value; }
        }

        public string photoVoiture
        {
            get { return photo; }
            set { photo = value; }
        }

        public string descrVoiture
        {
            get { return description; }
            set { description = value; }
        }

        public DateTime anneeVoiture
        {
            get { return AnneeVoiture; }
            set { AnneeVoiture = value; }
        }

        public int MarqId 
        {
            get { return MarqueId; }
            set { MarqueId = value; }
        }

    }

    public enum StatutVoiture
    {
        Disponible,
        Vendue,
    }

}