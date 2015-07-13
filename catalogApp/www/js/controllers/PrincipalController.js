var app = angular.module("PrincipalControllers",[
'ionic',
'services.verificarLogin',
'services.WebServices',
'services.modalAlerta'
])
.controller("PrincipalController",function($scope,$ionicModal,$ionicPopup,$timeout,verificarLogin, WebServices, modalAlerta)
{
	var chave = "{idUsuario:'"+window.localStorage.idUsuario+"',token:'"+window.localStorage.token+"',ultimoAcesso:'"+window.localStorage.ultimoAcesso+"'}";
	
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
	
	//_______________ PESQUISAR PRODUTO __________________//
	$ionicModal.fromTemplateUrl('templates/pesquisarProduto.html', {
		scope: $scope
	}).then(function(modal) {
		$scope.modalPesquisar = modal;
	});
	
	$scope.pesquisarProduto = function(produto)
	{
		if(produto != undefined)//campos foram preenchidos
		{
			var nome = produto.nome;
			var marca = produto.marca;
			var tipo = produto.tipo;
			
			if(nome==undefined)
			{
				nome="";
			}
			if(marca==undefined)
			{
				marca="";
			}
			if(tipo==undefined)
			{
				tipo="";
			}
			
			window.localStorage.dtoProduto = "{nome:'"+nome+"',marca:'"+marca+"',tipo:'"+tipo+"'}";
					
			modalAlerta.sucesso("Pesquisa","Pesquisando...","#/produtos-pesquisados");
			$scope.modalPesquisar.hide();
		}
		else //campos vazios
		{
			modalAlerta.alerta("Ocorreu um erro","Preencha todos os campos!");
			return false;
		}
	
	}
	
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