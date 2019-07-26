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
            name = name.Replace("&nbsp;", " ");

            if (order == enMovieCount.title)
            {
                return dataset.Where(a => a.nome == name).OrderBy(p => p.titulo).ToList();
            }
            else
            {
                return dataset.Where(a => a.nome == name).OrderByDescending(p => p.rating).ToList();
            }
        }
    }
}
