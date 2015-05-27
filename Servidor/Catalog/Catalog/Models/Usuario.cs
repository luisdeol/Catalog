using Catalog.DTO;
using Catalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
	public class Usuario : IUsuario
	{
		public DtoChave cadastrarUsuario(DtoUsuario usuario)
		{
			return new DtoChave();
		}

		public void alterarSenha(int idUsuario, string novaSenha)
		{

		}

		public void recuperarSenha(string email)
		{

		}

		public DtoChave logar(string email, string senha)
		{
			Chave mChave = new Chave();

            var dataContext = new DBCatalogDataContext();
            var usuarios = from users in dataContext.tb_Usuarios
                           where users.email == email && users.senha == senha
                           select users;

            if (usuarios.Count() == 1)
            {
                //cadastrar no banco e transformar o objeto em DTO
                DtoUsuario usuario = new DtoUsuario();

                DtoChave chave = mChave.criarChave(usuario.id);

                return chave;
            }
            else
            {
                throw new Exception();
            }
		}

		public void deslogar(DtoChave chave)
		{

		}
	}
}