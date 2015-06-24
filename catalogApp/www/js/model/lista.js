angular.module('model.lista', [])

.factory('listaModelo', [function () {

	var listaModelo = {};
	
	listaModelo.openDataBase = function()
	{
		var db = openDatabase(
			'Catalog', 
			'1.0', 
			'My database',
			2 * 1024 * 1024
		);
		return db;
	}

    listaModelo.insertInto = function(id, nome, id_usuario)
	{
		var db = listaModelo.openDataBase();
		db.transaction( function (tx) {
			tx.executeSql('CREATE TABLE IF NOT EXISTS lista (id, nome, id_usuario)',[],
				function ()
				{
					tx.executeSql(" INSERT INTO lista (id, nome , id_usuario) VALUES ("+id+",'"+nome+"','"+id_usuario+"') ");
				}
			);
		});
	}
	
	 listaModelo.update = function(id, nome, id_usuario, nomeAntigo)
	{
		var db = listaModelo.openDataBase();
		db.transaction( function (tx) {
			tx.executeSql('CREATE TABLE IF NOT EXISTS lista (id, nome, id_usuario)',[],
				function ()
				{
					console.log(nomeAntigo);
					tx.executeSql(" UPDATE lista SET id="+id+", nome='"+nome+"', id_usuario='"+id_usuario+"' WHERE nome='"+nomeAntigo+"'");
				}
			);
		});
	}
	
	listaModelo.deletar = function(nome, id_usuario)
	{
		var db = listaModelo.openDataBase();
		db.transaction( function (tx) {
			tx.executeSql('CREATE TABLE IF NOT EXISTS lista (id, nome, id_usuario)',[],
				function ()
				{
					tx.executeSql("DELETE FROM lista WHERE nome='"+nome+"' AND id_usuario='"+id_usuario+"' ");
				}
			);
		});
	}
	
	listaModelo.select = function(id_usuario,callback)
	{
		var db = listaModelo.openDataBase();
		db.transaction( function (tx) {
			tx.executeSql("SELECT * FROM lista WHERE id_usuario = '"+id_usuario+"' ",[],
				function (tx,results)
				{
					var row = results.rows;
					callback(row);
				}
			);
		});
	}


    return listaModelo;
}]);