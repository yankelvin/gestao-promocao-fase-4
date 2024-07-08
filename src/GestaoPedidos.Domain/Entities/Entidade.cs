using Amazon.DynamoDBv2.DataModel;

namespace GestaoPedidos.Domain.Entities
{
    public class Entidade
    {
        [DynamoDBHashKey]
        public int Id;
    }
}
