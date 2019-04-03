namespace Core
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cupoms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cupoms");
        }
    }
}
