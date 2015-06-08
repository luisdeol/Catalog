using Catalog.DTO;
using Catalog.Linq;
using Catalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
	public class TipoProduto : ITipoProduto
	{
		public DtoTipo abrirTipo(int idTipo)
		{
			if (idTipo < 1)
				throw new DtoExcecao(DTO.Enum.CampoInvalido, "Tipo do produto");

			DtoTipo tipo;
			DBCatalogDataContext dataContext = new DBCatalogDataContext();
			try
			{
				tb_Tipo tipoBanco = dataContext.tb_Tipos.First(t => t.id == idTipo);
				tipo = new DtoTipo();
				tipo.id = tipoBanco.id;
				tipo.tipo = tipoBanco.tipo;
			}
			catch
			{
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "Tipo do produto");
			}

			return tipo;
		}

		public DtoTipo abrirTipo(string tipo)
		{
			tipo = tipo.Trim();
			DtoTipo dtoTipo;
			DBCatalogDataContext dataContext = new DBCatalogDataContext();
			try
			{
				tb_Tipo tipoBanco = dataContext.tb_Tipos.First(t => t.tipo == tipo);
				dtoTipo = new DtoTipo();
				dtoTipo.id = tipoBanco.id;
				dtoTipo.tipo = tipoBanco.tipo;
			}
			catch
			{
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "Tipo do produto");
			}

			return dtoTipo;
		}

	}
}