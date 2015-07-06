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
					DtoChave chave = mUsuario.cadastrarUsuario(usuario);
                    retorno = new DtoRetornoObjeto(chave,"#/menu");
				}
				catch (DtoExcecao ex)
				{
					retorno = ex.ToDto();
				}
				catch (Exception ex)
				{
					retorno = new DtoRetornoErro(ex.Message);
				}
            }
            else
            {
				string camposInvalidos = "";
				if (usuario.nome.Length >= 3) camposInvalidos += "Nome\n";
	            if (rg.IsMatch(usuario.email)) camposInvalidos += "E-mail\n";
				if (usuario.senha.Length > 3) camposInvalidos += "Senha";
				retorno = (new DtoExcecao(DTO.Enum.CampoInvalido, camposInvalidos)).ToDto();
            }

            return js.Serialize(retorno);
		}

        //______________ ALTERAR SENHA ________________//
		public string alterarSenha(string dtoChave, string dtoUsuario)
		{
            JavaScriptSerializer js = new JavaScriptSerializer();
            DtoUsuario usuario = js.Deserialize<DtoUsuario>(dtoUsuario);
            DtoRetorno retorno;

            try
            {
                Usuario mUsuario = new Usuario();
                mUsuario.recuperarSenha(usuario.email);
                retorno = new DtoRetorno("ACK","");
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

		public string recuperarSenha(string dtoUsuario)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoRetorno retorno;

			return "";
		}

        //______________ LOGAR ________________//
		public string logar(string dtoUsuario)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			DtoUsuario usuario = js.Deserialize<DtoUsuario>(dtoUsuario);
			Usuario mUsuario = new Usuario();
			DtoRetorno retorno;

			try
			{
				DtoChave chave = mUsuario.logar(usuario.email, usuario.senha);
                retorno = new DtoRetornoObjeto(chave, "#/menu");
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

        //______________ ALTERAR DADOS CADASTRAIS ________________//
        public string alterarDadosCadastrais(string dtoUsuario, string novaSenha)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            DtoUsuario usuario = js.Deserialize<DtoUsuario>(dtoUsuario);
            Usuario mUsuario = new Usuario();
            DtoRetorno retorno;

            try
            {
                DtoChave chave = mUsuario.alterarDadosCadastrais(usuario.email, novaSenha);
                retorno = new DtoRetornoObjeto(chave, "#/menu");
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

	}
}