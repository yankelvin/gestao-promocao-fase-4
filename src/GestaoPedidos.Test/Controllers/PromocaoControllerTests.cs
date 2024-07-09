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

    public class PromocaoControllerTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IPromocaoService> _mockPromocaoService;
        private readonly PromocaoController _controller;

        public PromocaoControllerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockPromocaoService = new Mock<IPromocaoService>();
            _controller = new PromocaoController(_mockMapper.Object, _mockPromocaoService.Object);
            _controller.ControllerContext = new ControllerContext();
        }

        [Fact]
        public async Task Get_ReturnsPromocaoDTO_WhenPromocaoExists()
        {
            // Arrange
            var promocaoId = 1;
            var promocao = new Promocao("Promoção Teste", true) { Id = promocaoId };
            var promocaoDTO = new PromocaoDTO { Id = promocaoId, Texto = "Promoção Teste", Status = true };

            _mockPromocaoService.Setup(service => service.ObterPromocao(promocaoId))
                .ReturnsAsync(promocao);
            _mockMapper.Setup(mapper => mapper.Map<PromocaoDTO>(promocao))
                .Returns(promocaoDTO);

            // Act
            var result = await _controller.Get(promocaoId);

            // Assert
            Assert.Equal(promocaoId, result.Id);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenPromocaoDoesNotExist()
        {
            // Arrange
            var promocaoId = 1;

            _mockPromocaoService.Setup(service => service.ObterPromocao(promocaoId))
                .ReturnsAsync((Promocao)null);

            // Act
            var result = await _controller.Get(promocaoId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Get_ReturnsAllPromocoes()
        {
            // Arrange
            var promocoes = new List<Promocao>
            {
                new Promocao("Promoção Teste 1", true) { Id = 1 },
                new Promocao("Promoção Teste 2", false) { Id = 2 }
            };
            var promocoesDTO = new List<PromocaoDTO>
            {
                new PromocaoDTO { Id = 1, Texto = "Promoção Teste 1", Status = true },
                new PromocaoDTO { Id = 2, Texto = "Promoção Teste 2", Status = false }
            };

            _mockPromocaoService.Setup(service => service.ObterPromocao())
                .ReturnsAsync(promocoes);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<PromocaoDTO>>(promocoes))
                .Returns(promocoesDTO);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Post_CadastraPromocao()
        {
            // Arrange
            var promocaoDTO = new PromocaoDTO { Texto = "Promoção Teste", Status = true };
            var promocao = new Promocao(promocaoDTO.Texto, promocaoDTO.Status);

            _mockMapper.Setup(mapper => mapper.Map<Promocao>(promocaoDTO))
                .Returns(promocao);
            _mockPromocaoService.Setup(service => service.CadastrarPromocao(promocao))
                .Returns(Task.CompletedTask);

            // Act
            await _controller.Post(promocaoDTO);

            // Assert
            _mockPromocaoService.Verify(service => service.CadastrarPromocao(promocao), Times.Once);
        }

        [Fact]
        public async Task Put_AtualizaPromocao()
        {
            // Arrange
            var promocaoDTO = new PromocaoDTO { Texto = "Promoção Teste Atualizada", Status = true };
            var promocao = new Promocao(promocaoDTO.Texto, promocaoDTO.Status);

            _mockMapper.Setup(mapper => mapper.Map<Promocao>(promocaoDTO))
                .Returns(promocao);
            _mockPromocaoService.Setup(service => service.AtualizarPromocao(promocao))
                .Returns(Task.CompletedTask);

            // Act
            await _controller.Put(promocaoDTO);

            // Assert
            _mockPromocaoService.Verify(service => service.AtualizarPromocao(promocao), Times.Once);
        }

        [Fact]
        public async Task Delete_RemovePromocao()
        {
            // Arrange
            var promocaoId = 1;

            _mockPromocaoService.Setup(service => service.RemoverPromocao(promocaoId))
                .Returns(Task.CompletedTask);

            // Act
            await _controller.Delete(promocaoId);

            // Assert
            _mockPromocaoService.Verify(service => service.RemoverPromocao(promocaoId), Times.Once);
        }
    }
}