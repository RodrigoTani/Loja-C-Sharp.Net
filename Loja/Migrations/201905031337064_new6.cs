namespace Loja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Status", "NomeStatus", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Status", "NomeStatus", c => c.Boolean(nullable: false));
        }
    }
}
