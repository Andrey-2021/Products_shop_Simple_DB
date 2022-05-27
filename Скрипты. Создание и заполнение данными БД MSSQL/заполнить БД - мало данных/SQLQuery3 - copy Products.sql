
BULK INSERT DBMagazine.dbo.Products
FROM 'E:\1\1 Создание БД\заполнить БД - мало данных\2-products.csv' 
with
  (
  DATAFILETYPE = 'char',
  FIELDTERMINATOR = ';' ,
   RowTerminator = '\n'
   )
GO
