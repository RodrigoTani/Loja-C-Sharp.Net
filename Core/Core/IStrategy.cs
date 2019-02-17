using Dominio;

namespace Core.Core
{
    public interface IStrategy
    {
        string Processar(EntidadeDominio entidade);
    }
}
