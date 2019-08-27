using System.Collections.Generic;
using WebApi.Model;
using System.Threading;
using WebApi.Model.Context;
using System;
using System.Linq;
using WebApi.Repository;
using Tapioca.HATEOAS.Utils;
using System.Data.SqlClient;

namespace WebApi.Business.Implementattions
{
    public class MovieBusinessImpl : IMovieBusiness
    {

        private IMovieRepository _repository;

        public MovieBusinessImpl(IMovieRepository repository)
        {
            _repository = repository;
        }

        public Movie Create(Movie movie)
        {
            return _repository.Create(movie);
        }


        public Movie FindById(long id)
        {
            return _repository.FindById(id);
        }

        public List<Movie> FindByName(string name)
        {
            return _repository.FindByName(name);
        }

        public List<Movie> FindAll()
        {
			return _repository.FindAll();
        }

        public List<_vw_mc_filme_visto> FindWatched(enMovieCount order, bool isAscending)
        {
            return _repository.FindWatched(order, isAscending);
        }

        public List<_vw_mc_filme_ver> FindAvailable(enMovieCount order, bool isAscending)
        {
            return _repository.FindAvailable(order, isAscending);
        }

        public PagedSearchDTO<Movie> FindWithPagedSearch(string name, int pageSize, int page, bool isAscending)
        {
            var sort = isAscending ? "asc" : "desc";

            var query = MontarQuery("movies", name, sort, pageSize, page, enMovieCount.title);

            var countQuery = MontarCountQuery("movies", name);

            var pagedResults = _repository.FindWithPagedSearch(query);

            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchDTO<Movie>
            {
                CurrentPage = page + 1,
                List = pagedResults,
                PageSize = pageSize,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }



        public PagedSearchDTO<_vw_mc_filme_visto> FindWatchedPagedSearch(string name, int pageSize, int page, enMovieCount order, bool isAscending)
        {
            var sort = isAscending ? "asc" : "desc";

            var query = MontarQuery("vw_mc_filme_visto", name, sort, pageSize, page, order);

            var countQuery = MontarCountQuery("vw_mc_filme_visto", name);

            var pagedResults = _repository.FindWatchedPagedSearch(query);

            //int totalResults = _repository.GetCountWatched();
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchDTO<_vw_mc_filme_visto>
            {
                CurrentPage = page + 1,
                List = pagedResults,
                PageSize = pageSize,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public PagedSearchDTO<_vw_mc_filme_ver> FindAvailablePagedSearch(string name, int pageSize, int page, enMovieCount order, bool isAscending)
        {
            var sort = isAscending ? "asc" : "desc";

            var query = MontarQuery("vw_mc_filme_ver", name, sort, pageSize, page, order);

            var countQuery = MontarCountQuery("vw_mc_filme_ver", name);

            var pagedResults = _repository.FindAvailablePagedSearch(query);

            //int totalResults = _repository.GetCountAvailable();
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchDTO<_vw_mc_filme_ver>
            {
                CurrentPage = page + 1,
                List = pagedResults,
                PageSize = pageSize,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        private string MontarQuery(string tabela, string name, string sortDirection, int pageSize, int page, enMovieCount order)
        {
            //page = page > 0 ? page - 1 : 0;

            page = page < 1 ? 0 : (page - 1) * pageSize;

            string query = $"select * from {tabela} p where 1 = 1 ";
            if (!string.IsNullOrEmpty(name)) query = query + $" and p.titulo like '%{name}%'";

            string orderField;

            if (order == enMovieCount.rating)
                orderField = "p.rating";
            else if (order == enMovieCount.periodo)
                orderField = "p.periodo";
            else
                orderField = "p.titulo";

            return query + $" order by {orderField} {sortDirection} limit {pageSize} offset {page}";
        }

        private string MontarCountQuery(string tabela, string name)
        {
            string countQuery = $"select count(*) from {tabela} p where 1 = 1 ";
            if (!string.IsNullOrEmpty(name)) countQuery = countQuery + $" and p.titulo like '%{name}%'";

            return countQuery;
        }
    }
}
