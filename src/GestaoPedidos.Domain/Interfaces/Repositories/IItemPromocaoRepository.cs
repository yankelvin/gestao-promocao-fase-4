using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Domain.Interfaces.Repositories
{
    public interface IItemPromocaoRepository
    {
        Task Cadastrar(ItemPromocao itemPromocao);
        Task Atualizar(ItemPromocao itemPromocao);
        Task<IEnumerable<ItemPromocao>> Obter();
        Task<ItemPromocao?> Obter(int itemPromocaoId);
        Task Remover(int itemPromocao);
    }
}
