using System.Collections.Generic;
using WebApi.Model;
using WebApi.Repository;
using WebApi.Repository.Generic;
using WebApi.Data.Converters;
using WebApi.Data.VO;

namespace WebApi.Business.Implementattions
{
    public class GenreBusinessImpl : IGenreBusiness
    {
        private readonly IRepository<Genre> _repository;

        private readonly GenreConverter _converter;

        private readonly IViewRepository<_vw_mc_genero> _vrep;

        private readonly IViewMovieByRepository<_vw_mc_filme_por_genero> _vmbrep;

        public GenreBusinessImpl(IRepository<Genre> repository, IViewRepository<_vw_mc_genero> vrep, IViewMovieByRepository<_vw_mc_filme_por_genero> vmbrep)
        {
            _repository = repository;
            _converter = new GenreConverter();
            _vrep = vrep;
            _vmbrep = vmbrep;
        }

        public GenreVO Create(GenreVO item)
        {
            var ent = _converter.Parse(item);
            ent = _repository.Create(ent);
            return _converter.Parse(ent);
        }

        public GenreVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public List<GenreVO> FindByName(string name)
        {
            return _converter.ParseList(_repository.FindByName(name));
        }

        public GenreVO FindByExactName(string name)
        {
            return _converter.Parse(_repository.FindByExactName(name));
        }

        public List<GenreVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public GenreVO Update(GenreVO item)
        {
            var ent = _converter.Parse(item);
            ent = _repository.Update(ent);
            return _converter.Parse(ent);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<_vw_mc_genero> FindMovieCount(enMovieCount order)
        {
            return _vrep.FindMovieCount(order);
        }

        public List<_vw_mc_filme_por_genero> FindMovieBy(long id, enMovieCount order)
        {
            return _vmbrep.FindMovieBy(id, order);
        }
    }
}
