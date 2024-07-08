using Amazon.DynamoDBv2;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;

namespace GestaoPedidos.Infrastructure.Data.Repositories
{
    public class PromocaoRepository : DynamoDbService<Promocao>, IPromocaoRepository
    {
        public PromocaoRepository(IAmazonDynamoDB dynamoDbClient) : base(dynamoDbClient)
        {
        }
    }
}
