using WebApi.Model;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace WebApi.Business
{
    public interface IMovieBusiness
    {
        Movie Create(Movie movie);
        Movie FindById(long id);
        List<Movie> FindByName(string name);
        List<Movie> FindAll();

        List<_vw_mc_filme_visto> FindWatched(enMovieCount order, bool isAscending);
        List<_vw_mc_filme_ver> FindAvailable(enMovieCount order, bool isAscending);


        PagedSearchDTO<Movie> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);

        PagedSearchDTO<_vw_mc_filme_visto> FindWatchedPagedSearch(string name, string sortDirection, int pageSize, int page);

        PagedSearchDTO<_vw_mc_filme_ver> FindAvailablePagedSearch(string name, string sortDirection, int pageSize, int page);


    }
}
