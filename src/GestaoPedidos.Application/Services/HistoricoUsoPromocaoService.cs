using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Domain.Interfaces.Services;

namespace GestaoPedidos.Application.Services
{
    public class HistoricoUsoPromocaoService : IHistoricoUsoPromocaoService
    {
        private readonly IHistoricoUsoPromocaoRepository _historicoUsoPromocaoRepository;

        public HistoricoUsoPromocaoService(IHistoricoUsoPromocaoRepository historicoUsoPromocaoRepository)
        {
            _historicoUsoPromocaoRepository = historicoUsoPromocaoRepository;
        }

        public async Task CadastrarHistoricoUsoPromocao(HistoricoUsoPromocao historicoUsoPromocao)
        {
            await _historicoUsoPromocaoRepository.Cadastrar(historicoUsoPromocao);
        }

        public Task<IEnumerable<HistoricoUsoPromocao>> ObterHistoricoUsoPromocao(int clienteId)
        {
            return (Task<IEnumerable<HistoricoUsoPromocao>>)_historicoUsoPromocaoRepository.Obter().Result.Where(p => p.IdCliente.Equals(clienteId));
        }
    }
}
