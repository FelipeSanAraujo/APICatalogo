using APICatalogo.Contexts;

namespace APICatalogo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProdutoRepository _prodRepository;
        private CategoriaRepository _catRepository;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _prodRepository = _prodRepository ?? new ProdutoRepository(_context);
            }            
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _catRepository = _catRepository ?? new CategoriaRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
