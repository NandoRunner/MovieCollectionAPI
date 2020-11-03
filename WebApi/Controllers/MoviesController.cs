using Tapioca.HATEOAS;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.Business;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using FAndradeTI.Util.Database.Model;

namespace WebApi.Controllers
{
    [ApiVersion("1")]
    [Route("[controller]/v{version:apiVersion}")]
    public class MoviesController : Controller
    {
        //Declaração do serviço usado
        private IMovieBusiness _business;

        /* Injeção de uma instancia de IMovieBusiness ao criar
        uma instancia de MovieController */
        public MoviesController(IMovieBusiness movieBusiness)
        {
            _business = movieBusiness;
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/movie/
        //Get sem parâmetros para o FindAll --> Busca Todos
        [HttpGet]
        [ProducesResponseType(typeof(List<Movie>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_business.FindAll());
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/movie/{id}
        //recebendo um ID como no Path da requisição
        //Get com parâmetros para o FindById --> Busca Por ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Movie), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            var movie = _business.FindById(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }
		
        [Route("[action]/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Movie>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetByName(string name)
        {
            var ret = _business.FindByName(name);
            if (ret == null) return NotFound();
            return Ok(ret);
        }
		
        [Route("[action]/{order}/{isAscending}")]
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(List<_vw_mc_filme_visto>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetWatched(int order = (int)MovieField.period, bool isAscending = true)
        {
            var ret = _business.FindWatched((MovieField)order, isAscending);
            if (ret == null) return NotFound();
            ViewResponseMovie<_vw_mc_filme_visto> vr = new ViewResponseMovie<_vw_mc_filme_visto>
            {
                server_response = ret
            };
            return Ok(vr);
        }

        [Route("[action]/{order}/{isAscending}")]
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(List<_vw_mc_filme_ver>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAvailable(int order = (int)MovieField.rating, bool isAscending = true)
        {
            var ret = _business.FindAvailable((MovieField)order, isAscending);
            if (ret == null) return NotFound();
            ViewResponseMovie<_vw_mc_filme_ver> vr = new ViewResponseMovie<_vw_mc_filme_ver>
            {
                server_response = ret
            };
            return Ok(vr);
        }

        // Configura o Swagger para a operação
        // http://localhost:{porta}/api/persons/v1/
        // [SwaggerResponse((202), Type = typeof(List<Person>))]
        // determina o objeto de retorno em caso de sucesso List<Person>
        // O [SwaggerResponse(XYZ)] define os códigos de retorno 204, 400 e 401
        [HttpGet("find-with-paged-search/{pageSize}/{page}/{isAscending}")]
        [ProducesResponseType(typeof(List<Movie>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetPagedSearch([FromQuery] string name, int pageSize, int page, bool isAscending = true)
        {
            return new OkObjectResult(_business.FindWithPagedSearch(name, pageSize, page, isAscending));
        }

        [HttpGet("find-watched-paged-search/{pageSize}/{page}/{order}/{isAscending}")]
        [ProducesResponseType(typeof(List<_vw_mc_filme_visto>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetWatchedPagedSearch([FromQuery] string name, int pageSize, int page, int order = (int)MovieField.period, bool isAscending = true)
        {
            return new OkObjectResult(_business.FindWatchedPagedSearch(name, pageSize, page, (MovieField)order, isAscending));
        }

        [HttpGet("find-available-paged-search/{pageSize}/{page}/{order}/{isAscending}")]
        [ProducesResponseType(typeof(List<_vw_mc_filme_ver>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetAvailablePagedSearch([FromQuery] string name, int pageSize, int page, int order = (int)MovieField.rating, bool isAscending = false)
        {
            return new OkObjectResult(_business.FindAvailablePagedSearch(name, pageSize, page, (MovieField)order, isAscending));
        }

        //Mapeia as requisições POST para http://localhost:{porta}/api/movie/
        //O [FromBody] consome o Objeto JSON enviado no corpo da requisição
        [HttpPost]
        public IActionResult Post([FromBody]Movie movie)
        {
            if (movie == null) return BadRequest();
            return new  ObjectResult(_business.Create(movie));
        }

    }
}
