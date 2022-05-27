

BULK INSERT DBMagazine.dbo.Shops
FROM 'E:\1\1 Создание БД\заполнить БД - мало данных\1-shops.csv' 
with
  (
  DATAFILETYPE = 'char',
  FIELDTERMINATOR = ';' ,
   RowTerminator = '\n'
   )
GO
