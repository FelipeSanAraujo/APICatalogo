using APICatalogo.Contexts;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public ProdutosController(AppDbContext context, IConfiguration configuration,
            ILogger<ProdutosController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        //Utilizando o IConfiguration para acessar arquivos de configuração
        [HttpGet("BoasVindas")]
        public string BoasVindas()
        {
            var bemVindo = _configuration["BemVindo"];
            //Acessando a seção ConnectionStrings e a chave DefaultConnection e obtendo o valor da chave
            var conexao = _configuration["ConnectionStrings:DefaultConnection"];

            return $"{bemVindo} \n {conexao}";
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAsync()
        {
            try
            {
                _logger.LogInformation("=============== GET PRODUTOS =================");
                var produtos = await _context.Produtos.AsNoTracking().Take(5).ToListAsync();

                if (produtos is null)
                    return NotFound("Nenhum produto encontrado.");

                return produtos;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }            
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> GetByIdAsync(int id)
        {
            try
            {
                var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
                if (produto is null)
                    return NotFound();

                return produto;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }            
        }

        [HttpGet("saudacao/{nome}")]
        public ActionResult<string> Saudacao([FromServices]IMeuServico servico, string nome)
        {
            return servico.saudacao(nome);
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            try
            {
                if (produto is null)
                    return BadRequest();

                _context.Produtos.Add(produto);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }            
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            try
            {
                if (id == produto.ProdutoId)
                    return BadRequest();

                _context.Produtos.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(produto);
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
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

                if (produto is null)
                    return NotFound("Produto não localizado.");

                _context.Produtos.Remove(produto);
                _context.SaveChanges();

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }           
        }
    }
}
