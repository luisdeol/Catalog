using Catalog.DTO;
using Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Catalog.Webservices
{
	/// <summary>
	/// Summary description for Testes
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[System.Web.Script.Services.ScriptService]
	public class Testes : System.Web.Services.WebService
	{

		[WebMethod]
		public string cadastrarFabricante(string fabricante)
		{
			Fabricante fab = new Fabricante();
			DtoFabricante dtoFab = new DtoFabricante();
			dtoFab.fabricante = fabricante;
			dtoFab = fab.cadastrarFabricante(dtoFab);
			return "idFabricante = " + dtoFab.id;
		}
	}
}
