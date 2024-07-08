using Amazon.DynamoDBv2.DataModel;

namespace GestaoPedidos.Domain.Entities
{
    [DynamoDBTable("Promocao")]
    public class Promocao : Entidade
    {
        [DynamoDBProperty]
        public string Texto { get; private set; }

        [DynamoDBProperty]
        public bool Status { get; private set; }

        public Promocao(string texto, bool status)
        {
            Texto = texto;
            Status = status;
        }
    }
}
