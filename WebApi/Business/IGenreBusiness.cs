﻿using System.Collections.Generic;
using WebApi.Data.VO;
using FAndradeTI.Util.Database.Model;
using WebApi.Model;

namespace WebApi.Business
{
    public interface IGenreBusiness
    {
        GenreVO Create(GenreVO item);
        GenreVO FindById(long id);
        List<GenreVO> FindByName(string name);
		GenreVO FindByExactName(string name);
        List<GenreVO> FindAll();

        GenreVO Update(GenreVO item);
        void Delete(long id);

        List<_vw_mc_genero> FindMovieCount(MovieField order, bool isAscending);
        List<_vw_mc_filme_por_genero> FindMovieBy(long id, MovieField order);
		List<_vw_mc_filme_por_genero> FindMovieByName(string name, MovieField order, bool isAscending);
    }
}
