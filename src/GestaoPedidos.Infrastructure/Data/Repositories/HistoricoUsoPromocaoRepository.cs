using Amazon.DynamoDBv2;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;
using System.Diagnostics.CodeAnalysis;


namespace GestaoPedidos.Infrastructure.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class HistoricoUsoPromocaoRepository : DynamoDbService<HistoricoUsoPromocao>, IHistoricoUsoPromocaoRepository
    {
        public HistoricoUsoPromocaoRepository(IAmazonDynamoDB dynamoDbClient) : base(dynamoDbClient)
        {
        }
    }
}
