using Tapioca.HATEOAS;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business;
using WebApi.Data.VO;
using System.Collections.Generic;
using WebApi.Model;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [ApiVersion("1")]
    [Route("[controller]/v{version:apiVersion}")]
    public class DirectorsController : Controller
    {
        //Declaração do serviço usado
        private IDirectorBusiness _business;

        /* Injeção de uma instancia de IDirectorService ao criar
        uma instancia de DirectorController */
        public DirectorsController(IDirectorBusiness itemBusiness)
        {
            _business = itemBusiness;
        }

        //Get sem parâmetros para o FindAll --> Busca Todos
        [HttpGet]
        [ProducesResponseType(typeof(List<DirectorVO>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return new OkObjectResult(_business.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DirectorVO), 200)]
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
        
        /* Query Param - accepts null*/
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(List<DirectorVO>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetByName([FromQuery] string name)
        {
            var ret = _business.FindByName(name);
            if (ret == null) return NotFound();
            return Ok(ret);
        }

        [Route("[action]/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(DirectorVO), 200)]
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
        [ProducesResponseType(typeof(DirectorVO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody]DirectorVO item)
        {
            if (item == null) return BadRequest();
            var createdItem = _business.Create(item);
            if (createdItem == null) return BadRequest();
            return new OkObjectResult(createdItem);
        }


        [HttpPut]
        [ProducesResponseType(typeof(DirectorVO), 202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody]DirectorVO item)
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
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _business.Delete(id);
            return NoContent();
        }

        [Route("[action]/{order}/{isAscending}")]
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(List<_vw_mc_diretor>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetMovieCount(int order = (int)enMovieCount.count, bool isAscending = true)
        {
            var ret = _business.FindMovieCount((enMovieCount)order, isAscending);
            if (ret == null) return NotFound();
            ViewResponse<_vw_mc_diretor> vr = new ViewResponse<_vw_mc_diretor>();
            vr.server_response = ret;
            return Ok(vr);
        }

        [Route("[action]/{id}/{order}")]
        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(List<_vw_mc_filme_por_diretor>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetMovieBy(long id, int order = (int)enMovieCount.title)
        {
            var ret = _business.FindMovieBy(id, (enMovieCount)order);
            if (ret == null) return NotFound();
            ViewResponseMovieBy<_vw_mc_filme_por_diretor> vr = new ViewResponseMovieBy<_vw_mc_filme_por_diretor>();
            vr.server_response = ret;
            return Ok(vr);
        }


        [Route("[action]/{name}/{order}/{isAscending}")]
        [Route("[action]/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(List<_vw_mc_filme_por_diretor>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetMovieByName(string name, int order = (int)enMovieCount.title, bool isAscending = true)
        {
            var ret = _business.FindMovieByName(name, (enMovieCount)order, isAscending);
            if (ret == null) return NotFound();
            ViewResponseMovieBy<_vw_mc_filme_por_diretor> vr = new ViewResponseMovieBy<_vw_mc_filme_por_diretor>();
            vr.server_response = ret;
            return Ok(vr);
        }
    }
}
