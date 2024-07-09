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
    public class ItemPromocaoControllerTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IItemPromocaoService> _mockItemPromocaoService;
        private readonly ItemPromocaoController _controller;

        public ItemPromocaoControllerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockItemPromocaoService = new Mock<IItemPromocaoService>();
            _controller = new ItemPromocaoController(_mockMapper.Object, _mockItemPromocaoService.Object);
            _controller.ControllerContext = new ControllerContext();
        }

        [Fact]
        public async Task Get_ReturnsItemPromocaoDTO_WhenItemPromocaoExists()
        {
            // Arrange
            var itemPromocaoId = 1;
            var itemPromocao = new List<ItemPromocao>() { new ItemPromocao(1, 1, 10.0m) { Id = itemPromocaoId } };
            var itemPromocaoDTO = new List<ItemPromocaoDTO>() { new ItemPromocaoDTO { IdPromocao = 1, IdProduto = 1, Desconto = 10.0m } };

            _mockItemPromocaoService.Setup(service => service.ObterItemPromocaoPorPromocao(itemPromocaoId))
                .ReturnsAsync(itemPromocao);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<ItemPromocaoDTO>>(itemPromocao))
                .Returns(itemPromocaoDTO);

            // Act
            var result = await _controller.GetPorPromocao(itemPromocaoId);

            // Assert
            Assert.Equal(itemPromocaoId, result.First().IdPromocao);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenItemPromocaoDoesNotExist()
        {
            // Arrange
            var itemPromocaoId = 1;

            _mockItemPromocaoService.Setup(service => service.ObterItemPromocaoPorPromocao(itemPromocaoId))
                .ReturnsAsync(new List<ItemPromocao>());

            // Act
            var result = await _controller.GetPorPromocao(itemPromocaoId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_ReturnsAllItemPromocoes()
        {
            // Arrange
            var itemPromocoes = new List<ItemPromocao>
            {
                new ItemPromocao(1, 1, 10.0m) { Id = 1 },
                new ItemPromocao(2, 2, 20.0m) { Id = 2 }
            };
            var itemPromocoesDTO = new List<ItemPromocaoDTO>
            {
                new ItemPromocaoDTO { IdPromocao = 1, IdProduto = 1, Desconto = 10.0m },
                new ItemPromocaoDTO { IdPromocao = 2, IdProduto = 2, Desconto = 20.0m }
            };

            _mockItemPromocaoService.Setup(service => service.ObterItemPromocaoPorProduto(1))
                .ReturnsAsync(itemPromocoes);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<ItemPromocaoDTO>>(itemPromocoes))
                .Returns(itemPromocoesDTO);

            // Act
            var result = await _controller.GetPorProduto(1);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Post_CadastraItemPromocao()
        {
            // Arrange
            var itemPromocaoDTO = new ItemPromocaoDTO { IdPromocao = 1, IdProduto = 1, Desconto = 10.0m };
            var itemPromocao = new ItemPromocao(itemPromocaoDTO.IdPromocao, itemPromocaoDTO.IdProduto, itemPromocaoDTO.Desconto);

            _mockMapper.Setup(mapper => mapper.Map<ItemPromocao>(itemPromocaoDTO))
                .Returns(itemPromocao);
            _mockItemPromocaoService.Setup(service => service.CadastrarItemPromocao(itemPromocao))
                .Returns(Task.CompletedTask);

            // Act
            await _controller.Post(itemPromocaoDTO);

            // Assert
            _mockItemPromocaoService.Verify(service => service.CadastrarItemPromocao(itemPromocao), Times.Once);
        }

        [Fact]
        public async Task Put_AtualizaItemPromocao()
        {
            // Arrange
            var itemPromocaoDTO = new ItemPromocaoDTO { IdPromocao = 1, IdProduto = 1, Desconto = 15.0m };
            var itemPromocao = new ItemPromocao(itemPromocaoDTO.IdPromocao, itemPromocaoDTO.IdProduto, itemPromocaoDTO.Desconto);

            _mockMapper.Setup(mapper => mapper.Map<ItemPromocao>(itemPromocaoDTO))
                .Returns(itemPromocao);
            _mockItemPromocaoService.Setup(service => service.AtualizarItemPromocao(itemPromocao))
                .Returns(Task.CompletedTask);

            // Act
            await _controller.Put(itemPromocaoDTO);

            // Assert
            _mockItemPromocaoService.Verify(service => service.AtualizarItemPromocao(itemPromocao), Times.Once);
        }

        [Fact]
        public async Task Delete_RemoveItemPromocao()
        {
            // Arrange
            var itemPromocaoId = 1;

            _mockItemPromocaoService.Setup(service => service.RemoverItemPromocao(itemPromocaoId))
                .Returns(Task.CompletedTask);

            // Act
            await _controller.Delete(itemPromocaoId);

            // Assert
            _mockItemPromocaoService.Verify(service => service.RemoverItemPromocao(itemPromocaoId), Times.Once);
        }
    }
}
