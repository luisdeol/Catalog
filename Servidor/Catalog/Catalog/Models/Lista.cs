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
				lista.titulo = lista.titulo + " " + DateTime.Now.ToString();

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
            DBCatalogDataContext dataContext = new DBCatalogDataContext();
            tb_Lista listaBanco;
            try 
            { 
                listaBanco = dataContext.tb_Listas.First(l => l.id == idLista); 
            }
            catch 
            {
                throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado);
            }
            listaBanco.titulo = novoNome;
            dataContext.SubmitChanges();
		}

		public void excluirLista(int idLista)
		{
            DBCatalogDataContext dataContext = new DBCatalogDataContext();
            tb_Lista listaBanco;
            try
            {
                listaBanco = dataContext.tb_Listas.First(l => l.id == idLista);
            }
            catch { throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "lista"); }

            foreach(tb_ProdutoDaLista produto in listaBanco.tb_ProdutoDaListas)
            {
                dataContext.tb_ProdutoDaListas.DeleteOnSubmit(produto);
            }

            dataContext.tb_Listas.DeleteOnSubmit(listaBanco);
            dataContext.SubmitChanges();
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
            DBCatalogDataContext dataContext = new DBCatalogDataContext();
            

            var produtosListaExistentes = from p in dataContext.tb_ProdutoDaListas where p.idProduto.Equals(produto.id) select p;
            if (produtosListaExistentes.Count() > 1)
            {
                produto.quantidade = produto.quantidade + 1;
                dataContext.SubmitChanges();
            }
            else
            {
                tb_ProdutoDaLista produtoLista = new tb_ProdutoDaLista() ;
                produtoLista.idLista = produto.idLista;
                produtoLista.idProduto = produto.idProduto;
                produtoLista.quantidade = produto.quantidade;
                dataContext.tb_ProdutoDaListas.InsertOnSubmit(produtoLista);
                dataContext.SubmitChanges();
                produto.id = dataContext.tb_ProdutoDaListas.FirstOrDefault(p => p.idProduto == produto.idProduto &&
                    p.idLista == produto.idLista).id;
            }
			return produto;
		}

		public void removerProduto(int idProduto)
		{
            DBCatalogDataContext dataContext = new DBCatalogDataContext();
            tb_ProdutoDaLista produtoLista;
            try
            {
                produtoLista = (dataContext.tb_ProdutoDaListas.First(p => p.idProduto == idProduto));
            }
            catch { throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "produto da lista"); }
            dataContext.tb_ProdutoDaListas.DeleteOnSubmit(produtoLista);
            dataContext.SubmitChanges();
		}
	}
}