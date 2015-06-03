var app = angular.module("catalogApp",['ionic']);
app.controller("UsuarioController",function($scope,$http, $ionicPopup, $timeout){

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
					$scope.alerta("Sucesso","Logado com sucesso!",retorno.destino);	
				}
				else //erro
				{
					$scope.alerta("Ocorreu um erro",retorno.mensagem,"");
				}
			});
		}
		else //campos vazios
		{
			$scope.alerta("Ocorreu um erro","Preencha todos os campos!","");
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
							$scope.alerta("Sucesso","Cadastro realizado",retorno.destino);			
							
						}
						else //erro
						{
							$scope.alerta("Ocorreu um erro",retorno.mensagem,"");
						}
					});
				}
				else //email inválido
				{
					$scope.erro = true;
					document.getElementById("email").value = "Email incorreto!";
				}
			}
			else //senhas nao conferem
			{
				$scope.alerta("Ocorreu um erro","Senhas não conferem!","");
			}	
		}
		else
		{
			$scope.alerta("Ocorreu um erro","Preencha todos os campos!","");
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

//_______________ GEOLOCALIZAÇÃO ___________________//
function localizacao()
{
	window.localStorage.latitudeUsuario = -5.8123501;
	window.localStorage.longitudeUsuario = -35.2025723;
	
	if (navigator.geolocation)
		navigator.geolocation.getCurrentPosition(showPosition);
}
function showPosition(position) 
{
	window.localStorage.latitudeUsuario = position.coords.latitude;
	window.localStorage.longitudeUsuario = position.coords.longitude;
}