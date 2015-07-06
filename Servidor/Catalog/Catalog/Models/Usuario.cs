using Catalog.DTO;
using Catalog.Linq;
using Catalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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


        public DtoChave alterarDadosCadastrais(string email, string novaSenha)
		{
            Chave mChave = new Chave();
            DBCatalogDataContext dataContext = new DBCatalogDataContext();
            var usuarios = dataContext.tb_Usuarios.FirstOrDefault(u => u.email == email);
            if (usuarios != null)
            {
                DtoChave chave = mChave.criarChave(usuarios.id);
                usuarios.senha = novaSenha;
                dataContext.SubmitChanges();
                return chave;
            }
            else
            {
                throw new DtoExcecao(DTO.Enum.CampoInvalido, "Email incorreto!");
            }
		}


		public void recuperarSenha(string email)
		{
            DBCatalogDataContext dataContext = new DBCatalogDataContext();
            var usuarioBanco = dataContext.tb_Usuarios.FirstOrDefault(u => u.email == email); ;

            if(usuarioBanco != null)
            {
                Random senhaAlternativa = new Random ();
                senhaAlternativa.Next(999999);
                DateTime dataAtual = DateTime.Today;

                //enviar email
                SmtpClient cliente = new SmtpClient();
                cliente.Host = "smtp.gmail.com";
                cliente.EnableSsl = true;
                cliente.Credentials = new NetworkCredential("sistemadecomprasdigitais@gmail.com", "comprasdigitais"); //email e sennha 

                cliente.Send("sistemadecomprasdigitais@gmail.com", email,
                "Recuperar senha", "Sua senha alternativa é:" + senhaAlternativa.ToString());

                //banco
                var senhaAlternativaBanco = new Linq.tb_SenhaAlternativa();
                senhaAlternativaBanco.senha = senhaAlternativa.ToString();
                senhaAlternativaBanco.idUsuario = usuarioBanco.id;
                senhaAlternativaBanco.dataDeCriacao = dataAtual;

                dataContext.tb_SenhaAlternativas.InsertOnSubmit(senhaAlternativaBanco);
                dataContext.SubmitChanges();
            }
            else
            {
                throw new DtoExcecao(DTO.Enum.CampoInvalido, "E-mail não cadastrado!");
            }
		}

		public DtoChave logar(string email, string senha)
		{
			Chave mChave = new Chave();

            DBCatalogDataContext dataContext = new DBCatalogDataContext();
            var usuarioBanco = dataContext.tb_Usuarios.FirstOrDefault(u => u.email == email && u.senha == senha);
            var usuarioSenhaAlternativaBanco = dataContext.tb_SenhaAlternativas.FirstOrDefault(u => u.tb_Usuario.email == email && u.senha == senha);

            if (usuarioBanco != null)
            {
                if (usuarioSenhaAlternativaBanco != null)
                dataContext.tb_SenhaAlternativas.DeleteOnSubmit(usuarioSenhaAlternativaBanco);

                DtoChave chave = mChave.criarChave(usuarioBanco.id);
                return chave;
            }
            else if (usuarioSenhaAlternativaBanco != null)
            {
                DtoChave chave = mChave.criarChave(usuarioBanco.id);
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