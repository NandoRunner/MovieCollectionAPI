using System.Collections.Generic;
using WebApi.Model;
using System.Threading;
using WebApi.Model.Context;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FAndradeTI.Util.Database.Model;

namespace WebApi.Repository.Implementattions
{
    public class MovieRepositoryImpl : IMovieRepository
    {
        private MySQLContext _context;
        private DbSet<Movie> dataset;

        private DbSet<_vw_mc_filme_visto> ds2;
        private DbSet<_vw_mc_filme_ver> ds3;

        public MovieRepositoryImpl(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<Movie>();
            ds2 = _context.Set<_vw_mc_filme_visto>();
            ds3 = _context.Set<_vw_mc_filme_ver>();
        }

        // Metodo responsável por criar uma nova pessoa
        // nesse momento adicionamos o objeto ao contexto
        // e finalmente salvamos as mudanças no contexto
        // na base de dados
        public Movie Create(Movie movie)
        {
            try
            {
                _context.Add(movie);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return movie;
        }

        // Método responsável por retornar uma pessoa
        public Movie FindById(long id)
        {
            return dataset.SingleOrDefault(p => p.id.Equals(id));
        }

        public List<Movie> FindByName(string name)
        {
            return dataset.Where(a => a.titulo.Contains(name)).OrderBy(a => a.titulo).ToList();
        }


        // Método responsável por retornar todas as pessoas
        public List<Movie> FindAll()
        {
            return dataset.OrderBy(a => a.titulo).ToList();
        }

        public List<_vw_mc_filme_visto> FindWatched(MovieField order, bool isAscending)
        {
            if (order == MovieField.period)
            {
                if (isAscending)
                    return _context.vw_mc_filme_visto.OrderBy(p => p.periodo).ToList();
                else
                    return _context.vw_mc_filme_visto.OrderByDescending(p => p.periodo).ToList();
            }
            else
            {
                if (isAscending)
                    return _context.vw_mc_filme_visto.OrderBy(p => p.titulo).ToList();
                else
                    return _context.vw_mc_filme_visto.OrderByDescending(p => p.titulo).ToList();
            }
        }

        public List<_vw_mc_filme_ver> FindAvailable(MovieField order, bool isAscending)
        {
            if (order == MovieField.rating)
            {
                if (isAscending)
                    return _context.vw_mc_filme_ver.OrderBy(p => p.rating).ToList();
                else
                    return _context.vw_mc_filme_ver.OrderByDescending(p => p.rating).ToList();
            }
            else
            {
                if (isAscending)
                    return _context.vw_mc_filme_ver.OrderBy(p => p.titulo).ToList();
                else
                    return _context.vw_mc_filme_ver.OrderByDescending(p => p.titulo).ToList();
            }
        }

        public List<Movie> FindWithPagedSearch(string query)
        {
            return dataset.FromSqlRaw<Movie>(query).ToList();
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

        public List<_vw_mc_filme_visto> FindWatchedPagedSearch(string query)
        {
            return ds2.FromSqlRaw<_vw_mc_filme_visto>(query).ToList();
        }

        public List<_vw_mc_filme_ver> FindAvailablePagedSearch(string query)
        {
            return ds3.FromSqlRaw<_vw_mc_filme_ver>(query).ToList();
        }
    }
}
