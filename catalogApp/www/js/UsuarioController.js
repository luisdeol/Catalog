var app = angular.module("catalogApp",['ionic'])
.config(function($stateProvider, $urlRouterProvider) {
	
    $stateProvider
		.state('home', {
			url: "/home",
			controller: "UsuarioController",
			templateUrl: "home.html"
    });
	
	$stateProvider
		.state('logar', {
			url: "/logar",
			controller: "UsuarioController",
			templateUrl: "logar.html"
    });
	
	$stateProvider
		.state('cadastrar', {
			url: "/cadastrar",
			controller: "UsuarioController",
			templateUrl: "cadastrar.html"
    });
	
	$urlRouterProvider.otherwise('/home')
})

.controller("UsuarioController",function($scope,$ionicModal,$http,$ionicPopup,$timeout){

    //___________________ LOGAR__________________//
	$scope.logar = function(usuario)
	{
		if(usuario != undefined)//campos foram preenchidos
		{
			var email = usuario.email;
			var senha = usuario.senha;
			var json = "{email:'"+email+"',senha:'"+senha+"'}";

			$http.post('http://localhost:51786/Webservices/WsUsuario.asmx/logar', {dtoUsuario:json}).
			  success(function(data, status, headers, config)
			{
				var retorno = angular.fromJson(data.d);	
				if(retorno.tipoRetorno == "ACK") //logado
				{
					window.localStorage.idUsuario = retorno.chave.idUsuario;
					window.localStorage.token = retorno.chave.token;
					window.localStorage.ultimoAcesso = retorno.chave.ultimoAcesso;
					$scope.alerta("Sucesso","Logado com sucesso!","principal.html");	
				}
				else //erro
				{
					$scope.alerta("Ocorreu um erro",retorno.mensagem,"index.html#/tab/login");
				}
			});
		}
		else //campos vazios
		{
			$scope.alerta("Ocorreu um erro","Preencha todos os campos!","index.html#/tab/login");
			return false;
		}
	}
	
	//___________________ CADASTRAR___________________//
	$scope.cadastrar = function(user)
	{
		if(user != undefined) //campos foram preenchidos
		{	
			var usuario = user.nome;
			var email =  user.email;
			var senha = user.senha;
			var confirmarSenha = user.confirmarSenha;
			var json = "{email:'"+email+"',senha:'"+senha+"',nome:'"+usuario+"'}";
			
			if(senha == confirmarSenha) //senhas conferem
			{
				var filtro = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
				if(filtro.test(email))//email valido
				{
					$http.post('http://localhost:51786/Webservices/WsUsuario.asmx/realizarCadastro', {dtoUsuario:json}).
					  success(function(data, status, headers, config)
					{
						var retorno = angular.fromJson(data.d);	
						if(retorno.tipoRetorno == "ACK") //cadastrado
						{
							window.localStorage.idUsuario = retorno.chave.idUsuario;
							window.localStorage.token = retorno.chave.token;
							window.localStorage.ultimoAcesso = retorno.chave.ultimoAcesso;
							$scope.alerta("Sucesso","Cadastro realizado","principal.html");			
							return true;
						}
						else //erro
						{
							$scope.alerta("Ocorreu um erro",retorno.mensagem,"index.html#/tab/cadastro");
							return false;
						}
					});
				}
				else //email inválido
				{
					$scope.erro = true;
					document.getElementById("email").value = "Email incorreto!";
					return false;
				}
			}
			else //senhas nao conferem
			{
				$scope.alerta("Ocorreu um erro","Senhas não conferem!","index.html#/tab/cadastro");
				return false;
			}	
		}
		else
		{
			$scope.alerta("Ocorreu um erro","Preencha todos os campos!","index.html#/tab/cadastro");
			return false;
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
				window.location = "index.html#/tab/login";
		}	
	};

	
	//____________ ALERTA ____________//
	$scope.alerta = function(mensagem,subMensagem,destino)
	{
		var alertPopup = $ionicPopup.alert({
		title: mensagem,
		template: subMensagem
		});
		
		 $timeout(function() 
		{
		  window.location = destino;
		  alertPopup.close();
		}, 3000);
	};
});
