var app = angular.module("PrincipalControllers",['ionic','services.verificarLogin'])
.controller("PrincipalController",function($scope,$ionicModal,$ionicPopup,$timeout,verificarLogin){
	
	//___________ VERIFICAR LOGIN _____________//
	$scope.verificarLogin = function(lugarPagina)
	{
		localizacao();
		verificarLogin.verificarPrincipal(lugarPagina);
	};
	
	//______________ LOGOUT _____________//
	$scope.logout = function()
	{
		window.localStorage.idUsuario = "";
		window.localStorage.token = "";
		window.localStorage.ultimoAcesso = "";
		window.location = "#/home";
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
	
	//_______________ ABRIR MODAL DE CADASTRO __________________//
	$ionicModal.fromTemplateUrl('templates/alterarDados.html', {
		scope: $scope
	}).then(function(modal) {
		$scope.modal = modal;
	});
	
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