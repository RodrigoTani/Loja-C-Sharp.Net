using Core.Aplicacao;
using System.Data.Common;

namespace Core.Util
{
    public class ConexaoDB
    {
        protected static Resultado resultado = new Resultado();
        protected static DbConnection dbConnection { get; set; }
        protected DbTransaction dbTransaction { get; set; }
        /*
            Tentativa de fazer conexão com o banco de dados
             
        */
        public static DbConnection GetDbConnection()
        {
           // dbConnection = new NpgsqlConnection();
            try
            {
                dbConnection.ConnectionString = string.Format(
                    "Server={0};      Port={1};User Id={2};         Password={3}; Database={4};",
                     BDInfo.SERVIDOR, BDInfo.PORTA, BDInfo.USUARIO, BDInfo.SENHA, BDInfo.DATABASE);
                return dbConnection;
            }
            catch (DbException dbex)
            {
                resultado.Mensagem = dbex.Message;
                throw;
            }
        }
        public static DbCommand GetDbCommand(DbConnection dbConnection)
        {
            DbCommand Comando = dbConnection.CreateCommand();
            return Comando;
        }
    }
}
