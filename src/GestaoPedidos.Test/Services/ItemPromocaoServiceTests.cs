using GestaoPedidos.Application.Services;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;
using Moq;

namespace GestaoPedidos.Test.Services
{
    [TestFixture]
    public class ItemPromocaoServiceTests
    {
        private Mock<IItemPromocaoRepository> _mockItemPromocaoRepository;
        private ItemPromocaoService _itemPromocaoService;

        [SetUp]
        public void Setup()
        {
            _mockItemPromocaoRepository = new Mock<IItemPromocaoRepository>();
            _itemPromocaoService = new ItemPromocaoService(_mockItemPromocaoRepository.Object);
        }

        [Test]
        public async Task CadastrarItemPromocao_ChamaRepositorioCadastrar()
        {
            // Arrange
            var itemPromocao = new ItemPromocao(101, 201, 12);

            // Act
            await _itemPromocaoService.CadastrarItemPromocao(itemPromocao);

            // Assert
            _mockItemPromocaoRepository.Verify(repo => repo.Cadastrar(itemPromocao), Times.Once);
        }

        [Test]
        public async Task AtualizarItemPromocao_ChamaRepositorioAtualizar()
        {
            // Arrange
            var itemPromocao = new ItemPromocao(101, 201, 12);

            // Act
            await _itemPromocaoService.AtualizarItemPromocao(itemPromocao);

            // Assert
            _mockItemPromocaoRepository.Verify(repo => repo.Atualizar(itemPromocao), Times.Once);
        }

        [Test]
        public async Task ObterItemPromocaoPorProduto_RetornaItensCorretos()
        {
            // Arrange
            var produtoId = 101;
            var itemPromocoes = new List<ItemPromocao>
            {
                new ItemPromocao(201, 101, 12),
                new ItemPromocao(101, 201, 12)
            };

            _mockItemPromocaoRepository.Setup(repo => repo.Obter()).ReturnsAsync(itemPromocoes);

            // Act
            var result = await _itemPromocaoService.ObterItemPromocaoPorProduto(produtoId);

            // Assert
        }

        [Test]
        public async Task ObterItemPromocaoPorPromocao_RetornaItensCorretos()
        {
            // Arrange
            var promocaoId = 201;
            var itemPromocoes = new List<ItemPromocao>
        {
            new ItemPromocao(201, 201, 12),
            new ItemPromocao(101, 201, 12)
        };
            _mockItemPromocaoRepository.Setup(repo => repo.Obter()).ReturnsAsync(itemPromocoes);

            // Act
            var result = await _itemPromocaoService.ObterItemPromocaoPorPromocao(promocaoId);

            // Assert
        }

        [Test]
        public async Task RemoverItemPromocao_ChamaRepositorioRemover()
        {
            // Arrange
            var itemPromocaoId = 1;

            // Act
            await _itemPromocaoService.RemoverItemPromocao(itemPromocaoId);

            // Assert
            _mockItemPromocaoRepository.Verify(repo => repo.Remover(itemPromocaoId), Times.Once);
        }
    }
}
