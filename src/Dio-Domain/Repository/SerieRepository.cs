using Dio.Domain.Entities;
using Dio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dio.Data.Repository
{
    public class SerieRepository : IRepository<Serie>
    {
        List<Serie> series = new List<Serie>();

        public List<Serie> Listar()
        {
            return series;
        }

        public Serie RetornarPorId(Guid id)
        {
            return series.FirstOrDefault(s => s.Id == id);
        }

        public void Inserir(Serie serie)
        {
            series.Add(serie);
        }

        public void Atualizar(Guid id, Serie serie)
        {
            var serieUpdate = RetornarPorId(id);
            serieUpdate.AtualizarValores(serie.Genero, serie.Titulo, serie.Descricao, serie.Ano);
        }

        public void Excluir(Guid id)
        {
            var serie = series.Where(s => s.Id == id).FirstOrDefault();

            if (serie != null)
                series.Remove(serie);
        }
    }
}
