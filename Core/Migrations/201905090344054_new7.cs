namespace Core
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pagamentoes", "Cupon_id", c => c.Int());
            CreateIndex("dbo.Pagamentoes", "Cupon_id");
            AddForeignKey("dbo.Pagamentoes", "Cupon_id", "dbo.Cupoms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pagamentoes", "Cupon_id", "dbo.Cupoms");
            DropIndex("dbo.Pagamentoes", new[] { "Cupon_id" });
            DropColumn("dbo.Pagamentoes", "Cupon_id");
        }
    }
}
