using WebApi.Model;
using System.Collections.Generic;
using WebApi.Model.Base;
using FAndradeTecInfo.Utils.Model;
using Tapioca.HATEOAS.Utils;

namespace WebApi.Repository
{
    public interface IViewRepository<T> where T : BaseView
    {
        List<T> FindMovieCount(enMovieCount order, bool isAscending);

        PagedSearchDTO<T> FindPagedSearch(string view, ref Dictionary<string, object> filters, int pageSize, int page, enMovieCount order, bool isAscending);

        int GetCount(string query);
    }

    public interface IViewMovieByRepository<T> where T : BaseViewMovieBy
    {
        List<T> FindMovieById(long id, enMovieCount order);
        List<T> FindMovieByName(string name, enMovieCount order, bool isAscending);

        PagedSearchDTO<T> FindPagedSearch(string view, ref Dictionary<string, object> filters, int pageSize, int page, enMovieCount order, bool isAscending);

        int GetCount(string query);
    }


}
