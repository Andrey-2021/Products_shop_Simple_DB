   M S B A M L   `   `   `  ����C  ?WpfAppDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WpfAppDBWpfAppDB  5   	   "    WpfAppDB.View.MainWindowView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:WpfAppDB     5   	   -   5          �Name$  
MainWindow��5
   	    � Title$ База данных.��5   	   $	��500��6   $
��1100��6+    � WindowStartupLocation$ CenterScreen=�5	   	    � 	Resources 5       �� Source�� 5   
   $ ./skin/light.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      6   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      $	��70* �6   6-   �� 5      $	��30* �6   6-   .+�5   
   �� $��1��6   $��0��6!   ��5      �� *CreatingNewConnectToDbCommand+��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � ��	 �� Value$D	 >Нет соединения с БД (Подключиться)��60   �� 5       � ��
	 �  ��63   
 �� Triggers
 5       �� 5!       �� Binding 6&   �� *IsDbAvalaible+ �� UpdateSourceTrigger$ PropertyChanged=� �� Value true6m   .a�5"      ��  � ��$F	 @Есть соединение с БД (Подключиться)��68   �� 5#       � ��
	 � � �6;   5$      5&      5(      �� 5,   
   $��2��6   $��0��6"   $	��200��62   .+�5-      �� $��5q�6   
���������63   �� *
AddingShop+.��6R   !Добавить Магазин6d   �� 5.      $��5q�6   
���������63   �� *ChangingShop+.��6T   #!Изменить Магазины6g   �� 5/      $��5q�6   
���������63   �� *DeleitingShop+.��6U   Удалить Магазин6f   50      �� 54   
   $��2��6   $��1��6"   $	��200��62   .+�55      �� $��5q�6   
���������64   �� *AddingProduct+.��6V   Добавить Товар6f   �� 56      $��5q�6   
���������63   �� *ChangingProduct+.��6W   Изменить Товар6g   �� 57      $��5q�6   
���������63   �� *DeleitingProduct+.��6X   Удалить Товар6g   �� 59      $��5q�6   
��������68   �� *CloseWindowCommand+.��6_   Закрыть6h   5:      &   System.Windows.Controls.DataGrid  5=   
   $��1��6   $��0��6    
�������   AutoGenerateColumns$ False��5@         CanUserAddRows$ False��5A      ��5>      �� *Shops+ �� SelectedItem 5?      �� *SelectedShop+   Columns 5C      0  *System.Windows.Controls.DataGridTextColumn  5D      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   ��  �� Path$
 Name�6W     5E      $ 
Адрес��6%    64   �� $ Address�6W     5F      $ Телефон��6%    66   �� $ Phone�6W     5G      $ e-mail��6%    65   �� $ Email�6V    � ContextMenu 5I      �� 5J      .@�5K      m� $%��Добавить магазин��6    m� Command 69   �� *
AddingShop+6Z   m� 5L      $%��Изменить магазин��6    69   �� *ChangingShop+6\   m� 5M      $#��Удалить магазин��6    68   �� *DeleitingShop+6]   5N      5P        5S   
   $��1��6   $��1��6    
�������$ False��5V      $ False��5W      ��5T      �� *
Products+ 5U      �� *SelectedProduct+ 5Y        5Z      $ Название��6%    67   �� $
 Name�6W     5[      $ Цена��6%    63   �� $ Price�6T    5]      �� 5^      .@�5_      m� $!��Добавить товар��6    67   �� *AddingProduct+6[   m� 5`      $!��Изменить товар��6    67   �� *ChangingProduct+6]   m� 5a      $��Удалить товар��6    66   �� *DeleitingProduct+6^   5b      5f      5g      5h      