using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Core
{
    public class ApplicationUser : IdentityUser
    {
        public double saldo_cupom{ get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public System.Data.Entity.DbSet<Loja.Models.EnderecoEntrega> EnderecoEntregas { get; set; }


        public System.Data.Entity.DbSet<Loja.Models.Fornecedor> Fornecedors { get; set; }

        public System.Data.Entity.DbSet<Loja.Models.Produto> Produtoes { get; set; }

        public System.Data.Entity.DbSet<Loja.Models.EstoqueProdutos> EstoqueProdutos { get; set; }

        public System.Data.Entity.DbSet<Loja.Models.Carrinho.ItemVenda> ItemVendaes { get; set; }

        public System.Data.Entity.DbSet<Loja.Models.Carrinho.Pedido> Pedidoes { get; set; }

        public System.Data.Entity.DbSet<Loja.Models.Carrinho.DetalhesPedido> DetalhesPedidoes { get; set; }

        public System.Data.Entity.DbSet<Loja.Models.Motivo> Motivoes { get; set; }

        public System.Data.Entity.DbSet<Loja.Models.MotivoForn> MotivoForns { get; set; }

        public System.Data.Entity.DbSet<Loja.Models.Cartao> Cartaos { get; set; }

        public System.Data.Entity.DbSet<Loja.Models.Status> Status { get; set; }

        public System.Data.Entity.DbSet<Loja.Models.Carrinho.PedidoStatus> PedidoStatus { get; set; }

        public System.Data.Entity.DbSet<Dominio.Departamento> Departamentoes { get; set; }
       //public System.Data.Entity.DbSet<Loja.Models.Carrinho.Pagamento> Pagamentoes { get; set; }
        public System.Data.Entity.DbSet<Dominio.Cupom> Cupom { get; set; }
        public System.Data.Entity.DbSet<Loja.Models.Carrinho.Venda> Venda { get; set; }

        // public System.Data.Entity.DbSet<Loja.Models.Cartao> Cartaos { get; set; }
    }
}