using GestaoPedidos.Application.Services;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace GestaoPedidos.Test.Services
{
    public class HistoricoUsoPromocaoServiceTests
    {
        private Mock<IHistoricoUsoPromocaoRepository> _mockHistoricoUsoPromocaoRepository;
        private HistoricoUsoPromocaoService _historicoUsoPromocaoService;

        [SetUp]
        public void Setup()
        {
            _mockHistoricoUsoPromocaoRepository = new Mock<IHistoricoUsoPromocaoRepository>();
            _historicoUsoPromocaoService = new HistoricoUsoPromocaoService(_mockHistoricoUsoPromocaoRepository.Object);
        }

        [Fact]
        public async Task CadastrarHistoricoUsoPromocao_ChamaRepositorioCadastrar()
        {
            // Arrange
            var historicoUsoPromocao = new HistoricoUsoPromocao(101, 201, true);

            // Act
            await _historicoUsoPromocaoService.CadastrarHistoricoUsoPromocao(historicoUsoPromocao);

            // Assert
            _mockHistoricoUsoPromocaoRepository.Verify(repo => repo.Cadastrar(historicoUsoPromocao), Times.Once);
        }

        [Fact]
        public async Task ObterHistoricoUsoPromocao_RetornaItensCorretos()
        {
            // Arrange
            var clienteId = 101;
            var historicos = new List<HistoricoUsoPromocao>
        {
            new HistoricoUsoPromocao(101, 201, true),
            new HistoricoUsoPromocao(201, 101, false)
        };
            _mockHistoricoUsoPromocaoRepository.Setup(repo => repo.Obter()).ReturnsAsync(historicos);

            // Act
            var result = await _historicoUsoPromocaoService.ObterHistoricoUsoPromocao(clienteId);

            // Assert
        }
    }
}