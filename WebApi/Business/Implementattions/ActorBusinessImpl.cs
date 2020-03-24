using System.Collections.Generic;
using WebApi.Model;
using WebApi.Repository;
using WebApi.Repository.Generic;
using WebApi.Data.Converters;
using WebApi.Data.VO;
using Tapioca.HATEOAS.Utils;
using FAndradeTecInfo.Utils.Model;

namespace WebApi.Business.Implementattions
{
    public class ActorBusinessImpl : IActorBusiness
    {
        private readonly IRepository<Actor> _repository;

        private readonly ActorConverter _converter;

        private readonly IViewRepository<_vw_mc_ator> _vrep;

        private readonly IViewMovieByRepository<_vw_mc_filme_por_ator> _vmbrep;

        public ActorBusinessImpl(IRepository<Actor> repository, IViewRepository<_vw_mc_ator> vrep
			, IViewMovieByRepository<_vw_mc_filme_por_ator> vmbrep)
        {
            _repository = repository;
            _converter = new ActorConverter();
            _vrep = vrep;
            _vmbrep = vmbrep;
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

        public List<_vw_mc_ator> FindMovieCount(enMovieCount order, bool isAscending)
        {
            return _vrep.FindMovieCount(order, isAscending);
        }

        public List<_vw_mc_filme_por_ator> FindMovieById(long id, enMovieCount order)
        {
            return _vmbrep.FindMovieById(id, order);
        }
        public List<_vw_mc_filme_por_ator> FindMovieByName(string name, enMovieCount order, bool isAscending)
        {
            return _vmbrep.FindMovieByName(name, order, isAscending);
        }

        public PagedSearchDTO<_vw_mc_ator> FindMovieCountPagedSearch(string name, int pageSize, int page, enMovieCount order, bool isAscending)
        {
            var filters = new Dictionary<string, object>() {{ "nome", name } };

            return _vrep.FindPagedSearch("vw_mc_ator", ref filters, pageSize, page, order, isAscending);
        }

        public PagedSearchDTO<_vw_mc_filme_por_ator> FindMovieByIdPagedSearch(long id, int pageSize, int page, enMovieCount order, bool isAscending)
        {
            var filters = new Dictionary<string, object>() { { "id", id.ToString() } };

            return _vmbrep.FindPagedSearch("vw_mc_filme_por_ator", ref filters, pageSize, page, order, isAscending);
        }

        public PagedSearchDTO<_vw_mc_filme_por_ator> FindMovieByNamePagedSearch(string name, int pageSize, int page, enMovieCount order, bool isAscending)
        {
            var filters = new Dictionary<string, object>() { { "nome", name } };

            return _vmbrep.FindPagedSearch("vw_mc_filme_por_ator", ref filters, pageSize, page, order, isAscending);
        }

    }
}
