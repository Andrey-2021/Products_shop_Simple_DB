
--� ���� �������� �������� ��:
USE [master] 
GO 
 
-- ���� ���� ������  ��� ����������, ���������� �� 
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'DBMagazine') 
DROP DATABASE [DBMagazine] 
GO 

--������ �� 
CREATE DATABASE DBMagazine; 
GO --����� ������

Use DBMagazine;  -- �������������  �� � �������� ������� 
GO  --����� ������

-- �������� ������
--������� Shops � ������� � ���������� ���������
CREATE TABLE Shops
(
	Id		int				CONSTRAINT PK_Shops_Id  PRIMARY KEY IDENTITY(1,1),
	Name	NVARCHAR(75)	CONSTRAINT CK_Shops_Name NOT NULL CHECK(Name !=''),				--��������
	Address NVARCHAR(120)	CONSTRAINT CK_Client_Address NOT NULL CHECK(Address !=''),		-- �����
	Phone	NVARCHAR(20),																	-- �������
	Email	NVARCHAR(40)	NOT NULL
	CONSTRAINT UQ_Client_Email UNIQUE (Email)												--������� ����������� e-mail
);

GO

-- ������� Products � ������� � ���������� ���������
CREATE TABLE Products
(
	Id		int				CONSTRAINT PK_Products_Id PRIMARY KEY IDENTITY(1,1),
	Name	NVARCHAR(75)	CONSTRAINT CK_Shops_Name NOT NULL CHECK(Name !=''),		--��������
	Price	MONEY			NOT NULL,			                                    -- ����
	ShopsId int				NOT NULL,								  				-- ������� �����
	CONSTRAINT FK_Products_To_Shops FOREIGN KEY (ShopsId) REFERENCES Shops(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

 -- �������� ��������
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
