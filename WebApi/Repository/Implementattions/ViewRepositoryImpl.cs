using System.Collections.Generic;
using WebApi.Model;
using System.Threading;
using WebApi.Model.Context;
using System;
using System.Linq;
using WebApi.Model.Base;
using Microsoft.EntityFrameworkCore;
using FAndradeTecInfo.Utils.Model;
using Tapioca.HATEOAS.Utils;
using FAndradeTecInfo.Utils;

namespace WebApi.Repository.Implementattions
{
    public class ViewRepositoryImpl<T> : IViewRepository<T> where T : BaseView
    {
        private MySQLContext _context;
        private DbSet<T> dataset;

        public ViewRepositoryImpl(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }
        
        public List<T> FindMovieCount(enMovieCount order, bool isAscending)
        {
            if (order == enMovieCount.name)
            {
                if (isAscending)
                    return dataset.OrderBy(p => p.nome).ToList();
                else
                    return dataset.OrderByDescending(p => p.nome).ToList();
            }
            else
            {
                if (isAscending)
                    return dataset.OrderBy(p => p.filmes).ToList();
                else
                    return dataset.OrderByDescending(p => p.filmes).ToList();
            }
        }

        public PagedSearchDTO<T> FindPagedSearch(string view, ref Dictionary<string, object> filters, int pageSize, int page, enMovieCount order, bool isAscending)
        {
            var sort = isAscending ? "asc" : "desc";
            var sortFields = string.Empty;

            var query = SQL.BuildSelectQuery(view, ref filters, sort, pageSize, page, order, ref sortFields);

            var countQuery = SQL.BuildCountQuery(view, ref filters);

            var pagedResults = dataset.FromSql<T>(query).ToList();

            int totalResults = GetCount(countQuery);

            return new PagedSearchDTO<T>
            {
                CurrentPage = page,
                List = pagedResults,
                PageSize = pageSize,
                SortFields = sortFields,
                SortDirections = sort,
                Filters = filters,
                TotalResults = totalResults
            };
        }

        public int GetCount(string query)
        {
            // https://stackoverflow.com/questions/40557003/entity-framework-core-count-does-not-have-optimal-performance
            var result = "";
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }

            return Int32.Parse(result);
        }
    }

    public class ViewMovieByRepositoryImpl<T> : IViewMovieByRepository<T> where T : BaseViewMovieBy
    {
        private MySQLContext _context;
        private DbSet<T> dataset;

        public ViewMovieByRepositoryImpl(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public List<T> FindMovieById(long id, enMovieCount order)
        {
            if (order == enMovieCount.name)
            {
                return dataset.Where(a => a.id == id).OrderBy(p => p.nome).ToList();
            }
            else
            {
                return dataset.Where(a => a.id == id).OrderByDescending(p => p.titulo).ToList();
            }
        }

        public List<T> FindMovieByName(string name, enMovieCount order, bool isAscending)
        {
            name = name.Replace("&nbsp;", " ");

            if (order == enMovieCount.title)
            {
                if (isAscending)
                    return dataset.Where(a => a.nome == name).OrderBy(p => p.titulo).ToList();
                else
                    return dataset.Where(a => a.nome == name).OrderByDescending(p => p.titulo).ToList();
            }
            else
            {
                if (isAscending)
                    return dataset.Where(a => a.nome == name).OrderBy(p => p.rating).ToList();
                else
                    return dataset.Where(a => a.nome == name).OrderByDescending(p => p.rating).ToList();
            }
        }

        public PagedSearchDTO<T> FindPagedSearch(string view, ref Dictionary<string, object> filters, int pageSize, int page, enMovieCount order, bool isAscending)
        {
            var sort = isAscending ? "asc" : "desc";
            var sortFields = string.Empty;

            var query = SQL.BuildSelectQuery(view, ref filters, sort, pageSize, page, order, ref sortFields);

            var countQuery = SQL.BuildCountQuery(view, ref filters);

            var pagedResults = dataset.FromSql<T>(query).ToList();

            int totalResults = GetCount(countQuery);

            return new PagedSearchDTO<T>
            {
                CurrentPage = page,
                List = pagedResults,
                PageSize = pageSize,
                SortFields = sortFields,
                SortDirections = sort,
                Filters = filters,
                TotalResults = totalResults
            };
        }





        public int GetCount(string query)
        {
            // https://stackoverflow.com/questions/40557003/entity-framework-core-count-does-not-have-optimal-performance
            var result = "";
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }

            return Int32.Parse(result);
        }
    }
}
