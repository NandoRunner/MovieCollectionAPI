using System.Collections.Generic;
using WebApi.Model;
using System.Threading;
using WebApi.Model.Context;
using System;
using System.Linq;
using WebApi.Model.Base;
using Microsoft.EntityFrameworkCore;

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
        
        public List<T> FindMovieCount(enMovieCount order)
        {
            if (order == enMovieCount.name)
            {
                return dataset.OrderBy(p => p.nome).ToList();
            }
            else
            {
                return dataset.OrderByDescending(p => p.filmes).ToList();
            }
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

        public List<T> FindMovieBy(long id, enMovieCount order)
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

        public List<T> FindMovieByName(string name, enMovieCount order)
        {
            if (order == enMovieCount.name)
            {
                return dataset.Where(a => a.nome == name).OrderBy(p => p.nome).ToList();
            }
            else
            {
                return dataset.Where(a => a.nome == name).OrderByDescending(p => p.titulo).ToList();
            }
        }
    }
}
