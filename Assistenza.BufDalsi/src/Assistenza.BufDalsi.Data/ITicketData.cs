using Assistenza.BufDalsi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Data
{
    public interface ITicketData
    {
        //SEZIONE CLIENTI
        IEnumerable<Client> GetClients();
        Client GetClient(int id);
        IEnumerable<Region> GetRegions();
        Region GetRegion(int id);
        void InsertCliente(Client c);
        void DeleteCliente(int clt_Id);
        void UpdateCliente(Client c);

        // SEZIONE MANUTENZIONE
        Manutenzione GetManutenzione(int id);
        IEnumerable<Manutenzione> GetManutenzioniDaEseguire();
        IEnumerable<Manutenzione> GetManutenzioniStorico();
        void InsertManutenzione(Manutenzione m);
        void DeleteManutenzione(int id);
        void UpdateManutenzione(Manutenzione m);
        IEnumerable<Manutenzione> GetManutenzioniDaEseguireByCliente(int IdEsterno);
        IEnumerable<Manutenzione> GetManutenzioniStoricoByCliente(int IdEsterno);

        //SEZIONE IMPIANTI
        IEnumerable<Impianto> GetImpianti();
        IEnumerable<Impianto> GetImpiantiByClient(int clt_Id);
        Impianto GetImpiantoById(int ipt_Id);
        void InsertImpianto(Impianto i);
        void DeleteImpianto(int ipt_Id);
        void UpdateImpianto(Impianto i);
        //SEZIONE TICKETING

        //SEZIONE COGENERATORI
        void InsertCogeneratore(Cogeneratore c);
        void DeleteCogeneratore(int cgn_Id);
        Cogeneratore GetCogeneratore(int id);
        void UpdateCogeneratore(Cogeneratore c);
        IEnumerable<Cogeneratore> GetCogeneratoreByImpianto(int ipt_Id);

        //SEZIONE VASCA
        void InsertVasca(Vasca v);
        void DeleteVasca(int vsc_Id);
        Vasca GetVasca(int id);
        void UpdateVasca(Vasca v);
        IEnumerable<Vasca> GetVascheByImpianto(int ipt_Id);

        //SEZIONE SENSORE
        void InsertSensore(Sensore s);
        void DeleteSensore(int ssr_Id);
        Sensore GetSensore(int id);
        void UpdateSensore(Sensore s);
        IEnumerable<Sensore> GetSensoriByVasche(int vsc_Id);

        //SEZIONE GENERICO
        IEnumerable<Generico> GetGenericoByImpianto(int ipt_Id);
        void InsertGenerico(Generico g);
        void DeleteGenerico(int gnr_Id);
        void UpdateGenerico(Generico g);
        Generico GetGenerico(int id);

        //SEZIONE AGITATORI
        void InsertAgitatore(Agitatore a);
        void DeleteAgitatore(int agt_Id);
        void UpdateAgitatore(Agitatore a);
        Agitatore GetAgitatore(int id);
        IEnumerable<Agitatore> GetAgitatoriByVasche(int vsc_Id);
    }
}
