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

            Linq.DBCatalogDataContext dataContext = new Linq.DBCatalogDataContext();
            var usuarios = dataContext.tb_Usuarios.FirstOrDefault(u => u.email == email && u.senha == senha);
            if (usuarios != null)
            {
                DtoChave chave = mChave.criarChave(usuarios.id);
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