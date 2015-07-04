using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Controllers.Interfaces
{
	public interface IControllerUsuario
	{
		string realizarCadastro(string dtoUsuario);
		string alterarSenha(string dtoChave, string dtoUsuario);
		string recuperarSenha(string dtoUsuario);
		string logar(string dtoUsuario);
	}
}