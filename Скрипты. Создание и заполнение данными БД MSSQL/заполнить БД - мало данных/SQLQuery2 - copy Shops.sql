

BULK INSERT DBMagazine.dbo.Shops
FROM 'E:\1\1 �������� ��\��������� �� - ���� ������\1-shops.csv' 
with
  (
  DATAFILETYPE = 'char',
  FIELDTERMINATOR = ';' ,
   RowTerminator = '\n'
   )
GO
