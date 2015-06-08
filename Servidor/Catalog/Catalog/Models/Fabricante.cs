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
			fabricante.fabricante = fabricante.fabricante.Trim();
			if (fabricante.fabricante == "")
				throw new Exception(); //nome do fabricante inválido

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
			if (idFabricante < 1)
				throw new Exception(); //id do fabricante inválido

			DtoFabricante fabricante;
			DBCatalogDataContext dataContext = new DBCatalogDataContext();
			try
			{
				tb_Fabricante fabricanteBanco = dataContext.tb_Fabricantes.First(f => f.id == idFabricante);

				fabricante = new DtoFabricante();
				fabricante.fabricante = fabricanteBanco.fabricante;
				fabricante.id = fabricanteBanco.id;
			}
			catch (Exception ex)
			{
				throw ex; //fabricante inexistente
			}

			return fabricante;
		}

		public DtoFabricante[] procurarFabricante(string fabricante)
		{
			List<DtoFabricante> fabricantes = new List<DtoFabricante>();

			DBCatalogDataContext dataContext = new DBCatalogDataContext();
			var fabricantesBanco = from f in dataContext.tb_Fabricantes where f.fabricante.Contains(fabricante) select f;

			if (fabricantesBanco.Count() < 1)
				throw new Exception(); //nenhum fabricante encontrado

			DtoFabricante fab;
			foreach(tb_Fabricante fabBanco in fabricantesBanco)
			{
				fab = new DtoFabricante();
				fab.id = fabBanco.id;
				fab.fabricante = fabBanco.fabricante;
				fabricantes.Add(fab);
			}

			return fabricantes.ToArray();
		}
	}
}