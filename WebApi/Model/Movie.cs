﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model.Base;

namespace WebApi.Model
{
    public class Movie
    {
        public long? Id { get; set; }
        public string titulo { get; set; }
        public string titulo_original { get; set; }
               
        public long ano { get; set; }
        public decimal rating { get; set; }

        public long diretor_id { get; set; }

        public string visto { get; set; }

        public DateTime? periodo { get; set; }
        public DateTime update { get; set; }
    }

    public class _vw_mc_filme_ver : BaseViewMovie { }

    public class _vw_mc_filme_visto : BaseViewMovie
    {
        public DateTime periodo { get; set; }

        public string periodo_exibe { get; set; }
    }


}
