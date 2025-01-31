namespace EMG_MED1000_BACKEND.Entities
{
    public class Modele
    {
        private int ModeleId;
        private String NomModele;
        private DateTime AnneeModele;

        //Pour une marque donnée d'une voiture, il doit être disponible ses modèles
        //Donc dans l'entité Modele, il y'aura l'id de la marque qui est une clé étrangère
        private int MarqueId;

        //Propriété de navigation 
        public Marque Marque { get; set; }

        // Constructeur par défaut sans paramètres pour EF Core
        public Modele() {}

        //Déclaration d'un constructeur de la classe pour y initaliser notre objet Modele
        public Modele (String _NomModele, DateTime _AnneeModele, int marqueId)
        {
            NomModele = _NomModele;
            AnneeModele = _AnneeModele;
            MarqueId = marqueId;
        }


        //Définition des getters et setters
        public int ModelId
        {
            get { return ModeleId; }
            set { ModeleId = value; }
        }

        public String nomModele
        {
            get { return NomModele; }
            set { NomModele= value; }
        }

        public DateTime anneeModele
        {
            get { return AnneeModele; }
            set { AnneeModele = value; }
        }

        public int MarqId
        {
            get { return MarqueId; }
            set { MarqueId = value; }
        }



    }
}