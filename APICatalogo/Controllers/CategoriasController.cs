using APICatalogo.Contexts;
using APICatalogo.Models;
using APICatalogo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{       
    [ApiController]
    [Route("[Controller]")]
    public class CategoriasController : Controller
    {
        private readonly IUnitOfWork _uof;
        public CategoriasController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            try
            {
                return _uof.CategoriaRepository.GetCategoriaProduto().ToList();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var categorias = _uof.CategoriaRepository.Get().ToList();

                return categorias;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }            
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _uof.CategoriaRepository.GetById(c => c.CategoriaId == id);
                if (categoria is null)
                    return NotFound();

                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }            
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                    return BadRequest();

                _uof.CategoriaRepository.Add(categoria);
                _uof.Commit();

                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }           
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            try
            {
                if (!(id == categoria.CategoriaId))
                    return BadRequest();

                _uof.CategoriaRepository.Update(categoria);
                _uof.Commit();

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }           
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoria = _uof.CategoriaRepository.GetById(c => c.CategoriaId == id);

                if (categoria is null)
                    return NotFound("Produto não localizado.");

                _uof.CategoriaRepository.Delete(categoria);
                _uof.Commit();

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }            
        }
    }
}
