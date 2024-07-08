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

        public void AlterarTexto(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                throw new ArgumentException("Texto da promocao nao pode ser nulo e nem vazio.");

            Texto = texto;
        }

        public void AlterarStatus(bool status)
        {
            Status = status;
        }
    }
}
