namespace Core
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dev2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pagamentoes", "Cupon_id", "dbo.Cupoms");
            DropIndex("dbo.Pagamentoes", new[] { "Cupon_id" });
            DropColumn("dbo.Pagamentoes", "Cupon_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pagamentoes", "Cupon_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Pagamentoes", "Cupon_id");
            AddForeignKey("dbo.Pagamentoes", "Cupon_id", "dbo.Cupoms", "Id", cascadeDelete: true);
        }
    }
}
