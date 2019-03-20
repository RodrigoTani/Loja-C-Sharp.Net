namespace Loja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produtoes", "DimensaodoProduto", c => c.String(nullable: false));
            AlterColumn("dbo.Produtoes", "Desenvolvedor", c => c.String(nullable: false));
            AlterColumn("dbo.Produtoes", "EspacoHd", c => c.String(nullable: false));
            AlterColumn("dbo.Produtoes", "ConteudoEmbalagem", c => c.String(nullable: false));
            AlterColumn("dbo.Produtoes", "GarantiaFornecedor", c => c.String(nullable: false));
            AlterColumn("dbo.Produtoes", "Descricao1", c => c.String(nullable: false));
            AlterColumn("dbo.Produtoes", "Descricao2", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produtoes", "Descricao2", c => c.String());
            AlterColumn("dbo.Produtoes", "Descricao1", c => c.String());
            AlterColumn("dbo.Produtoes", "GarantiaFornecedor", c => c.String());
            AlterColumn("dbo.Produtoes", "ConteudoEmbalagem", c => c.String());
            AlterColumn("dbo.Produtoes", "EspacoHd", c => c.String());
            AlterColumn("dbo.Produtoes", "Desenvolvedor", c => c.String());
            AlterColumn("dbo.Produtoes", "DimensaodoProduto", c => c.String());
        }
    }
}
