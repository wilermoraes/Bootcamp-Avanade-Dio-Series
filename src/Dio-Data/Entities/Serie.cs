using Dio.Domain.Entities.Base;
using Dio.Domain.Enum;
using System;

namespace Dio.Domain.Entities
{
    public class Serie : BaseEntity
    {
		public Genero Genero { get; private set; }
		public string Titulo { get; private set; }
		public string Descricao { get; private set; }
		public int Ano { get; private set; }

		// Métodos
		public Serie(Genero genero, string titulo, string descricao, int ano)
		{
			Genero = genero;
			Titulo = titulo;
			Descricao = descricao;
			Ano = ano;
		}

		public override string ToString()
		{
			string retorno = "";
			retorno += $"Gênero: {Genero}{Environment.NewLine}";
			retorno += $"Titulo: {Titulo}{Environment.NewLine}";
			retorno += $"Descrição: {Descricao}{Environment.NewLine}";
			retorno += $"Ano de Início: {Ano}{Environment.NewLine}";
			return retorno;
		}

		public void AtualizarValores(Genero genero, string titulo, string descricao, int ano)
        {
			Genero = genero;
			Titulo = titulo;
			Descricao = descricao;
			Ano = ano;
		}

	}
}
