angular.module('model.produto', [])

.factory('produto', [function () {

	var produto = {};
	
	produto.openDataBase = function()
	{
		var db = openDatabase(
			'Catalog', 
			'1.0', 
			'My database',
			2 * 1024 * 1024
		);
		return db;
	}

    produto.insertInto = function(id, nome, quantidade, categoria, idLista)
	{
		var db = produto.openDataBase();
		db.transaction( function (tx) {
			tx.executeSql('CREATE TABLE IF NOT EXISTS produto (id, nome, quantidade, categoria, idLista)',[],
				function ()
				{
				tx.executeSql(" INSERT INTO produto (id, nome, quantidade, categoria, idLista) VALUES ("+id+",'"+nome+"','"+quantidade+"','"+categoria+"','"+idLista+"') ");
				}
			);
		});
	}
	
	 produto.update = function(id, nome, quantidade, categoria, idLista)
	{
		var db = produto.openDataBase();
		db.transaction( function (tx) {
			tx.executeSql('CREATE TABLE IF NOT EXISTS produto (id, nome, quantidade, categoria, idLista)',[],
				function ()
				{
				tx.executeSql(" UPDATE produto SET id="+id+", nome='"+nome+"', quantidade='"+quantidade+"', categoria='"+categoria+"', idLista='"+idLista+"' WHERE id='"+id+"'");
				}
			);
		});
	}
	
	produto.deletar = function(id)
	{
		var db = produto.openDataBase();
		db.transaction( function (tx) {
			tx.executeSql('CREATE TABLE IF NOT EXISTS produto (id, nome, quantidade, categoria, idLista)',[],
				function ()
				{
					tx.executeSql("DELETE FROM produto WHERE id='"+id+"' ");
				}
			);
		});
	}
	
	produto.select = function(idLista, callback)
	{
		var db = produto.openDataBase();
		db.transaction( function (tx) {
			tx.executeSql("SELECT * FROM produto WHERE idLista='"+idLista+"' ",[],
				function (tx,results)
				{
					var row = results.rows;
					callback(row);
				}
			);
		});
	}

    return produto;
}]);