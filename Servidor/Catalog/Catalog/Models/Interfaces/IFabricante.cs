using Catalog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models.Interfaces
{
	public class IFabricante
	{
		public DtoFabricante cadastrarFabricante(DtoFabricante fabricante);
		public DtoFabricante abrirFabricante(int idFabricante);
		public DtoFabricante[] procurarFabricante(string fabricante);
	}
}