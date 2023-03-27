using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository
{    
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<Produto> ConsultarPorPreco()
        {
            return Get().OrderBy(p => p.Preco).ToList(); 
        }

        public IEnumerable<Produto> GetProdutos(ProdutoParameters param)
        {
            return Get()
                .OrderBy(p => p.Nome)
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .ToList();
        }
    }
}
