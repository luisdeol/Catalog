CREATE DATABASE Catalog;
USE Catalog;

CREATE TABLE tb_Usuario(
	id int primary key identity(1,1),
	email varchar(50) not null,
	senha varchar(50) not null,
	nome varchar(100) not null
);

CREATE TABLE tb_SenhaAlternativa(
	idUsuario int,
	senha varchar(50),
	dataDeCriacao Date,
	PRIMARY KEY CLUSTERED(idUsuario),
	FOREIGN KEY (idUsuario) REFERENCES tb_Usuario(id)
);

CREATE TABLE tb_Chave(
	idUsuario int,
	token varchar(50),
	ultimoAcesso time,
	PRIMARY KEY CLUSTERED(idUsuario),
	FOREIGN KEY (idUsuario) REFERENCES tb_Usuario(id)
);

CREATE TABLE tb_Lista(
	id int primary key identity(1,1),
	titulo varchar(50),
	idUsuario int foreign key references tb_Usuario(id)
)

CREATE TABLE tb_Fabricante(
	id int primary key identity(1,1),
	fabricante varchar(50)
)

CREATE TABLE tb_Tipo(
	id int primary key identity(1,1),
	tipo varchar(50)
)

CREATE TABLE tb_Produto(
	id int primary key identity(1,1),
	nome varchar(100),
	codigoDeBarras varchar(50),
	idFabricante int foreign key references tb_Fabricante(id),
	idTipo int foreign key references tb_tipo(id)
);

CREATE TABLE tb_ProdutoDaLista(
	id int primary key identity(1,1),
	quantidade int,
	idProduto int foreign key references tb_Produto(id),
	idLista int foreign key references tb_Lista(id)
)

CREATE TABLE tb_Estabelecimento(
	id int primary key identity(1,1),
	estabelecimento varchar(50)
)

CREATE TABLE tb_EnderecoEstabelecimento(
	id int primary key identity(1,1),
	rua varchar(100),
	cidade varchar(100),
	estado varchar(100),
	numero varchar(50),
	cep varchar(100),
	latitude DOUBLE PRECISION,
	longitude DOUBLE PRECISION,
	idEstabelecimento int foreign key references tb_Estabelecimento(id)
)

CREATE TABLE tb_Item(
	id int primary key identity(1,1),
	preco DOUBLE PRECISION,
	compraRecente date,
	qualificacao int,
	idEstabelecimento int foreign key references tb_EnderecoEstabelecimento(id),
	idProduto int foreign key references tb_Produto(id)
);

select * from tb_Estabelecimento
select * from tb_EnderecoEstabelecimento
select * from tb_Usuario
select * from tb_Lista
select * from tb_Produto
select * from tb_Item
select * from tb_ProdutoDaLista

insert into tb_Estabelecimento values('nordestao');
insert into tb_Estabelecimento values('Carrefour');
insert into tb_Estabelecimento values('SuperCoop');
insert into tb_Estabelecimento values('MinePreco');

insert into tb_Usuario values('bruno@gmail.com','1234','bruno');
insert into tb_Lista values('semanal',1);
insert into tb_Fabricante values('DaTerra');
insert into tb_Tipo values('Super-mercado');

insert into tb_Produto values('banana','43543',1,1);
insert into tb_Produto values('maça','43543',1,1);
insert into tb_Produto values('limão','43543',1,1);
insert into tb_Produto values('goiaba','43543',1,1);


insert into tb_Item values(2.50,'12/07/1995',2,1,1);
insert into tb_Item values(3.20,'12/07/1995',2,1,2);
insert into tb_Item values(1.60,'12/07/1995',2,1,3);
insert into tb_Item values(2.35,'12/07/1995',2,1,4);

insert into tb_EnderecoEstabelecimento values('floriano peixoto','natal','rn',12,'59162000',-32.83727,8.23432,1);
insert into tb_EnderecoEstabelecimento values('floriano peixoto','natal','rn',12,'59162000',-32.83727,8.23432,2);
insert into tb_EnderecoEstabelecimento values('floriano peixoto','natal','rn',12,'59162000',-32.83727,8.23432,3);
insert into tb_EnderecoEstabelecimento values('floriano peixoto','natal','rn',12,'59162000',-32.83727,8.23432,4);

insert into tb_ProdutoDaLista values(2,1,1);
insert into tb_ProdutoDaLista values(4,2,1);
insert into tb_ProdutoDaLista values(3,3,1);
insert into tb_ProdutoDaLista values(1,4,1);

update tb_EnderecoEstabelecimento set latitude=-5.75753,longitude=-35.247805 where id=1;
update tb_EnderecoEstabelecimento set latitude=-5.75753,longitude=-35.247805 where id=2;