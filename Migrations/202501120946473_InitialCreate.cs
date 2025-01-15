namespace WebFORNECEDOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
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
                        Id_Medicamento = c.Int(nullable: false),
                        Id_Cliente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.Id_Cliente, cascadeDelete: true)
                .ForeignKey("dbo.Medicamentos", t => t.Id_Medicamento)
                .Index(t => t.Id_Medicamento)
                .Index(t => t.Id_Cliente);
            
            CreateTable(
                "dbo.Medicamentos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Lote = c.String(),
                        DataProducao = c.DateTime(nullable: false),
                        DataValidade = c.DateTime(nullable: false),
                        QuantidadeDisponivel = c.Int(nullable: false),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidoes", "Id_Medicamento", "dbo.Medicamentos");
            DropForeignKey("dbo.Pedidoes", "Id_Cliente", "dbo.Clientes");
            DropIndex("dbo.Pedidoes", new[] { "Id_Cliente" });
            DropIndex("dbo.Pedidoes", new[] { "Id_Medicamento" });
            DropTable("dbo.Medicamentos");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Clientes");
        }
    }
}
