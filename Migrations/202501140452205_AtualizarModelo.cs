namespace WebFORNECEDOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizarModelo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pedidoes", "FornecedorId", "dbo.Fornecedors");
            AddForeignKey("dbo.Pedidoes", "FornecedorId", "dbo.Fornecedors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidoes", "FornecedorId", "dbo.Fornecedors");
            AddForeignKey("dbo.Pedidoes", "FornecedorId", "dbo.Fornecedors", "Id", cascadeDelete: true);
        }
    }
}
