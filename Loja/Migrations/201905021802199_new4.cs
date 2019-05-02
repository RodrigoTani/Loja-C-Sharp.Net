namespace Loja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cartaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Usuario = c.String(),
                        NomeCartao = c.String(),
                        NumeroCartao = c.String(),
                        DataExpira = c.DateTime(nullable: false),
                        CVV = c.String(),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cartaos");
        }
    }
}
