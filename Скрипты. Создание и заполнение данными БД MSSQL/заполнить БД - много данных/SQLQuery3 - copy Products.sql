
BULK INSERT DBMagazine.dbo.Products
FROM 'E:\1\1 Создание БД\заполнить БД - много данных\Product1000v1.csv' 
--WITH (CODEPAGE = 'ACP', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n'); 
with
  (
  --CODEPAGE = 'ACP', 
  DATAFILETYPE = 'char',
  FIELDTERMINATOR = ';' ,
   RowTerminator = '\n')
GO


BULK INSERT DBMagazine.dbo.Products
FROM 'E:\1\1 Создание БД\заполнить БД - много данных\Product1000v2.csv' 
with
  (
  DATAFILETYPE = 'char',
  FIELDTERMINATOR = ';' ,
   RowTerminator = '\n')
GO


BULK INSERT DBMagazine.dbo.Products
FROM 'E:\1\1 Создание БД\заполнить БД - много данных\Product1000v3.csv' 
with
  (
  DATAFILETYPE = 'char',
  FIELDTERMINATOR = ';' ,
   RowTerminator = '\n')
GO

BULK INSERT DBMagazine.dbo.Products
FROM 'E:\1\1 Создание БД\заполнить БД - много данных\Product1000v4.csv' 
with
  (
  DATAFILETYPE = 'char',
  FIELDTERMINATOR = ';' ,
   RowTerminator = '\n')
GO
