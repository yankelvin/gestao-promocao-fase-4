using GestaoPedidos.Application.Services;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;
using Moq;

namespace GestaoPedidos.Test.Services
{
    [TestFixture]
    public class PromocaoServiceTests
    {
        private Mock<IPromocaoRepository> _mockPromocaoRepository;
        private PromocaoService _promocaoService;

        [SetUp]
        public void Setup()
        {
            _mockPromocaoRepository = new Mock<IPromocaoRepository>();
            _promocaoService = new PromocaoService(_mockPromocaoRepository.Object);
        }

        [Test]
        public async Task CadastrarPromocao_ChamaRepositorioCadastrar()
        {
            // Arrange
            var promocao = new Promocao("Promoção Teste", true);

            // Act
            await _promocaoService.CadastrarPromocao(promocao);

            // Assert
            _mockPromocaoRepository.Verify(repo => repo.Cadastrar(promocao), Times.Once);
        }

        [Test]
        public async Task AtualizarPromocao_ChamaRepositorioAtualizar()
        {
            // Arrange
            var promocao = new Promocao("Promoção Atualizada", true);

            // Act
            await _promocaoService.AtualizarPromocao(promocao);

            // Assert
            _mockPromocaoRepository.Verify(repo => repo.Atualizar(promocao), Times.Once);
        }

        [Test]
        public async Task ObterPromocao_ChamaRepositorioObterTodos()
        {
            // Arrange
            var promocoes = new List<Promocao>
        {
            new Promocao("Promoção 1", true),
            new Promocao("Promoção 2", false)
        };
            _mockPromocaoRepository.Setup(repo => repo.Obter()).ReturnsAsync(promocoes);

            // Act
            var result = await _promocaoService.ObterPromocao();

            // Assert
            _mockPromocaoRepository.Verify(repo => repo.Obter(), Times.Once);
        }

        [Test]
        public async Task ObterPromocaoPorId_ChamaRepositorioObterPorId()
        {
            // Arrange
            var promocaoId = 1;
            var promocao = new Promocao("Promoção teste", true);
            _mockPromocaoRepository.Setup(repo => repo.Obter(promocaoId)).ReturnsAsync(promocao);

            // Act
            var result = await _promocaoService.ObterPromocao(promocaoId);

            // Assert
            _mockPromocaoRepository.Verify(repo => repo.Obter(promocaoId), Times.Once);
        }

        [Test]
        public async Task RemoverPromocao_ChamaRepositorioRemover()
        {
            // Arrange
            var promocaoId = 1;

            // Act
            await _promocaoService.RemoverPromocao(promocaoId);

            // Assert
            _mockPromocaoRepository.Verify(repo => repo.Remover(promocaoId), Times.Once);
        }
    }
}