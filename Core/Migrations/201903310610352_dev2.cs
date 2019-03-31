namespace Core
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dev2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Departamentoes", "DataCadastro");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departamentoes", "DataCadastro", c => c.DateTime(nullable: false));
        }
    }
}
