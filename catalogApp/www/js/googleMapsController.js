angular.module('catalogApp', ['ionic'])

    .controller('googleMapsController', function($scope, $ionicLoading, $compile) {
      function initialize() {
        var localizacaoUsuario = new google.maps.LatLng(window.localStorage.latitudeUsuario,window.localStorage.longitudeUsuario);
        var localizacaoEstabelecimento = new google.maps.LatLng(window.localStorage.latitudeEstabelecimento,window.localStorage.longitudeEstabelecimento);
      
        var mapOptions = {
          streetViewControl:true,
          center: localizacaoUsuario,
          zoom: 18,
          mapTypeId: google.maps.MapTypeId.TERRAIN
        };
        var map = new google.maps.Map(document.getElementById("map"),
            mapOptions);
        
        //Marker + infowindow + angularjs compiled ng-click
        var contentString = "<div><a ng-click='clickTest()'>Click me!</a></div>";
        var compiled = $compile(contentString)($scope);

        var infowindow = new google.maps.InfoWindow({
          content: compiled[0]
        });

        var marker = new google.maps.Marker({
          position: localizacaoUsuario,
          map: map,
          title: 'Strathblane (Job Location)'
        });
        
        var localizacaoEstabelecimentoRoute = new google.maps.Marker({
          position: localizacaoEstabelecimento,
          map: map,
          title: 'localizacaoEstabelecimento (Stobhill)'
        });
        
        var infowindow = new google.maps.InfoWindow({
             content: "Sua posição"
        });

        infowindow.open(map,marker);
        
        var localizacaoEstabelecimentowindow = new google.maps.InfoWindow({
             content: window.localStorage.nomeEstabelecimento
        });

        localizacaoEstabelecimentowindow.open(map,localizacaoEstabelecimentoRoute);
       
        google.maps.event.addListener(marker, 'click', function() {
          infowindow.open(map,marker);
        });

        $scope.map = map;
        
        var directionsService = new google.maps.DirectionsService();
        var directionsDisplay = new google.maps.DirectionsRenderer();

        var request = {
            origin : localizacaoUsuario,
            destination : localizacaoEstabelecimento,
            travelMode : google.maps.TravelMode.DRIVING
        };
        directionsService.route(request, function(response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
            }
        });

        directionsDisplay.setMap(map); 
       
      }
  
      google.maps.event.addDomListener(window, 'load', initialize);
    
      $scope.centerOnMe = function() {
        if(!$scope.map) {
          return;
        }

        $scope.loading = $ionicLoading.show({
          content: 'Getting current location...',
          showBackdrop: false
        });
        navigator.geolocation.getCurrentPosition(function(pos) {
          $scope.map.setCenter(new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude));
          $scope.loading.hide();
        }, function(error) {
          alert('Unable to get location: ' + error.message);
        });
      };
      
      $scope.clickTest = function() {
        alert('Example of infowindow with ng-click')
      };
	  
	  $scope.nomeEstabelecimento = window.localStorage.nomeEstabelecimento;
      
    });