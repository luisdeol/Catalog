angular.module("EstabelecimentoControllers",['ionic','services.verificarLogin','services.googleMaps'])
.config(function($stateProvider, $urlRouterProvider) {
	
    $stateProvider
		.state('estabelecimento', {
			url: '/estabelecimentos',
			templateUrl: 'estabelecimentos.html',
			controller: 'EstabelecimentoController'
		})
})
.controller("EstabelecimentoController",function($scope,$http,$ionicModal,$ionicLoading,$compile,verificarLogin,googleMaps){
	$scope.estabelecimentos = [];
	
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

	//_______________ CHAMAR MAPA _________________// 
	$scope.chamarMapa = function(latitudeEstabelecimento,longitudeEstabelecimento,nomeEstabelecimento)
	{
		window.localStorage.latitudeEstabelecimento = latitudeEstabelecimento;
		window.localStorage.longitudeEstabelecimento = longitudeEstabelecimento;
		window.localStorage.nomeEstabelecimento = nomeEstabelecimento;
		window.location = "googleMaps.html";
	}	
	
	//_______________ CADASTRAR ESTABELECIMENTO _________________// 
	$scope.cadastrarEstabelecimento = function(estab)
	{
		if(estab != undefined)
		{
			$scope.estabelecimentos.push
			({ 
				nome: estab.nome, 
				rua: estab.rua, 
				cidade: estab.cidade, 
				estado: estab.estado, 
				numero: estab.numero, 
				cep: estab.cep 
			});
			
			googleMaps.pegarLatitudeLongitude(estab.nome +" - "+ estab.rua +" - "+ estab.cidade +" - "+ estab.estado,function(){
				
				var json = "{nome: '"+estab.nome+"', rua: '"+estab.rua+"', cidade: '"+estab.cidade+"', estado: '"+estab.estado+"', numero: '"+estab.numero+"', cep: '"+estab.cep+"',latitude: '"+window.localStorage.latCadastroEstab+"',longitude: '"+window.localStorage.lonCadastroEstab+"'}";
				
				$http.post('http://localhost:51786/Webservices/WsEstabelecimento.asmx/criarEstabelecimento', {dtoEstabelecimento:json}).
				  success(function(data, status, headers, config)
				{
					var retorno = data.d;	
				});
				$scope.modal.hide();
			});
			
		
		}
		else
		{
			
		}
	}	
});