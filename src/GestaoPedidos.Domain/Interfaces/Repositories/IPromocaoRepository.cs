using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Domain.Interfaces.Repositories
{
    public interface IPromocaoRepository
    {
        Task Cadastrar(Promocao promocao);
        Task Atualizar(Promocao promocao);
        Task<IEnumerable<Promocao>> Obter();
        Task<Promocao?> Obter(int promocaoId);
        Task Remover(int promocaoId);
    }
}
