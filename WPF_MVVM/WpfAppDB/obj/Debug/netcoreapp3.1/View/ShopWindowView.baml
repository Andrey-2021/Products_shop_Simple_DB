   M S B A M L   `   `   `  ����C  ?WpfAppDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfAppDB.ViewWpfAppDB.View  5   	   "    WpfAppDB.View.ShopWindowView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WpfAppDB.View     5   	     � Title$  Магазин��5   	   $	��200��6   $	��200��6&   $	��500��66   $	��500��6B    � WindowStartupLocation$ CenterScreen=�5	   	    � 	Resources 5       �� MergedDictionaries �� Source �� 
TargetType �� Property �� Value �� Triggers	 P� Property
 P� Value �� Self �� RelativeSource �� Path�� 5   
    5      �� 5      $ ./skin/light.xaml?�6%   �� 5      �� 5      " ��6   .�5      ��  � ��$ 5q�63   6?    5      P� 5      	 �@=�HasError$

 True��6E   . �5   "   ��  �@�ToolTip 5      �� # Z $2 ,(Validation.Errors).CurrentItem.ErrorContent��� 5   "    � ��
 �  ��6?   5      5      5      %
   '��       5!      .��5%      �  � RowDefinitions 5&   
   �� 5'      $��5 �6   6)   �� 5(      $
��Auto �6   6,   �� 5)      $
��Auto �6   6,   �� 5*      $
��Auto �6   6,   �� 5+      $
��Auto �6   6,   �� 5,      $
��Auto �6   6,    � ColumnDefinitions 5/   
   �� 50      $	��30* �6   6-   �� 51      $	��70* �6   6-   .+�54   
   �� $��1��6   $��0��6!   $��Right=�55      $��Center=�60   Название:6W   �� 56   
   $��2��6   $��0��6!   $��Right=�57      $��Center=�60   Адрес:6T   �� 58   
   $��3��6   $��0��6!   $��Right=�59      $��Center=�60   Телефон:6V   �� 5:   
   $��4��6   $��0��6!   $��Right=�5;      $��Center=�60   	e-mail:6U   �� 5>   
   $��1��6   $��1��6   $	��250��60   $
��Left=�6<   .��6W   ��5?      �� 5@      $ CurrentShop.Name�6    �� UpdateSourceTrigger$ PropertyChanged=�62    �� ValidationRules 5A      5  /System.Windows.Controls.DataErrorValidationRule  5B      5D      .��5E      5F      �� 5I   
   $��2��6   $��1��6   $	��250��60   $
��Left=�6<   .��6W   ��5J      �� 5K      $ CurrentShop.Address�6   $ PropertyChanged=�65    5L        5M      5O      .��5P      5Q      �� 5T   
   $��3��6   $��1��6   $	��250��60   $
��Left=�6<   .��6W   ��5U      �� 5V      $ CurrentShop.Phone�6   $ PropertyChanged=�63    5W        5X      5Z      .��5[      5\      �� 5_   
   $��4��6   $��1��6   $	��250��60   $
��Left=�6<   .��6W   ��5`      �� 5a      $ CurrentShop.Email�6   $ PropertyChanged=�63    5b        5c      5e      .��5f      5g      � 5i   
   $��5��6   $��0��6   $��2��6-    5j      �� 5k      6%   �� 5l      6%   .+�5n      �� $��0��6   $��0��6"   ��5o      �� *SaveShopCommand+.��65   Сохранить6@   �� 5p      $��0��6   $��1��6"    �� IsCancel$
 True��5q      ��5r      �� *CloseWindowCommand+.��68   Отмена6@   5s      5v      5w      