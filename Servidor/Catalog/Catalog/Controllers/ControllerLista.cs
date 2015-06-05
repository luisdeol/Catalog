using Catalog.Controllers.Interfaces;
using Catalog.DTO;
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

			/*codigo simulado*/
			lista.id = 1;
			lista.idUsuario = chave.idUsuario;
			lista.titulo = lista.titulo;
			retorno = new DtoRetornoObjeto(chave, lista);
			/*codigo simulado*/

			/*Objeto: DtoLista puro*/
			return js.Serialize(retorno);
		}

		public string abrirLista(string dtoChave, string dtoLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);

			/*codigo simulado*/
			DtoFabricante fabricante = new DtoFabricante();
			fabricante.id = 1;
			fabricante.fabricante = "IFRN";
			lista.produtosDaLista = new DtoProdutoDaLista[10];
			for (int i = 0; i < 10; i++)
			{
				lista.produtosDaLista[i] = new DtoProdutoDaLista();
				lista.produtosDaLista[i].id = i;
				lista.produtosDaLista[i].idLista = lista.id;
				lista.produtosDaLista[i].idProduto = i;
				lista.produtosDaLista[i].quantidade = i % 3 + 1;
				lista.produtosDaLista[i].produto = new DtoProduto();

				lista.produtosDaLista[i].produto.id = i;
				lista.produtosDaLista[i].produto.idFabricante = 1;
				lista.produtosDaLista[i].produto.idTipo = 1;
				lista.produtosDaLista[i].produto.nome = "produto " + i;
				lista.produtosDaLista[i].produto.tipoCodigoDeBarras = "asd";
				lista.produtosDaLista[i].produto.codigoDeBarras = ((i * 57928) % 1000).ToString();
				lista.produtosDaLista[i].produto.fabricante = fabricante;
			}
			retorno = new DtoRetornoObjeto(chave, lista);
			/*codigo simulado*/

			/*Objeto: DtoLista com array de DtoProdutosDaLista com o DtoProduto*/
			return js.Serialize(retorno);
		}

		public string editarLista(string dtoChave, string dtoLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);

			/*codigo simulado*/
			retorno = new DtoRetornoObjeto(chave);
			/*codigo simulado*/

			/*Objeto: DtoLista puro*/
			return js.Serialize(retorno);
		}

		public string excluirLista(string dtoChave, string dtoLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);

			/*codigo simulado*/
			retorno = new DtoRetornoObjeto(chave);
			/*codigo simulado*/

			/*Objeto: apenas a chave*/
			return js.Serialize(retorno);
		}

		public string pesquisarLista(string dtoChave, string parametros)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);

			/*codigo simulado*/
			DtoLista[] listas = new DtoLista[5];
			for (int i = 0; i < 5; i++)
			{
				listas[i] = new DtoLista();
				listas[i].id = i + 1;
				listas[i].idUsuario = chave.idUsuario;
				listas[i].titulo = "lista " + (i + 1);
			}
			retorno = new DtoRetornoObjeto(chave, listas);
			/*codigo simulado*/

			/*Objeto: Array de DtoLista*/
			return js.Serialize(retorno);
		}

		public string listarProdutos(string dtoChave, string dtoLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);

			/*codigo simulado*/
			DtoFabricante fabricante = new DtoFabricante();
			fabricante.id = 1;
			fabricante.fabricante = "IFRN";
			lista.produtosDaLista = new DtoProdutoDaLista[10];
			for (int i = 0; i < 10; i++)
			{
				lista.produtosDaLista[i] = new DtoProdutoDaLista();
				lista.produtosDaLista[i].id = i;
				lista.produtosDaLista[i].idLista = lista.id;
				lista.produtosDaLista[i].idProduto = i;
				lista.produtosDaLista[i].quantidade = i % 3 + 1;
				lista.produtosDaLista[i].produto = new DtoProduto();

				lista.produtosDaLista[i].produto.id = i;
				lista.produtosDaLista[i].produto.idFabricante = 1;
				lista.produtosDaLista[i].produto.idTipo = 1;
				lista.produtosDaLista[i].produto.nome = "produto " + i;
				lista.produtosDaLista[i].produto.tipoCodigoDeBarras = "asd";
				lista.produtosDaLista[i].produto.codigoDeBarras = ((i * 57928) % 1000).ToString();
				lista.produtosDaLista[i].produto.fabricante = fabricante;
			}
			retorno = new DtoRetornoObjeto(chave, lista);
			/*codigo simulado*/

			/*Objeto: DtoLista com array de DtoProdutosDaLista com o DtoProduto*/
			return js.Serialize(retorno);
		}

		public string listarItensEm(string dtoChave, string dtoLista, string dtoEnderecoEstabelecimento)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);
			DtoEnderecoEstabelecimento estabelecimento = js.Deserialize<DtoEnderecoEstabelecimento>(dtoEnderecoEstabelecimento);

			/*codigo simulado*/
			DtoFabricante fabricante = new DtoFabricante();
			fabricante.id = 1;
			fabricante.fabricante = "IFRN";
			lista.produtosDaLista = new DtoProdutoDaLista[10];
			for (int i = 0; i < 10; i++)
			{
				lista.produtosDaLista[i] = new DtoProdutoDaLista();
				lista.produtosDaLista[i].id = i;
				lista.produtosDaLista[i].idLista = lista.id;
				lista.produtosDaLista[i].idProduto = i;
				lista.produtosDaLista[i].quantidade = i % 3 + 1;
				lista.produtosDaLista[i].produto = new DtoProduto();

				lista.produtosDaLista[i].produto.id = i;
				lista.produtosDaLista[i].produto.idFabricante = 1;
				lista.produtosDaLista[i].produto.idTipo = 1;
				lista.produtosDaLista[i].produto.nome = "produto " + i;
				lista.produtosDaLista[i].produto.tipoCodigoDeBarras = "asd";
				lista.produtosDaLista[i].produto.codigoDeBarras = ((i * 57928) % 1000).ToString();
				lista.produtosDaLista[i].produto.fabricante = fabricante;

				if (i % 4 != 0)
				{
					lista.produtosDaLista[i].item = new DtoItem();
					lista.produtosDaLista[i].item.data = new DateTime(2015, 05, (i + 15));
					lista.produtosDaLista[i].item.id = i;
					lista.produtosDaLista[i].item.preco = (i * 2.87);
					lista.produtosDaLista[i].item.qualificacao = (i % 5) + 1;
					lista.produtosDaLista[i].item.idProduto = i;
					lista.produtosDaLista[i].item.idEstabelecimento = estabelecimento.id;
					lista.produtosDaLista[i].item.estabelecimento = estabelecimento;
				}

			}
			retorno = new DtoRetornoObjeto(chave, lista);
			/*codigo simulado*/

			/*Objeto: DtoLista com array de DtoProdutoDaLista contendo DtoProduto e DtoItem (no mesmo indice, caso o item exista), contendo o endereço do estab e o estab*/
			return js.Serialize(retorno);
		}

		public string adicionarProduto(string dtoChave, string dtoLista, string dtoProdutoDaLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);
			DtoLista lista = js.Deserialize<DtoLista>(dtoLista);
			DtoProdutoDaLista produtoDaLista = js.Deserialize<DtoProdutoDaLista>(dtoProdutoDaLista);

			/*codigo simulado*/
			produtoDaLista.id = 99;
			produtoDaLista.idLista = lista.id;
			produtoDaLista.idProduto = produtoDaLista.produto.id;
			retorno = new DtoRetornoObjeto(chave, produtoDaLista);
			/*codigo simulado*/

			/*Objeto: DtoProdutoDaLista com o DtoProduto*/
			return js.Serialize(retorno);
		}

		public string removerProduto(string dtoChave, string dtoLista, string dtoProduto)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;
			DtoChave chave = js.Deserialize<DtoChave>(dtoChave);

			/*codigo simulado*/
			retorno = new DtoRetornoObjeto(chave);
			/*codigo simulado*/

			/*Objeto: apenas a chave*/
			return js.Serialize(retorno);
		}
	}
}