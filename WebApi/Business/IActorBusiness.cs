using System.Collections.Generic;
using WebApi.Data.VO;
using WebApi.Model;
using Tapioca.HATEOAS.Utils;
using FAndradeTI.Util.Database.Model;

namespace WebApi.Business
{
    public interface IActorBusiness
    {
        ActorVO Create(ActorVO item);
        ActorVO FindById(long id);
        List<ActorVO> FindByName(string name);
		ActorVO FindByExactName(string name);
        List<ActorVO> FindAll();

        ActorVO Update(ActorVO item);
        void Delete(long id);

        List<_vw_mc_ator> FindMovieCount(MovieField order, bool isAscending);
		List<_vw_mc_filme_por_ator> FindMovieById(long id, MovieField order);
		List<_vw_mc_filme_por_ator> FindMovieByName(string name, MovieField order, bool isAscending);

        PagedSearchDTO<_vw_mc_ator> FindMovieCountPagedSearch(string name, int pageSize, int page, MovieField order, bool isAscending);
        PagedSearchDTO<_vw_mc_filme_por_ator> FindMovieByIdPagedSearch(long id, int pageSize, int page, MovieField order, bool isAscending);
        PagedSearchDTO<_vw_mc_filme_por_ator> FindMovieByNamePagedSearch(string name, int pageSize, int page, MovieField order, bool isAscending);


    }
}
