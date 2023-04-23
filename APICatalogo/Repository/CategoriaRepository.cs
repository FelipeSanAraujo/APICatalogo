using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<Categoria> GetCategoriaProduto()
        {
            return Get().Include(c => c.Produtos).ToList();
        }
    }
}
