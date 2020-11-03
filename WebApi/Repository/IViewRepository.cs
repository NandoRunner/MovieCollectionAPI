using WebApi.Model;
using System.Collections.Generic;
using WebApi.Model.Base;
using FAndradeTI.Util.Database.Model;
using Tapioca.HATEOAS.Utils;

namespace WebApi.Repository
{
    public interface IViewRepository<T> where T : BaseView
    {
        List<T> FindMovieCount(MovieField order, bool isAscending);

        PagedSearchDTO<T> FindPagedSearch(string view, ref Dictionary<string, object> filters, int pageSize, int page, MovieField order, bool isAscending);

        int GetCount(string query);
    }

    public interface IViewMovieByRepository<T> where T : BaseViewMovieBy
    {
        List<T> FindMovieById(long id, MovieField order);
        List<T> FindMovieByName(string name, MovieField order, bool isAscending);

        PagedSearchDTO<T> FindPagedSearch(string view, ref Dictionary<string, object> filters, int pageSize, int page, MovieField order, bool isAscending);

        int GetCount(string query);
    }


}
