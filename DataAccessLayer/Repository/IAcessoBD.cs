using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IAcessoBD<T>
    {
        T Adicionar(T dto);
        T Alterar(T dto);
        bool Eliminar(T dto);
        List<T> ObterPorFiltro(T dto);
        T ObterPorPK(T dto);
    }
}
