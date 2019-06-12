using System.Collections.Generic;
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
    public class ActorBusinessImpl : IActorBusiness
    {
        private readonly IRepository<Actor> _repository;

        private readonly ActorConverter _converter;

        private readonly IViewRepository<_vw_mc_ator> _vrep;

        public ActorBusinessImpl(IRepository<Actor> repository, IViewRepository<_vw_mc_ator> vrep)
        {
            _repository = repository;
            _converter = new ActorConverter();
            _vrep = vrep;
        }

        public ActorVO Create(ActorVO item)
        {
            var ent = _converter.Parse(item);
            ent = _repository.Create(ent);
            return _converter.Parse(ent);
        }

        public ActorVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public List<ActorVO> FindByName(string name)
        {
            return _converter.ParseList(_repository.FindByName(name));
        }

        public ActorVO FindByExactName(string name)
        {
            return _converter.Parse(_repository.FindByExactName(name));
        }

        public List<ActorVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public ActorVO Update(ActorVO item)
        {
            var ent = _converter.Parse(item);
            ent = _repository.Update(ent);
            return _converter.Parse(ent);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<_vw_mc_ator> FindMovieCount(enMovieCount order)
        {
            return _vrep.FindMovieCount(order);
        }
    }
}
