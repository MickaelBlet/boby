   M S B A M L   `   `   `  ���� P  Lboby_multitools, Version=2.0.5796.7710, Culture=neutral, PublicKeyToken=null%clr-namespace:MyControl	MyControl  /clr-namespace:BobyMultitoolsBobyMultitools  %    BobyMultitools.Win_Buff_Setting   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,Controlsclr-namespace:MyControl     .localclr-namespace:BobyMultitools       � Title$  Setting��$	��375��$	��322�� � WindowStartupLocation$ CenterScreen=� � Topmost .  � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � 	Resources *  $System.Windows.Controls.DataGridCell �� 
TargetType	 �� Property
 �� Value)  #System.Windows.Controls.DataGridRow �� 
TargetType ��Name J� SnapsToDevicePixels?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid � ColumnDefinitions � RowDefinitions?  9System.Windows.Controls.Primitives.DataGridCellsPresenter   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter   DetailsVisibility$   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical �� ConverterParameter&   System.Windows.Controls.DataGrid#   RowDetailsScrollingConverter �� 	Converter �� RelativeSource �� AncestorType:  4System.Windows.Controls.Primitives.DataGridRowHeader7	  1System.Windows.Controls.DataGridHeadersVisibility
 	  Row!   HeadersVisibilityConverter �� Triggers P� Property P� Value �� 
TargetName   ColumnHeaderStyle=
  7System.Windows.Controls.Primitives.DataGridColumnHeader#   BobyMultitools.BuffCollection  PersonBuffCollection " PersonBuffCollectionViewSource �� Source)   #BobyMultitools.PlayerBuffCollection  PersonPlayerBuffCollection ( $PersonPlayerBuffCollectionViewSource��%  '        '  h     &	  �    &	 �    &	 �    7  &	 �    &	 �    7  �� "  .��� 	 � ��$
 0q��� 	 � ��

 ���� �� 	 � ��$
 20���� 	 � ��

 ������� "  .��� 	 � ��$
 0q��� 	 � ��

 ���� �� 	 � ��

 ������� 	 � ��
 �� "  .R��� $ 
DGR_Border��
������ #��z��#��z�� . ._�  $��bg�� �� $
��Auto ��� $��* � �� $��* ��� $
��Auto �.+�  $��1��#��z # z   $��1��$��1��#y�z  �� *AreRowDetailsFrozen+# Z # Z  �� "    $��2��$ Vertical=�y��� *HeadersVisibility+# Z # Z  �� "   P�  �@J�IsMouseOver . . ��� $ 
DGR_Border��	 � ��
 �	#2255FFFFP�  �@ 
IsSelected . . ��� $ 
DGR_Border��	 � ��
 �	#4455FFFF�� " 
 .��� 	 � ��$
 15���� 	 � ��
 �#000�� 	 � ��
 �#FFF�� 	 � ��$
 10(��� 	 �@
 SeparatorVisibility$
 Visible=��� 	 �@
 SeparatorBrush
 �#FFF�� 	 � ��$
 Center=�  �� 8     �� 8   .��� .+�   MyControl.Windows      Title$  buff setting��� $��0,0,5,0q�$��Right=�$	��Top=�.+��� ! �� Orientation$! 
Horizontal=�.+��� -   $��bt_Close����.    WindowButton.Close.Blue#��[ $��35��$��-1,0,0,0q�� $	��Top=�$
��Left=�$
��auto��$��3,26,0,0q�.+��� $	��Top=�$
��Left=�$��0,0,0,0q���.  $	��570��  BG1#��[ �� $	��Top=�$
��Left=�$��0,2,0,5q�$
��auto��.+��� $��1,0,0,0q�$! 
Horizontal=�.+��� -   $	��new��  Style_File_New_Button#��[ �� -   $
��open��  Style_File_Open_Button#��[ �� -   $
��save�� 	 Style_File_Save_Button#��[	 �� -   $��tFile��$	��Top=�$
��Left=�$	��226��$��24��$��1,1,0,0q�
�������  -   $��dataGrid��$	��Top=�$
��Left=�$��-2,0,0,0q�
������ "   AutoGenerateColumns" .  #   CanUserReorderColumns# .  $   CanUserResizeColumns$ .  %   CanUserResizeRows% .  &   CanUserSortColumns& .  '   CanUserAddRows' .  (   CanUserDeleteRows( .  )   RowDetailsVisibilityMode$) 	Collapsed=�*   VerticalGridLinesBrush* �	#00FFFFFF+   HorizontalGridLinesBrush+ �	#00FFFFFF,   SelectionMode$, Single=�-   RowHeaderWidth$- 0Q�$	��162��$	��321��.   ColumnHeaderStyle#. [  ���	#00FFFFFF$��Disabled=�$��Visible=����� / �� Source#/ [ 0   Columns0 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn1   Header$1 Id��2   Width5  /System.Windows.Controls.DataGridLengthConverter$2 50 3   CellTemplate3 �� .R��� -   $��Right=�$��48��4 �� TextAlignment$4 Center=����� *ID+5 �� UpdateSourceTrigger$5 PropertyChanged=�  $1 Command��$	2 260 3 �� .R��� -   $	��258��$
��Left=�$
4 Left=����� *	COMMAND+$5 PropertyChanged=��� $��	-2,-2,0,0q�$! 
Horizontal=�.+��� -	   $
��plus��$	��160��.��+�� -
   $
��moin��$	��160��.��-�� -   $��lb_Buff��$��0,-2,0,2q�
������ ���	#00FFFFFF$��Stretch=�$��Disabled=�$��Visible=�$	��321��$	��115������ #/ [ ���� .R��  6 J� Opacity7 J� IsHitTestVisible8 �� TextAlignment��%�  '��       �� " ��.��� 	 � ��$
 Stretch=��� 	 � ��$
 40���� 	 � ��$
 40���� 	 � ��$
 0q��� 	 � ��$
 0q��� 	 � ��$
 0q��� 	 � ��
 �� " ��.R�� .+��� -   $	��Top=�$��40��$��40��$6 0Q��� 7 .  $��40��$��40������ *IMG+�� 7 .  $��Bottom=�$��Stretch=�$8 Center=�
����������	#88000000$��9(����� *ID+