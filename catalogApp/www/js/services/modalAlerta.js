angular.module('services.modalAlerta', ['ionic'])

.factory('modalAlerta', ['$ionicPopup', '$timeout', function ($ionicPopup, $timeout) {

	var modalAlerta = {};

	modalAlerta.alerta = function(mensagem,subMensagem)
	{
		var alertPopup = $ionicPopup.alert({
		title: mensagem,
		template: subMensagem
		});
	}
	
	modalAlerta.confirmar = function(mensagem,subMensagem, callback)
	{
		var confirmPopup = $ionicPopup.confirm
		({
			title: mensagem,
			template: subMensagem
		});
		confirmPopup.then(function(res) {
			callback(res);
		});
	}

	modalAlerta.sucesso = function(mensagem,subMensagem,destino)
	{
		var myPopup = $ionicPopup.show({
		title: mensagem,
		subTitle: subMensagem,
		template: '<p class="svg"><ion-spinner icon="android"></ion-spinner></p>'
		});
		
		$timeout(function() 
		{
		  window.location = destino;
		   myPopup.close();
		}, 3000);
	}

    return modalAlerta;
}]);