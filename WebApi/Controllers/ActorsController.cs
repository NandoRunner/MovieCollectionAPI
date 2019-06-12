using Tapioca.HATEOAS;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business;
using WebApi.Data.VO;
using System.Collections.Generic;
using WebApi.Model;

namespace WebApi.Controllers
{
    [ApiVersion("1")]
    [Route("[controller]/v{version:apiVersion}")]
    public class ActorsController : Controller
    {
        //Declaração do serviço usado
        private IActorBusiness _business;
        
        /* Injeção de uma instancia de IActorBusiness ao criar
        uma instancia de ActorController */
        public ActorsController(IActorBusiness actorBusiness)
        {
            _business = actorBusiness;
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/actor/
        //Get sem parâmetros para o FindAll --> Busca Todos
        [HttpGet]
        [ProducesResponseType(typeof(List<ActorVO>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return new OkObjectResult(_business.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActorVO), 200)]
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
        [ProducesResponseType(typeof(List<ActorVO>), 200)]
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
        [ProducesResponseType(typeof(ActorVO), 200)]
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
        [ProducesResponseType(typeof(ActorVO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody]ActorVO item)
        {
            if (item == null) return BadRequest();
            var createdItem = _business.Create(item);
            if (createdItem == null) return BadRequest();
            return new OkObjectResult(createdItem);
        }


        [HttpPut]
        [ProducesResponseType(typeof(ActorVO), 202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody]ActorVO item)
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

        [Route("[action]/{order}")]
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(List<_vw_mc_ator>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetMovieCount(int order = (int)enMovieCount.count)
        {
            var ret = _business.FindMovieCount((enMovieCount)order);
            if (ret == null) return NotFound();
            ViewResponse<_vw_mc_ator> vr = new ViewResponse<_vw_mc_ator>();
            vr.server_response = ret;
            return Ok(vr);
        }
    }
}
