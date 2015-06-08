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
		void alterarSenha(int idUsuario, string novaSenha);
		void recuperarSenha(string email);
		DtoChave logar(string email, string senha);
		void deslogar(DtoChave chave);
	}
}