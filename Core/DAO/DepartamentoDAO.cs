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
    class DepartamentoDAO : AbstractDAO
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public DepartamentoDAO() : base("tb_departamento", "id") { }
        public DepartamentoDAO(string table, string idTable) : base(table, idTable) { }
        public DepartamentoDAO(DbConnection dbConnection, string table, string idTable) : base(dbConnection, table, idTable) { }

        public override Resultado Atualizar(EntidadeDominio entidade)
        {
            return base.Atualizar(entidade);
        }

        public override List<EntidadeDominio> Consultar(EntidadeDominio entidade)
        {
            return db.Departamentoes.ToList().Cast<EntidadeDominio>().ToList();
        }

        public override Resultado Excluir(EntidadeDominio entidade)
        {
            return base.Excluir(entidade);
        }

        public override Resultado Inserir(EntidadeDominio entidade)
        {
            db.Departamentoes.Add((Departamento)entidade);
            db.SaveChanges();
            return new Resultado();
        }
    }
}
