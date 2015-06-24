angular.module("EstabelecimentoControllers",[
'ionic',
'services.verificarLogin',
'services.googleMaps',
'model.estabelecimento',
'services.modalAlerta',
'services.WebServices'
])
.config(function($stateProvider, $urlRouterProvider) {
	
    $stateProvider
		.state('estabelecimento', {
			url: '/estabelecimentos',
			templateUrl: 'estabelecimentos.html',
			controller: 'EstabelecimentoController'
		})
})
.controller("EstabelecimentoController",function($scope,$http,$ionicModal,$ionicLoading,$compile,verificarLogin,googleMaps,estabelecimento,modalAlerta,WebServices){
	$scope.estabelecimentos = [];
	
	//___________ VERIFICAR LOGIN _____________//
	$scope.verificarLogin = function(lugarPagina)
	{
		verificarLogin.verificarEstabelecimento(lugarPagina);
		$scope.pesquisarEstabelecimento();
		estabelecimento.openDataBase();
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
		if(latitudeEstabelecimento!="semLatitude" && longitudeEstabelecimento!="semLongitude")
		{
			window.localStorage.latitudeEstabelecimento = latitudeEstabelecimento;
			window.localStorage.longitudeEstabelecimento = longitudeEstabelecimento;
			window.localStorage.nomeEstabelecimento = nomeEstabelecimento;
			window.location = "googleMaps.html";			
		}
		else
		{
			modalAlerta.alerta('Sem Localização!','Estabelecimento não possui localização');
		}
	}	
	
	//_______________ PEGAR ENDEREÇO POR CEP __________________//
	$scope.getEnderecoCep = function(cep)
	{
		if(cep.length == 9)
		{
			cep.split().splice(5,1);
			WebServices.getCep(cep)
			.success(function(data, status, headers, config)
			{
				var retorno = angular.fromJson(data);	
				document.getElementById("cidade").value = retorno.cidade;
				document.getElementById("estado").value = retorno.estado_info.nome;
				if(retorno.logradouro != undefined)
				document.getElementById("logradouro").value = retorno.logradouro;
			});			
		}
		else if(cep.length == 5)
		{
			document.getElementById("cep").value = cep +"-";
		}
		else if(!isNaN(cep.replace(/-/g, "")) == false) //se cep nao for um numero
		{
			modalAlerta.alerta('CEP inválido!','CEP deve conter apenas números');
			document.getElementById("cep").value = "";
		}

	}
	
	//_______________ SALVAR IMAGEM __________________//
	$scope.salvarImagem = function()
	{
		var oFReader = new FileReader(); 
		try 
		{
			oFReader.readAsDataURL(document.getElementById("file").files[0]);
			oFReader.onload = function (oFREvent) 
			{ 
				window.localStorage.estabImagem = oFREvent.target.result; 
			}; 		
		}
		catch(err) 
		{
			window.localStorage.estabImagem = "";
		}
	}
	
	//________________ PESQUISAR ESTABELECIMENTO _____________//
	$scope.pesquisarEstabelecimento = function()
	{	
		estabelecimento.select(function(retorno){
			for(var l=0; retorno.length > l; l++)
			{
				var id = retorno[l].id;
				var nome = retorno[l].nome;
				var rua = retorno[l].rua;
				var cidade = retorno[l].cidade;
				var estado = retorno[l].estado;
				var numero = retorno[l].numero;
				var cep = retorno[l].cep;
				var latitude = retorno[l].latitude;
				var longitude = retorno[l].longitude; 
				var imagem = retorno[l].imagem;
				
				$scope.estabelecimentos[l] = {id:id, nome:nome, rua:rua, cidade:cidade, estado:estado, numero:numero, cep:cep, latitude:latitude, longitude:longitude, imagem:imagem};
			}		
		});	
	} 

	//_______________ CADASTRAR ESTABELECIMENTO _________________// 
	$scope.cadastrarEstabelecimento = function()
	{
		$scope.salvarImagem();
		var estab = new Object();
		estab.nome = document.getElementById("nome").value;
		estab.rua = document.getElementById("logradouro").value;
		estab.cidade = document.getElementById("cidade").value;
		estab.estado = document.getElementById("estado").value;
		estab.numero = document.getElementById("numero").value;
		estab.cep = document.getElementById("cep").value;
		
		if( estab.nome.length>=3   && 
		    estab.rua.length>=3    && 
		    estab.cidade.length>=3 && 
		    estab.estado.length>=2 && 
		    estab.numero!=""       &&
		    estab.numero>0)
		{
			// $scope.estabelecimentos.push
			// ({ 
				// nome: estab.nome, 
				// rua: estab.rua, 
				// cidade: estab.cidade, 
				// estado: estab.estado, 
				// numero: estab.numero, 
				// cep: estab.cep,
				// latitude: window.localStorage.latCadastroEstab,
				// longitude: window.localStorage.lonCadastroEstab,
				// imagem: window.localStorage.estabImagem
			// });
			
			googleMaps.pegarLatitudeLongitude(estab.nome +" - "+ estab.rua +" - "+ estab.cidade +" - "+ estab.estado,function(){
				
				var json = "{nome: '"+estab.nome+"', rua: '"+estab.rua+"', cidade: '"+estab.cidade+"', estado: '"+estab.estado+"', numero: '"+estab.numero+"', cep: '"+estab.cep+"',latitude: '"+window.localStorage.latCadastroEstab+"',longitude: '"+window.localStorage.lonCadastroEstab+"'}";
				estabelecimento.insertInto(0, estab.nome, estab.rua, estab.cidade, estab.estado, estab.numero, estab.cep, window.localStorage.latCadastroEstab, window.localStorage.lonCadastroEstab, window.localStorage.estabImagem);
				
				// WebServices.cadastrarEstabelecimento(json)
				// .success(function(data, status, headers, config)
				// {
					// var retorno = data.d;	
				// });
				
				$scope.pesquisarEstabelecimento();
				$scope.modal.hide();
			});		
		}
		else
		{
			modalAlerta.alerta('ERRO!','Existem campos inválidos!');
		}
	}	
});