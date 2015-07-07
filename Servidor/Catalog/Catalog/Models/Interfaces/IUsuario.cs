using Catalog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models.Interfaces
{
	public interface IUsuario
	{
		DtoChave cadastrarUsuario(DtoUsuario usuario);
        DtoChave alterarDadosCadastrais(string email, string novaSenha);
		void recuperarSenha(string email);
		DtoChave logar(string email, string senha);
	}
}