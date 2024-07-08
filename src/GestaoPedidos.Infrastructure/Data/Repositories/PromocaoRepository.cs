using Amazon.DynamoDBv2;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace GestaoPedidos.Infrastructure.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class PromocaoRepository : DynamoDbService<Promocao>, IPromocaoRepository
    {
        public PromocaoRepository(IAmazonDynamoDB dynamoDbClient) : base(dynamoDbClient)
        {
        }
    }
}
