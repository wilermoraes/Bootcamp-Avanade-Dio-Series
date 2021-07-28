using Dio.Data.Repository;
using Dio.Domain.Entities;
using Dio.Domain.Enum;
using Dio_Application;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dio.Application
{
    class Program
    {
		static SerieRepository _serieRepository = new SerieRepository();
		public static List<Option> options;

		static void Main(string[] args)
        {
			ObterOpcoes();

			SelecionarOpcaoMenu(options, options[0]);

			NavegarMenu(0);

			Console.ReadKey();
		}

		private static void ObterOpcoes()
		{
			options = new List<Option>
			{
				new Option("Listar séries", () => ListarSeries(), 1),
				new Option("Inserir nova série", () =>  InserirSerie(), 2),
				new Option("Atualizar série", () =>  AtualizarSerie(), 3),
				new Option("Excluir série", () =>  ExcluirSerie(), 4),
				new Option("Visualizar série", () =>  VisualizarSerie(), 5),
				new Option("Exit", () => Environment.Exit(0), 0),
			};
		}

		static void SelecionarOpcaoMenu(List<Option> options, Option selectedOption)
		{
			Console.Clear();

			foreach (Option option in options)
			{
				if (option == selectedOption)
				{
					Console.Write(">>> ");
				}
				else
				{
					Console.Write(" ");
				}

				ColorMessage(option.Nome, ConsoleColor.Yellow);
			}
		}

		private static void NavegarMenu(int index)
        {
			ConsoleKeyInfo keyinfo;
			do
			{
				keyinfo = Console.ReadKey();

				if (keyinfo.Key == ConsoleKey.DownArrow)
				{
					if (index + 1 < options.Count)
					{
						index++;
						SelecionarOpcaoMenu(options, options[index]);
					}
				}
				else if (keyinfo.Key == ConsoleKey.UpArrow)
				{
					if (index - 1 >= 0)
					{
						index--;
						SelecionarOpcaoMenu(options, options[index]);
					}
				}
				else if (keyinfo.Key == ConsoleKey.Enter)
				{
					options[index].Metodo.Invoke();
				}
			}
			while (keyinfo.Key != ConsoleKey.X);
		}

		private static void ListarSeries()
		{
			Console.WriteLine("");
			ColorMessage("=== Listar séries ====", ConsoleColor.Cyan);

			var lista = _serieRepository.Listar();

			if (lista.Count == 0)
			{
				ColorMessage("*** Nenhuma série cadastrada. ***", ConsoleColor.Red);
				NavegarMenu(0);
			}

			foreach (var serie in lista)
			{
				ColorMessage($"#ID {serie.Id}: - {serie.Titulo}", ConsoleColor.Green);
			}
			NavegarMenu(0);
		}

		private static void VisualizarSerie()
		{
			Console.WriteLine("");
			ColorMessage("=== Atualizar série ===", ConsoleColor.Cyan);

			Console.WriteLine("");
			Console.Write("Informe o ID da série que deseja visualizar: ");

			if (!Guid.TryParse(Console.ReadLine(), out Guid indiceSerie))
			{
				ColorMessage("*** ID inválido ***", ConsoleColor.Red);
				NavegarMenu(4);
				return;
			}

			var serie = _serieRepository.RetornarPorId(indiceSerie);

			if (serie == null)
			{
				ColorMessage("*** Série não encontrada ***", ConsoleColor.Red);
				NavegarMenu(4);
				return;
			}

			ColorMessage(serie.ToString(), ConsoleColor.Green);
			NavegarMenu(4);
		}

		private static void InserirSerie()
		{
			Console.WriteLine("");
			ColorMessage("=== Inserir nova série ===", ConsoleColor.Cyan);

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero), i)}");
			}

			Console.WriteLine("");
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Serie novaSerie = new Serie(genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			_serieRepository.Inserir(novaSerie);
			SelecionarOpcaoMenu(options, options[1]);
		}

		private static void AtualizarSerie()
		{
			Console.WriteLine("");
			ColorMessage("=== Atualizar série ===", ConsoleColor.Cyan);
			
			Console.WriteLine("");
			Console.Write("Informe o ID da série que deseja atualizar: ");

            if (!Guid.TryParse(Console.ReadLine(), out Guid indiceSerie))
            {
                ColorMessage("*** ID inválido ***", ConsoleColor.Red);
                return;
            }

			var serie = _serieRepository.RetornarPorId(indiceSerie);
			if (serie == null)
			{
				ColorMessage("*** Série não encontrada ***", ConsoleColor.Red);
				return;
			}

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero), i)}");
			}

			Console.WriteLine("");
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Serie atualizaSerie = new Serie((Genero)entradaGenero, entradaTitulo, entradaDescricao, entradaAno );

			_serieRepository.Atualizar(indiceSerie, atualizaSerie);
			SelecionarOpcaoMenu(options, options.Where(x => x.Ordem == 3).FirstOrDefault());
		}

		private static void ExcluirSerie()
		{
			Console.WriteLine("");
			ColorMessage("=== Atualizar série ===", ConsoleColor.Cyan);

			Console.WriteLine("");
			Console.Write("Informe o ID da série que deseja excluir: ");
			
			if (!Guid.TryParse(Console.ReadLine(), out Guid indiceSerie))
			{
				ColorMessage("*** ID inválido ***", ConsoleColor.Red);
				return;
			}

			var serie = _serieRepository.RetornarPorId(indiceSerie);
			if (serie == null)
			{
				ColorMessage("*** Série não encontrada ***", ConsoleColor.Red);
				return;
			}

			_serieRepository.Excluir(indiceSerie);
			SelecionarOpcaoMenu(options, options.Where(x => x.Ordem == 4).FirstOrDefault());
		}

		private static void ColorMessage(string mensagem, ConsoleColor color)
        {
			Console.ForegroundColor = color;
			Console.WriteLine(mensagem);
			Console.ResetColor();
		}
	}
}
