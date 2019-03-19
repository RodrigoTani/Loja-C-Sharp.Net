namespace Loja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carrinhoes",
                c => new
                {
                    RecordId = c.Int(nullable: false, identity: true),
                    CarrinhoId = c.String(),
                    Quantidade = c.Int(nullable: false),
                    DataCriacao = c.DateTime(nullable: false),
                    ProdutoId = c.Int(nullable: false),
                    FormaPagamento = c.String(),
                })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Produtoes", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.ProdutoId);

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
                    DimensaodoProduto = c.String(),
                    genero = c.Int(nullable: false),
                    classificacaoIndicativa = c.Int(nullable: false),
                    Desenvolvedor = c.String(),
                    EspacoHd = c.String(),
                    ConteudoEmbalagem = c.String(),
                    Fornecedor = c.Int(nullable: false),
                    GarantiaFornecedor = c.String(),
                    Descricao1 = c.String(),
                    Descricao2 = c.String(),
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
                "dbo.EnderecoEntregas",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Usuario = c.String(),
                    CEP = c.String(),
                    Estado = c.String(),
                    Cidade = c.String(),
                    Bairro = c.String(),
                    Logradouro = c.String(nullable: false),
                    Numero = c.String(),
                    Observacao = c.String(),
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
                .ForeignKey("dbo.Fornecedors", t => t.Fornecedores)
                .Index(t => t.Produto)
                .Index(t => t.Fornecedores);

            CreateTable(
                "dbo.Pedidoes",
                c => new
                {
                    PedidoId = c.Int(nullable: false, identity: true),
                    Usuario = c.String(),
                    Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    DataPedido = c.DateTime(nullable: false),
                    FormaPagamento = c.String(),
                })
                .PrimaryKey(t => t.PedidoId);

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
        }
    }
}
