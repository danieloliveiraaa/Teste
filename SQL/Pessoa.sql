-- DANIEL OLIVEIRA --
create database Sistema

use Sistema

create table Pessoa
(
Id int identity (1,1) not null primary key,
NomeCompleto varchar(50) NOT NULL,
DataNascimento datetime NOT NULL,
RendaValor varchar(max) NOT NULL,
CPF varchar (14) NOT NULL
)


INSERT INTO Pessoa values ('Daniel Oliveira', GETDATE(), 'R$ 545.000.000,00', '123.456.789-10')

select * from Pessoa order by Id desc