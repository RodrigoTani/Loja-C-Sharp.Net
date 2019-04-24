namespace Loja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novo51 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fornecedors", "NomeVendedor", c => c.String());
            AlterColumn("dbo.Fornecedors", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fornecedors", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Fornecedors", "NomeVendedor", c => c.String(nullable: false));
        }
    }
}
