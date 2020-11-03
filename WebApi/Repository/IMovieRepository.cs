using WebApi.Model;
using System.Collections.Generic;
using FAndradeTI.Util.Database.Model;

namespace WebApi.Repository
{
    public interface IMovieRepository
    {
        Movie Create(Movie movie);
        Movie FindById(long id);
        List<Movie> FindByName(string name);
        List<Movie> FindAll();

        List<_vw_mc_filme_visto> FindWatched(MovieField order, bool isAscending);
        List<_vw_mc_filme_ver> FindAvailable(MovieField order, bool isAscending);

        List<Movie> FindWithPagedSearch(string query);
        List<_vw_mc_filme_visto> FindWatchedPagedSearch(string query);
        List<_vw_mc_filme_ver> FindAvailablePagedSearch(string query);

        int GetCount(string query);
    }
}
