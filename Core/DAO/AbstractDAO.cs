using Core.Aplicacao;
using Core.Core;
using Core.Util;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Core.DAO
{
    public class AbstractDAO : ConexaoDB, IDAO
    {


        protected string table, idTable;

        protected new DbConnection dbConnection;

        //Constutores

        public AbstractDAO(string table, string idTable)
        {
            this.table = table;
            this.idTable = idTable;
        }

        public AbstractDAO(DbConnection dbConnection, string table, string idTable)
        {
            this.table = table;
            this.idTable = idTable;
            this.dbConnection = dbConnection;
        }
        /*--- Abre conexao com o banco ---*/
        public void OpenConnection()
        {
            try
            {
                if (dbConnection == null)
                {
                    dbConnection = ConexaoDB.GetDbConnection();
                    dbConnection.Open();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /*--- Fecha a conexao com o banco ---*/
        public void CloseConnection()
        {
            if (dbConnection != null)
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }
        public void BeginTransaction()
        {
            try
            {
                dbTransaction = dbConnection.BeginTransaction();
            }
            catch (DbException dbex)
            {
                dbex.ToString();

                throw dbex;
            }
        }
        public void Rollback()
        {
            try
            {
                dbTransaction.Rollback();
            }
            catch (DbException dbex)
            {
                dbex.ToString();

                throw dbex;
            }
        }
        public void Commit()
        {
            try
            {
                dbTransaction.Commit();
            }
            catch (DbException dbex)
            {
                dbex.ToString();

                throw dbex;
            }

        }

        public virtual Resultado Atualizar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public virtual List<EntidadeDominio> Consultar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public virtual Resultado Excluir(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public virtual Resultado Inserir(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }
    }
}
