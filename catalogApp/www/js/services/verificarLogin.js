angular.module('services.verificarLogin', [])

.factory('verificarLogin', [function () {

	var verificarLogin = {};
	var idUsuario = window.localStorage.idUsuario;
	var token = window.localStorage.token;
	var ultimoAcesso = window.localStorage.ultimoAcesso;

   verificarLogin.verificarUsuario = function(lugarPagina)
   {
		var idUsuario = window.localStorage.idUsuario;
		var token = window.localStorage.token;
		var ultimoAcesso = window.localStorage.ultimoAcesso;
		
		if((idUsuario != undefined && idUsuario != "") && 
			(token != undefined && token != "") && 
			(ultimoAcesso != undefined && ultimoAcesso != "")) //ta logado
		{
			if(lugarPagina != "principal.html")
				window.location = "#/menu";				
		}
		else //nao esta logado
		{
			if(lugarPagina == "principal.html")
				window.location = "#/home";
		}	
   }
   
   verificarLogin.verificarLista = function(lugarPagina)
   {
		var idUsuario = window.localStorage.idUsuario;
		var token = window.localStorage.token;
		var ultimoAcesso = window.localStorage.ultimoAcesso;
	   
		if((idUsuario != undefined && idUsuario != "") && 
			(token != undefined && token != "") && 
			(ultimoAcesso != undefined && ultimoAcesso != "")) //ta logado
		{
			if(lugarPagina != "listas.html")
				window.location = "#/listas";	
		}
		else //nao esta logado
		{
			window.location = "#/home";
		}	
   }
   
    verificarLogin.verificarProduto = function(lugarPagina)
   {
		var idUsuario = window.localStorage.idUsuario;
		var token = window.localStorage.token;
		var ultimoAcesso = window.localStorage.ultimoAcesso;
	   
		if((idUsuario != undefined && idUsuario != "") && 
			(token != undefined && token != "") && 
			(ultimoAcesso != undefined && ultimoAcesso != "")) //ta logado
		{
			if(lugarPagina != "produtos-lista.html")
				window.location = "#/produtos-lista";	
		}
		else //nao esta logado
		{
			window.location = "#/home";
		}	
   }
   
   verificarLogin.verificarEstabelecimento = function(lugarPagina)
   {	
		var idUsuario = window.localStorage.idUsuario;
		var token = window.localStorage.token;
		var ultimoAcesso = window.localStorage.ultimoAcesso;
		
		if((idUsuario != undefined && idUsuario != "") && 
			(token != undefined && token != "") && 
			(ultimoAcesso != undefined && ultimoAcesso != "")) //ta logado
		{
			if(lugarPagina != "estabelecimentos.html")
				window.location = "#/estabelecimentos";				
		}
		else //nao esta logado
		{
			window.location = "#/home";
		}	
   }
   
    verificarLogin.verificarPrincipal = function(lugarPagina)
   {
		var idUsuario = window.localStorage.idUsuario;
		var token = window.localStorage.token;
		var ultimoAcesso = window.localStorage.ultimoAcesso;
	
		if((idUsuario != undefined && idUsuario != "") && 
			(token != undefined && token != "") && 
			(ultimoAcesso != undefined && ultimoAcesso != "")) //ta logado
		{
			if(lugarPagina != "principal.html")
				window.location = "#/menu";				
		}
		else //nao esta logado
		{
			if(lugarPagina == "principal.html")
			window.location = "#/home";
		}	
   }

    return verificarLogin;
}]);