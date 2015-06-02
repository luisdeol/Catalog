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
		public DtoChave cadastrarUsuario(string email, string senha, string nome)
		{
            Chave mChave = new Chave();

            Linq.DBCatalogDataContext dataContext = new Linq.DBCatalogDataContext();
            var usuario = dataContext.tb_Usuarios.FirstOrDefault(u => u.email == email);

            if(usuario == null) //nenhum email encontrado (cadastrar usuario)
            {
                usuario = new Linq.tb_Usuario();
                usuario.email = email;
                usuario.senha = senha;
                usuario.nome = nome;
                dataContext.tb_Usuarios.InsertOnSubmit(usuario);
                dataContext.SubmitChanges();

                //criando chave
                var usuarioRecemCadastrado = dataContext.tb_Usuarios.FirstOrDefault(u => u.email == email);
                DtoChave chave = mChave.criarChave(usuarioRecemCadastrado.id);
                return chave;
            }
            else
            {
                throw new Exception();
            }
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