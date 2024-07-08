using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Domain.Interfaces.Services;

namespace GestaoPedidos.Application.Services
{
    public class ItemPromocaoService : IItemPromocaoService
    {
        private readonly IItemPromocaoRepository _itemPromocaoRepository;

        public ItemPromocaoService(IItemPromocaoRepository itemPromocaoRepository)
        {
            _itemPromocaoRepository = itemPromocaoRepository;
        }

        public async Task CadastrarItemPromocao(ItemPromocao itemPromocao)
        {
            await _itemPromocaoRepository.Cadastrar(itemPromocao);
        }

        public async Task AtualizarItemPromocao(ItemPromocao itemPromocao)
        {
            await _itemPromocaoRepository.Atualizar(itemPromocao);
        }

        public Task<IEnumerable<ItemPromocao>> ObterItemPromocaoPorProduto(int produtoId)
        {
            return Task.FromResult(_itemPromocaoRepository.Obter().Result.Where(p => p.IdProduto.Equals(produtoId)));
        }

        public Task<IEnumerable<ItemPromocao>> ObterItemPromocaoPorPromocao(int promocaoId)
        {
            return Task.FromResult(_itemPromocaoRepository.Obter().Result.Where(p => p.IdPromocao.Equals(promocaoId)));
        }

        public async Task RemoverItemPromocao(int promocaoId)
        {
            await _itemPromocaoRepository.Remover(promocaoId);
        }
    }
}
