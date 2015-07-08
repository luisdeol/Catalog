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
			DtoProduto produto = js.Deserialize<DtoProduto>(dtoProduto);

			if (produto.nome.Length < 3 || produto.idTipo < 1)
			{
				retorno = (new DtoExcecao(DTO.Enum.CriteriosDeCadastroInsuficientes, "Nome do produto e Tipo do Produto")).ToDto();
				return js.Serialize(retorno);
			}

			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
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

		public string abrirProduto(string dtoChave, string dtoProduto)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno = new DtoRetorno("ACK");
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				DtoProduto produto = js.Deserialize<DtoProduto>(dtoProduto);
				Produto mProduto = new Produto();
				produto = mProduto.abrirProduto(produto.id);
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

		public string pesquisarProduto(string dtoChave, string parametros)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoProduto param = js.Deserialize<DtoProduto>(parametros);

			if (param.nome.Length < 3 &&
				param.tipoCodigoDeBarras.Length < 3 &&
				(param.tipo == null || param.tipo.tipo.Length < 3) &&
				(param.fabricante == null || param.fabricante.fabricante.Length < 3))
			{
				retorno = (new DtoExcecao(DTO.Enum.CriteriosDeBuscaInsuficientes)).ToDto();
				return js.Serialize(retorno);
			}

			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Produto mProduto = new Produto();
				DtoProduto[] produtos = mProduto.pesquisarProduto(param);
				chave = mChave.atualizarChave(chave);
				retorno = new DtoRetornoObjeto(chave, produtos);
			}
			catch (DtoExcecao ex)
			{
				retorno = ex.ToDto();
			}
			catch (Exception ex)
			{
				retorno = new DtoRetornoErro(ex.Message);
			}

			/*Objeto: Array de DtoProdutos com DtoTipoProduto e DtoFabricante*/
			return js.Serialize(retorno);
		}

		public string buscarItem(string dtoChave, string dtoProduto, string dtoEstabelecimento)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno = new DtoRetorno("ACK");
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoProduto produto = js.Deserialize<DtoProduto>(dtoProduto);
			DtoEnderecoEstabelecimento enderecoEstabelecimento = js.Deserialize<DtoEnderecoEstabelecimento>(dtoEstabelecimento);

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Item mItem = new Item();
				DtoItem item = mItem.abrirItem(produto.id, enderecoEstabelecimento.id);
				chave = mChave.atualizarChave(chave);
				retorno = new DtoRetornoObjeto(chave, item);
			}
			catch (DtoExcecao ex)
			{
				retorno = ex.ToDto();
			}
			catch (Exception ex)
			{
				retorno = new DtoRetornoErro(ex.Message);
			}

			/*Objeto: DtoItem com DtoProduto com DtoTipoProduto e DtoFabricante*/
			return js.Serialize(retorno);
		}

		public string listarEstabelecimentosProssuidores(string dtoChave, string dtoProduto)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno = new DtoRetorno("ACK");
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoProduto produto = js.Deserialize<DtoProduto>(dtoProduto);

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Produto mProduto = new Produto();
				DtoItem[] itens = mProduto.estabelecimentosPossuidores(produto.id);
				chave = mChave.atualizarChave(chave);
				retorno = new DtoRetornoObjeto(chave, itens);
			}
			catch (DtoExcecao ex)
			{
				retorno = ex.ToDto();
			}
			catch (Exception ex)
			{
				retorno = new DtoRetornoErro(ex.Message);
			}

			/*Objeto: DtoItem com DtoProduto com DtoTipoProduto e DtoFabricante*/
			return js.Serialize(retorno);
		}
	}
}