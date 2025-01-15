namespace WebFORNECEDOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedidoes", "MensagemResposta", c => c.String());
            AlterColumn("dbo.Pedidoes", "QuantidadeAprovada", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pedidoes", "QuantidadeAprovada", c => c.Int(nullable: false));
            DropColumn("dbo.Pedidoes", "MensagemResposta");
        }
    }
}
