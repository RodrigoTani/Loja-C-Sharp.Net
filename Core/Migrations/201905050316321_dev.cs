namespace Core
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dev : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cartaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Usuario = c.String(),
                        NomeCartao = c.String(),
                        NumeroCartao = c.String(),
                        DataExpira = c.DateTime(nullable: false),
                        CVV = c.String(),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cupoms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departamentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome_Departamento = c.String(),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DetalhesPedidoes",
                c => new
                    {
                        DetalhesPedidoId = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        PrecoUnitario = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DetalhesPedidoId)
                .ForeignKey("dbo.Pedidoes", t => t.PedidoId, cascadeDelete: true)
                .Index(t => t.PedidoId);
            
            CreateTable(
                "dbo.EnderecoEntregas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Usuario = c.String(),
                        CEP = c.String(nullable: false),
                        Estado = c.String(nullable: false),
                        Cidade = c.String(nullable: false),
                        Bairro = c.String(nullable: false),
                        Logradouro = c.String(nullable: false),
                        Numero = c.String(nullable: false),
                        Observacao = c.String(),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EstoqueProdutos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Produto = c.Int(nullable: false),
                        Fornecedores = c.Int(nullable: false),
                        PorcentagemPrecificacao = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Custo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantidade = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produtoes", t => t.Produto, cascadeDelete: true)
                .ForeignKey("dbo.Fornecedors", t => t.Fornecedores, cascadeDelete: false)
                .Index(t => t.Produto)
                .Index(t => t.Fornecedores);
            
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false),
                        ImagemUrl = c.String(),
                        plataforma = c.Int(nullable: false),
                        idioma = c.Int(nullable: false),
                        legenda = c.Int(nullable: false),
                        DimensaodoProduto = c.String(nullable: false),
                        genero = c.Int(nullable: false),
                        classificacaoIndicativa = c.Int(nullable: false),
                        Desenvolvedor = c.String(nullable: false),
                        EspacoHd = c.String(nullable: false),
                        ConteudoEmbalagem = c.String(nullable: false),
                        Fornecedor = c.Int(nullable: false),
                        GarantiaFornecedor = c.String(nullable: false),
                        Descricao1 = c.String(nullable: false),
                        Descricao2 = c.String(nullable: false),
                        ValorFinal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fornecedors", t => t.Fornecedor, cascadeDelete: true)
                .Index(t => t.Fornecedor);
            
            CreateTable(
                "dbo.Fornecedors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RazaoSocial = c.String(nullable: false),
                        tipoPessoa = c.Int(nullable: false),
                        CNPJ = c.String(),
                        NomeVendedor = c.String(),
                        CPF = c.String(),
                        Observacao = c.String(),
                        Email = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        EnderecoEntrega_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EnderecoEntregas", t => t.EnderecoEntrega_Id)
                .Index(t => t.EnderecoEntrega_Id);
            
            CreateTable(
                "dbo.ItemVendas",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        ItemVendaId = c.String(),
                        Quantidade = c.Int(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        FormaPagamento = c.String(),
                        Venda_id = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Produtoes", t => t.ProdutoId, cascadeDelete: true)
                .ForeignKey("dbo.Vendas", t => t.Venda_id)
                .Index(t => t.ProdutoId)
                .Index(t => t.Venda_id);
            
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Pagamentoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cartaoid = c.Int(nullable: false),
                        Venda_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Cartaos", t => t.Cartaoid, cascadeDelete: true)
                .ForeignKey("dbo.Vendas", t => t.Venda_id)
                .Index(t => t.Cartaoid)
                .Index(t => t.Venda_id);
            
            CreateTable(
                "dbo.Motivoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DataMotivo = c.DateTime(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Usuario = c.String(),
                        MotivoAtivacao = c.String(),
                        MotivoInativacao = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Produtoes", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.MotivoForns",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DataMotivo = c.DateTime(nullable: false),
                        fornecedo = c.Int(nullable: false),
                        Usuario = c.String(),
                        MotivoAtivacao = c.String(),
                        MotivoInativacao = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Fornecedors", t => t.fornecedo, cascadeDelete: true)
                .Index(t => t.fornecedo);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        PedidoId = c.Int(nullable: false, identity: true),
                        Usuario = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataPedido = c.DateTime(nullable: false),
                        FormaPagamento = c.String(),
                        Endereco = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PedidoId);
            
            CreateTable(
                "dbo.PedidoStatus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Usuario = c.String(),
                        DataStatus = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Pedidoes", t => t.PedidoId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.PedidoId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        NomeStatus = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        saldo_cupom = c.Double(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PedidoStatus", "StatusId", "dbo.Status");
            DropForeignKey("dbo.PedidoStatus", "PedidoId", "dbo.Pedidoes");
            DropForeignKey("dbo.DetalhesPedidoes", "PedidoId", "dbo.Pedidoes");
            DropForeignKey("dbo.MotivoForns", "fornecedo", "dbo.Fornecedors");
            DropForeignKey("dbo.Motivoes", "ProdutoId", "dbo.Produtoes");
            DropForeignKey("dbo.ItemVendas", "Venda_id", "dbo.Vendas");
            DropForeignKey("dbo.Pagamentoes", "Venda_id", "dbo.Vendas");
            DropForeignKey("dbo.Pagamentoes", "Cartaoid", "dbo.Cartaos");
            DropForeignKey("dbo.ItemVendas", "ProdutoId", "dbo.Produtoes");
            DropForeignKey("dbo.EstoqueProdutos", "Fornecedores", "dbo.Fornecedors");
            DropForeignKey("dbo.EstoqueProdutos", "Produto", "dbo.Produtoes");
            DropForeignKey("dbo.Produtoes", "Fornecedor", "dbo.Fornecedors");
            DropForeignKey("dbo.Fornecedors", "EnderecoEntrega_Id", "dbo.EnderecoEntregas");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PedidoStatus", new[] { "StatusId" });
            DropIndex("dbo.PedidoStatus", new[] { "PedidoId" });
            DropIndex("dbo.MotivoForns", new[] { "fornecedo" });
            DropIndex("dbo.Motivoes", new[] { "ProdutoId" });
            DropIndex("dbo.Pagamentoes", new[] { "Venda_id" });
            DropIndex("dbo.Pagamentoes", new[] { "Cartaoid" });
            DropIndex("dbo.ItemVendas", new[] { "Venda_id" });
            DropIndex("dbo.ItemVendas", new[] { "ProdutoId" });
            DropIndex("dbo.Fornecedors", new[] { "EnderecoEntrega_Id" });
            DropIndex("dbo.Produtoes", new[] { "Fornecedor" });
            DropIndex("dbo.EstoqueProdutos", new[] { "Fornecedores" });
            DropIndex("dbo.EstoqueProdutos", new[] { "Produto" });
            DropIndex("dbo.DetalhesPedidoes", new[] { "PedidoId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Status");
            DropTable("dbo.PedidoStatus");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.MotivoForns");
            DropTable("dbo.Motivoes");
            DropTable("dbo.Pagamentoes");
            DropTable("dbo.Vendas");
            DropTable("dbo.ItemVendas");
            DropTable("dbo.Fornecedors");
            DropTable("dbo.Produtoes");
            DropTable("dbo.EstoqueProdutos");
            DropTable("dbo.EnderecoEntregas");
            DropTable("dbo.DetalhesPedidoes");
            DropTable("dbo.Departamentoes");
            DropTable("dbo.Cupoms");
            DropTable("dbo.Cartaos");
        }
    }
}
