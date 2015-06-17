angular.module('services.googleMaps', [])

.factory('googleMaps', [function ( $http) {

	var googleMaps = {};


	googleMaps.pegarLatitudeLongitude = function(endereco, callback)
	{
		var geocoder = new google.maps.Geocoder();
		geocoder.geocode( { 'address': endereco}, function(results, status)
		{
			if (status == google.maps.GeocoderStatus.OK)
			{
				var latitude = results[0].geometry.location.A;
				var longitude = results[0].geometry.location.F;
				window.localStorage.latCadastroEstab = latitude;
				window.localStorage.lonCadastroEstab = longitude;		
			} 
			else 
			{
			  alert("Latitude e Longitude não encontradas");
			}
			
			callback();
	    });
	}

    return googleMaps;
}]);