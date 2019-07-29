using WebApi.Model;
using System.Collections.Generic;
using WebApi.Model.Base;

namespace WebApi.Repository
{
    public interface IViewRepository<T> where T : BaseView
    {
        List<T> FindMovieCount(enMovieCount order, bool isAscending);
    }

    public interface IViewMovieByRepository<T> where T : BaseViewMovieBy
    {
        List<T> FindMovieBy(long id, enMovieCount order);

        List<T> FindMovieByName(string name, enMovieCount order, bool isAscending);

    }


}
