namespace Mongo.Domain.Dtos.Nota
{
    public class Avaliacao 
    {
        public int Estrelas { get; private set; }
        public string Comentario { get; private set; }

        public Avaliacao(int estrelas, string comentario)
        {
            Estrelas = estrelas;
            Comentario = comentario;
        }
    }
}
