using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Assistenza.BufDalsi.Data.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Assistenza.BufDalsi.Web.Models.ImpiantoViewModels;
/*______        __   _____      _ _               _____                       
  | ___ \      / _| |_   _|    | (_)             |  ___|                      
  | |_/ /_   _| |_    | | _   _| |_  __ _ _ __   | |__ _   _  __ _  ___ _ __           ︻╦╤─
  | ___ \ | | |  _|   | || | | | | |/ _` | '_ \  |  __| | | |/ _` |/ _ \ '_ \ 
  | |_/ / |_| | |    _| || |_| | | | (_| | | | | | |__| |_| | (_| |  __/ | | |                  
  \____/ \__,_|_|    \___/\__,_|_|_|\__,_|_| |_| \____/\__,_|\__, |\___|_| |_|                          
                                                            __/  |           
                                                            |___/       
  ___  _               _        ______      _   _____             _                                 (ಠ_ಠ)
 / _ \| |             (_)       |  _  \    | | /  ___|           | |       
/ /_\ \ | ___  ___ ___ _  ___   | | | |__ _| | \ `--.  __ _ _ __ | |_ ___  
|  _  | |/ _ \/ __/ __| |/ _ \  | | | / _` | |  `--. \/ _` | '_ \| __/ _ \                  (╯°□°)--︻╦╤─ - - - 
| | | | |  __/\__ \__ \ | (_) | | |/ / (_| | | /\__/ / (_| | | | | || (_) |
\_| |_/_|\___||___/___/_|\___/  |___/ \__,_|_| \____/ \__,_|_| |_|\__\___/     [̲̅$̲̅(̲̅ιοο̲̅)̲̅$̲̅]       (stacks xD)
                                                                                                             ▐――――――――――▌
					                                                                                           ヽʕ•ᴥ•ʔﾉ                      

                                                                                                               */
