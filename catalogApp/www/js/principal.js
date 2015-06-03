var app = angular.module("catalogApp",['ionic']);
app.controller("UsuarioController",function($scope,$http, $ionicPopup, $timeout){

    //___________________ LOGAR__________________//
	$scope.logar = function(usuario)
	{
		var email = usuario.email;
		var senha = usuario.senha;
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
					$scope.alerta("Ocorreu um erro",retorno.mensagem);
				}
			});
		}
		else //campos vazios
		{
			$scope.alerta("Ocorreu um erro","Preencha todos os campos!");
		}
	}
	
	//___________________ CADASTRAR___________________//
	$scope.cadastrar = function(user)
	{
		var usuario = user.nome;
		var email =  user.email;
		var senha = user.senha;
		var confirmarSenha = user.confirmarSenha;
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
							$scope.alerta("Ocorreu um erro",retorno.mensagem);
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
				$scope.alerta("Ocorreu um erro","Preencha todos os campos!");
			}
		}
		else //senhas nao conferem
		{
			$scope.alerta("Ocorreu um erro","Senhas não conferem!");
		}	
	}	
	
	//___________ VERIFICAR LOGIN _____________//
	$scope.verificarLogin = function(lugarPagina)
	{
		var idUsuario = window.localStorage.idUsuario;
		var token = window.localStorage.token;
		var ultimoAcesso = window.localStorage.ultimoAcesso;
		
		if((idUsuario != undefined && idUsuario != "") && 
			(token != undefined && token != "") && 
			(ultimoAcesso != undefined && ultimoAcesso != "")) //ta logado
		{
			if(lugarPagina != "principal.html")
				window.location = "principal.html";				
		}
		else //nao esta logado
		{
			if(lugarPagina == "principal.html")
			window.location = "login.html";
		}	
	};
	
	//______________ LOGOUT _____________//
	$scope.logout = function()
	{
		window.localStorage.idUsuario = "";
		window.localStorage.token = "";
		window.localStorage.ultimoAcesso = "";
		window.location = "login.html";
	};
	
	//____________ ALERTA ____________//
	$scope.alerta = function(mensagem,subMensagem)
	{
		var alertPopup = $ionicPopup.alert({
		title: mensagem,
		template: subMensagem
		});
	};
	
});