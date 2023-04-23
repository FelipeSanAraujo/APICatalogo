using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("insert into Categorias(Nome, ImagemUrl) Values('Bebidas', 'Bebida.jpg')");
            mb.Sql("insert into Categorias(Nome, ImagemUrl) Values('Lanches', 'Lanche.jpg')");
            mb.Sql("insert into Categorias(Nome, ImagemUrl) Values('Sobremesas', 'Sobremesa.jpg')");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
