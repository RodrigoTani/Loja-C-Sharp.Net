using Core.Aplicacao;
using Core.Util;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace Core.DAO
{
    class CupomDAO : AbstractDAO
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public CupomDAO() : base("tb_Cupom", "id") { }
        public CupomDAO(string table, string idTable) : base(table, idTable) { }
        public CupomDAO(DbConnection dbConnection, string table, string idTable) : base(dbConnection, table, idTable) { }

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
