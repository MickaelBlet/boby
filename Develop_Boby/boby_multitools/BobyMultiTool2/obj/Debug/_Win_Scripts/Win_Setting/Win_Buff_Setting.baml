   M S B A M L   `   `   `  ����Q  Mboby_multitools, Version=2.0.5794.10237, Culture=neutral, PublicKeyToken=null%clr-namespace:MyControl	MyControl  5   	   /clr-namespace:BobyMultitoolsBobyMultitools  5   	   %    BobyMultitools.Win_Buff_Setting   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,Controlsclr-namespace:MyControl     5   	   .localclr-namespace:BobyMultitools     5   	     � Title$  Setting��5      $	��375��5      $	��322��5       � WindowStartupLocation$ CenterScreen=�5	       � Topmost .  � WindowStyle$
 None=�5      
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5       � ShowInTaskbar .   � 	Resources 5      *  $System.Windows.Controls.DataGridCell �� 
TargetType	 �� Property
 �� Value)  #System.Windows.Controls.DataGridRow �� 
TargetType ��Name J� SnapsToDevicePixels?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid � ColumnDefinitions � RowDefinitions?  9System.Windows.Controls.Primitives.DataGridCellsPresenter   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter   DetailsVisibility$   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical �� ConverterParameter&   System.Windows.Controls.DataGrid#   RowDetailsScrollingConverter �� 	Converter �� RelativeSource �� AncestorType:  4System.Windows.Controls.Primitives.DataGridRowHeader7	  1System.Windows.Controls.DataGridHeadersVisibility
 	  Row!   HeadersVisibilityConverter �� Triggers P� Property P� Value �� 
TargetName   ColumnHeaderStyle=
  7System.Windows.Controls.Primitives.DataGridColumnHeader#   BobyMultitools.BuffCollection  PersonBuffCollection " PersonBuffCollectionViewSource �� Source)   #BobyMultitools.PlayerBuffCollection  PersonPlayerBuffCollection ( $PersonPlayerBuffCollectionViewSource��%�  '        '  �     &	  �    &	 <    &	 S    7  &	 o    &	 �    7  �� 5   
   "  6   .�5      �� 	 � ��$
 0q�60   �� 5      	 � ��

 ���� 6+   �� 5      	 � ��$
 20��6*   �� 5      	 � ��

 �����6+   5      �� 5   
   "  6   .�5      �� 	 � ��$
 0q�60   �� 5      	 � ��

 ���� 6+   �� 5      	 � ��

 �����6+   �� 5      	 � ��
 5      �� 5      "  6&   .R�5      �� $ 
DGR_Border��6!   
������ #��z��5       #��z��5!       . ._�5#        $��bg��65    5$   "   �� 5%   &   $
��Auto �67   �� 5&   &   $��* �67    5(   "   �� 5)   &   $��* �64   �� 5*   &   $
��Auto �64   .+�5,   "     $��1��69   #��z 5-      # z 5.        5/   "   $��1��6;   $��1��50      #y�z 56       51      �� *AreRowDetailsFrozen+# Z # Z  �� "  56        57   "   $��2��64   $ Vertical=�58      y�59      �� *HeadersVisibility+# Z # Z  �� "  5=      5>       5?      P� 5@       �@J�IsMouseOver . . �5A   "   �� $ 
DGR_Border��6?   	 � ��
 �	#2255FFFF6W   5B      P� 5C       �@ 
IsSelected . . �5D   "   �� $ 
DGR_Border��6?   	 � ��
 �	#4455FFFF6W   5E      5G      5I      5J      �� 5K   
   " 
 6*   .�5L      �� 	 � ��$
 15��6'   �� 5M      	 � ��
 �#0006+   �� 5N      	 � ��
 �#FFF6+   �� 5O      	 � ��$
 10(�6)   �� 5P      	 �@
 SeparatorVisibility$
 Visible=�64   �� 5Q      	 �@
 SeparatorBrush
 �#FFF6/   �� 5R      	 � ��$
 Center=�6;   5S        5U   
   5V      �� 5W   
   8   5Y        5Z   
   5[      �� 5\   
   8   5^      5_      .��5`      � .+�5b   
      MyControl.Windows      Title$  buff setting��6   � 5d   
   $��0,0,5,0q�6   $��Right=�5e      $	��Top=�5f      .+�5g      �� ! �� Orientation$! 
Horizontal=�6   .+�5h      �� -   5        $��bt_Close��5i   	   ��.    WindowButton.Close.Blue#��[ 5k   	   $��35��5l      $��-1,0,0,0q�5m   	   5o      5p      5q      � 5r   
   $	��Top=�6   $
