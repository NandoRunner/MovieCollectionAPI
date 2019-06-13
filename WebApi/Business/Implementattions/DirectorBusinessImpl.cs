﻿using System.Collections.Generic;
using WebApi.Model;
using System.Threading;
using WebApi.Model.Context;
using System;
using System.Linq;
using WebApi.Repository;
using WebApi.Repository.Generic;
using WebApi.Data.Converters;
using WebApi.Data.VO;

namespace WebApi.Business.Implementattions
{
    public class DirectorBusinessImpl : IDirectorBusiness
    {
        private readonly IRepository<Director> _repository;

        private readonly DirectorConverter _converter;
		
        private readonly IViewRepository<_vw_mc_diretor> _vrep;

        public DirectorBusinessImpl(IRepository<Director> repository, IViewRepository<_vw_mc_diretor> vrep)
        {
            _repository = repository;
            _converter = new DirectorConverter();
            _vrep = vrep;
        }

        public DirectorVO Create(DirectorVO item)
        {
            var ent = _converter.Parse(item);
            ent = _repository.Create(ent);
            return _converter.Parse(ent);
        }

        public DirectorVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public List<DirectorVO> FindByName(string name)
        {
            return _converter.ParseList(_repository.FindByName(name));
        }

        public DirectorVO FindByExactName(string name)
        {
            return _converter.Parse(_repository.FindByExactName(name));
        }

        public List<DirectorVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public DirectorVO Update(DirectorVO item)
        {
            var ent = _converter.Parse(item);
            ent = _repository.Update(ent);
            return _converter.Parse(ent);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<_vw_mc_diretor> FindMovieCount(enMovieCount order)
        {
            return _vrep.FindMovieCount(order);
        }
    }
}