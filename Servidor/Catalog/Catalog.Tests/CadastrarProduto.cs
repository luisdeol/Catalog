using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Script.Serialization;
using Catalog.Models;
using Catalog.Controllers;
using Catalog.DTO;

namespace Catalog.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class CadastrarProduto
	{
		public CadastrarProduto()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		//Variáveis
		private int idUsuario = 1;
		private JavaScriptSerializer js;
		private Chave mChave;
		private DtoChave chave;
		private ControllerProduto cProduto;
		private Produto mProduto;
		private DtoProduto produto;

		/*---------Initialize---------*/

		[TestInitialize]
		public void IniciarTestes()
		{
			js = new JavaScriptSerializer();
			mChave = new Chave();
			chave = mChave.criarChave(idUsuario);
			produto = new DtoProduto();
			cProduto = new ControllerProduto();
			mProduto = new Produto();
		}



		/*---------Testes Válidos---------*/



		[TestMethod] /*221.PV01*/
		public void CriarUmProdutoComValoresMinimos()
		{
			produto.id = 1;
			produto.nome = "aaa";
			produto.idTipo = 1;
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno sucesso
			Assert.Equals(retorno.tipoRetorno, "ACK");

			//Chave atualizada
			Assert.IsNotNull(((DtoRetornoObjeto)retorno).chave);
			DtoChave chaveAtualizada = (DtoChave)((DtoRetornoObjeto)retorno).chave;
			Assert.Equals(chaveAtualizada.idUsuario, chave.idUsuario);
			Assert.AreNotEqual(chaveAtualizada.token, chave.token);
			Assert.AreNotEqual(chaveAtualizada.ultimoAcesso, chave.ultimoAcesso);

			//Produto criado
			Assert.IsNotNull(((DtoRetornoObjeto)retorno).objeto);
			DtoProduto produtoCadastrado = (DtoProduto)((DtoRetornoObjeto)retorno).objeto;
			Assert.AreNotEqual(produtoCadastrado.id, produto.id);
			Assert.AreEqual(produtoCadastrado.nome, produto.nome);
			Assert.AreEqual(produtoCadastrado.idTipo, produto.idTipo);

			//Fabricante
			Assert.AreEqual(produtoCadastrado.idFabricante, produto.fabricante.id);
			Assert.AreEqual(produtoCadastrado.fabricante.id, 1);
			Assert.AreEqual(produtoCadastrado.fabricante.fabricante, "");

			chave = ((DtoRetornoObjeto)retorno).chave;
		}

		[TestMethod] /*221.PV02*/
		public void CriarUmProdutoComValoresMaximosEFabricanteComNomeMinimo()
		{
			produto.id = 2;
			produto.nome = "z99a a aerz99a a aerz99a a aerz99a a aerz99a a aerz99a a aerz99a a aerz99a a aerz99a a aerz99a a aer";
			produto.idTipo = 1;
			produto.codigoDeBarras = "01234567890123456789012345678901234567890123456789";
			produto.idFabricante = 2;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 2;
			produto.fabricante.fabricante = "zas";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno sucesso
			Assert.Equals(retorno.tipoRetorno, "ACK");

			//Chave atualizada
			Assert.IsNotNull(((DtoRetornoObjeto)retorno).chave);
			DtoChave chaveAtualizada = (DtoChave)((DtoRetornoObjeto)retorno).chave;
			Assert.Equals(chaveAtualizada.idUsuario, chave.idUsuario);
			Assert.AreNotEqual(chaveAtualizada.token, chave.token);
			Assert.AreNotEqual(chaveAtualizada.ultimoAcesso, chave.ultimoAcesso);

			//Produto criado
			Assert.IsNotNull(((DtoRetornoObjeto)retorno).objeto);
			DtoProduto produtoCadastrado = (DtoProduto)((DtoRetornoObjeto)retorno).objeto;
			Assert.AreNotEqual(produtoCadastrado.id, produto.id);
			Assert.AreEqual(produtoCadastrado.nome, produto.nome);
			Assert.AreEqual(produtoCadastrado.idTipo, produto.idTipo);
			Assert.AreEqual(produtoCadastrado.codigoDeBarras, "01234567890123456789012345678901234567890123456789");

			//Fabricante
			Assert.AreEqual(produtoCadastrado.idFabricante, produto.fabricante.id);
			Assert.AreEqual(produtoCadastrado.fabricante.id, produto.fabricante.id);
			Assert.AreEqual(produtoCadastrado.fabricante.fabricante, "zas");

			chave = ((DtoRetornoObjeto)retorno).chave;
		}

		[TestMethod] /*221.PV03*/
		public void EspacosConsecutivosIgnorados()
		{
			produto.id = 3;
			produto.nome = "z99a   p aerz99a a   aerz99a a aerz99a a aerz99a a aerz99a a aerz99a a aerz99a a aerz99a a aerz99a a aer";
			produto.idTipo = 1;
			produto.codigoDeBarras = "56789012345678901234567890123456789";
			produto.idFabricante = 3;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 3;
			produto.fabricante.fabricante = "zas  zas123zas  zas123zas  zas123zas  zas123zas  zas123";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno sucesso
			Assert.Equals(retorno.tipoRetorno, "ACK");

			//Chave atualizada
			Assert.IsNotNull(((DtoRetornoObjeto)retorno).chave);
			DtoChave chaveAtualizada = (DtoChave)((DtoRetornoObjeto)retorno).chave;
			Assert.Equals(chaveAtualizada.idUsuario, chave.idUsuario);
			Assert.AreNotEqual(chaveAtualizada.token, chave.token);
			Assert.AreNotEqual(chaveAtualizada.ultimoAcesso, chave.ultimoAcesso);

			//Produto criado
			Assert.IsNotNull(((DtoRetornoObjeto)retorno).objeto);
			DtoProduto produtoCadastrado = (DtoProduto)((DtoRetornoObjeto)retorno).objeto;
			Assert.AreNotEqual(produtoCadastrado.id, produto.id);
			Assert.AreEqual(produtoCadastrado.nome, produto.nome);
			Assert.AreEqual(produtoCadastrado.idTipo, produto.idTipo);
			Assert.AreEqual(produtoCadastrado.codigoDeBarras, "56789012345678901234567890123456789");

			//Fabricante
			Assert.AreEqual(produtoCadastrado.idFabricante, produto.fabricante.id);
			Assert.AreEqual(produtoCadastrado.fabricante.id, produto.fabricante.id);
			Assert.AreEqual(produtoCadastrado.fabricante.fabricante, ("zas  zas123zas  zas123zas  zas123zas  zas123zas  zas123").Trim());

			chave = ((DtoRetornoObjeto)retorno).chave;
		}

		[TestMethod] /*221.PV04*/
		public void CodigoDeBarrasSemEspacoEFabricanteJaCadastrado()
		{
			produto.id = 4;
			produto.nome = "zerr";
			produto.idTipo = 1;
			produto.codigoDeBarras = "0123456789 0123456789 0123456789 0123456789 0123456789";
			produto.idFabricante = 3;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 3;
			produto.fabricante.fabricante = "zas zas123zas zas123zas zas123zas zas123zas zas123";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno sucesso
			Assert.Equals(retorno.tipoRetorno, "ACK");

			//Chave atualizada
			Assert.IsNotNull(((DtoRetornoObjeto)retorno).chave);
			DtoChave chaveAtualizada = (DtoChave)((DtoRetornoObjeto)retorno).chave;
			Assert.Equals(chaveAtualizada.idUsuario, chave.idUsuario);
			Assert.AreNotEqual(chaveAtualizada.token, chave.token);
			Assert.AreNotEqual(chaveAtualizada.ultimoAcesso, chave.ultimoAcesso);

			//Produto criado
			Assert.IsNotNull(((DtoRetornoObjeto)retorno).objeto);
			DtoProduto produtoCadastrado = (DtoProduto)((DtoRetornoObjeto)retorno).objeto;
			Assert.AreNotEqual(produtoCadastrado.id, produto.id);
			Assert.AreEqual(produtoCadastrado.nome, produto.nome);
			Assert.AreEqual(produtoCadastrado.idTipo, produto.idTipo);
			Assert.AreEqual(produtoCadastrado.codigoDeBarras, "01234567890123456789012345678901234567890123456789");

			//Fabricante
			Assert.AreEqual(produtoCadastrado.idFabricante, produto.fabricante.id);
			Assert.AreEqual(produtoCadastrado.fabricante.id, produto.fabricante.id);
			Assert.AreEqual(produtoCadastrado.fabricante.fabricante, "zas zas123zas zas123zas zas123zas zas123zas zas123");

			chave = ((DtoRetornoObjeto)retorno).chave;
		}

		[TestMethod] /*221.PV05*/
		public void CadastrarProdutoRepetido()
		{
			produto.id = 4;
			produto.nome = "zerr";
			produto.idTipo = 1;
			produto.codigoDeBarras = "01234567890123456789012345678901234567890123456789";
			produto.idFabricante = 3;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 3;
			produto.fabricante.fabricante = "zas zas123zas zas123zas zas123zas zas123zas zas123";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno sucesso
			Assert.Equals(retorno.tipoRetorno, "ACK");

			//Chave atualizada
			Assert.IsNotNull(((DtoRetornoObjeto)retorno).chave);
			DtoChave chaveAtualizada = (DtoChave)((DtoRetornoObjeto)retorno).chave;
			Assert.Equals(chaveAtualizada.idUsuario, chave.idUsuario);
			Assert.AreNotEqual(chaveAtualizada.token, chave.token);
			Assert.AreNotEqual(chaveAtualizada.ultimoAcesso, chave.ultimoAcesso);

			//Produto criado
			Assert.IsNotNull(((DtoRetornoObjeto)retorno).objeto);
			DtoProduto produtoCadastrado = (DtoProduto)((DtoRetornoObjeto)retorno).objeto;
			Assert.AreNotEqual(produtoCadastrado.id, produto.id);
			Assert.AreEqual(produtoCadastrado.nome, produto.nome);
			Assert.AreEqual(produtoCadastrado.idTipo, produto.idTipo);
			Assert.AreEqual(produtoCadastrado.codigoDeBarras, "01234567890123456789012345678901234567890123456789");

			//Fabricante
			Assert.AreEqual(produtoCadastrado.idFabricante, produto.fabricante.id);
			Assert.AreEqual(produtoCadastrado.fabricante.id, produto.fabricante.id);
			Assert.AreEqual(produtoCadastrado.fabricante.fabricante, "zas zas123zas zas123zas zas123zas zas123zas zas123");

			chave = ((DtoRetornoObjeto)retorno).chave;
		}



		/*---------Testes VInválidos---------*/



		[TestMethod] /*221.PI01*/
		public void DtoChaveNulo()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "";

			string retornoJson = cProduto.criarProduto(js.Serialize(null), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "100");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "É nescessário realizar login para utilizar o aplicativo!");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI02*/
		public void ChaveDeAcessoEmFormatoDiferenteDeJSONDtoChave()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "";

			string retornoJson = cProduto.criarProduto(js.Serialize("ChaveDeAcessoEmFormatoDiferenteDeJSONDtoChave"), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "100");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "É nescessário realizar login para utilizar o aplicativo!");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI03*/
		public void ChaveDeAcessoComID0()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "";

			DtoChave chaveId0 = new DtoChave();
			chaveId0.idUsuario = 0;
			chaveId0.token = chave.token;
			chaveId0.ultimoAcesso = chave.ultimoAcesso;

			string retornoJson = cProduto.criarProduto(js.Serialize(chaveId0), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "100");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "É nescessário realizar login para utilizar o aplicativo!");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI04*/
		public void ChaveDeAcessoComIDInvalido()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "";

			DtoChave chaveInvalida = new DtoChave();
			chaveInvalida.idUsuario = 9999;
			chaveInvalida.token = chave.token;
			chaveInvalida.ultimoAcesso = chave.ultimoAcesso;

			string retornoJson = cProduto.criarProduto(js.Serialize(chaveInvalida), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "100");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "É nescessário realizar login para utilizar o aplicativo!");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI05*/
		public void ChaveDeAcessoComTokenInvalido()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "";

			DtoChave chaveInvalida = new DtoChave();
			chaveInvalida.idUsuario = chave.idUsuario;
			chaveInvalida.token = chave.token + "qwe";
			chaveInvalida.ultimoAcesso = chave.ultimoAcesso;

			string retornoJson = cProduto.criarProduto(js.Serialize(chaveInvalida), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "100");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "É nescessário realizar login para utilizar o aplicativo!");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI06*/
		public void ChaveDeAcessoComStringDeUltimoAcessoNaoConvertivelParaDateTime()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "";

			DtoChave chaveInvalida = new DtoChave();
			chaveInvalida.idUsuario = chave.idUsuario;
			chaveInvalida.token = chave.token;
			chaveInvalida.ultimoAcesso = "dataInvalida";

			string retornoJson = cProduto.criarProduto(js.Serialize(chaveInvalida), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "100");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "É nescessário realizar login para utilizar o aplicativo!");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI07*/
		public void DtoProdutoNulo()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(null));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "101");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "Dados foram perdidos durante o processo!");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI08*/
		public void DtoProdutoEmFormatoDiferenteDeJSONDtoProduto()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize("DtoProdutoEmFormatoDiferenteDeJSONDtoProduto"));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "101");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "Dados foram perdidos durante o processo!");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI09*/
		public void ProdutoComNomeComecandoComCaractereQueNaoSejaLetra()
		{
			produto.id = 5;
			produto.nome = "1we";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "104");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "Existem campos com valores inválidos:\nNome do Produto");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI10*/
		public void ProdutoComNomeComMenosCaracteresDoQueOPermitido()
		{
			produto.id = 5;
			produto.nome = "we";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "104");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "Existem campos com valores inválidos:\nNome do Produto");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI11*/
		public void ProdutoComNomeDeTamanhoMaiorDoQueOPermitido()
		{
			produto.id = 5;
			produto.nome = "a0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "104");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "Existem campos com valores inválidos:\nNome do Produto");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI12*/
		public void ProdutoComCodigoDeBarrasDeTamanhoMaiorQueOPermitido()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "101234567890123456789012345678901234567890123456789";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "104");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "Existem campos com valores inválidos:\nCódigo de Barras");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI13*/
		public void DtoProdutoNulo()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 0;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "104");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "Existem campos com valores inválidos:\nTipo do Produto");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI14*/
		public void ProdutoSemFabricante()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "101");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "Dados foram perdidos durante o processo!");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI15*/
		public void ProdutoComNomeDeFabricanteComPrimeiroCaractereInvalido()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "1sd";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "104");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "Existem campos com valores inválidos:\nNome do Fabricante");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI16*/
		public void ProdutoComNomeDeFabricanteMenorQueOtamanhoMinimoNaoNulo()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "as";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "104");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "Existem campos com valores inválidos:\nNome do Fabricante");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}

		[TestMethod] /*221.PI17*/
		public void ProdutoComFabricanteComNomeMaiorDoQueOPermitido()
		{
			produto.id = 5;
			produto.nome = "qwe";
			produto.idTipo = 1;
			produto.codigoDeBarras = "";
			produto.idFabricante = 1;
			produto.fabricante = new DtoFabricante();
			produto.fabricante.id = 1;
			produto.fabricante.fabricante = "a01234567890123456789012345678901234567890123456789";

			string retornoJson = cProduto.criarProduto(js.Serialize(chave), js.Serialize(produto));
			DtoRetorno retorno = new DtoRetorno("");
			try
			{
				retorno = js.Deserialize<DtoRetornoObjeto>(retornoJson);
			}
			catch
			{
				Assert.Fail();
			}

			//Retorno falha
			Assert.Equals(retorno.tipoRetorno, "NAK");

			//Dados do Erro
			Assert.Equals(((DtoRetornoErro)retorno).codigoErro, "104");
			Assert.Equals(((DtoRetornoErro)retorno).mensagem, "Existem campos com valores inválidos:\nNome do Fabricante");

			DtoProduto[] produtosCadastrados = mProduto.pesquisarProduto(produto);
			if (produtosCadastrados != null && produtosCadastrados.Length > 1)
				Assert.Fail();
		}
	}
}
