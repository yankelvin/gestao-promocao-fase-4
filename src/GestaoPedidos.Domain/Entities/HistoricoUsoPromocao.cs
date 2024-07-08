using Amazon.DynamoDBv2.DataModel;

namespace GestaoPedidos.Domain.Entities
{
    [DynamoDBTable("HistoricoUsoPromocao")]
    public class HistoricoUsoPromocao : Entidade
    {
        [DynamoDBProperty]
        public int IdPromocao { get; private set; }

        [DynamoDBProperty]
        public int IdCliente { get; private set; }

        [DynamoDBProperty]
        public bool Utilizado { get; private set; }

        public HistoricoUsoPromocao(int idPromocao, int idCliente, bool utilizado)
        {
            IdPromocao = idPromocao;
            IdCliente = idCliente;
            Utilizado = utilizado;
        }
    }
}
