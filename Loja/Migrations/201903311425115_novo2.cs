namespace Loja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EstoqueProdutos", "DataCadastro", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EstoqueProdutos", "DataCadastro");
        }
    }
}
