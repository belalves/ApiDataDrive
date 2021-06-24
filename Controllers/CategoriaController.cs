using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers{

    [Route("categorias")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Get([FromServices]DataContext context)
        {
            try
            {
                //Sempre que for feito algum filtro, colocar antes do to list, para não realizar o link em memoria e gerar impacto de performance
                var categorias = await context.Categorias.AsNoTracking().ToListAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(new { messagem = $"Ocorreu um erro ao listar as categorias {ex.InnerException.Message}"});
            }            
        }
        [HttpGet]
        [Route("{id:long}")]
        public async Task<ActionResult<Categoria>> Get(long id,[FromServices]DataContext context)
        {
            try
            {
                var categoria = await context.Categorias.AsNoTracking().FirstOrDefaultAsync( x => x.Id == id);

                if (categoria == null)
                    return BadRequest("Categoria não encontrada");

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest($"Não foi possivel obter categoria. {ex.InnerException.Message}");
            }            
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Categoria>> Post([FromBody]Categoria model, [FromServices]DataContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                context.Categorias.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = $"Não foi possivel criar a categoria {ex.InnerException.Message}" }); 
            }
           
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<ActionResult<Categoria>> Put(long id, 
            [FromBody]Categoria model,
            [FromServices]DataContext context)
        {
            if(id != model.Id)
                return NotFound(new {message ="Categoria não encontrada"});

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Categoria>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = $"O registro já foi atualizado por outro usuario. {ex.InnerException.Message}" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = $"Não foi possivel salvar os dados na base.{ex.Message}" });
            }

            return Ok(model);
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<ActionResult<Categoria>> Excluir(long id, [FromServices]DataContext context)
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync( x=> x.Id == id);

            if (categoria == null)
                return NotFound("Categoria não encontrada");

            try
            {
                context.Categorias.Remove(categoria);
                await context.SaveChangesAsync();
                return Ok(new { message = $"Categoria removida com sucesso. {categoria.Titulo}" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possivel remover a categoria.{ex.Message}" });
            }
        }
    }

}
