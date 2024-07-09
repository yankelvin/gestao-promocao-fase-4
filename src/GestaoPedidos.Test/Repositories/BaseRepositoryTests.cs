using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Infrastructure.Data.Repositories;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace GestaoPedidos.Test.Repositories
{
    public class BaseRepositoryTests
    {
        [Fact]
        public async Task Cadastrar_EntityIsSaved_Successfully()
        {
            // Arrange
            var mockDynamoDbClient = new Mock<IAmazonDynamoDB>();
            var mockContext = new Mock<IDynamoDBContext>();

            var entity = new Entidade();
            mockContext.Setup(c => c.SaveAsync(It.IsAny<Entidade>(), default)).Returns(Task.CompletedTask);

            var service = new DynamoDbService<Entidade>(mockDynamoDbClient.Object, mockContext.Object);

            // Act
            await service.Cadastrar(entity);

            // Assert
            mockContext.Verify(c => c.SaveAsync(It.IsAny<Entidade>(), default), Times.Once);
        }

        [Fact]
        public async Task Atualizar_EntityIsUpdated_Successfully()
        {
            // Arrange
            var mockDynamoDbClient = new Mock<IAmazonDynamoDB>();
            var mockContext = new Mock<IDynamoDBContext>();

            var entity = new Entidade();
            mockContext.Setup(c => c.SaveAsync(It.IsAny<Entidade>(), default)).Returns(Task.CompletedTask);

            var service = new DynamoDbService<Entidade>(mockDynamoDbClient.Object, mockContext.Object);

            // Act
            await service.Atualizar(entity);

            // Assert
            mockContext.Verify(c => c.SaveAsync(It.IsAny<Entidade>(), default), Times.Once);
        }

        [Fact]
        public async Task Remover_EntityIsRemoved_Successfully()
        {
            // Arrange
            var mockDynamoDbClient = new Mock<IAmazonDynamoDB>();
            var mockContext = new Mock<IDynamoDBContext>();

            int entityId = 1;
            mockContext.Setup(c => c.DeleteAsync<Entidade>(It.IsAny<int>(), default)).Returns(Task.CompletedTask);

            var service = new DynamoDbService<Entidade>(mockDynamoDbClient.Object, mockContext.Object);

            // Act
            await service.Remover(entityId);

            // Assert
            mockContext.Verify(c => c.DeleteAsync<Entidade>(It.IsAny<int>(), default), Times.Once);
        }

        [Fact]
        public async Task Obter_NoParameters_ReturnsAllEntities()
        {
            // Arrange
            var mockDynamoDbClient = new Mock<IAmazonDynamoDB>();
            var mockContext = new Mock<IDynamoDBContext>();

            var entities = new List<Entidade>();
            var mockAsyncSearch = new Mock<AsyncSearch<Entidade>>();
            mockAsyncSearch.Setup(x => x.GetRemainingAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(entities);
            mockContext.Setup(c => c.ScanAsync<Entidade>(It.IsAny<List<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>()))
                        .Returns(mockAsyncSearch.Object);

            var service = new DynamoDbService<Entidade>(mockDynamoDbClient.Object, mockContext.Object);

            // Act
            var result = await service.Obter();

            // Assert
            Assert.Equal(entities, result);
        }
    }
}
