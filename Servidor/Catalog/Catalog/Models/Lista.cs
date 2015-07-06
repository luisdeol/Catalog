using Catalog.DTO;
using Catalog.Linq;
using Catalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
	public class Lista : ILista
	{
		public DtoLista criarLista(DtoLista lista)
		{
			lista.titulo = lista.titulo.Trim();
			if (lista.titulo == "")
				throw new DtoExcecao(DTO.Enum.CampoInvalido, "Titulo inválido");

			DBCatalogDataContext dataContext = new DBCatalogDataContext();

			var ListasSimilaresBanco = from l in dataContext.tb_Listas where l.titulo.StartsWith(lista.titulo) select l;

			if (ListasSimilaresBanco.Count() >= 1)
				lista.titulo += " " + DateTime.Now.ToString();

			tb_Lista listaBanco = new tb_Lista();
			listaBanco.titulo = lista.titulo;
			listaBanco.idUsuario = lista.idUsuario;
			dataContext.tb_Listas.InsertOnSubmit(listaBanco);
			dataContext.SubmitChanges();

			lista.id = dataContext.tb_Listas.First(l => l.idUsuario == lista.idUsuario && l.titulo == lista.titulo).id;

			return lista;
		}

		public DtoLista abrirLista(int idLista)
		{
			if (idLista < 1)
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "lista solicitada");

			DBCatalogDataContext dataContext = new DBCatalogDataContext();
			tb_Lista listaBanco;

			try
			{
				listaBanco = dataContext.tb_Listas.First(l => l.id == idLista);
			}
			catch
			{
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "lista solicitada");
			}

			DtoLista lista = new DtoLista();
			lista.id = listaBanco.id;
			lista.titulo = listaBanco.titulo;
			lista.idUsuario = Convert.ToInt32(listaBanco.idUsuario);

			if (listaBanco.tb_ProdutoDaListas.Count > 0)
			{
				lista.produtosDaLista = new DtoProdutoDaLista[listaBanco.tb_ProdutoDaListas.Count];
				int i = 0;
				DtoProdutoDaLista produtoDaLista;
				Produto mProduto = new Produto();
				foreach (tb_ProdutoDaLista produtoDaListaBanco in listaBanco.tb_ProdutoDaListas)
				{
					produtoDaLista = new DtoProdutoDaLista();
					produtoDaLista.id = Convert.ToInt32(produtoDaListaBanco.id);
					produtoDaLista.idLista = Convert.ToInt32(produtoDaListaBanco.idLista);
					produtoDaLista.idProduto = Convert.ToInt32(produtoDaListaBanco.idProduto);
					produtoDaLista.produto = mProduto.abrirProduto(produtoDaLista.idProduto);
					produtoDaLista.quantidade = Convert.ToInt32(produtoDaListaBanco.quantidade);
					lista.produtosDaLista[i++] = produtoDaLista;
				}
			}

			return lista;
		}

		public void editarLista(int idLista, string novoNome)
		{
			/**/
		}

		public void excluirLista(int idLista)
		{
			/**/
		}

		public DtoLista[] pesquisarListas(int idUsuario)
		{
			DtoLista[] listas;
			DBCatalogDataContext dataContext = new DBCatalogDataContext();

			var listasBanco = from l in dataContext.tb_Listas where l.idUsuario == idUsuario select l;
			if (listasBanco.Count() < 1)
			{
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "suas listas");
			}
			else
			{
				listas = new DtoLista[listasBanco.Count()];
				int i = 0;
				foreach (tb_Lista listaBanco in listasBanco)
				{
					listas[i] = new DtoLista();
					listas[i].id = listaBanco.id;
					listas[i].titulo = listaBanco.titulo;
					listas[i].idUsuario = idUsuario;
					i++;
				}
			}

			return listas;
		}

		public DtoLista listarItensEm(int idLista, int idEstabelecimento)
		{
			if (idLista < 1)
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "lista solicitada");
			if (idEstabelecimento < 1)
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "estabelecimento solicitado");

			DBCatalogDataContext dataContext = new DBCatalogDataContext();
			tb_Lista listaBanco;
            tb_EnderecoEstabelecimento EnderecoEstabelecimento;

			try
				{listaBanco = dataContext.tb_Listas.First(l => l.id == idLista);}
			catch
				{throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "lista solicitada");}

			try
				{EnderecoEstabelecimento = dataContext.tb_EnderecoEstabelecimentos.First(ee => ee.id == idEstabelecimento);}
			catch
				{throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "estabelecimento solicitado");}

			DtoLista lista = new DtoLista();
			lista.id = listaBanco.id;
			lista.titulo = listaBanco.titulo;
			lista.idUsuario = Convert.ToInt32(listaBanco.idUsuario);

			if (listaBanco.tb_ProdutoDaListas.Count > 0)
			{
				lista.produtosDaLista = new DtoProdutoDaLista[listaBanco.tb_ProdutoDaListas.Count];
				int i = 0;
				DtoProdutoDaLista produtoDaLista;
				Item mItem = new Item();
				foreach (tb_ProdutoDaLista produtoDaListaBanco in listaBanco.tb_ProdutoDaListas)
				{
					produtoDaLista = new DtoProdutoDaLista();
					produtoDaLista.id = Convert.ToInt32(produtoDaListaBanco.id);
					produtoDaLista.idLista = Convert.ToInt32(produtoDaListaBanco.idLista);
					produtoDaLista.idProduto = Convert.ToInt32(produtoDaListaBanco.idProduto);
					produtoDaLista.item = mItem.abrirItem(produtoDaLista.idProduto, idEstabelecimento);
					produtoDaLista.quantidade = Convert.ToInt32(produtoDaListaBanco.quantidade);
					lista.produtosDaLista[i++] = produtoDaLista;
				}
			}

			return lista;
		}

		public DtoProdutoDaLista adicionarProduto(DtoProdutoDaLista produto)
		{
			return null;
		}

		public void removerProduto(int idProduto)
		{
			/**/
		}
	}
}