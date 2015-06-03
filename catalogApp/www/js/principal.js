var app = angular.module("catalogApp",[]);
app.controller("UsuarioController",function($scope,$http){

    //___________________ LOGAR__________________//
	$scope.logar = function()
	{
		var email = $( "#email" ).val();
		var senha = $( "#senha" ).val();
		var json = "{email:'"+email+"',senha:'"+senha+"'}";
		
		
		if(email != "" && senha != "") //campos foram preenchidos
		{
			$http.post('http://localhost:51786/Webservices/WsUsuario.asmx/logar', {dtoUsuario:json}).
			  success(function(data, status, headers, config)
			{
				var retorno = $.parseJSON(data.d);	
				if(retorno.tipoRetorno == "ACK") //logado
				{
					window.localStorage.idUsuario = retorno.chave.idUsuario;
					window.localStorage.token = retorno.chave.token;
					window.localStorage.ultimoAcesso = retorno.chave.ultimoAcesso;
					alert("Logado com sucesso!");			
					window.location = retorno.destino;		
				}
				else //erro
				{
					alert(retorno.mensagem);
				}
			});
		}
		else //campos vazios
		{
			$scope.campoVazio = "Preencha todos os campos!";
		}
	}
	
	//___________________ CADASTRAR___________________//
	$scope.cadastrar = function()
	{
		var usuario = $( "#usuario" ).val();
		var email = $( "#email" ).val();		
		var senha = $( "#senha" ).val();
		var confirmarSenha = $( "#confirmarSenha" ).val();
		var json = "{email:'"+email+"',senha:'"+senha+"',nome:'"+usuario+"'}";
		
		if(senha == confirmarSenha) //senhas conferem
		{
			if(email != "" && senha != "" && usuario != "") //campos foram preenchidos
			{
				var filtro = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
				if(filtro.test(email))//email valido
				{
					$http.post('http://localhost:51786/Webservices/WsUsuario.asmx/realizarCadastro', {dtoUsuario:json}).
					  success(function(data, status, headers, config)
					{
						var retorno = $.parseJSON(data.d);	
						if(retorno.tipoRetorno == "ACK") //cadastrado
						{
							window.localStorage.idUsuario = retorno.chave.idUsuario;
							window.localStorage.token = retorno.chave.token;
							window.localStorage.ultimoAcesso = retorno.chave.ultimoAcesso;
							alert("Cadastro realizado com sucesso!");			
							window.location = retorno.destino;
						}
						else //erro
						{
							alert(retorno.mensagem);
						}
					});
				}
				else //email inválido
				{
					$scope.erro = true;
					document.getElementById("email").value = "Email incorreto!";
				}
			}
			else //campos vazios
			{
				$scope.campoVazio = "Preencha todos os campos!";
			}
		}
		else //senhas nao conferem
		{
			$scope.campoVazio = "Senhas não conferem!";
		}	
	}	

});