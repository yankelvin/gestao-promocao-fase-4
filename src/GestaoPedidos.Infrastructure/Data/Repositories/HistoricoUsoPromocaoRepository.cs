using Amazon.DynamoDBv2;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;

namespace GestaoPedidos.Infrastructure.Data.Repositories
{
    public class HistoricoUsoPromocaoRepository : DynamoDbService<HistoricoUsoPromocao>, IHistoricoUsoPromocaoRepository
    {
        public HistoricoUsoPromocaoRepository(IAmazonDynamoDB dynamoDbClient) : base(dynamoDbClient)
        {
        }
    }
}
