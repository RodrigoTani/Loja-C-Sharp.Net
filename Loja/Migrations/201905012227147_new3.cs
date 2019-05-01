namespace Loja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EnderecoEntregas", "Ativo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EnderecoEntregas", "Ativo", c => c.Boolean(nullable: false));
        }
    }
}
