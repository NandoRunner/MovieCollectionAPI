﻿using System.Collections.Generic;
using WebApi.Model;
using System.Threading;
using WebApi.Model.Context;
using System;
using System.Linq;
using WebApi.Repository;
using Tapioca.HATEOAS.Utils;
using System.Data.SqlClient;
using FAndradeTecInfo.Utils;
using FAndradeTecInfo.Utils.Model;

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
            var filters = new Dictionary<string, object>() { { "titulo", name } };

            var sort = isAscending ? "asc" : "desc";
            var sortFields = string.Empty;

            var query = MyQuery.BuildSelectQuery("movies", ref filters, sort, pageSize, page, enMovieCount.title, ref sortFields);

            var countQuery = MyQuery.BuildCountQuery("movies", filters);

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
            var filters = new Dictionary<string, object>() { { "titulo", name } };

            var sort = isAscending ? "asc" : "desc";
            var sortFields = string.Empty;

            var query = MyQuery.BuildSelectQuery("vw_mc_filme_visto", ref filters, sort, pageSize, page, order, ref sortFields);

            var countQuery = MyQuery.BuildCountQuery("vw_mc_filme_visto", filters);

            var pagedResults = _repository.FindWatchedPagedSearch(query);

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
            var filters = new Dictionary<string, object>() { { "titulo", name } };

            var sort = isAscending ? "asc" : "desc";
            var sortFields = string.Empty;

            var query = MyQuery.BuildSelectQuery("vw_mc_filme_ver", ref filters, sort, pageSize, page, order, ref sortFields);

            var countQuery = MyQuery.BuildCountQuery("vw_mc_filme_ver", filters);

            var pagedResults = _repository.FindAvailablePagedSearch(query);

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




    }
}
