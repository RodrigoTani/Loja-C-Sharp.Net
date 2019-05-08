namespace Core
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novo4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PedidoStatus", "Motivo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PedidoStatus", "Motivo");
        }
    }
}
