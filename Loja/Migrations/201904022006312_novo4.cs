namespace Loja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novo4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MotivoForns",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DataMotivo = c.DateTime(nullable: false),
                        fornecedo = c.Int(nullable: false),
                        Usuario = c.String(),
                        MotivoAtivacao = c.String(),
                        MotivoInativacao = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Fornecedors", t => t.fornecedo, cascadeDelete: true)
                .Index(t => t.fornecedo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MotivoForns", "fornecedo", "dbo.Fornecedors");
            DropIndex("dbo.MotivoForns", new[] { "fornecedo" });
            DropTable("dbo.MotivoForns");
        }
    }
}
