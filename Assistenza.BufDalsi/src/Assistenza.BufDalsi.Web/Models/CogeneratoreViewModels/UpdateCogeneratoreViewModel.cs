namespace Assistenza.BufDalsi.Web.Models.CogeneratoreViewModels
{
    public class UpdateCogeneratoreViewModel
    {
        public int ipt_Id { get; set; }
        public int clt_id { get; set; }
        public int cgn_Id { get; set; }
        public int cgn_Potenza { get; set; }
        public string cgn_Marca { get; set; }
        public string cgn_Modello { get; set; }
        public string cgn_Serie { get; set; }
        public int cgn_Impianto { get; set; }

        public UpdateCogeneratoreViewModel() { }

        public UpdateCogeneratoreViewModel(int Id, int Potenza, string Marca, string Modello, string Serie, int Impianto)
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
