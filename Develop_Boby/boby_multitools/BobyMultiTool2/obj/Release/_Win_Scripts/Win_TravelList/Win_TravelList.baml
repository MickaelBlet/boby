   M S B A M L   `   `   `  ���� P  Lboby_multitools, Version=2.0.5796.7710, Culture=neutral, PublicKeyToken=null%clr-namespace:MyControl	MyControl  /clr-namespace:BobyMultitoolsBobyMultitools  #    BobyMultitools.Win_TravelList   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,Controlsclr-namespace:MyControl     .localclr-namespace:BobyMultitools       � Title$  Setting��$	��350��$	��422�� � WindowStartupLocation$ CenterScreen=� � Topmost .  � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � 	Resources *  $System.Windows.Controls.DataGridCell �� 
TargetType	 �� Property
 �� Value)  #System.Windows.Controls.DataGridRow �� 
TargetType ��Name J� SnapsToDevicePixels?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid � ColumnDefinitions � RowDefinitions?  9System.Windows.Controls.Primitives.DataGridCellsPresenter   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter   DetailsVisibility$   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical �� ConverterParameter&   System.Windows.Controls.DataGrid#   RowDetailsScrollingConverter �� 	Converter �� RelativeSource �� AncestorType:  4System.Windows.Controls.Primitives.DataGridRowHeader7	  1System.Windows.Controls.DataGridHeadersVisibility
 	  Row!   HeadersVisibilityConverter �� Triggers P� Property P� Value �� 
TargetName=
  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Padding  �Name! �� RadiusX" �� RadiusY# �� EndPoint$ �� 
StartPoint% � Color& � Offset' ��Name%   BobyMultitools.TravelCollection   TravelCollection  TravelCollectionViewSource( �� Source��%�  '        '  ~     '
      '�     &	  �    &	 �    7   �� "  .��� 	 � ��$
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
������ #��z��#��z�� . ._�  $��bg�� �� $
��Auto ��� $��* � �� $��* ��� $
��Auto �.+�  $��1��#��z # z   $��1��$��1��#y�z  �� *AreRowDetailsFrozen+# Z # Z  �� "    $��2��$ Vertical=�y��� *HeadersVisibility+# Z # Z  �� "   P�  �@J�IsMouseOver . . ��� $ 
DGR_Border��	 � ��
 �	#2255FFFFP�  �@ 
IsSelected . . ��� $ 
DGR_Border��	 � ��
 �	#4455FFFF�� " 
 .��� 	 � ��
 �	#00000000�� 	 � ��
 �	#AAFFFFFF�� 	 � ��$
 26���� 	 � ��
 �� " 
 .R�� .+��� $��HeaderBorder��$��0q�$ 2,0,2,0q�._��� $��HeaderContent��
�������$��Center=�$��Center=�$��0,0,0,1q��� " �.��� 	 � ��
 �� " �.R�� $  
PART_Track��.+��� #��z��#��z��$
��Left=�$! 2��$" 2������ $	# 0,1&�$	$ 1,0&�.��� $% 	#44000000��$& 0Q�� $% 	#00000000��$	& 0.5Q�� $% 	#44000000��$& 1Q��� $' PART_Indicator��$
��Left=�$! 2��$" 2������ $# 0.5,1&�$$ 0.5,0&�.��� $% 	#9900A599��$& 0Q�� $% 	#9900A599��$& 1Q�  �� 8(   .��� .+�   MyControl.Windows  )   Title$) Travel list��� $��0,0,5,0q�$��Right=�$	��Top=�.+��� * �� Orientation$* 
Horizontal=�.+��� -   $	��new��  Style_File_New_Button#��[ �� -   $
��save��  Style_File_Save_Button#��[ �� -   $��tFile��$	��Top=�$
��Left=�$	��200��$��24��$��1,2,2,0q�
��������� -   $��bt_Close����.    WindowButton.Close.Blue#��[ $��35��$��-1,0,0,0q�� $��1,28,1,1q�.+��� .+�� $	��262��.+�  -   $��dgTravel��$	��Top=�$��Stretch=�$	��262��$��0,0,0,0q�+   AutoGenerateColumns+ .  ,   CanUserReorderColumns, .  -   CanUserResizeColumns- .  .   CanUserResizeRows. .  /   CanUserSortColumns/ .  0   CanUserAddRows0 .  1   CanUserDeleteRows1 .  2   RowDetailsVisibilityMode$2 	Collapsed=�3   VerticalGridLinesBrush3 �	#00FFFFFF4   HorizontalGridLinesBrush4 �	#00FFFFFF5   SelectionMode$5 Single=�6   RowHeaderWidth$6 0Q����	#00FFFFFF$��Disabled=�$��Visible=����� 7 �� Source#7 [ ��L� 8 j� TileMode$
8 Tile=�9 j� Stretch$
9 Fill=�: j� Viewport$: 0,0,1,52�; j� ViewportUnits$; Absolute=�< L� Drawing< I� .��� = � Brush= �	#55000000> � Geometry> �� ? �� Rect$? 0,0,1,1�� = �	#55555555> �� $? 0,1,1,1�@   Columns@ 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumnA   Header$
A Name��B   Width5  /System.Windows.Controls.DataGridLengthConverter$B 50 C   CellTemplateC �� .R��� $��50��D �� TextAlignment$
D Left=����� *NAME+E �� UpdateSourceTrigger$E PropertyChanged=�  $A X��$B 60 C �� .R��� -   $��60��$
D Left=����� *X+F �� StringFormat$F F2��  $A Y��$B 60 C �� .R��� -   $��60��$
D Left=����� *Y+$F F2��  $A Z��$B 60 C �� .R��� -   $��60��$
D Left=����� *Z+$F F2��  $	A Fly��$B 30 C �� .R��� $��30��$��	-9,0,-5,0q�G d� 	IsCheckedG �� *FLY+$E PropertyChanged=�  $
A Type��$B 60 C �� .R��� -	   $��60��$��	-9,0,-5,0q�H �� TextH �� *TYPE+$E PropertyChanged=�.@��� $
��None���� $��Action���� $
��Rest��  $A Comment��$B 87 C �� .R��� $* 
Horizontal=�.+��� $��87��$
D Left=����� *	COMMENT+$E PropertyChanged=��� -
   $��img_ShugoLoading��$��Center=�$��Center=�I J� Opacity$I 1Q�$	��115��J J� EffectJ 3  -System.Windows.Media.Effects.DropShadowEffect  K   ShadowDepth$K 0Q�L   Color$L 	#FFFFFFFF��M   Opacity$M 1Q�N   
BlurRadius$N 10Q�� -   $��	pgLoading��$��Bottom=�$��2q�$��5��A� O A� Columns$O 5��$��0,2,0,1q�.+��� -   $��btRecord��$��1,0,1,0q�.��Record�� $��1,0,1,0q����#EEE$��Right=�.��Default Type :�� -   $��cDefaultType��$	��Top=�$��26��$��1,0,1,0q�
�������.@��� P �� 
IsSelectedP . $
��None���� $��Action���� $
��Rest��A� $O 2��$��0,0,0,0q�.+��� -   $��btAdd��$��1,0,1,0q�$��18(�.��+�� -   $��btRemove��$��1,0,1,0q�$��18(�.��-A� $O 2��$��0,0,0,0q�.+��� -   $
��btUp��$��1,0,1,0q�.��▲�� -   $��btDown��$��1,0,1,0q�.��▼A� $O 4��$��0,1,0,2q�.+��� -   $��btRun��$��1,0,1,0q�.��
Test Run�� -   $��	btFullRun��$��1,0,1,0q�.��Test Full Run�� $��1,0,1,0q����#EEE$��Right=�.��Travel Path :�� -   $��cTravelPath��$	��Top=�$��26��$��1,0,1,0q�
�������.@��� P . $��Circular���� $��Reverse��