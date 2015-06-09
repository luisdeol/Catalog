angular.module('ionicApp', ['ionic'])

.controller('ListaController', function($scope,$ionicModal,$http,$ionicPopup,$timeout) {
  
  $scope.data = {
    showDelete: false
  };
  
  $scope.editarLista = function(lista) {
    $scope.listas[0].nome = lista.nome;
  };
  
  $scope.share = function(lista) {
    alert('Share Item: ' + lista.id);
  };
  
  $scope.moverLista = function(lista, fromIndex, toIndex) {
    $scope.listas.splice(fromIndex, 1);
    $scope.listas.splice(toIndex, 0, lista);
  };
  
  $scope.deletarLista = function(lista) 
  {
    $scope.listas.splice($scope.listas.indexOf(lista), 1);
  };
  
  
	$scope.listas = [{id:"2",nome:"Semanal"},{id:"3",nome:"Mensal"},{id:"4",nome:"Anual"}];

	//________________ CRIAR LISTA _________________//
	$scope.criarLista = function(lista)
	{
		if(lista != undefined){
			$scope.listas.push({ nome: lista.nome});
			$scope.modal.hide();	
			$scope.alerta("Lista","Lista criada com sucesso!");
		}
		else
		{
			$scope.alerta("Lista","Adicione um nome a lista");
		}
	}
  
	//________________ VERIFICAR LOGIN _________________//
	$scope.verificarLogin = function(lugarPagina)
	{
		var idUsuario = window.localStorage.idUsuario;
		var token = window.localStorage.token;
		var ultimoAcesso = window.localStorage.ultimoAcesso;
		document.getElementById("imgAdd").className = "img-add-lista";
		
		if((idUsuario != undefined && idUsuario != "") && 
			(token != undefined && token != "") && 
			(ultimoAcesso != undefined && ultimoAcesso != "")) //ta logado
		{
			if(lugarPagina != "listas.html")
				window.location = lugarPagina;	
		}
		else //nao esta logado
		{
			window.location = "index.html#/tab/login";
		}	
	}
	
	//_______________ ABRIR MODAL DE CADASTRO DA LISTA __________________//
	$ionicModal.fromTemplateUrl('templates/modal.html', {
		scope: $scope
	}).then(function(modal) {
		$scope.modal = modal;
	});
	
	//_______________ MODAL EDITAR LISTA __________________//
	$scope.modalEditarLista = function() {
	   $scope.lista = {}

	   var myPopup = $ionicPopup.show
	   ({
		 template: '<input type="text" ng-model="lista.nome">',
		 title: 'Editar Lista',
		 subTitle: 'Edite o nome da lista',
		 scope: $scope,
		 buttons: [
		   { text: 'Cancel' },
		   {
			 text: '<b>Save</b>',
			 type: 'button-positive',
			 onTap: function(e) {
			   if (!$scope.lista.nome) {
				 e.preventDefault();
			   } else {
				 myPopup.close;  
				 $scope.editarLista($scope.lista);
			   }
			 }
		   },
		 ]
		});
	}
	
	//____________ ALERTA ____________//
	$scope.alerta = function(mensagem,subMensagem)
	{
		var alertPopup = $ionicPopup.alert({
		title: mensagem,
		template: subMensagem
		});
		
		 $timeout(function() 
		{
		  alertPopup.close();
		}, 3000);
	};
  
});