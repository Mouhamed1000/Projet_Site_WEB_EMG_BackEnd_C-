namespace EMG_MED1000_BACKEND.Entities
{
    public class Marque
    {
        private int MarqueId { get; set; }
        private String NomMarque { get; set; }

        //La liste des modèles associés à la marque
        private List<Modele> Modeles { get; set; }

    }
}