namespace Loja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novo5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EnderecoEntregas", "CEP", c => c.String(nullable: false));
            AlterColumn("dbo.EnderecoEntregas", "Estado", c => c.String(nullable: false));
            AlterColumn("dbo.EnderecoEntregas", "Cidade", c => c.String(nullable: false));
            AlterColumn("dbo.EnderecoEntregas", "Bairro", c => c.String(nullable: false));
            AlterColumn("dbo.EnderecoEntregas", "Numero", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EnderecoEntregas", "Numero", c => c.String());
            AlterColumn("dbo.EnderecoEntregas", "Bairro", c => c.String());
            AlterColumn("dbo.EnderecoEntregas", "Cidade", c => c.String());
            AlterColumn("dbo.EnderecoEntregas", "Estado", c => c.String());
            AlterColumn("dbo.EnderecoEntregas", "CEP", c => c.String());
        }
    }
}
