angular.module('model.estabelecimento', [])

.factory('estabelecimento', [function () {

	var estabelecimento = {};
	
	estabelecimento.openDataBase = function()
	{
		var db = openDatabase(
			'Catalog', 
			'1.0', 
			'My database',
			2 * 1024 * 1024
		);
		return db;
	}

    estabelecimento.insertInto = function(id, nome, rua, cidade, estado, numero, cep, latitude, longitude, imagem)
	{
		var db = estabelecimento.openDataBase();
		db.transaction( function (tx) {
			tx.executeSql('CREATE TABLE IF NOT EXISTS estabelecimento (id, nome, rua, cidade, estado, numero, cep, latitude, longitude, imagem)',[],
				function ()
				{
				tx.executeSql(" INSERT INTO estabelecimento (id, nome, rua, cidade, estado, numero, cep, latitude, longitude, imagem) VALUES ("+id+",'"+nome+"','"+rua+"','"+cidade+"','"+estado+"','"+numero+"','"+cep+"','"+latitude+"','"+longitude+"','"+imagem+"') ");
				}
			);
		});
	}
	
	 estabelecimento.update = function(id, nome, rua, cidade, estado, numero, cep)
	{
		var db = estabelecimento.openDataBase();
		db.transaction( function (tx) {
			tx.executeSql('CREATE TABLE IF NOT EXISTS estabelecimento (id, nome, rua, cidade, estado, numero, cep, latitude, longitude, imagem)',[],
				function ()
				{
				tx.executeSql(" UPDATE estabelecimento SET id="+id+", nome='"+nome+"', rua='"+rua+"', cidade='"+cidade+"', estado='"+estado+"', numero='"+numero+"', cep='"+cep+"' , latitude='"+latitude+"', longitude='"+longitude+"', imagem='"+imagem+"' WHERE nome='"+nome+"'");
				}
			);
		});
	}
	
	estabelecimento.deletar = function(nome, latitude, longitude)
	{
		var db = estabelecimento.openDataBase();
		db.transaction( function (tx) {
			tx.executeSql('CREATE TABLE IF NOT EXISTS estabelecimento (id, nome, rua, cidade, estado, numero, cep, latitude, longitude, imagem)',[],
				function ()
				{
					tx.executeSql("DELETE FROM estabelecimento WHERE nome='"+nome+"' AND latitude='"+latitude+"' AND longitude='"+longitude+"'");
				}
			);
		});
	}
	
	estabelecimento.select = function(callback)
	{
		var db = estabelecimento.openDataBase();
		db.transaction( function (tx) {
			tx.executeSql("SELECT * FROM estabelecimento",[],
				function (tx,results)
				{
					var row = results.rows;
					callback(row);
				}
			);
		});
	}

    return estabelecimento;
}]);