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
	
	modalAlerta.sucesso = function(mensagem,subMensagem,destino)
	{
		var alertPopup = $ionicPopup.alert({
			 title: mensagem,
			 subTitle: subMensagem,
			 template: '<p class="svg"><ion-spinner icon="android"></ion-spinner></p>'
		});
		
		 $timeout(function() 
		{
		  window.location = destino;
		  alertPopup.close();
		}, 3000);
	}

    return modalAlerta;
}]);