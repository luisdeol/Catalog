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

    estabelecimento.insertInto = function(id, nome, rua, cidade, estado, numero, cep)
	{
		var db = estabelecimento.openDataBase();
		db.transaction( function (tx) {
			tx.executeSql('CREATE TABLE IF NOT EXISTS estabelecimento (id, nome, rua, cidade, estado, numero, cep)',[],
				function ()
				{
				tx.executeSql(" INSERT INTO estabelecimento (id, nome, rua, cidade, estado, numero, cep) VALUES ("+id+",'"+nome+"','"+rua+"','"+cidade+"','"+estado+"','"+numero+"','"+cep+"') ");
				}
			);
		});
	}

    return estabelecimento;
}]);