using Core.Aplicacao;
using Dominio;

namespace Core.Core
{
    public interface IFachada
    {
        Resultado Inserir(EntidadeDominio entidade);
        Resultado Atualizar(EntidadeDominio entidade);
        Resultado Excluir(EntidadeDominio entidade);
        Resultado Consultar(EntidadeDominio entidade);
    }
}
