using Catalog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models.Interfaces
{
	public interface IFabricante
	{
		DtoFabricante cadastrarFabricante(DtoFabricante fabricante);
		DtoFabricante abrirFabricante(int idFabricante);
		DtoFabricante[] procurarFabricante(string fabricante);
	}
}