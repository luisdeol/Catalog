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
	public class ControllerLista : IControllerLista
	{
		public string criarLista(string dtoChave, string dtoLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Lista mLista = new Lista();
				lista.idUsuario = chave.idUsuario;
				lista = mLista.criarLista(lista);
				chave = mChave.atualizarChave(chave);
				retorno = new DtoRetornoObjeto(chave, lista);
			}
			catch (DtoExcecao ex)
			{
				retorno = ex.ToDto();
			}
			catch (Exception ex)
			{
				retorno = new DtoRetornoErro(ex.Message);
			}

			/*Objeto: DtoLista puro*/
			return js.Serialize(retorno);
		}

		public string abrirLista(string dtoChave, string dtoLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Lista mLista = new Lista();
				lista = mLista.abrirLista(lista.id);
				chave = mChave.atualizarChave(chave);
				retorno = new DtoRetornoObjeto(chave, lista);
			}
			catch (DtoExcecao ex)
			{
				retorno = ex.ToDto();
			}
			catch (Exception ex)
			{
				retorno = new DtoRetornoErro(ex.Message);
			}

			/*Objeto: DtoLista com array de DtoProdutosDaLista com o DtoProduto*/
			return js.Serialize(retorno);
		}


		public string editarLista(string dtoChave, string dtoLista) 
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
            DtoLista lista = js.Deserialize<DtoLista>(dtoLista);

            Chave mChave = new Chave();

            try
            {
                mChave.validarChave(chave);
                Lista mLista = new Lista();
                mLista.editarLista(lista.id, lista.titulo);
                chave = mChave.atualizarChave(chave);
                retorno = new DtoRetornoObjeto(chave);
            }
            catch (DtoExcecao ex)
            {
                retorno = ex.ToDto();
            }
            catch (Exception ex)
            {
                retorno = new DtoRetornoErro(ex.Message);
            }

			/*Objeto: DtoLista puro*/
			return js.Serialize(retorno);
		}


		public string excluirLista(string dtoChave, string dtoLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);

            Chave mChave = new Chave();
            try
            {
                mChave.validarChave(chave);
                Lista mLista = new Lista();
                mLista.excluirLista(lista.id);
                chave = mChave.atualizarChave(chave);
                retorno = new DtoRetornoObjeto(chave); 
            }
            catch (DtoExcecao ex)
            {
                retorno = ex.ToDto();
            }
            catch (Exception ex)
            {
                retorno = new DtoRetornoErro(ex.Message);
            }

			/*Objeto: apenas a chave*/
			return js.Serialize(retorno);
		}

		public string pesquisarLista(string dtoChave)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoLista[] listas;

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Lista mLista = new Lista();
				listas = mLista.pesquisarListas(chave.idUsuario);
				chave = mChave.atualizarChave(chave);
				retorno = new DtoRetornoObjeto(chave, listas);
			}
			catch (DtoExcecao ex)
			{
				retorno = ex.ToDto();
			}
			catch (Exception ex)
			{
				retorno = new DtoRetornoErro(ex.Message);
			}

			/*Objeto: Array de DtoLista*/
			return js.Serialize(retorno);
		}

		public string listarItensEm(string dtoChave, string dtoLista, string dtoEnderecoEstabelecimento)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);
			DtoEnderecoEstabelecimento estabelecimento = js.Deserialize<DtoEnderecoEstabelecimento>(dtoEnderecoEstabelecimento);

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Lista mLista = new Lista();
				lista = mLista.listarItensEm(lista.id, estabelecimento.id);
				chave = mChave.atualizarChave(chave);
				retorno = new DtoRetornoObjeto(chave, lista);
			}
			catch (DtoExcecao ex)
			{
				retorno = ex.ToDto();
			}
			catch (Exception ex)
			{
				retorno = new DtoRetornoErro(ex.Message);
			}

			/*Objeto: DtoLista com array de DtoProdutoDaLista contendo DtoProduto e DtoItem (no mesmo indice, caso o item exista), contendo o endereço do estab e o estab*/
			return js.Serialize(retorno);
		}

		/*Não Implementado*/
		public string adicionarProduto(string dtoChave, string dtoLista, string dtoProdutoDaLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);
		

            Chave mChave = new Chave();

            try
            {
                mChave.validarChave(chave);
                DtoProdutoDaLista produtoDaLista = js.Deserialize<DtoProdutoDaLista>(dtoProdutoDaLista);
                Lista mLista = new Lista();
                produtoDaLista = mLista.adicionarProduto(produtoDaLista);
                chave = mChave.atualizarChave(chave);
                retorno = new DtoRetornoObjeto(chave, produtoDaLista);
            }
            catch (DtoExcecao ex)
            {
                retorno = ex.ToDto();
            }
            catch (Exception ex)
            {
                retorno = new DtoRetornoErro(ex.Message);
            }

			/*Objeto: DtoProdutoDaLista com o DtoProduto*/
			return js.Serialize(retorno);
		}

		public string removerProduto(string dtoChave, string dtoLista, string dtoProduto)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
            DtoProdutoDaLista produtoDaLista = js.Deserialize<DtoProdutoDaLista>(dtoProduto);

            try
            {
                Chave mChave = new Chave();
                mChave.validarChave(chave);
                Lista mLista = new Lista();
                mLista.removerProduto(produtoDaLista.id);
                chave = mChave.atualizarChave(chave);
                retorno = new DtoRetornoObjeto(chave);
            }
            catch (DtoExcecao ex)
            {
                retorno = ex.ToDto();
            }
            catch (Exception ex)
            {
                retorno = new DtoRetornoErro(ex.Message);
            }

			/*Objeto: apenas a chave*/
			return js.Serialize(retorno);
		}
	}
}