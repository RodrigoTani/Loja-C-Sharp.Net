namespace Loja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novo3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Motivoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DataMotivo = c.DateTime(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Usuario = c.String(),
                        MotivoAtivacao = c.String(),
                        MotivoInativacao = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Produtoes", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.ProdutoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Motivoes", "ProdutoId", "dbo.Produtoes");
            DropIndex("dbo.Motivoes", new[] { "ProdutoId" });
            DropTable("dbo.Motivoes");
        }
    }
}
