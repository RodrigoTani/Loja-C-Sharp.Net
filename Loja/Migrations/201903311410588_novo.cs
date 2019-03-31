namespace Loja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EstoqueProdutos", "DataCadastro");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EstoqueProdutos", "DataCadastro", c => c.DateTime(nullable: false));
        }
    }
}
