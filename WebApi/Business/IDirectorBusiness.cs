﻿using System.Collections.Generic;
using WebApi.Data.VO;
using WebApi.Model;

namespace WebApi.Business
{
    public interface IDirectorBusiness
    {
        DirectorVO Create(DirectorVO item);
        DirectorVO FindById(long id);
        List<DirectorVO> FindByName(string name);
		DirectorVO FindByExactName(string name);
        List<DirectorVO> FindAll();

        DirectorVO Update(DirectorVO item);
        void Delete(long id);

        List<_vw_mc_diretor> FindMovieCount(enMovieCount order);
    }
}