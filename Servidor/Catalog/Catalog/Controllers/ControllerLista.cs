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
		public string criarLista(string dtoLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = new DtoChave();
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Lista mLista = new Lista();
				lista.idUsuario = chave.idUsuario;
				lista = mLista.criarLista(lista);
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

		public string abrirLista(string dtoLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = new DtoChave();
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Lista mLista = new Lista();
				lista = mLista.abrirLista(lista.id);
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


		public string editarLista(string dtoLista) 
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = new DtoChave();
            DtoLista lista = js.Deserialize<DtoLista>(dtoLista);

            Chave mChave = new Chave();

            try
            {
                mChave.validarChave(chave);
                Lista mLista = new Lista();
                mLista.editarLista(lista.id, lista.titulo);
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


		public string excluirLista(string dtoLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = new DtoChave();
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);

            Chave mChave = new Chave();
            try
            {
                mChave.validarChave(chave);
                Lista mLista = new Lista();
                mLista.excluirLista(lista.id);
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
            DtoChave chave = new DtoChave();
			DtoLista[] listas;

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Lista mLista = new Lista();
				listas = mLista.pesquisarListas(chave.idUsuario);
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

		public string listarItensEm(string dtoLista, string dtoEnderecoEstabelecimento)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
            DtoChave chave = new DtoChave();
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);
			DtoEnderecoEstabelecimento estabelecimento = js.Deserialize<DtoEnderecoEstabelecimento>(dtoEnderecoEstabelecimento);

			Chave mChave = new Chave();

			try
			{
				mChave.validarChave(chave);
				Lista mLista = new Lista();
				lista = mLista.listarItensEm(lista.id, estabelecimento.id);
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
		public string adicionarProduto(string dtoLista, string dtoProdutoDaLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
            DtoChave chave = new DtoChave();
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);
		

            Chave mChave = new Chave();

            try
            {
                mChave.validarChave(chave);
                DtoProdutoDaLista produtoDaLista = js.Deserialize<DtoProdutoDaLista>(dtoProdutoDaLista);
                Lista mLista = new Lista();
                produtoDaLista = mLista.adicionarProduto(produtoDaLista);
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

		public string removerProduto(string dtoLista, string dtoProduto)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
            DtoChave chave = new DtoChave();
            DtoProdutoDaLista produtoDaLista = js.Deserialize<DtoProdutoDaLista>(dtoProduto);

            try
            {
                Chave mChave = new Chave();
                mChave.validarChave(chave);
                Lista mLista = new Lista();
                mLista.removerProduto(produtoDaLista.id);
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