angular.module('services.googleMaps', [])

.factory('googleMaps', [function () {

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
				if(latitude == undefined && longitude == undefined)
				{
					var latitude = results[0].geometry.location.G;
					var longitude = results[0].geometry.location.K;
				}
				window.localStorage.latCadastroEstab = latitude;
				window.localStorage.lonCadastroEstab = longitude;		
			} 
			else 
			{
				window.localStorage.latCadastroEstab = 0;
				window.localStorage.lonCadastroEstab = 0		
			}
			
			callback();
	    });
	}

    return googleMaps;
}]);