��Left=�5s      $
��auto��5t      $��3,26,0,0q�6   .+�5u      �� $	��Top=�6   $
��Left=�5v   	   $��0,0,0,0q�6$   ��.  $	��570��6G     BG1#��[ 6T   �� 5w      $	��Top=�6   $
��Left=�5x   	   $��0,2,0,5q�6$   $
��auto��65   .+�5y      �� $��1,0,0,0q�6   $! 
Horizontal=�6.   .+�5z      �� -   5        $	��new��5z        Style_File_New_Button#��[ 6(   6k   �� 5{      -   5        $
��open��5{        Style_File_Open_Button#��[ 6*   6o   �� 5|      -   5        $
��save��5|       	 Style_File_Save_Button#��[	 6)   6n   �� 5}      -   5        $��tFile��5�      $	��Top=�5}      $
��Left=�5~      $	��226��5      $��24��5�      $��1,1,0,0q�5�      
�������5�      5�        5�      -   5        $��dataGrid��5�      $	��Top=�5�      $
��Left=�5�      $��-2,0,0,0q�5�      
������ "   AutoGenerateColumns" .  #   CanUserReorderColumns# .  $   CanUserResizeColumns$ .  %   CanUserResizeRows% .  &   CanUserSortColumns& .  '   CanUserAddRows' .  (   CanUserDeleteRows( .  )   RowDetailsVisibilityMode$) 	Collapsed=�5�      *   VerticalGridLinesBrush* �	#00FFFFFF+   HorizontalGridLinesBrush+ �	#00FFFFFF,   SelectionMode$, Single=�5�      -   RowHeaderWidth$- 0Q�5�      $	��162��5�      $	��321��5�      .   ColumnHeaderStyle#. [  5�      ���	#00FFFFFF$��Disabled=�5�      $��Visible=�5�      ��5�      �� / �� Source#/ [ 0   Columns0 5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      ,  &System.Windows.Controls.DataGridColumn1   Header$1 Id��5�      2   Width5  /System.Windows.Controls.DataGridLengthConverter$2 50 5�      3   CellTemplate3 5�      �� 5�   "   .R�5�   &   �� -   5        $��Right=�5�   .   $��48��6J   4 �� TextAlignment$4 Center=�6U   ��6l   �� *ID+5 �� UpdateSourceTrigger$5 PropertyChanged=�6�   5�   #   5�        5�      $1 Command��5�      $	2 260 5�      3 5�      �� 5�   "   .R�5�   &   �� -   5        $	��258��5�   .   $
��Left=�6:   $
4 Left=�6U   ��6j   �� *	COMMAND+$5 PropertyChanged=�6�   5�   #   5�      5�      �� 5�      $��	-2,-2,0,0q�6   $! 
Horizontal=�60   .+�5�      �� -	   5        $
��plus��5�      $	��160��6)   .��6H   +6K   �� 5�      -
   5        $
��moin��5�      $	��160��6)   .��6H   -6K   5�      �� 5�      -   5        $��lb_Buff��5�      $��0,-2,0,2q�5�      
������ ���	#00FFFFFF$��Stretch=�5�      $��Disabled=�5�      $��Visible=�5�      $	��321��5�      $	��115��5�      ��5�      �� #/ [ ��5�      �� 5�      .R�5�      � 5�       5�      6 J� Opacity7 J� IsHitTestVisible8 �� TextAlignment��%�  '��       �� 5�      " ��6    .�5�      �� 	 � ��$
 Stretch=�6K   �� 5�      	 � ��$
 40��67   �� 5�      	 � ��$
 40��67   �� 5�      	 � ��$
 0q�68   �� 5�      	 � ��$
 0q�67   �� 5�      	 � ��$
 0q�6@   �� 5�      	 � ��
 5�   "   �� 5�   &   " ��5�      .R�5�   *   � .+�5�   .   �� -   5        $	��Top=�5�   )   $��40��5�   5   $��40��5�   5   $6 0Q�5�   5   �� 5�   .   7 .  $��40��5�   4   $��40��5�   4   ��5�   4   �� *IMG+�� 5�   .   7 .  $��Bottom=�5�   1   $��Stretch=�5�   1   $8 Center=�5�   1   
����������	#88000000$��9(�5�   1   ��5�   1   �� *ID+5�   +   5�   '   5�      5�      5�      5�      5�      5�      5�      5�      