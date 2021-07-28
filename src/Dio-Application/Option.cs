using System;

namespace Dio_Application
{
    public class Option
    {
        public string Nome { get; }
        public Action Metodo { get; }
        public int Ordem { get; }

        public Option(string nome, Action metodo, int ordem)
        {
            Nome = nome;
            Metodo = metodo;
            Ordem = ordem;
        }
    }
}
