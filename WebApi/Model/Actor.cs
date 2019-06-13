﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Model.Base;

namespace WebApi.Model
{
    [Table("mc_ato_ator")]
    public class Actor : BaseEntity { }
    
    public class _vw_mc_ator : BaseView { }
    
    public class _vw_mc_filme_por_ator : BaseViewMovieBy { }
}
