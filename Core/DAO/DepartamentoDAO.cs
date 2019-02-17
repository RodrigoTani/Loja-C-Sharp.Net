using Core.Aplicacao;
using Core.Util;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Core.DAO
{
    class DepartamentoDAO : AbstractDAO
    {
        public DepartamentoDAO() : base("tb_departamento", "id") { }
        public DepartamentoDAO(string table, string idTable) : base(table, idTable) { }
        public DepartamentoDAO(DbConnection dbConnection, string table, string idTable) : base(dbConnection, table, idTable) { }

        public override Resultado Atualizar(EntidadeDominio entidade)
        {
            return base.Atualizar(entidade);
        }

        public override List<EntidadeDominio> Consultar(EntidadeDominio entidade)
        {
            List<Departamento> departamentos = new List<Departamento>();

            try
            {
                OpenConnection();
                DbCommand cmd = ConexaoDB.GetDbCommand(dbConnection);

                cmd.CommandText = string.Format("SELECT id,Nome_Departamento from tb_departamento ORDER BY Nome_Departamento");

                DbDataReader dr = cmd.ExecuteReader();
                if (!dr.HasRows) throw new Exception();

                while (dr.Read())
                {
                    Departamento d = new Departamento()
                    {
                        Id = Convert.ToInt32(dr["id"].ToString()),
                        Nome_Departamento = dr["Nome_Departamento"].ToString(),

                    };

                    departamentos.Add(d);
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return departamentos.ToList<EntidadeDominio>();
        }

        public override Resultado Excluir(EntidadeDominio entidade)
        {
            return base.Excluir(entidade);
        }

        public override Resultado Inserir(EntidadeDominio entidade)
        {
            return base.Inserir(entidade);
        }
    }
}
