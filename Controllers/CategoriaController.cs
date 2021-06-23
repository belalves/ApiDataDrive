using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.Controllers{

    [Route("categorias")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            return new List<Categoria>();
        }
        [HttpGet]
        [Route("{id:long}")]
        public async Task<ActionResult<Categoria>> Get(long id)
        {
            return new Categoria(){Id = id};
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Categoria>> Post([FromBody]Categoria categoria)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categoria);
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<ActionResult<Categoria>> Put(long id, [FromBody]Categoria categoria)
        {
            if(id != categoria.Id)
                return NotFound(new {message ="Categoria não encontrada"});

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categoria);
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<ActionResult<Categoria>> Excluir(long id)
        {
            return Ok();
        }
    }

}
