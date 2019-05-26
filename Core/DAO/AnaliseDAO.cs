using Core.Aplicacao;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DAO
{
    class AnaliseDAO : AbstractDAO
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public AnaliseDAO() : base("", "")
        {
        }

        public override Resultado Atualizar(EntidadeDominio entidade)
        {
            return base.Atualizar(entidade);
        }

        public override List<EntidadeDominio> Consultar(EntidadeDominio entidade)
        {
            return db.Cupom.ToList().Cast<EntidadeDominio>().ToList();
        }

        public override Resultado Excluir(EntidadeDominio entidade)
        {
            return base.Excluir(entidade);
        }

        public override Resultado Inserir(EntidadeDominio entidade)
        {
            db.Cupom.Add((Cupom)entidade);
            db.SaveChanges();
            return new Resultado();
        }
    }
}
