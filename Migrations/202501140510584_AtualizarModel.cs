namespace WebFORNECEDOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizarModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Pedidos", newName: "Pedidoes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Pedidoes", newName: "Pedidos");
        }
    }
}
