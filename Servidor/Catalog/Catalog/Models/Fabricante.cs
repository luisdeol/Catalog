using Catalog.DTO;
using Catalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
	public class Fabricante : IFabricante
	{
		public DtoFabricante cadastrarFabricante(DtoFabricante fabricante);
		public DtoFabricante abrirFabricante(int idFabricante);
		public DtoFabricante[] procurarFabricante(string fabricante);
	}
}