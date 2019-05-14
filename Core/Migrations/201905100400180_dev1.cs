namespace Core
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dev1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendas", "cupom_promocinal_Id", c => c.Int());
            CreateIndex("dbo.Vendas", "cupom_promocinal_Id");
            AddForeignKey("dbo.Vendas", "cupom_promocinal_Id", "dbo.Cupoms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vendas", "cupom_promocinal_Id", "dbo.Cupoms");
            DropIndex("dbo.Vendas", new[] { "cupom_promocinal_Id" });
            DropColumn("dbo.Vendas", "cupom_promocinal_Id");
        }
    }
}
