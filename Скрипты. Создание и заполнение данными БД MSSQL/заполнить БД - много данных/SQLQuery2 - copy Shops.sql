

BULK INSERT DBMagazine.dbo.Shops
FROM 'E:\1\1 �������� ��\��������� �� - ����� ������\Shops-1000-2000.csv' 
with
  (
  DATAFILETYPE = 'char',
  FIELDTERMINATOR = ';' ,
   RowTerminator = '\n'
   )
GO

BULK INSERT DBMagazine.dbo.Shops
FROM 'E:\1\1 �������� ��\��������� �� - ����� ������\Shops-2001-3000.csv' 
with
  (
  DATAFILETYPE = 'char',
  FIELDTERMINATOR = ';' ,
   RowTerminator = '\n'
   )
GO


BULK INSERT DBMagazine.dbo.Shops
FROM 'E:\1\1 �������� ��\��������� �� - ����� ������\Shops-3001-4000.csv' 
with
  (
  DATAFILETYPE = 'char',
  FIELDTERMINATOR = ';' ,
   RowTerminator = '\n'
   )
GO

