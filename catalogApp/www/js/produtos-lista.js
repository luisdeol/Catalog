angular.module('ionicApp', ['ionic'])

.controller('MyCtrl', function($scope) {
  
  $scope.data = {
    showDelete: false
  };
  
  $scope.edit = function(item) {
    alert('Edit Item: ' + item.id);
  };
  $scope.share = function(item) {
    alert('Share Item: ' + item.id);
  };
  
  $scope.moveItem = function(item, fromIndex, toIndex) {
    $scope.items.splice(fromIndex, 1);
    $scope.items.splice(toIndex, 0, item);
  };
  
  $scope.onItemDelete = function(item) {
    $scope.items.splice($scope.items.indexOf(item), 1);
  };
  
  $scope.items = [
    { id: 0, nome:"Arrox" },
    { id: 1 ,nome:"Feijão" },
    { id: 2 ,nome:"Frango" },
    { id: 3 ,nome:"Linguiça" },
    { id: 4 ,nome:"Carne" }
  ];
  
  $scope.edit = function(farmacia) {
    alert('Edit farmacia: ' + farmacia.id);
  };
  $scope.share = function(farmacia) {
    alert('Share farmacia: ' + farmacia.id);
  };
  
  $scope.moveItem = function(farmacia, fromIndex, toIndex) {
    $scope.farmacias.splice(fromIndex, 1);
    $scope.farmacias.splice(toIndex, 0, farmacia);
  };
  
  $scope.onItemDelete = function(farmacia) {
    $scope.farmacias.splice($scope.farmacias.indexOf(farmacia), 1);
  };
  
  $scope.farmacias = [
    { id: 0, nome:"Dipirona" },
    { id: 1 ,nome:"Xarope" },
    { id: 2 ,nome:"Termômetro" }
  ];
  
});