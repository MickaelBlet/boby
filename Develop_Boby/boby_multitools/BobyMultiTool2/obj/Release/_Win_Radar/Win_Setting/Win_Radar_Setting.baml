   M S B A M L   `   `   `  ���� P  Lboby_multitools, Version=2.0.5796.7710, Culture=neutral, PublicKeyToken=null/clr-namespace:BobyMultitoolsBobyMultitools  %clr-namespace:MyControl	MyControl  &     BobyMultitools.Win_Radar_Setting   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     .localclr-namespace:BobyMultitools     ,Controlsclr-namespace:MyControl       � Title$  Setting��$	��326��$	��408�� � WindowStartupLocation$ CenterScreen=� � Topmost .  � WindowStyle$
 None=� � ShowInTaskbar .   � AllowsTransparency .  � 
ResizeMode$ NoResize=�
������  � 	Resources *  $System.Windows.Controls.DataGridCell �� 
TargetType	 �� Property
 �� Value)  #System.Windows.Controls.DataGridRow �� 
TargetType ��Name?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid?  9System.Windows.Controls.Primitives.DataGridCellsPresenter   
ItemsPanel �� Triggers P� Property P� Value �� 
TargetName=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Padding   Button_Scale  Button_Icone.Ini �� TextAlignment  Button_Icone.Hover  Button_Icone.Pressed#   BobyMultitools.IconCollection  PersonCollection  PersonCollectionViewSource �� Source��%@  '        '  ~     '      &	      7  7  7  &	 �    &	 �    7  �� "  .��� 	 � ��$
 0q��� 	 � ��

 ���� �� 	 � ��$
 20���� 	 � ��$
 26���� 	 � ��

 ������� "  .��� 	 � ��$
 0q��� 	 � ��

 ���� �� 	 � ��

 ������� 	 � ��$
 26���� 	 � ��
 �� "  .R��� $ 
DGR_Border��
������ #��z��#��z��._�  $��bg��.+�  $��1��#��z  P�  �@J�IsMouseOver . . ��� $ 
DGR_Border��	 � ��
 �	#2255FFFFP�  �@ 
IsSelected . . ��� $ 
DGR_Border��	 � ��
 �	#4455FFFF�� "  .��� 	 � ��
 �	#00000000�� 	 � ��
 �	#AAFFFFFF�� 	 � ��$
 26���� 	 � ��
 �� "  .R�� .+��� $��HeaderBorder��$��0q�$ 0,0,0,0q�._��� $��HeaderContent��
�������$��Center=�$��Center=�$��0,0,0,1q��� " ��.��� 	 � ��

 ���� �� 	 � ��

 ���� �� 	 � ��$
 20���� 	 � ��
 �� " ��.R�� $��20��.+��� $��bg��$��20��$��20��$
��Left=�$	��Top=�$	��0,0q�$
��Fill=�8��  �� $��content_back��$��20�����	#FF000000$��Center=�$	��Top=�$��1,3,0,0q�#��z��$ Center=�#��z��#��z���� $��content��$��20�����	#FFEFEDC4$��Center=�$	��Top=�$��0,2,0,0q�#��z��$ Center=�#��z��#��z�� P�  �@J�IsMouseOver . . ��� $ bg��8
  	 � ���� $ content��	 � ��
 �	#FFCBA85BP�  � �� . . ��� $ bg��8
  	 � ���� $ content��	 � ��
 �	#FFCBA85B�� $ content��	 � ��$
 0,3,0,0q��� $ content_back��	 � ��$
 1,4,0,0q�  �� 8   .��� .+�   MyControl.Windows     Title$ radar setting��� $��0,0,5,0q�$��Right=�$	��Top=�.+���  �� Orientation$ 
Horizontal=�.+��� -   $��bt_Close����.    WindowButton.Close.Blue#��[ $��35��$��-1,0,0,0q�� .+�� $��1,26,1,1q�.+��� .+�A�  A� Columns$ 3��.+�� $��50��.+��� $
��Zoom��$��12(����	#FFEFEDC4$��24��$��70��$	��Top=�$��Center=�$	��Top=��� -   $��
btDownZoom��$��2,0,0,2q�$��20��$��20��#��[  $��Bottom=�$
��Left=�.��-�� -   $��tZoom��$��60�� �� TextAlignment$ Center=�$	��0.0��$��Bottom=��� -   $��btUpZoom��$��0,0,2,2q�$��20��$��20��#��[  $��Bottom=�$��Right=�.��+� $��50��.+��� $
��Size��$��12(����	#FFEFEDC4$��24��$��70��$	��Top=�$��Center=�$	��Top=��� -   $��
btDownSize��$��2,0,0,2q�$��20��$��20��#��[  $��Bottom=�$
��Left=�.��-�� -   $��tSize��$��60��$ Center=�$	��0.0��$��Bottom=��� -   $��btUpSize��$��0,0,2,2q�$��20��$��20��#��[  $��Bottom=�$��Right=�.��+� $��50��.+��� $��	Icon Size��$��12(����	#FFEFEDC4$��24��$��70��$	��Top=�$��Center=�$	��Top=��� -   $��
btDownIcon��$��2,0,0,2q�$��20��$��20��#��[  $��Bottom=�$
��Left=�.��-�� -	   $��tIcon��$��60��$ Center=�$	��0.0��$��Bottom=��� -
   $��btUpIcon��$��0,0,2,2q�$��20��$��20��#��[  $��Bottom=�$��Right=�.��+&   System.Windows.Controls.DataGrid  -   $��dataGrid��$	��Top=�$
��Left=�$��0,2,0,0q�   AutoGenerateColumns .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     CanUserSortColumns .     CanUserAddRows .     CanUserDeleteRows .      RowDetailsVisibilityMode$  	Collapsed=�!   VerticalGridLinesBrush! �	#00FFFFFF"   HorizontalGridLinesBrush" �	#00FFFFFF#   SelectionMode$# Single=�$   RowHeaderWidth$$ 0Q�$	��184��$	��324�����	#00FFFFFF$��Disabled=�$��Visible=����� % �� Source#% [ ��L� & j� TileMode$
& Tile=�' j� Stretch$
' Fill=�( j� Viewport$( 0,0,1,52�) j� ViewportUnits$) Absolute=�* L� Drawing* I� .��� + � Brush+ �	#55000000, � Geometry, �� - �� Rect$- 0,0,1,1�� + �	#55555555, �� $- 0,1,1,1�.   Columns. 4	  .System.Windows.Controls.DataGridTemplateColumn	  ,
  &System.Windows.Controls.DataGridColumn/ 
  Header$	/ Img��0 
  Width5  /System.Windows.Controls.DataGridLengthConverter$0 20 1 	  CellTemplate1 �� .R��� $��-3,-3,-3,-3q�$
��None=����� *	IMG_SRC+	  $
/ Name��$	0 130 1 �� .R��� -   $	��128��$
 Left=����� *NAME+2 �� UpdateSourceTrigger$2 PropertyChanged=�	  $/ Scale��$0 81 1 �� .R��� $ 
Horizontal=�.+��� -   $��20��$��20��#��[  .��-�� -   $��40��$ Center=����� *SCALE+3 �� StringFormat$3 N1���� -   $��20��$��20��#��[  .��+	  $/ Aggro��$0 81 1 �� .R��� $ 
Horizontal=�.+��� -   $��20��$��20��#��[  .��-�� -   $��40��$ Center=����� *AGGRO_SCALE+�� -   $��20��$��20��#��[  .��+�� $��1,0,0,0q�$ 
Horizontal=�.+��� -   $
��plus��$	��107��.��+�� -   $
��moin��$	��107��.��-�� -   $��target��$	��107��.��Target�� -   $��lb_Items��$��0,-2,0,2q�
������ ���	#00FFFFFF$��Stretch=�$��Disabled=�$��Visible=�$	��325��$	��120������ .R��    Button_Icon��%  '��       7  �� " ��.��� 	 � ��$
 Stretch=��� 	 � ��$
 28���� 	 � ��$
 28���� 	 � ��$
 0q��� 	 � ��$
 0q��� 	 � ��
 �� *graph_forground+�� 	 � ��$
 0q��� 	 � ��
 �� " ��.R�� .+��� -   8��  #��z��