using Catalog.DTO;
using Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Catalog.Controllers
{
    public class ControllerEstabelecimento
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
                estabelecimento = mEstabelecimento.criarEstabelecimento(enderecoEstabelecimento);
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
			/*Objeto: Array de DtoItem com DtoProduto*/
			return null;
		}

		public string pesquisarProdutos(string dtoChave, string dtoEnderecoEstabelecimento)
		{
			/*Objeto: Array de DtoItem com DtoProduto*/
			return null;
		}
    }
}