namespace EMG_MED1000_BACKEND.Entities
{
    public class Marque
    {
        private int MarqueId;
        private String NomMarque;

        //La liste des modèles associés à la marque
        private List<Modele> Modeles;

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