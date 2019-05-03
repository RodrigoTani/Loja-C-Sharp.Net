namespace Loja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PedidoStatus", "PedidoId", "dbo.Pedidoes");
            DropPrimaryKey("dbo.PedidoStatus");
            AddColumn("dbo.PedidoStatus", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PedidoStatus", "id");
            AddForeignKey("dbo.PedidoStatus", "PedidoId", "dbo.Pedidoes", "PedidoId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PedidoStatus", "PedidoId", "dbo.Pedidoes");
            DropPrimaryKey("dbo.PedidoStatus");
            DropColumn("dbo.PedidoStatus", "id");
            AddPrimaryKey("dbo.PedidoStatus", "PedidoId");
            AddForeignKey("dbo.PedidoStatus", "PedidoId", "dbo.Pedidoes", "PedidoId");
        }
    }
}
