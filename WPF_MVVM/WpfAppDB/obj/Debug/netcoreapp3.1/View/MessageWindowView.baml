   M S B A M L   `   `   `  ����C  ?WpfAppDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfAppDB.ViewModelWpfAppDB.ViewModel  5   	   *    $WpfAppDB.ViewModel.MessageWindowView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:WpfAppDB.ViewModel     5   	     � Title$  Сообщение��5   	   $	��220��6   $	��500��6(    � WindowStartupLocation$ CenterScreen=�5	   	    � 	Resources 5       �� Source�� 5   
   $ ./skin/light.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      6   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      6!   .+�5   
   �� $��0��6   $��Center=�5      Сообщение!6>   �� 5   
   $��1��6   $��20��5      $	��150��6#   $
��Wrap=�5      
���������5   !   �� *	Message+5       �� 5"   
   $��2��6    �� IsCancel$
 True��5#       �� 	IsDefault$
 True��5$      ��5%      �� *CloseWindowCommand+.��68   Закрыть6A   5&      5'      