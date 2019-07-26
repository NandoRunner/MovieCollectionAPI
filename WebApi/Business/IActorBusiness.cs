using System.Collections.Generic;
using WebApi.Data.VO;
using WebApi.Model;

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

        List<_vw_mc_ator> FindMovieCount(enMovieCount order, bool isAscending);
		List<_vw_mc_filme_por_ator> FindMovieBy(long id, enMovieCount order);
		List<_vw_mc_filme_por_ator> FindMovieByName(string name, enMovieCount order);
    }
}
