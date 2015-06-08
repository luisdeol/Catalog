using Catalog.DTO;
using Catalog.Linq;
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
            Chave mChave = new Chave();

            DBCatalogDataContext dataContext = new DBCatalogDataContext();
			var usuarioBanco = dataContext.tb_Usuarios.FirstOrDefault(u => u.email == usuario.email);

            if(usuarioBanco == null) //nenhum email encontrado (cadastrar usuario)
            {
                usuarioBanco = new Linq.tb_Usuario();
				usuarioBanco.email = usuario.email;
				usuarioBanco.senha = usuario.senha;
				usuarioBanco.nome = usuario.nome;
                dataContext.tb_Usuarios.InsertOnSubmit(usuarioBanco);
                dataContext.SubmitChanges();

                //criando chave
				var usuarioRecemCadastrado = dataContext.tb_Usuarios.FirstOrDefault(u => u.email == usuario.email);
                DtoChave chave = mChave.criarChave(usuarioRecemCadastrado.id);
                return chave;
            }
            else
            {
				throw new DtoExcecao(DTO.Enum.CampoInvalido, "Email ja existente");
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

            DBCatalogDataContext dataContext = new DBCatalogDataContext();
            var usuarios = dataContext.tb_Usuarios.FirstOrDefault(u => u.email == email && u.senha == senha);
            if (usuarios != null)
            {
                DtoChave chave = mChave.criarChave(usuarios.id);
                return chave;
            }
            else
            {
				throw new DtoExcecao(DTO.Enum.CampoInvalido, "Email e Senha não conferem");
            }
		}

		public void deslogar(DtoChave chave)
		{

		}
	}
}