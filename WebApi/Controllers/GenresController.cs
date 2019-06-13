﻿using Tapioca.HATEOAS;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business;
using WebApi.Data.VO;
using System.Collections.Generic;
using WebApi.Model;

namespace WebApi.Controllers
{
    [ApiVersion("1")]
    [Route("[controller]/v{version:apiVersion}")]
    public class GenresController : Controller
    {
        //Declaração do serviço usado
        private IGenreBusiness _business;

        /* Injeção de uma instancia de IGenreService ao criar
        uma instancia de GenreController */
        public GenresController(IGenreBusiness itemBusiness)
        {
            _business = itemBusiness;
        }

        //Get sem parâmetros para o FindAll --> Busca Todos
        [HttpGet]
        [ProducesResponseType(typeof(List<GenreVO>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return new OkObjectResult(_business.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GenreVO), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var item = _business.FindById(id);
            if (item == null) return NotFound();
            return new OkObjectResult(item);
        }

        [Route("[action]/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(List<GenreVO>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetByName(string name)
        {
            var ret = _business.FindByName(name);
            if (ret == null) return NotFound();
            return Ok(ret);
        }

        [Route("[action]/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(GenreVO), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetByExactName(string name)
        {
            var ret = _business.FindByExactName(name);
            if (ret == null) return NotFound();
            return Ok(ret);
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenreVO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody]GenreVO item)
        {
            if (item == null) return BadRequest();
            var createdItem = _business.Create(item);
            if (createdItem == null) return BadRequest();
            return new OkObjectResult(createdItem);
        }


        [HttpPut]
        [ProducesResponseType(typeof(GenreVO), 202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody]GenreVO item)
        {
            if (item == null) return BadRequest();
            var updatedItem = _business.Update(item);
            if (updatedItem == null) return NoContent();
            return new OkObjectResult(updatedItem);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _business.Delete(id);
            return NoContent();
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        [Route("[action]/{order}")]
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(List<_vw_mc_genero>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetMovieCount(int order = (int)enMovieCount.count)
        {
            var ret = _business.FindMovieCount((enMovieCount)order);
            if (ret == null) return NotFound();
            ViewResponse<_vw_mc_genero> vr = new ViewResponse<_vw_mc_genero>();
            vr.server_response = ret;
            return Ok(vr);
        }

        [Route("[action]/{id}/{order}")]
        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(List<_vw_mc_filme_por_genero>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetMovieBy(long id, int order = (int)enMovieCount.count)
        {
            var ret = _business.FindMovieBy(id, (enMovieCount)order);
            if (ret == null) return NotFound();
            ViewResponseMovieBy<_vw_mc_filme_por_genero> vr = new ViewResponseMovieBy<_vw_mc_filme_por_genero>();
            vr.server_response = ret;
            return Ok(vr);
        }
    }
}