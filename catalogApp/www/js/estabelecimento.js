var app = angular.module("catalogApp",['ionic']);
app.controller("EstabelecimentoController",function($scope,$http,$ionicModal){
  
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
			if(lugarPagina != "estabelecimentos.html")
				window.location = lugarPagina;				
		}
		else //nao esta logado
		{
			window.location = "login.html";
		}	
	}
	
	 $ionicModal.fromTemplateUrl('templates/modal.html', {
		scope: $scope
	  }).then(function(modal) {
		$scope.modal = modal;
	  });
  
	  $scope.createContact = function(u) {        
		$scope.contacts.push({ name: u.firstName + ' ' + u.lastName });
		$scope.modal.hide();
	  };
	
});