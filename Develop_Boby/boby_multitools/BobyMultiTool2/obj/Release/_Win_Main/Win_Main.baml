   M S B A M L   `   `   `  ���� P  Lboby_multitools, Version=2.0.5796.7710, Culture=neutral, PublicKeyToken=null%clr-namespace:MyControl	MyControl      BobyMultitools.Win_Main   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,Controlsclr-namespace:MyControl     -     � Title$  BOBY_Multitools��$	��200��$	��318�� � WindowStartupLocation$ CenterScreen=� � Topmost .  � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=� +� FocusedElement ��  �� ElementName$ lb_Game�� � 	Resources 	 �� 
TargetType
 �� Property �� Value �� 
TargetType �Name �� RadiusX �� RadiusY �� EndPoint �� 
StartPoint � Color � Offset ��Name   myHeaderStyle  ItemContStyle J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   ShadowDepth   Color   Opacity   
BlurRadius �� Content �� Triggers P� Property P� Value �� 
TargetName��%G
  '�       &	  ~    &	 �    �� "	 �.��� 
 � �� �� " �.R�� $ 
PART_Track��.+��� #��z��#��z��$
��Left=�$ 2��$ 2������ $	 0,1&�$	 1,0&�.��� $ 	#44000000��$ 0Q�� $ 	#00000000��$	 0.5Q�� $ 	#44000000��$ 1Q��� $ PART_Indicator��$
��Left=�$ 2��$ 2������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#9900A599��$ 0Q�� $ 	#9900A599��$ 1Q��� "	 ��.��� 
 � y�$ 	Collapsed=��� "	 ��.��� 
 � ��$ Stretch=��� 
 � ��$ 20���� 
 � ��$ 13(��� 
 � ��$ 0q��� 
 � ��$ 0q��� 
 � ��
 ������� 
 � ��$ 0q��� 
 � �� �� " ��.R�� $
��g_st��
������ .+��� $��r_top_select��$y�Hidden=�$	��Top=�$��0,0,0,0q�$��1������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ 	#FF84AF7E��� $	 0.5Q�$ 	#FF84AF7E��� $ 1Q�$ 	#8884AF7E��   $ 4Q�$ Black��$ 1Q�$ 3Q��� $��r_bottom_select��$y�Hidden=�$��Bottom=�$��0,0,0,0q�$��1������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ 	#8884AF7E��� $	 0.5Q�$ 	#FF84AF7E��� $ 1Q�$ 	#FF84AF7E���� $��r_left_select��$y�Hidden=�$
��Left=�$��0,0,0,0q�$��1��$��20������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ 	#FF84AF7E��� $	 0.5Q�$ 	#FF84AF7E��� $ 1Q�$ Transparent��   $ 4Q�$ Black��$ 1Q�$ 3Q��� $��r_right_select��$y�Hidden=�$��Right=�$��0,0,0,0q�$��1��$��20������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ Transparent��� $	 0.5Q�$ 	#FF84AF7E��� $ 1Q�$ 	#FF84AF7E���� $��r_top��$y�Hidden=�$	��Top=�$��0,0,0,0q�$��1������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ Transparent��� $	 0.5Q�$ 	#FFA4A75E��� $ 1Q�$ Transparent���� $��r_bottom��$y�Hidden=�$��Bottom=�$��0,0,0,0q�$��1������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ Transparent��� $	 0.5Q�$ 	#FFA4A75E��� $ 1Q�$ Transparent���� $��image_class��$��right=�$��18��$��18��$��0,0,4,0q����� *graph_class+�� $��content��$��Stretch=�#��z��$
��Auto��$��0q�# z�� P�  �@J�IsMouseOver . . ��� $ r_top��
 � y�$ Visible=��� $ r_bottom��
 � y�$ Visible=��� 
 � �� �	#FFCBA85BP�  �@��
IsSelected . . ��� $ r_top��
 � y�$ Hidden=��� $ r_bottom��
 � y�$ Hidden=��� $ r_top_select��
 � y�$ Visible=��� $ r_bottom_select��
 � y�$ Visible=��� $ r_left_select��
 � y�$ Visible=��� $ r_right_select��
 � y�$ Visible=��� 
 � �� �	#FFCBA85B�� $
 g_st��
 � �� �	#2255FFFF.��� .+�   MyControl.Windows     Title$ boby multitools��� $��0,0,5,0q�$��Right=�$	��Top=�.+���   �� Orientation$  
Horizontal=�.+��� -   $��cb_hide��$��27��  WindowButton.Checkbox#��[ ��.  ! d� 	IsChecked$
! TrueY�$�� ��$��-1,0,0,0q��� -   $��bt_Minimise����.    WindowButton.Minimise#��[ $��27��$��-1,0,0,0q��� -   $��bt_Close����.    WindowButton.Close#��[ $��35��$��-1,0,0,0q��� $��3,28,3,3q�.+��� $  
Horizontal=�.+�� $	��218��$��Stretch=�$
��Left=�$��0,0,0,0q�.+��� $	��Top=�$��Stretch=�$��0,0,0,0q�.+�A� " A� Columns$" 2��$��0,2,0,2q�.+��� -   $��bt_Start��$��0,0,1,0q�.��START�� -   $��
bt_Refresh��$��1,0,0,0q�.��	REFRESH� ���	#00555555.+�� -   $��pg_ShugoLoading��$��Center=�$	��Top=�# J� Opacity$# 0Q�$��0,90,0,0q�$	��190��$��10���� -   $��img_ShugoLoading��$��Center=�$��Center=�$# 1Q�$	��115��   $ 0Q�$ 	#FFFFFFFF��$ 1Q�$ 10Q��� -	   $��lb_Game��$
��Left=�$	��Top=�$��0,0,0,0q�$	��218��$	��122��$��0q����	#22FFFFFF$��Hidden=�$ �� SelectionMode$$ Single=�#��[ $��13(����� "	 ��.��� 
 �@�FocusVisualStyle Z� ��L� % j� TileMode$
% Tile=�& j� Stretch$
& Fill=�' j� Viewport$' 0,1,1,40�( j� ViewportUnits$( Absolute=�) L� Drawing) I� .��� * � Brush* �	#55000000+ � Geometry+ �� , �� Rect$, 0,0,1,20�� * �	#55555555+ �� $, 	0,20,1,20�- �� View- �� !. �� ColumnHeaderContainerStyle#. [  / �� AllowsColumnReorder/ .  .P��� 0 �� Width$	0 157��$
��Name��1 �� CellTemplate1 �� .R��� $��-3,0,0,0q�$��CharacterEllipsis=����� *Name+�� $0 55��$	��Lvl��1 �� .R��� $��-13,0,-13,0q�$
��Left=����� *Lvl+� $	��Top=�$
��Left=�$��93��$��2,3,0,0q����	#00555555$	��152��.+��� $	��Top=�$��Stretch=�$��0,0,0,0q�.+��� -
   $��cb_Radar��$��0,1,0,1q�.��Radar�� -   $��	cb_Entity��$��0,1,0,1q�.��Entity�� -   $��cb_Cheat��$��0,1,0,1q�.��Cheat�� -   $��	cb_Script��{�.  $��0,1,0,1q�.��Script�� -   $��cb_Quick��{�.  $��0,1,0,1q�.��
????????�� -   $��cb_Bot��{�.  .��
????????�� $��8(����#9992 �� TextAlignment$2 Center=�B@COPYRIGHT © NCSOFT CORPORATION. AION LOGO, IMAGES, GAME CONTENT