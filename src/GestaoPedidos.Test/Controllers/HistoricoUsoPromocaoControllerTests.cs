using AutoMapper;
using GestaoPedidos.Application.DTOs.Promocao;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Services;
using GestaoPedidos.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace GestaoPedidos.Test.Controllers
{
    public class HistoricoUsoPromocaoControllerTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHistoricoUsoPromocaoService> _mockHistoricoUsoPromocaoService;
        private readonly HistoricoUsoPromocaoController _controller;

        public HistoricoUsoPromocaoControllerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockHistoricoUsoPromocaoService = new Mock<IHistoricoUsoPromocaoService>();
            _controller = new HistoricoUsoPromocaoController(_mockMapper.Object, _mockHistoricoUsoPromocaoService.Object);
            _controller.ControllerContext = new ControllerContext();
        }

        [Fact]
        public async Task Get_ReturnsHistoricoUsoPromocaoDTO_WhenHistoricoExists()
        {
            // Arrange
            var clienteId = 1;
            var historico = new List<HistoricoUsoPromocao>
            {
                new HistoricoUsoPromocao(1, 1, true) { Id = 1 },
                new HistoricoUsoPromocao(2, 2, true) { Id = 2 }
            };
            var historicoDTO = new List<HistoricoUsoPromocaoDTO>
            {
                new HistoricoUsoPromocaoDTO {IdCliente = clienteId, IdPromocao = 1 },
                new HistoricoUsoPromocaoDTO {IdCliente = clienteId, IdPromocao = 2 }
            };

            _mockHistoricoUsoPromocaoService.Setup(service => service.ObterHistoricoUsoPromocao(clienteId))
                .ReturnsAsync(historico);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<HistoricoUsoPromocaoDTO>>(historico))
                .Returns(historicoDTO);

            // Act
            var result = await _controller.Get(clienteId);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenHistoricoDoesNotExist()
        {
            // Arrange
            var clienteId = 1;

            _mockHistoricoUsoPromocaoService.Setup(service => service.ObterHistoricoUsoPromocao(clienteId))
                .ReturnsAsync((IEnumerable<HistoricoUsoPromocao>)null);

            // Act
            var result = await _controller.Get(clienteId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task Post_CadastraHistoricoUsoPromocao()
        {
            // Arrange
            var historicoUsoPromocaoDTO = new HistoricoUsoPromocaoDTO { IdCliente = 1, IdPromocao = 1 };
            var historicoUsoPromocao = new HistoricoUsoPromocao(historicoUsoPromocaoDTO.IdPromocao, historicoUsoPromocaoDTO.IdCliente, true);

            _mockMapper.Setup(mapper => mapper.Map<HistoricoUsoPromocao>(historicoUsoPromocaoDTO))
                .Returns(historicoUsoPromocao);
            _mockHistoricoUsoPromocaoService.Setup(service => service.CadastrarHistoricoUsoPromocao(historicoUsoPromocao))
                .Returns(Task.CompletedTask);

            // Act
            await _controller.Post(historicoUsoPromocaoDTO);

            // Assert
            _mockHistoricoUsoPromocaoService.Verify(service => service.CadastrarHistoricoUsoPromocao(historicoUsoPromocao), Times.Once);
        }
    }
}