namespace Assistenza.BufDalsi.Data
{
    public class TicketData : ITicketData
    {
        string _connectionString;
        public TicketData(string cs)
        {
            _connectionString = cs;
        }
        [Authorize(Roles = "Admin,Operator,User")]
        public Manutenzione GetManutenzione(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Manutenzione>(
                    @"USE [tick]
                SELECT [mtz_Id]
                      ,[mtz_Scadenza]
                      ,[mtz_Addetto]
                      ,[mtz_Data]
                      ,[mtz_OreLavoro]
                      ,[mtz_Impianto]
                      ,[mtz_Effettuato]
                      ,[mtz_Descrizione]
                      ,[ipt_RagioneSociale]
                  FROM [dbo].[Manutenzione]
                  INNER JOIN   [dbo].[Impianto]
                  ON          [Impianto].[ipt_Id]=[Manutenzione].[mtz_Impianto]
                  WHERE [mtz_Id]=@Id
               ", new { Id = id });
            }
        }
        [Authorize(Roles = "Admin,Operator")]
        public IEnumerable<Client> GetClients()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Client>(
                    @"USE [tick]
                    SELECT[clt_Id]
                          ,[clt_RagioneSociale]
                          ,[clt_Indirizzo]
                          ,[clt_Mail]
                          ,[clt_Telefono]
                          ,[clt_Mobile]
                      FROM [tick].[dbo].[Cliente]
                    "
                );
            }
        }
        [Authorize(Roles = "Admin,Operator")]
        public Client GetClient(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Client>(
                    @"USE [tick]
                    SELECT[clt_Id]
                          ,[clt_RagioneSociale]
                          ,[clt_Indirizzo]
                          ,[clt_Mail]
                          ,[clt_Telefono]
                          ,[clt_Mobile]
                      FROM [tick].[dbo].[Cliente]
                    WHERE [clt_Id]=@Id
               ", new { Id = id });
            }
        }
        [Authorize(Roles = "Admin,Operator,User")]
        public IEnumerable<Impianto> GetImpiantiByClient(int clt_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Impianto>(
                    @"USE [tick]
                    SELECT           [Impianto].[ipt_Id]
                                    ,[Impianto].[ipt_PosizioneLat]
                                    ,[Impianto].[ipt_PosizioneLong]
                                    ,[Impianto].[ipt_PotenzaNominale]
                                    ,[Impianto].[ipt_RagioneSociale]
                                    ,[Impianto].[ipt_Cliente]
                                    ,[Impianto].[ipt_Torcia]
                                    ,[Impianto].[ipt_Separatore]
                                    ,[Impianto].[ipt_Soffiante]
                                    ,[Impianto].[ipt_Pompa]
                                    ,[Impianto].[ipt_Regione]
                              FROM [tick].[dbo].[Impianto]
                              WHERE[Impianto].ipt_Cliente=@id
                    ", new { id = clt_Id }
                );

            }
        }
        [Authorize(Roles = "Admin,Operator,User")]
        public IEnumerable<Region> GetRegions()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Region>(
                    @"USE [tick]
                    SELECT[rgn_Id]
                          ,[rgn_Nome]
                      FROM [tick].[dbo].[Regione]
                    "
                );
            }
        }
        [Authorize(Roles = "Admin,Operator")]
        public Region GetRegion(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Region>(
                    @"USE [tick]
                    SELECT[rgn_Id]
                          ,[rgn_Nome]
                      FROM [tick].[dbo].[Regione]
                    WHERE [rgn_Id]=@Id
               ", new { Id = id });
            }
        }
        [Authorize(Roles = "Admin,Operator")]
        public IEnumerable<Impianto> GetImpianti()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Impianto>(
                    @"USE [tick]
                    SELECT[ipt_Id]
                          ,[ipt_PosizioneLat]
                          ,[ipt_PosizioneLong]
                          ,[ipt_PotenzaNominale]
                          ,[ipt_RagioneSociale]
                          ,[ipt_Cliente]
                          ,[ipt_Torcia]
                          ,[ipt_Separatore]
                          ,[ipt_Soffiante]
                          ,[ipt_Pompa]
                          ,[ipt_Regione]
                      FROM [tick].[dbo].[Impianto]
                    "
                );
            }
        }
        [Authorize(Roles = "Admin,Operator")]
        public IEnumerable<Manutenzione> GetManutenzioniDaEseguire()
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Manutenzione>(
                    @"USE [tick]
                    SELECT [Manutenzione].[mtz_Id]
                          ,[Manutenzione].[mtz_Scadenza]
                          ,[Manutenzione].[mtz_Addetto]
                          ,[Manutenzione].[mtz_Data]
                          ,[Manutenzione].[mtz_OreLavoro]
                          ,[Manutenzione].[mtz_Impianto]
                          ,[Manutenzione].[mtz_Effettuato]
                          ,[Manutenzione].[mtz_Descrizione]
                          ,[Impianto].[ipt_RagioneSociale]
                      FROM [tick].[dbo].[Manutenzione]
                      INNER JOIN        [Impianto]
                      ON                [Impianto].[ipt_Id]=[Manutenzione].[mtz_Impianto]
                      WHERE             [mtz_Effettuato]=0
                      ORDER BY          [mtz_Scadenza]
                    "
                );
            }
        }//ordinate per scadenza
        [Authorize(Roles = "Admin,Operator")]
        public IEnumerable<Manutenzione> GetManutenzioniStorico()
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Manutenzione>(
                    @"USE [tick]
                     SELECT[Manutenzione].[mtz_Id]
                          ,[Manutenzione].[mtz_Scadenza]
                          ,[Manutenzione].[mtz_Addetto]
                          ,[Manutenzione].[mtz_Data]
                          ,[Manutenzione].[mtz_OreLavoro]
                          ,[Manutenzione].[mtz_Impianto]
                          ,[Manutenzione].[mtz_Effettuato]
                          ,[Manutenzione].[mtz_Descrizione]
                          ,[Impianto].[ipt_RagioneSociale]
                     FROM [tick].[dbo].[Manutenzione]
                     INNER JOIN        [Impianto]
                     ON                [Impianto].[ipt_Id]=[Manutenzione].[mtz_Impianto]
                     WHERE             [mtz_Effettuato]=1
                     ORDER BY          [mtz_Scadenza]
                    "
                );
            }
        }//storico delle manutenzioni
        [Authorize(Roles = "Admin,Operator,User")]
        public void DeleteManutenzione(int id)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute(@"
                DELETE from Manutenzione 
                WHERE mtz_Id = @Id", new { Id = id });
            }
        }
        [Authorize(Roles = "Admin,Operator,User")]
        public void InsertManutenzione(Manutenzione m)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute(@"USE[tick]
                  INSERT INTO [dbo].[Manutenzione]
                           ([mtz_Scadenza]
                           ,[mtz_Addetto]
                           ,[mtz_Data]
                           ,[mtz_OreLavoro]
                           ,[mtz_Impianto]
                           ,[mtz_Effettuato]
                           ,[mtz_Descrizione])
                     VALUES
                           (@mtz_Scadenza
                           ,@mtz_Addetto
                           ,@mtz_Data
                           ,@mtz_OreLavoro
                           ,@mtz_Impianto
                           ,@mtz_Effettuato
                           ,@mtz_Descrizione)", m);
            }
        }
        [Authorize(Roles = "Admin,Operator,User")]
        public void UpdateManutenzione(Manutenzione m)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    connection.Execute(@"
                USE [tick]
                UPDATE [dbo].[Manutenzione]
                   SET [mtz_Scadenza] = @mtz_Scadenza
                      ,[mtz_Addetto] = @mtz_Addetto
                      ,[mtz_Data] = @mtz_Data
                      ,[mtz_OreLavoro] = @mtz_OreLavoro
                      ,[mtz_Impianto] = @mtz_Impianto
                      ,[mtz_Effettuato] = @mtz_Effettuato
                      ,[mtz_Descrizione] =@mtz_Descrizione
                WHERE mtz_Id = @mtz_Id", m);
                }
                catch (Exception e )
                {
                    Console.WriteLine("Manutenzione fallita->" + "Data:" + m.mtz_Data+"Scadenza:"+m.mtz_Scadenza+e.ToString());
                }
            }
        }
        [Authorize(Roles = "Admin,Operator")]
        public void UpdateImpianto(Impianto i)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(@"
                USE [tick]
                UPDATE [dbo].[Impianto]
                   SET [ipt_PosizioneLat] = @ipt_PosizioneLat
                      ,[ipt_PosizioneLong] = @ipt_PosizioneLong
                      ,[ipt_PotenzaNominale] = @ipt_PotenzaNominale
                      ,[ipt_RagioneSociale] = @ipt_RagioneSociale
                      ,[ipt_Cliente] = @ipt_Cliente
                      ,[ipt_Torcia] = @ipt_Torcia
                      ,[ipt_Separatore] =@ipt_Separatore
                      ,[ipt_Soffiante] = @ipt_Soffiante
                      ,[ipt_Pompa] = @ipt_Pompa
                      ,[ipt_Regione] = @ipt_Regione
                WHERE ipt_Id = @ipt_Id", i);
            }
        }
        [Authorize(Roles = "User")]
        public IEnumerable<Manutenzione> GetManutenzioniDaEseguireByCliente(int IdEsterno)//passo l'id dell'utente corrente presente su entrambi i db(non l'id del db locale)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Manutenzione>(
                    @"USE [tick]
                    SELECT   [Manutenzione].[mtz_Id]
		                    ,[Manutenzione].[mtz_Scadenza]
		                    ,[Manutenzione].[mtz_Addetto]
		                    ,[Manutenzione].[mtz_Data]
		                    ,[Manutenzione].[mtz_OreLavoro]
		                    ,[Manutenzione].[mtz_Impianto]
		                    ,[Manutenzione].[mtz_Effettuato]
		                    ,[Manutenzione].[mtz_Descrizione]
		                    ,[Impianto].[ipt_RagioneSociale]
                      FROM [tick].[dbo].[Manutenzione]
                      INNER JOIN        [Impianto]
                      ON                [Impianto].[ipt_Id]=[Manutenzione].[mtz_Impianto]
					  INNER JOIN        [Cliente]
					  ON				[Cliente].[clt_Id]=[Impianto].[ipt_Cliente]
                      WHERE             [mtz_Effettuato]=0 AND [Cliente].[clt_Id]=@Id
                      ORDER BY          [mtz_Scadenza]
                    ", new { Id = IdEsterno }
                );
            }
        }
        [Authorize(Roles = "User")]
        public IEnumerable<Manutenzione> GetManutenzioniStoricoByCliente(int IdEsterno)//passo l'id dell'utente corrente presente su entrambi i db(non l'id del db locale)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Manutenzione>(
                    @"USE [tick]
                    SELECT   [Manutenzione].[mtz_Id]
		                    ,[Manutenzione].[mtz_Scadenza]
		                    ,[Manutenzione].[mtz_Addetto]
		                    ,[Manutenzione].[mtz_Data]
		                    ,[Manutenzione].[mtz_OreLavoro]
		                    ,[Manutenzione].[mtz_Impianto]
		                    ,[Manutenzione].[mtz_Effettuato]
		                    ,[Manutenzione].[mtz_Descrizione]
		                    ,[Impianto].[ipt_RagioneSociale]
                      FROM [tick].[dbo].[Manutenzione]
                      INNER JOIN        [Impianto]
                      ON                [Impianto].[ipt_Id]=[Manutenzione].[mtz_Impianto]
					  INNER JOIN        [Cliente]
					  ON				[Cliente].[clt_Id]=[Impianto].[ipt_Cliente]
                      WHERE             [mtz_Effettuato]=1 AND [Cliente].[clt_Id]=@Id
                      ORDER BY          [mtz_Data]
                    ", new { Id = IdEsterno }
                );
            }
        }
        [Authorize(Roles = "Operator,Admin,User")]
        [HttpGet]
        public Impianto GetImpiantoById(int ipt_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Impianto>(
                    @"USE [tick]
                    SELECT           [Impianto].[ipt_Id]
                                    ,[Impianto].[ipt_PosizioneLat]
                                    ,[Impianto].[ipt_PosizioneLong]
                                    ,[Impianto].[ipt_PotenzaNominale]
                                    ,[Impianto].[ipt_RagioneSociale]
                                    ,[Impianto].[ipt_Cliente]
                                    ,[Impianto].[ipt_Torcia]
                                    ,[Impianto].[ipt_Separatore]
                                    ,[Impianto].[ipt_Soffiante]
                                    ,[Impianto].[ipt_Pompa]
                                    ,[Impianto].[ipt_Regione]
                              FROM [tick].[dbo].[Impianto]
                              WHERE[Impianto].ipt_Id=@id
                    ", new { id = ipt_Id }
                );

            }
        }
        [Authorize(Roles = "Admin,Operator,User")]
        public IEnumerable<Cogeneratore> GetCogeneratoreByImpianto(int ipt_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Cogeneratore>(
                    @"USE [tick]
                    SELECT           [Cogeneratore].[cgn_Id]
                                    ,[Cogeneratore].[cgn_Potenza]
                                    ,[Cogeneratore].[cgn_Marca]
                                    ,[Cogeneratore].[cgn_Modello]
                                    ,[Cogeneratore].[cgn_Serie]
                                    ,[Cogeneratore].[cgn_Impianto]
                              FROM [tick].[dbo].[Cogeneratore]
                              INNER JOIN [Impianto] I
                              ON I.ipt_Id =[Cogeneratore].cgn_Impianto
                              WHERE[Cogeneratore].cgn_Impianto=@id
                    ", new { id = ipt_Id }
                );

            }
        }
        public IEnumerable<Generico> GetGenericoByImpianto(int ipt_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Generico>(
                    @"USE [tick]     
                      SELECT         [Generico].[gnr_Id]
                                    ,[Generico].[gnr_Nome]
                                    ,[Generico].[gnr_Marca]
                                    ,[Generico].[gnr_Modello]
                                    ,[Generico].[gnr_Serie]
                                    ,[Generico].[gnr_Descrizione]
                                    ,[Generico].[gnr_UltimaInstallazione]
                                    ,[Generico].[gnr_UltimoIntervento]
                                    ,[Generico].[gnr_OreUltimoIntervento]
                                    ,[Generico].[gnr_Impianto]
                                    ,[Generico].[gnr_Rimosso]
                              FROM [tick].[dbo].[Generico]
                              INNER JOIN [Impianto] I
                              ON I.ipt_Id =[Generico].gnr_Impianto
                              WHERE[Generico].gnr_Impianto=@id
                    ", new { id = ipt_Id }
                );

            }
        }
        [Authorize(Roles = "Admin,Operator,User")]
        public IEnumerable<Vasca> GetVascheByImpianto(int ipt_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Vasca>(
                    @"USE [tick]
                    SELECT           [Vasca].[vsc_Id]
                                    ,[Vasca].[vsc_Altezza]
                                    ,[Vasca].[vsc_Nome]
                                    ,[Vasca].[vsc_Coperta]
                                    ,[Vasca].[vsc_Riscaldata]
                                    ,[Vasca].[vsc_Recupero]
                                    ,[Vasca].[vsc_Interrata]
									,[Vasca].[vsc_Interramento]
                                    ,[Vasca].[vsc_NSoffiantine]
                                    ,[Vasca].[vsc_Diametro]
                                    ,[Vasca].[vsc_Impianto]
                              FROM [tick].[dbo].[Vasca]
                              INNER JOIN [Impianto] I
                              ON I.ipt_Id =[Vasca].vsc_Impianto
                              WHERE[Vasca].vsc_Impianto=@id
                    ", new { id = ipt_Id }
                );

            }
        }
        [Authorize(Roles = "Admin,Operator,User")]
        public IEnumerable<Sensore> GetSensoriByVasche(int vsc_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Sensore>(
                    @"USE [tick]        
                    SELECT			 [Sensore].[ssr_Id]
                                    ,[Sensore].[ssr_Modello]
                                    ,[Sensore].[ssr_Marca]
                                    ,[Sensore].[ssr_Serie]
                                    ,[Sensore].[ssr_UltimaInstallazione]
                                    ,[Sensore].[ssr_Vasca]
                                    ,[Sensore].[ssr_Nome]
                              FROM [tick].[dbo].[Sensore]
                              INNER JOIN Vasca V
                              ON V.vsc_Id =[Sensore].ssr_Vasca
                              WHERE[Sensore].ssr_Vasca=@id
                    ", new { id = vsc_Id }
                );

            }
        }
        [Authorize(Roles = "Admin,Operator,User")]
        public IEnumerable<Agitatore> GetAgitatoriByVasche(int vsc_Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Agitatore>(
                    @"USE [tick]
                    SELECT			 [Agitatore].[agt_Id]
                                    ,[Agitatore].[agt_Nome]
                                    ,[Agitatore].[agt_UltimaInstallazione]
                                    ,[Agitatore].[agt_UltimoIntervento]
                                    ,[Agitatore].[agt_OreUltimoIntervento]
                                    ,[Agitatore].[agt_Marca]
                                    ,[Agitatore].[agt_Modello]
									,[Agitatore].[agt_SerialNumber]
                                    ,[Agitatore].[agt_Rimosso]
                                    ,[Agitatore].[agt_Vasca]
                              FROM [tick].[dbo].[Agitatore]
                              INNER JOIN Vasca V
                              ON V.vsc_Id =[Agitatore].agt_Vasca
                              WHERE[Agitatore].agt_Vasca=@id
                    ", new { id = vsc_Id }
                );

            }
        }
        public void InsertImpianto(Impianto i)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute(@"USE[tick]
                  INSERT INTO [dbo].[Impianto]
                           ([ipt_PosizioneLat]
                           ,[ipt_PosizioneLong]
                           ,[ipt_PotenzaNominale]
                           ,[ipt_RagioneSociale]
                           ,[ipt_Cliente]
                           ,[ipt_Torcia]
                           ,[ipt_Separatore]
                           ,[ipt_Soffiante]
                           ,[ipt_Pompa]
                           ,[ipt_Regione])
                     VALUES
                           (@ipt_PosizioneLat
                           ,@ipt_PosizioneLong
                           ,@ipt_PotenzaNominale
                           ,@ipt_RagioneSociale
                           ,@ipt_Cliente
                           ,@ipt_Torcia
                           ,@ipt_Separatore
                           ,@ipt_Soffiante
                           ,@ipt_Pompa
                           ,@ipt_Regione)", i);
            }
        }
        public void InsertGenerico(Generico g)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute(@"USE[tick]
                  INSERT INTO [dbo].[Generico]
                           ([gnr_Nome]
                           ,[gnr_Marca]
                           ,[gnr_Modello]
                           ,[gnr_Serie]
                           ,[gnr_Descrizione]
                           ,[gnr_UltimaInstallazione]
                           ,[gnr_UltimoIntervento]
                           ,[gnr_OreUltimoIntervento]
                           ,[gnr_Impianto]
                           ,[gnr_Rimosso])
                     VALUES
                           (@gnr_Nome
                           ,@gnr_Marca
                           ,@gnr_Modello
                           ,@gnr_Serie
                           ,@gnr_Descrizione
                           ,@gnr_UltimaInstallazione
                           ,@gnr_UltimoIntervento
                           ,@gnr_OreUltimoIntervento
                           ,@gnr_Impianto
                           ,@gnr_Rimosso)", g);
            }
        }
        public void DeleteImpianto(int ipt_Id)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute(@"
                DELETE from Impianto 
                WHERE ipt_Id = @Id", new { Id = ipt_Id });
            }
        }
        public void DeleteGenerico(int gnr_Id)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute(@"
                DELETE from Generico 
                WHERE gnr_Id = @Id", new { Id = gnr_Id });
            }
        }
        public void InsertCogeneratore(Cogeneratore c)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute(@"USE[tick]
                  INSERT INTO [dbo].[Cogeneratore]
                           ([cgn_Potenza]
                           ,[cgn_Marca]
                           ,[cgn_Modello]
                           ,[cgn_Serie]
                           ,[cgn_Impianto])
                     VALUES
                           (@cgn_Potenza
                           ,@cgn_Marca
                           ,@cgn_Modello
                           ,@cgn_Serie
                           ,@cgn_Impianto)", c);
            }
        }
        public void DeleteCogeneratore(int cgn_Id)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();

                    connection.Execute(@"
                        DELETE from Cogeneratore 
                        WHERE cgn_Id = @Id", new { Id = cgn_Id });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public void InsertVasca(Vasca v)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute(@"USE[tick]
                  INSERT INTO [dbo].[Vasca]
                           ([vsc_Altezza]
                           ,[vsc_Nome]
                           ,[vsc_Coperta]
                           ,[vsc_Interrata]
                           ,[vsc_Riscaldata]
                           ,[vsc_Recupero]
                           ,[vsc_Interramento]
                           ,[vsc_NSoffiantine]
                           ,[vsc_Diametro]
                           ,[vsc_Impianto])
                     VALUES
                           (@vsc_Altezza
                            ,@vsc_Nome
                            ,@vsc_Coperta
                            ,@vsc_Interrata
                            ,@vsc_Riscaldata
                            ,@vsc_Recupero
                            ,@vsc_Interramento
                            ,@vsc_NSoffiantine
                            ,@vsc_Diametro
                            ,@vsc_Impianto)", v);
            }
        }
        public void DeleteVasca(int vsc_Id)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();

                    connection.Execute(@"
                        DELETE from Vasca 
                        WHERE vsc_Id = @Id", new { Id = vsc_Id });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public void InsertSensore(Sensore s)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute(@"USE[tick]
                  INSERT INTO [dbo].[Sensore]
                           ([ssr_Modello]
                           ,[ssr_Marca]
                           ,[ssr_Serie]
                           ,[ssr_Nome]
                           ,[ssr_UltimaInstallazione]
                           ,[ssr_Vasca])
                     VALUES
                           (@ssr_Modello
                            ,@ssr_Marca
                            ,@ssr_Serie
                            ,@ssr_Nome
                            ,@ssr_UltimaInstallazione
                            ,@ssr_Vasca)", s);
            }
        }
        public void DeleteSensore(int ssr_Id)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();

                    connection.Execute(@"
                        DELETE from Sensore 
                        WHERE ssr_Id = @Id", new { Id = ssr_Id });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public void InsertCliente(Client c)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute(@"USE[tick]
                  INSERT INTO [dbo].[Cliente]
                           ([clt_RagioneSociale]
                           ,[clt_Indirizzo]
                           ,[clt_Mail]
                           ,[clt_Telefono]
                           ,[clt_Mobile])
                     VALUES
                           (@clt_RagioneSociale
                            ,@clt_Indirizzo
                            ,@clt_Mail
                            ,@clt_Telefono
                            ,@clt_Mobile)", c);
            }
        }
        public void DeleteCliente(int clt_Id)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();

                    connection.Execute(@"
                        DELETE from Cliente 
                        WHERE clt_Id = @Id", new { Id = clt_Id });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public void InsertAgitatore(Agitatore a)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute(@"USE[tick]
                  INSERT INTO [dbo].[Agitatore]
                           ([agt_UltimaInstallazione]
                           ,[agt_Nome]
                           ,[agt_UltimoIntervento]
                           ,[agt_OreUltimoIntervento]
                           ,[agt_Marca]
                           ,[agt_Modello]
                           ,[agt_SerialNumber]
                           ,[agt_Rimosso]
                           ,[agt_Vasca])
                     VALUES
                           (@agt_UltimaInstallazione
                            ,@agt_Nome
                            ,@agt_UltimoIntervento
                            ,@agt_OreUltimoIntervento
                            ,@agt_Marca
                            ,@agt_Modello
                            ,@agt_SerialNumber
                            ,@agt_Rimosso
                            ,@agt_Vasca)", a);
            }
        }
        public void DeleteAgitatore(int agt_Id)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();

                    connection.Execute(@"
                        DELETE from Agitatore 
                        WHERE agt_Id = @Id", new { Id = agt_Id });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        [Authorize(Roles = "Admin,Operator")]
        public void UpdateAgitatore(Agitatore a)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(@"
                USE [tick]
                UPDATE [dbo].[Agitatore]
                   SET [agt_UltimaInstallazione] = @agt_UltimaInstallazione
                      ,[agt_UltimoIntervento] = @agt_UltimoIntervento
                      ,[agt_OreUltimoIntervento] = @agt_OreUltimoIntervento
                      ,[agt_Marca] = @agt_Marca
                      ,[agt_Modello] = @agt_Modello
                      ,[agt_SerialNumber] = @agt_SerialNumber
                      ,[agt_Rimosso] =@agt_Rimosso
                      ,[agt_Vasca] = @agt_Vasca
                      ,[agt_Nome] = @agt_Nome
                WHERE agt_Id = @agt_Id", a);
            }
        }
        public void UpdateCliente(Client c)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(@"
                USE [tick]
                UPDATE [dbo].[Cliente]
                   SET [clt_RagioneSociale] = @clt_RagioneSociale
                      ,[clt_Indirizzo] = @clt_Indirizzo
                      ,[clt_Mail] = @clt_Mail
                      ,[clt_Telefono] = @clt_Telefono
                      ,[clt_Mobile] = @clt_Mobile
                WHERE clt_Id = @clt_Id", c);
            }
        }
        public void UpdateGenerico(Generico g)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(@"
                USE [tick]
                UPDATE [dbo].[Generico]
                   SET [gnr_Nome] = @gnr_Nome
                      ,[gnr_Marca] = @gnr_Marca
                      ,[gnr_Modello] = @gnr_Modello
                      ,[gnr_Serie] = @gnr_Serie
                      ,[gnr_Descrizione] = @gnr_Descrizione
                      ,[gnr_UltimaInstallazione] = @gnr_UltimaInstallazione
                      ,[gnr_UltimoIntervento] =@gnr_UltimoIntervento
                      ,[gnr_OreUltimoIntervento] = @gnr_OreUltimoIntervento
                      ,[gnr_Impianto] = @gnr_Impianto
                      ,[gnr_Rimosso] = @gnr_Rimosso
                WHERE gnr_Id = @gnr_Id", g);
            }
        }
        public Agitatore GetAgitatore(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Agitatore>(
                    @"USE [tick]
                SELECT [agt_Id]
                      ,[agt_UltimaInstallazione]
                      ,[agt_UltimoIntervento]
                      ,[agt_OreUltimoIntervento]
                      ,[agt_Marca]
                      ,[agt_Modello]
                      ,[agt_SerialNumber]
                      ,[agt_Rimosso]
                      ,[agt_Vasca]
                      ,[agt_Nome]
                  FROM [dbo].[Agitatore]
                  WHERE [agt_Id]=@Id
               ", new { Id = id });
            }
        }
        public Generico GetGenerico(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Generico>(
                    @"USE [tick]
                SELECT [gnr_Id]
                      ,[gnr_UltimaInstallazione]
                      ,[gnr_UltimoIntervento]
                      ,[gnr_OreUltimoIntervento]
                      ,[gnr_Marca]
                      ,[gnr_Modello]
                      ,[gnr_Serie]
                      ,[gnr_Descrizione]
                      ,[gnr_Rimosso]
                      ,[gnr_Impianto]
                      ,[gnr_Nome]
                  FROM [dbo].[Generico]
                  WHERE [gnr_Id]=@Id
               ", new { Id = id });
            }
        }
        public void UpdateSensore(Sensore s)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(@"
                USE [tick]
                UPDATE [dbo].[Sensore]
                   SET [ssr_Nome]=@ssr_Nome
                      ,[ssr_Modello] = @ssr_Modello
                      ,[ssr_Marca] = @ssr_Marca
                      ,[ssr_Serie] = @ssr_Serie
                      ,[ssr_UltimaInstallazione] = @ssr_UltimaInstallazione
                      ,[ssr_Vasca] = @ssr_Vasca
                WHERE ssr_Id = @ssr_Id", s);
            }
        }

        public Sensore GetSensore(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Sensore>(
                    @"USE [tick]
                    SELECT   [ssr_Id]
                            ,[ssr_Nome]
                            ,[ssr_Modello]
                            ,[ssr_Marca]
                            ,[ssr_Serie]
                            ,[ssr_UltimaInstallazione]
                            ,[ssr_Vasca]
                    FROM [dbo].[Sensore]
                    WHERE [ssr_Id]=@Id
                ", new { Id = id });
            }
        }

        public Vasca GetVasca(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Vasca>(
                    @"USE [tick]
                    SELECT [vsc_Id]
                           ,[vsc_Altezza]
                           ,[vsc_Coperta]
                           ,[vsc_Riscaldata]
                           ,[vsc_Recupero]
                           ,[vsc_Interrata]
                           ,[vsc_Interramento]
                           ,[vsc_NSoffiantine]
                           ,[vsc_Diametro]
                           ,[vsc_Impianto]
                            ,[vsc_Nome]
                            FROM [dbo].[Vasca]
                            WHERE [vsc_Id]=@Id", new { Id = id });
            }
        }

        public void UpdateVasca(Vasca v)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(@"
                USE [tick]
                UPDATE [dbo].[Vasca]
                   SET [vsc_Nome]=@vsc_Nome
                           ,[vsc_Altezza]=@vsc_Altezza
                           ,[vsc_Coperta]=@vsc_Coperta
                           ,[vsc_Riscaldata]=@vsc_Riscaldata
                           ,[vsc_Recupero]=@vsc_Recupero
                           ,[vsc_Interrata]=@vsc_Interrata
                           ,[vsc_Interramento]=@vsc_Interramento
                           ,[vsc_NSoffiantine]=@vsc_NSoffiantine
                           ,[vsc_Diametro]=@vsc_Diametro
                           ,[vsc_Impianto]=@vsc_Impianto
                WHERE [vsc_Id] = @vsc_Id", v);
            }
        }

        public Cogeneratore GetCogeneratore(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Cogeneratore>(
                    @"USE [tick]
                        SELECT [cgn_Id]
                                    ,[cgn_Potenza]
                                    ,[cgn_Marca]
                                    ,[cgn_Modello]
                                    ,[cgn_Serie]
                                    ,[cgn_Impianto]
                              FROM [dbo].[Cogeneratore]
                                WHERE [cgn_Id]=@Id", new { Id = id });
            }
        }


        public void UpdateCogeneratore(Cogeneratore c)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(@"
                        USE [tick]
                        UPDATE [dbo].[Cogeneratore]
                            SET [cgn_Potenza]=@cgn_Potenza
                                ,[cgn_Marca]=@cgn_Marca
                                ,[cgn_Modello]=@cgn_Modello
                                ,[cgn_Serie]=@cgn_Serie
                                ,[cgn_Impianto]=@cgn_Impianto
                        WHERE [cgn_Id]=@cgn_Id", c);
            }
        }
    }
}
