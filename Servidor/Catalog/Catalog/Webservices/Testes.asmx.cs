using Catalog.DTO;
using Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
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
		public string abrirItem(int idProduto, int idEstabelecimento)
		{
			Item mItem = new Item();
			JavaScriptSerializer js = new JavaScriptSerializer();
			return js.Serialize(mItem.abrirItem(idProduto, idEstabelecimento));
		}

		[WebMethod]
		public string cadastrarFabricante(string fabricante)
		{
			Fabricante fab = new Fabricante();
			DtoFabricante dtoFab = new DtoFabricante();
			dtoFab.fabricante = fabricante;
			dtoFab = fab.cadastrarFabricante(dtoFab);
			return "idFabricante = " + dtoFab.id;
		}

		[WebMethod]
		public string pesquisarFabricante(string fabricante)
		{
			Fabricante fab = new Fabricante();
			DtoFabricante[] fabs = fab.procurarFabricante(fabricante);
			string retorno = "";
			foreach (DtoFabricante f in fabs)
				retorno += "id: " + f.id + " - nome: " + f.fabricante + "\n";
			return retorno;
		}

		[WebMethod]
		public string criarEstabelecimento(string fabricante)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			string retorno = "";
			DtoEnderecoEstabelecimento ee = new DtoEnderecoEstabelecimento();
			ee.cep = "59129-020";
			ee.cidade = "natal";
			ee.estabelecimento = new DtoEstabelecimento();
			ee.estabelecimento.nome = "nordestao";
			ee.estado = "rn";
			ee.numero = "123";
			ee.rua = "rua";
			return js.Serialize(ee); ;
		}
	}
}
