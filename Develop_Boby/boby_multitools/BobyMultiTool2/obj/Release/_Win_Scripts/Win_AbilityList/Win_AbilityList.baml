   M S B A M L   `   `   `  ���� P  Lboby_multitools, Version=2.0.5796.7710, Culture=neutral, PublicKeyToken=null%clr-namespace:MyControl	MyControl  /clr-namespace:BobyMultitoolsBobyMultitools  $    BobyMultitools.Win_AbilityList   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,Controlsclr-namespace:MyControl     .localclr-namespace:BobyMultitools       � Title$  Setting��$	��375��$	��322�� � WindowStartupLocation$ CenterScreen=� � Topmost .  � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � 	Resources    myHeaderStyle �� 
TargetType	 �� Property
 �� Value �� 
TargetType �Name �� RadiusX �� RadiusY �� EndPoint �� 
StartPoint � Color � Offset ��Name  ItemContStyle J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   ShadowDepth   Color   Opacity   
BlurRadius �� Content �� Triggers P� Property P� Value �� 
TargetName��%�	  &	        '� *     &	 �    �� " ��.��� 	 � y�$
 	Collapsed=��� " �.��� 	 � ��
 �� " �.R�� $ 
PART_Track��.+��� #��z��#��z��$
��Left=�$ 2��$ 2������ $	 0,1&�$	 1,0&�.��� $ 	#44000000��$ 0Q�� $ 	#00000000��$	 0.5Q�� $ 	#44000000��$ 1Q��� $ PART_Indicator��$
��Left=�$ 2��$ 2������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#9900A599��$ 0Q�� $ 	#9900A599��$ 1Q��� " ��.��� 	 � ��$
 Stretch=��� 	 � ��$
 30���� 	 � ��$
 13(��� 	 � ��$
 0q��� 	 � ��$
 0q��� 	 � ��

 ������� 	 � ��$
 0q��� 	 � ��
 �� " ��.R�� $
��g_st��
������ .+��� $��r_top_select��$y�Hidden=�$	��Top=�$��0,0,0,0q�$��1������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ 	#FF84AF7E��� $	 0.5Q�$ 	#FF84AF7E��� $ 1Q�$ 	#8884AF7E��   $ 4Q�$ Black��$ 1Q�$ 3Q��� $��r_bottom_select��$y�Hidden=�$��Bottom=�$��0,0,0,0q�$��1������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ 	#8884AF7E��� $	 0.5Q�$ 	#FF84AF7E��� $ 1Q�$ 	#FF84AF7E���� $��r_left_select��$y�Hidden=�$
��Left=�$��0,0,0,0q�$��1��$��40������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ 	#FF84AF7E��� $	 0.5Q�$ 	#FF84AF7E��� $ 1Q�$ Transparent��   $ 4Q�$ Black��$ 1Q�$ 3Q��� $��r_right_select��$y�Hidden=�$��Right=�$��0,0,0,0q�$��1��$��40������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ Transparent��� $	 0.5Q�$ 	#FF84AF7E��� $ 1Q�$ 	#FF84AF7E���� $��r_top��$y�Hidden=�$	��Top=�$��0,0,0,0q�$��1������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ Transparent��� $	 0.5Q�$ 	#FFA4A75E��� $ 1Q�$ Transparent���� $��r_bottom��$y�Hidden=�$��Bottom=�$��0,0,0,0q�$��1������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ Transparent��� $	 0.5Q�$ 	#FFA4A75E��� $ 1Q�$ Transparent���� $��content��$��Stretch=�#��z��$
��Auto��$��0q�# z�� P�  �@J�IsMouseOver . . ��� $ r_top��	 � y�$
 Visible=��� $ r_bottom��	 � y�$
 Visible=��� 	 � ��
 �	#FFCBA85BP�  �@��
IsSelected . . ��� $ r_top��	 � y�$
 Hidden=��� $ r_bottom��	 � y�$
 Hidden=��� $ r_top_select��	 � y�$
 Visible=��� $ r_bottom_select��	 � y�$
 Visible=��� $ r_left_select��	 � y�$
 Visible=��� $ r_right_select��	 � y�$
 Visible=��� 	 � ��
 �	#FFCBA85B�� $
 g_st��	 � ��
 �	#2255FFFF.��� .+�   MyControl.Windows     Title$ 
skill list��� $��0,0,5,0q�$��Right=�$	��Top=�.+���  �� Orientation$ 
Horizontal=�.+��� -   $��
bt_Refresh����.    WindowButton.Refresh#��[ $��28��$��-1,0,0,0q��� -   $��bt_Close����.    WindowButton.Close.Blue#��[ $��35��$��-1,0,0,0q�� $��1,28,1,1q�.+��� .+�� $	��318��.+��� -   $��lbSkills��$��Stretch=�$��Stretch=�$��0,0,0,0q�$��0q����	#22FFFFFF$��Hidden=�  �� SelectionMode$  Single=�#��[ $��13(����� " ��.��� 	 �@�FocusVisualStyle
 Z� ��L� ! j� TileMode$
! Tile=�" j� Stretch$
" Fill=�# j� Viewport$# 0,0,1,60�$ j� ViewportUnits$$ Absolute=�% L� Drawing% I� .��� & � Brush& �	#55000000' � Geometry' �� ( �� Rect$( 0,0,1,1�� & �	#55555555' �� $( 0,1,1,1�) �� View) �� !* �� ColumnHeaderContainerStyle#* [  + �� AllowsColumnReorder+ .  .P��� , �� Width$, 30��$��Image��- �� CellTemplate- �� .R��� $��	-3,0,-3,0q�$��24��$��24������ *Image+�� $	, 278��$
��Name��- �� .R��� $��-3,0,0,0q�$��CharacterEllipsis=����� *Name+�� -   $��img_ShugoLoading��$��Center=�$��Center=�. J� Opacity$. 1Q�$	��115��   $ 0Q�$ 	#FFFFFFFF��$ 1Q�$ 10Q�� -   $��	pgLoading��$��Bottom=�$��2q�$��5��A� / A� Columns$/ 1��$��0,2,0,2q�.+��� -   $��btClipboard��$��1,0,1,0q�.��Copy