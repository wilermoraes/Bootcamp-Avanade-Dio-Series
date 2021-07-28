using System;
using System.Collections.Generic;

namespace Dio.Domain.Interfaces
{
    public interface IRepository<T>
    {
        List<T> Listar();
        T RetornarPorId(Guid id);
        void Inserir(T entidade);
        void Excluir(Guid id);
        void Atualizar(Guid id, T entidade);
    }
}
