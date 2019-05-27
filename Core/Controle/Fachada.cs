using Core.Aplicacao;
using Core.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio;
using Core.DAO;
using Core;
using System.Data.Entity;

namespace Core.Controle
{
    public class Fachada : IFachada
    {
        private Resultado resultado;
        /* 
          Mapa de DAOS, será indexado pelo nome da entidade 
          O valor é uma instância do DAO para uma dada entidade; 
         */
        private Dictionary<string, IDAO> daos;
        /*
         Mapa para conter as regras de negócio de todas operações por entidade;
         O valor é um mapa que de regras de negócio indexado pela operação
        */
        private Dictionary<string, Dictionary<string, List<IStrategy>>> rns;
        
        public Fachada()
        {
            daos = new Dictionary<string, IDAO>();
            /* Intânciando o Map de Regras de Negócio */
            rns = new Dictionary<string, Dictionary<string, List<IStrategy>>>();
            Database.SetInitializer<ApplicationDbContext>(null);
            //Departamento
            daos.Add("Departamento", new DepartamentoDAO());
            List<IStrategy> strategiesDepartamentos = new List<IStrategy>();

            Dictionary<string, List<IStrategy>> dictionaryDepartamento = new Dictionary<string, List<IStrategy>>();
            dictionaryDepartamento.Add("Inserir", new List<IStrategy>());
            dictionaryDepartamento.Add("Alterar", new List<IStrategy>());
            dictionaryDepartamento.Add("Excluir", new List<IStrategy>());
            dictionaryDepartamento.Add("Consultar", new List<IStrategy>());
            rns.Add("Departamento", dictionaryDepartamento);

            //Cupom
            daos.Add("Cupom", new CupomDAO());
            List<IStrategy> strategiesCupoms = new List<IStrategy>();

            Dictionary<string, List<IStrategy>> dictionaryCupom = new Dictionary<string, List<IStrategy>>();
            dictionaryCupom.Add("Inserir", new List<IStrategy>());
            dictionaryCupom.Add("Alterar", new List<IStrategy>());
            dictionaryCupom.Add("Excluir", new List<IStrategy>());
            dictionaryCupom.Add("Consultar", new List<IStrategy>());
            rns.Add("Cupom", dictionaryCupom);

            //Análise
            AnaliseDAO AnaDAO = new AnaliseDAO();
            daos.Add(typeof(Analise).Name, AnaDAO);

            Dictionary<string, List<IStrategy>> dictionaryAnalise = new Dictionary<string, List<IStrategy>>();
            dictionaryAnalise.Add("Inserir", new List<IStrategy>());
            dictionaryAnalise.Add("Alterar", new List<IStrategy>());
            dictionaryAnalise.Add("Excluir", new List<IStrategy>());
            dictionaryAnalise.Add("Consultar", new List<IStrategy>());
            rns.Add("Analise", dictionaryAnalise);
            

        }
        /* --------------------------------------------------------------  */
        public Resultado Atualizar(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nomeClasse = entidade.GetType().Name;
            string mensagem = ExecutarRegras(entidade, "Alterar");

            if (mensagem == null)
            {
                IDAO dao = daos[nomeClasse.ToString()];
                try
                {
                    resultado = dao.Atualizar(entidade);
                }
                catch (Exception ex)
                {
                    resultado.Mensagem = ex.Message;
                    throw;
                }
            }
            else
            {
                resultado.Mensagem = mensagem;
            }
            return resultado;
        }
        /* --------------------------------------------------------------  */
        public Resultado Consultar(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nmClasse = entidade.GetType().Name;
            string msg = ExecutarRegras(entidade, "Consultar");
            if (msg == null)
            {
                IDAO dao = daos[nmClasse.ToString()];
                try
                {
                    resultado.Entidades = dao.Consultar(entidade);
                }
                catch (Exception ex)
                {
                    resultado.Mensagem = ex.Message;
                }
            }
            else
                resultado.Mensagem = msg;

            return resultado;
        }
        /* --------------------------------------------------------------  */
        public Resultado Excluir(EntidadeDominio entidade)
        {

            resultado = new Resultado();
            string nomeClasse = entidade.GetType().Name;
            string mensagem = ExecutarRegras(entidade, "Excluir");

            if (mensagem == null)
            {
                IDAO dao = daos[nomeClasse.ToString()];
                try
                {
                    resultado = dao.Excluir(entidade);
                }
                catch (Exception ex)
                {
                    resultado.Mensagem = ex.Message;
                    throw;
                }
            }
            else
            {
                resultado.Mensagem = mensagem;
            }

            return resultado;
        }
        /* --------------------------------------------------------------  */
        public Resultado Inserir(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nomeClasse = entidade.GetType().Name;
            string mensagem = ExecutarRegras(entidade, "Inserir");

            if (mensagem == null)
            {
                IDAO dao = daos[nomeClasse.ToString()];
                try
                {
                    resultado = dao.Inserir(entidade);
                }
                catch (Exception ex)
                {
                    resultado.Mensagem = ex.Message;
                    throw;
                }
            }
            else
            {
                resultado.Mensagem = mensagem;
            }

            return resultado;
        }
        /*---------------------------------------------------------------*/
        private string ExecutarRegras(EntidadeDominio entidade, string operacao)
        {
            string nomeClasse = entidade.GetType().Name;
            StringBuilder msg = new StringBuilder();
            Dictionary<string, List<IStrategy>> regrasOperacao = rns[nomeClasse.ToString()];

            if (regrasOperacao != null)
            {
                List<IStrategy> regras = regrasOperacao[operacao.ToString()];
                if (regras != null)
                {
                    foreach (IStrategy s in regras)
                    {
                        string m = s.Processar(entidade);
                        if (m != null)
                        {
                            msg.Append(m);
                            msg.Append("\n");
                        }
                    }
                }
            }
            if (msg.Length > 0)
                return msg.ToString();
            else
                return null;
        }
    }
    /*---------------------------------------------------------------------------*/
}
