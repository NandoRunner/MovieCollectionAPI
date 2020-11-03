using WebApi.Model;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;
using FAndradeTI.Util.Database.Model;

namespace WebApi.Business
{
    public interface IMovieBusiness
    {
        Movie Create(Movie movie);
        Movie FindById(long id);
        List<Movie> FindByName(string name);

        List<Movie> FindAll();
        List<_vw_mc_filme_visto> FindWatched(MovieField order, bool isAscending);
        List<_vw_mc_filme_ver> FindAvailable(MovieField order, bool isAscending);


        PagedSearchDTO<Movie> FindWithPagedSearch(string name, int pageSize, int page, bool isAscending);
        PagedSearchDTO<_vw_mc_filme_visto> FindWatchedPagedSearch(string name, int pageSize, int page, MovieField order, bool isAscending);
        PagedSearchDTO<_vw_mc_filme_ver> FindAvailablePagedSearch(string name, int pageSize, int page, MovieField order, bool isAscending);


    }
}
