using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Domain.Interfaces.Repositories
{
    public interface IHistoricoUsoPromocaoRepository
    {
        Task Cadastrar(HistoricoUsoPromocao historicoUsoPromocao);
        Task Atualizar(HistoricoUsoPromocao historicoUsoPromocao);
        Task<IEnumerable<HistoricoUsoPromocao>> Obter();
        Task<HistoricoUsoPromocao?> Obter(int historicoUsoPromocaoId);
        Task Remover(int historicoUsoPromocao);
    }
}
