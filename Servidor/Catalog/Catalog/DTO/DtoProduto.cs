using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.DTO
{
	public class DtoProduto
	{
		public int id { get; set; }
		public string nome { get; set; }
		public string codigoDeBarras { get; set; }
		public string tipoCodigoDeBarras { get; set; }
		public int idTipo { get; set; }
		public DtoTipo tipo { get; set; }
		public int idFabricante { get; set; }
		public DtoFabricante fabricante { get; set; }
	}
}