namespace Assistenza.BufDalsi.Data.Models
{
    public class Cogeneratore
    {
        public int cgn_Id { get; set; }
        public int cgn_Potenza { get; set; }
        public string cgn_Marca { get; set; }
        public string cgn_Modello { get; set; }
        public string cgn_Serie { get; set; }
        public int cgn_Impianto { get; set; }

        public Cogeneratore() { }

        public Cogeneratore(int Id, int Potenza, string Marca, string Modello, string Serie, int Impianto)
        {
            cgn_Id = Id;
            cgn_Potenza = Potenza;
            cgn_Marca = Marca;
            cgn_Modello = Modello;
            cgn_Serie = Serie;
            cgn_Impianto = Impianto;
        }
    }
}