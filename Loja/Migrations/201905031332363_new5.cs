namespace Loja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PedidoStatus",
                c => new
                    {
                        PedidoId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Usuario = c.String(),
                        DataStatus = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PedidoId)
                .ForeignKey("dbo.Pedidoes", t => t.PedidoId)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.PedidoId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        NomeStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PedidoStatus", "StatusId", "dbo.Status");
            DropForeignKey("dbo.PedidoStatus", "PedidoId", "dbo.Pedidoes");
            DropIndex("dbo.PedidoStatus", new[] { "StatusId" });
            DropIndex("dbo.PedidoStatus", new[] { "PedidoId" });
            DropTable("dbo.Status");
            DropTable("dbo.PedidoStatus");
        }
    }
}
