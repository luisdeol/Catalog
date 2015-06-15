angular.module("EstabelecimentoControllers",['ionic','services.verificarLogin'])
.config(function($stateProvider, $urlRouterProvider) {
	
    $stateProvider
		.state('estabelecimento', {
			url: '/estabelecimentos',
			templateUrl: 'estabelecimentos.html',
			controller: 'EstabelecimentoController'
		})
})
.controller("EstabelecimentoController",function($scope,$http,$ionicModal,$ionicLoading, $compile,verificarLogin){
  
	//___________ VERIFICAR LOGIN _____________//
	$scope.verificarLogin = function(lugarPagina)
	{
		verificarLogin.verificarEstabelecimento(lugarPagina);
	}
	
	//_______________ ABRIR MODAL DE CADASTRO __________________//
	 $ionicModal.fromTemplateUrl('templates/modal.html', {
		scope: $scope
	  }).then(function(modal) {
		$scope.modal = modal;
	  });
  
	  $scope.createContact = function(u) {        
		$scope.contacts.push({ name: u.firstName + ' ' + u.lastName });
		$scope.modal.hide();
	  };
	 
	//_______________ CHAMAR MAPA _________________// 
	$scope.chamarMapa = function(latitudeEstabelecimento,longitudeEstabelecimento,nomeEstabelecimento)
	{
		window.localStorage.latitudeEstabelecimento = latitudeEstabelecimento;
		window.localStorage.longitudeEstabelecimento = longitudeEstabelecimento;
		window.localStorage.nomeEstabelecimento = nomeEstabelecimento;
		window.location = "googleMaps.html";
	}	
});