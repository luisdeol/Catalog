using Catalog.Controllers.Interfaces;
using Catalog.DTO;
using Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Catalog.Controllers
{
	public class ControllerProduto : IControllerProduto
	{
		public string criarProduto(string dtoChave, string dtoProduto)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				DtoProduto produto = js.Deserialize<DtoProduto>(dtoProduto);
				Produto mProduto = new Produto();
				produto = mProduto.cadastrarProduto(produto);
				chave = mChave.atualizarChave(chave);
				retorno = new DtoRetornoObjeto(chave, produto);
			}
			catch (DtoExcecao ex)
			{
				retorno = ex.ToDto();
			}
			catch (Exception ex)
			{
				retorno = new DtoRetornoErro(ex.Message);
			}

			/*Objeto: DtoProduto com DtoTipoProduto e DtoFabricante*/
			return js.Serialize(retorno);
		}

		/*---------------Não Implementados---------------*/

		public string abrirProduto(string dtoChave, string dtoProduto)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno = new DtoRetorno("ACK");
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoProduto produto = js.Deserialize<DtoProduto>(dtoProduto);

			/*Objeto: */
			return js.Serialize(retorno);
		}

		public string pesquisarProduto(string dtoChave, string parametros)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno = new DtoRetorno("ACK");
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);

			/*Objeto: */
			return js.Serialize(retorno);
		}

		public string buscarItem(string dtoChave, string dtoProduto, string dtoEstabelecimento)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno = new DtoRetorno("ACK");
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoProduto produto = js.Deserialize<DtoProduto>(dtoProduto);

			/*Objeto: */
			return js.Serialize(retorno);
		}

		public string buscarItens(string dtoChave, string dtoProduto)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno = new DtoRetorno("ACK");
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoProduto produto = js.Deserialize<DtoProduto>(dtoProduto);

			/*Objeto: */
			return js.Serialize(retorno);
		}

		public string listarEstabelecimentosProssuidores(string dtoChave, string dtoProduto)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno = new DtoRetorno("ACK");
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoProduto produto = js.Deserialize<DtoProduto>(dtoProduto);

			/*Objeto: */
			return js.Serialize(retorno);
		}
	}
}