CREATE DATABASE [db-weather]
GO

USE [db-weather]
GO

CREATE TABLE dbo.Weather(
	City char(50) NOT NULL,
	Temperature float NOT NULL,
    Magnitude char(1) NOT NULL

	CONSTRAINT PK_Cotacoes PRIMARY KEY (City)
)
GO

INSERT INTO Weather VALUES ('Novo Hamburgo',35,'C'),('Estância Velha',32.13,'C'),('Gravatai',38,'C'),('Porto Alegre',49,'C'),('Gramado',-5,'C'),('Ivoti',15,'C')

SELECT * FROM Weather ORDER BY City FOR JSON PATH, ROOT('Weather')


--DROP TABLE Weather