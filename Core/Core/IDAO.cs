using Core.Aplicacao;
using Dominio;
using System.Collections.Generic;

namespace Core.Core
{
    public interface IDAO
    {
        Resultado Inserir(EntidadeDominio entidade);
        Resultado Atualizar(EntidadeDominio entidade);
        List<EntidadeDominio> Consultar(EntidadeDominio entidade);
        Resultado Excluir(EntidadeDominio entidade);
    }
}
