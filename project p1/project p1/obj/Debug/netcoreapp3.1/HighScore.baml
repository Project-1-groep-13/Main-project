   M S B A M L   `   `   `  ����E  Aproject p1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:project_p1
project_p1  5   	       project_p1.HighScore   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:project_p1     5   	     � Title$  	HighScore��5   	   $	��600��6   $	��500��6(   .��5	      � .+�5
   
   �� $��Single Player Mode��6   $��algerian)�6-   $��33(�6C   $	��Top=�6Q   $��
66,37,66,0q�6i    J� Opacity$ 2Q�6}   �� 5   
   -   5        $��	Main Menu��5      
�������$��22(�5      $��algerian)�5      $��138,493,138,28q�6'   6?   �� 5   
   $��Multiplayer Mode��6   $��algerian)�6+   $��33(�6A   $��Center=�6O   $��	76,0,66,0q�6j   $ 2Q�6}   $��60��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dtGrid��5   	   $	��150��5   	   $	��450��5   	   $��25,91,25,343q�5   	   $��Hidden=�5   	   $��Hidden=�5   	      HeadersVisibility$ Column=�5   	      ColumnHeaderHeight$ 30Q�5   	      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 205 5   	      	RowHeight$ 20Q�5   	      
IsReadOnly$
 True��5   	      GridLinesVisibility$
 None=�5    	   $��Center=�5!   	   ��5   	   �� 	 � 	Resources	 5"      =  7System.Windows.Controls.Primitives.DataGridColumnHeader
 �� 
TargetType �� Property �� Value��%�   '        �� 5#      "
  6   .�5$      ��  � ��
 �����6G   �� 5%       � ��$ SemiBold"�63   6F   �� 5&       � ��$ 0,0,1,2q�68   6J   �� 5'       � ��$ Center=�6C   5(      5)         RowStyle 5+      �� 5-      )  #System.Windows.Controls.DataGridRow"
  6   .�5.      ��  � ��
 ��ح�63   �� 5/       � ��
 ��ح�6H   �� 50       �@��TextAlignment$ Center=�6@   6Q   51      56        57   
   -   5        $ dtGrid_Copy��5;      $	��150��58      $	��450��59      $��25,322,25,112q�5:      $��Hidden=�5<      $��Hidden=�5=      $ Column=�5>      $ 30Q�5?      $	 150 5@      $ 20Q�5A      $
 True��5C      $
 None=�5D      $��Center=�5E      ��5B      �� 	 5F      ��%�   '        �� 5G      "
  6   .�5H      ��  � ��
 �����63   �� 5I       � ��$ SemiBold"�63   �� 5J       � ��$ 0,0,1,2q�68   �� 5K       � ��$ Center=�6C   5L      5M       5O      �� 5P      "
  6   .�5Q      ��  � ��
 ��ح�63   �� 5R       � ��
 ��ح�64   �� 5S       �@��TextAlignment$ Center=�6@   5T      5X      5Z      5[      