using System;

namespace Mongo.Domain.Enums
{
    public enum CozinhaEnum
    {
        Brasileira = 1,
        Italiana = 2,
        Japonesa = 3,
        FastFood = 4
    }

    public static class CozinhaHelper
    {
        public static CozinhaEnum ToString(int valor)
        {
            if (Enum.TryParse(valor.ToString(), out CozinhaEnum cozinha))
                return cozinha;
            
            throw new ArgumentOutOfRangeException("cozinha");
        }
    }
}