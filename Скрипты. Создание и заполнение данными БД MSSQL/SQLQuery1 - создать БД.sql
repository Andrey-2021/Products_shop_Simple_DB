
--В этом сценарии создаётся БД:
USE [master] 
GO 
 
-- Если база данных  уже существует, уничтожаем ее 
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'DBMagazine') 
DROP DATABASE [DBMagazine] 
GO 

--создаём БД 
CREATE DATABASE DBMagazine; 
GO --конец пакета

Use DBMagazine;  -- установливаем  БД в качестве текущей 
GO  --Конец пакета

-- Создание таблиц
--Таблица Shops – таблица с названиями магазинов
CREATE TABLE Shops
(
	Id		int				CONSTRAINT PK_Shops_Id  PRIMARY KEY IDENTITY(1,1),
	Name	NVARCHAR(75)	CONSTRAINT CK_Shops_Name NOT NULL CHECK(Name !=''),				--Название
	Address NVARCHAR(120)	CONSTRAINT CK_Client_Address NOT NULL CHECK(Address !=''),		-- адрес
	Phone	NVARCHAR(20),																	-- телефон
	Email	NVARCHAR(40)	NOT NULL
	CONSTRAINT UQ_Client_Email UNIQUE (Email)												--сделаем уникальными e-mail
);

GO

-- Таблица Products – таблица с названиями продуктов
CREATE TABLE Products
(
	Id		int				CONSTRAINT PK_Products_Id PRIMARY KEY IDENTITY(1,1),
	Name	NVARCHAR(75)	CONSTRAINT CK_Shops_Name NOT NULL CHECK(Name !=''),		--Название
	Price	MONEY			NOT NULL,			                                    -- Цена
	ShopsId int				NOT NULL,								  				-- внешние ключи
	CONSTRAINT FK_Products_To_Shops FOREIGN KEY (ShopsId) REFERENCES Shops(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

 -- Создание Индексов
 CREATE NONCLUSTERED INDEX [IX_Shops_Name] ON [dbo].[Shops]
(
	[Name] ASC
);
GO


 CREATE NONCLUSTERED INDEX [IX_Products_ProductID] ON [dbo].[Products]
(
	[ShopsId] ASC
);
GO
