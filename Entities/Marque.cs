namespace EMG_MED1000_BACKEND.Entities
{
    public class Marque
    {
        private int MarqueId;
        private String NomMarque;

        //La liste des modèles associés à la marque
        private List<Modele> Modeles;

        //Déclaration d'un constructeur de la classe pour y initaliser notre objet Marque
        public Marque(String _NomMarque, List<Modele> _Modeles)
        {
            NomMarque = _NomMarque;
            Modeles = _Modeles ?? new List<Modele>();
        }

        //Déclaration des getters et setters
        public int MarqId
        {
            get { return MarqueId; }
            set { MarqueId = value; }
        }

        public String NomMarq
        {
            get { return NomMarque; }
            set { NomMarque = value; }
        }

        public List<Modele> ListModele 
        {
            get { return Modeles; }
            set { Modeles = value; }
        }

    }
}