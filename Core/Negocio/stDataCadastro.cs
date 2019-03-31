using Core.Core;
using Dominio;
using System;

namespace Core.Negocio
{
    public class StDataCadastro : IStrategy
    {
        public string Processar(EntidadeDominio entidade)
        {
            /*
            if (entidade != null)
                entidade.DataCadastro = DateTime.Now;
            else
                return "Erro ao encontrar entidade!";*/
            return null;
        }
    }
}
