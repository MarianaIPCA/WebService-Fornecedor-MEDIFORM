namespace WebFORNECEDOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NomeDaMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fornecedors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuantidadeSolicitada = c.Int(nullable: false),
                        Estado = c.String(),
                        QuantidadeAprovada = c.Int(nullable: false),
                        Descricao = c.String(),
                        Id_Stock = c.Int(nullable: false),
                        Id_Fornecedor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fornecedors", t => t.Id_Fornecedor, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.Id_Stock, cascadeDelete: true)
                .Index(t => t.Id_Stock)
                .Index(t => t.Id_Fornecedor);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descricao = c.String(),
                        QuantidadeDisponivel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidoes", "Id_Stock", "dbo.Stocks");
            DropForeignKey("dbo.Pedidoes", "Id_Fornecedor", "dbo.Fornecedors");
            DropIndex("dbo.Pedidoes", new[] { "Id_Fornecedor" });
            DropIndex("dbo.Pedidoes", new[] { "Id_Stock" });
            DropTable("dbo.Stocks");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Fornecedors");
        }
    }
}
