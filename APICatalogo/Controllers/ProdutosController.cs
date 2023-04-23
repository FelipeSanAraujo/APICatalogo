using APICatalogo.DTOs;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using APICatalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : Controller
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork uof, IConfiguration configuration,
            ILogger<ProdutosController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _uof = uof;
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

        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<Produto>> GetProdutoPorPreco()
        {
            return _uof.ProdutoRepository.ConsultarPorPreco().ToList();
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery]ProdutoParameters param)
        {
            try
            {
                //_logger.LogInformation("=============== GET PRODUTOS =================");
                var produtos = _uof.ProdutoRepository.GetProdutos(param);

                var metadata = new
                {
                    produtos.TotalCount,
                    produtos.PageSize,
                    produtos.CurrentPage,
                    produtos.TotalPages,
                    produtos.HasNext,
                    produtos.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);

                return produtosDto;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }            
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> GetByIdAsync(int id)
        {
            try
            {
                var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
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
        public ActionResult Post(ProdutoDTO produtoDTO)
        {
            try
            {
                if (produtoDTO is null)
                    return BadRequest();

                var produto = _mapper.Map<Produto>(produtoDTO);

                _uof.ProdutoRepository.Add(produto);
                _uof.Commit();

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produtoDTO);
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

                _uof.ProdutoRepository.Update(produto);
                _uof.Commit();

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
                var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);

                if (produto is null)
                    return NotFound("Produto não localizado.");

                _uof.ProdutoRepository.Delete(produto);
                _uof.Commit();

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }           
        }
    }
}
