using Catalog.DTO;
using Catalog.Linq;
using Catalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
	public class Fabricante : IFabricante
	{
		public DtoFabricante cadastrarFabricante(DtoFabricante fabricante)
		{
			DBCatalogDataContext dataContext = new DBCatalogDataContext();
			tb_Fabricante fabricanteEmBanco = dataContext.tb_Fabricantes.FirstOrDefault(f => f.fabricante.ToLower() == fabricante.fabricante.ToLower());
			if (fabricanteEmBanco != null)
			{
				fabricante.id = fabricanteEmBanco.id;
			}
			else
			{
				fabricanteEmBanco = new tb_Fabricante();
				fabricanteEmBanco.fabricante = fabricante.fabricante;
				dataContext.tb_Fabricantes.InsertOnSubmit(fabricanteEmBanco);
				dataContext.SubmitChanges();

				fabricante.id = dataContext.tb_Fabricantes.FirstOrDefault(f => f.fabricante.ToLower() == fabricante.fabricante.ToLower()).id;
			}
			return fabricante;
		}

		public DtoFabricante abrirFabricante(int idFabricante)
		{
			return new DtoFabricante();
		}

		public DtoFabricante[] procurarFabricante(string fabricante)
		{
			return new DtoFabricante[5];
		}
	}
}