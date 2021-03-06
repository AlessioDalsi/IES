﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assistenza.BufDalsi.Data.Models
{
    public class Sensore
    {
        public int ssr_Id { get; set; }
        public string ssr_Nome { get; set; }
        public string ssr_Modello { get; set; }
        public string ssr_Marca { get; set; }
        public string ssr_Serie { get; set; }
        [DataType(DataType.Date)]
        public DateTime ssr_UltimaInstallazione { get; set; }
        public int ssr_Vasca { get; set; }

        public Sensore()
        {
            ssr_UltimaInstallazione= new DateTime(1900, 01, 01);
        }

        public Sensore(int id, string Nome, string Modello, string Marca, string Serie, DateTime uInst, int vsc)
        {
            ssr_Id = id;
            ssr_Nome = Nome;
            ssr_Modello = Modello;
            ssr_Marca = Marca;
            ssr_Serie = Serie;
            ssr_UltimaInstallazione = uInst;
            ssr_Vasca = vsc;
        }
    }
}
