using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql(@"insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) Values('Coca Cola',
                'Refrigerante de Cola', 12.0, 'cocacola.jpg', 28, now(), 1)");

            mb.Sql(@"insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) Values('Hanburguer',
                'Lanche rápido', 15.50, 'lanche.jpg', 16, now(), 2)");

            mb.Sql(@"insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) Values('Bata frita',
                'Lanche convencional', 8.10, 'lanchce.jpg', 49, now(), 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
        }
    }
}
