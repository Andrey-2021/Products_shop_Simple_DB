   M S B A M L   `   `   `  ����C  ?WpfAppDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfAppDB.ViewWpfAppDB.View  5   	   %    WpfAppDB.View.ProductWindowView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WpfAppDB.View     5   	     � Title$  Продукты��5   	   $	��250��6   $	��250��6'   $	��500��67   $	��500��6C    � WindowStartupLocation$ CenterScreen=�5	   	    � 	Resources 5       �� MergedDictionaries �� Source!   WpfAppDB.View.CostConverter   Key �� 
TargetType �� Property �� Value �� Triggers	 P� Property
 P� Value �� Self �� RelativeSource �� Path�� 5   
    5      �� 5      $ ./skin/light.xaml?�6%   �� 5        5      myDateConverter  6*   �� 5      " ��6   .�5      ��  � ��$ 5q�63   6?    5      P� 5      	 �@=�HasError$

 True��6E   . �5   "   ��  �@�ToolTip 5      �� # Z $2 ,(Validation.Errors).CurrentItem.ErrorContent��� 5   "    � ��
 �  ��6?   5      5       5"      %
   '��       5%      .��5,      �  � ColumnDefinitions 5-   
   �� 5.      $	��30* �6   6-   �� 5/      $	��70* �6   6-    � RowDefinitions 51   
   �� 52      $��10 �6   6*   �� 53      6   �� 54      6   �� 55      6   �� 56      6   .+�59   
   �� $��1��6   $��0��6!   Название6;   �� 5:   
   $��2��6   $��0��6!   
Цена67   �� 5;   
   $��3��6   $��0��6!   Магазин6:   �� 5=   
   $��1��6   $��1��6   .��6/   ��5>      �� 5?      $ CurrentProduct.Name�6    �� UpdateSourceTrigger$ PropertyChanged=�65    �� ValidatesOnDataErrors$
 True��6[    �� ValidationRules 5@      5  /System.Windows.Controls.DataErrorValidationRule  5A      5C      .��5D      5E      �� 5G   
   $��2��6   $��1��6   .��6/   ��5H      �� 5I      $ CurrentProduct.Price�6   $ PropertyChanged=�66   $
 True��5J        myDateConverter �� 	Converter# [ 67    5K        5L      5N      .��5O      5P      �� 5R   
   $��3��6   $��1��6    �� 
IsReadOnly$
 True��5S      ��5T      �� *CurrentProduct.Shops.Name+$ PropertyChanged=�6d   � 5V   
   $��4��6   $��0��6   $��2��6,    5W      �� 5X      6%   �� 5Y      6%   .+�5[      �� $��0��6   $��0��6"   ��5\      �� *SaveProductCommand+.��68   Применить6C   �� 5]      $��0��6   $��1��6"    �� IsCancel$
 True��5^      ��5_      �� *CloseWindowCommand+.��68   Отмена6@   5`      5d      5e      