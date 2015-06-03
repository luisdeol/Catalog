using Catalog.Controllers.Interfaces;
using Catalog.DTO;
using Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace Catalog.Controllers
{
	public class ControllerUsuario : IControllerUsuario
	{
        //_____________ CADASTRAR USUARIO _____________//
		public string realizarCadastro(string dtoUsuario)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
            DtoUsuario usuario = js.Deserialize<DtoUsuario>(dtoUsuario);
            Usuario mUsuario = new Usuario();
			DtoRetorno retorno;
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            //---validando nome, email, senha
            if(
              (usuario.nome.Length >= 3) &&
              (rg.IsMatch(usuario.email)) &&  
              (usuario.senha.Length >3)  )
            {
                try
                {
                    DtoChave chave = mUsuario.cadastrarUsuario(usuario.email, usuario.senha, usuario.nome);
                    retorno = new DtoRetornoObjeto(chave,"principal.html");
                }
                catch
                {
                    retorno = new DtoRetornoErro("E-mail já cadastrado!");
                }
            }
            else
            {
                return js.Serialize(new DtoRetornoErro("Existem campos inválidos!"));
            }

            return js.Serialize(retorno);
		}

        //______________ ALTERAR SENHA ________________//
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
				retorno = new DtoRetornoObjeto(chave,"principal.html");
			}
			catch
			{
				retorno = new DtoRetornoErro("Combinação Email e Senha inválida!");
			}

			return js.Serialize(retorno);
		}

	}
}