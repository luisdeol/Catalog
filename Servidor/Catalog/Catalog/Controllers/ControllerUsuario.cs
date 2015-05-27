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
	public class ControllerUsuario : IControllerUsuario
	{
		public string realizarCadastro(string dtoUsuario)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;

			return "";
		}

		public string alterarSenha(string dtoChave, string dtoUsuario)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;

			return "";
		}

		public string recuperarSenha(string dtoUsuario)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;

			return "";
		}

		public string logar(string dtoUsuario)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoUsuario usuario = js.Deserialize<DtoUsuario>(dtoUsuario);
			Usuario mUsuario = new Usuario();
			DtoRetorno retorno;

			try
			{
				DtoChave chave = mUsuario.logar(usuario.email, usuario.senha);
				retorno = new DtoRetornoObjeto(chave);
			}
			catch
			{
				retorno = new DtoRetornoErro("Combinação Email e Senha inválida!");
			}

			return js.Serialize(retorno);
		}

	}
}