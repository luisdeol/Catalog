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
    public class ControllerEstabelecimento : IControllerEstabelecimento
	{
		public string criarEstabelecimento(string dtoChave, string dtoEnderecoEstabelecimento)
		{

            JavaScriptSerializer js = new JavaScriptSerializer();
            DtoRetorno retorno;
            DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
            DtoEnderecoEstabelecimento enderecoEstabelecimento = js.Deserialize<DtoEnderecoEstabelecimento>(dtoEnderecoEstabelecimento);
            DtoEnderecoEstabelecimento estabelecimento;

            Chave mChave = new Chave();

            try
            {
                mChave.validarChave(chave);
                Estabelecimento mEstabelecimento = new Estabelecimento();
                estabelecimento = mEstabelecimento.cadastrarEstabelecimento(enderecoEstabelecimento);
                chave = mChave.atualizarChave(chave);
                retorno = new DtoRetornoObjeto(chave, estabelecimento);
            }
            catch (DtoExcecao ex)
            {
                retorno = ex.ToDto();
            }
            catch (Exception ex)
            {
                retorno = new DtoRetornoErro(ex.Message);
            }

			/*Objeto: DtoEnderecoEstabelecimento com DtoEstabelecimento*/
            return js.Serialize(retorno);
		}

		public string pesquisarEstabelecimentos(string dtoChave, string dtoEnderecoEstabelecimento)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoEnderecoEstabelecimento enderecoEstabelecimento = js.Deserialize<DtoEnderecoEstabelecimento>(dtoEnderecoEstabelecimento);
			DtoEnderecoEstabelecimento[] enderecosEstabelecimento;

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Estabelecimento mEstabelecimento = new Estabelecimento();
				enderecosEstabelecimento = mEstabelecimento.procurarEstabelecimento(enderecoEstabelecimento);
				chave = mChave.atualizarChave(chave);
				retorno = new DtoRetornoObjeto(chave, enderecosEstabelecimento);
			}
			catch (DtoExcecao ex)
			{
				retorno = ex.ToDto();
			}
			catch (Exception ex)
			{
				retorno = new DtoRetornoErro(ex.Message);
			}

			/*Objeto: Array de DtoEnderecoEstabelecimento com DtoEstabelecimento*/
			return js.Serialize(retorno);
		}

		public string listarProdutos(string dtoChave, string dtoEnderecoEstabelecimento)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoEnderecoEstabelecimento enderecoEstabelecimento = js.Deserialize<DtoEnderecoEstabelecimento>(dtoEnderecoEstabelecimento);
			DtoItem[] produtosDoEstabelecimento;

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Estabelecimento mEstabelecimento = new Estabelecimento();
				chave = mChave.atualizarChave(chave);

				DtoProduto parametros = new DtoProduto();
				parametros.nome = "";
				parametros.idTipo = 0;
				parametros.codigoDeBarras = "";
				parametros.fabricante = new DtoFabricante();
				parametros.fabricante.fabricante = "";

				produtosDoEstabelecimento = mEstabelecimento.procurarProduto(enderecoEstabelecimento, parametros);
				retorno = new DtoRetornoObjeto(chave, produtosDoEstabelecimento);
			}
			catch (DtoExcecao ex)
			{
				retorno = ex.ToDto();
			}
			catch (Exception ex)
			{
				retorno = new DtoRetornoErro(ex.Message);
			}

			/*Objeto: Array de DtoItem com DtoProduto*/
			return js.Serialize(retorno);
		}

		public string pesquisarProdutos(string dtoChave, string dtoEnderecoEstabelecimento, string parametros)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoEnderecoEstabelecimento enderecoEstabelecimento = js.Deserialize<DtoEnderecoEstabelecimento>(dtoEnderecoEstabelecimento);
			DtoProduto parametrosProduto = js.Deserialize<DtoProduto>(parametros);
			DtoItem[] produtosDoEstabelecimento;

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Estabelecimento mEstabelecimento = new Estabelecimento();
				chave = mChave.atualizarChave(chave);
				produtosDoEstabelecimento = mEstabelecimento.procurarProduto(enderecoEstabelecimento, parametrosProduto);
				retorno = new DtoRetornoObjeto(chave, produtosDoEstabelecimento);
			}
			catch (DtoExcecao ex)
			{
				retorno = ex.ToDto();
			}
			catch (Exception ex)
			{
				retorno = new DtoRetornoErro(ex.Message);
			}

			/*Objeto: Array de DtoItem com DtoProduto*/
			return js.Serialize(retorno);
		}

		public string abrirEstabelecimento(string dtoChave, string dtoEnderecoEstabelecimento)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoEnderecoEstabelecimento enderecoEstabelecimento = js.Deserialize<DtoEnderecoEstabelecimento>(dtoEnderecoEstabelecimento);
			DtoEnderecoEstabelecimento estabelecimento;

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Estabelecimento mEstabelecimento = new Estabelecimento();
				estabelecimento = mEstabelecimento.abrirEstabelecimento(enderecoEstabelecimento.id);
				//estabelecimento.itens = mEstabelecimento.procurarProduto(estabelecimento, new DtoProduto());
				chave = mChave.atualizarChave(chave);
				retorno = new DtoRetornoObjeto(chave, estabelecimento);
			}
			catch (DtoExcecao ex)
			{
				retorno = ex.ToDto();
			}
			catch (Exception ex)
			{
				retorno = new DtoRetornoErro(ex.Message);
			}

			/*Objeto: DtoEnderecoEstabelecimento com DtoEstabelecimento e Array de DtoItem com DtoProduto*/
			return js.Serialize(retorno);
		}
    }
}