   M S B A M L   `   `   `  ���� N  Jboby_shulack, Version=1.0.5728.39482, Culture=neutral, PublicKeyToken=null+clr-namespace:BOBY_ShulackBOBY_Shulack      BOBY_Shulack.Win_Loot   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,localclr-namespace:BOBY_Shulack       � Title$  	BOBY_Loot�� � Topmost .  � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�$	��495��$	��480�� � 	Resources    NormalBrush �� 
StartPoint �� EndPoint � Color	 � Offset  HorizontalNormalBrush  
LightBrush  HorizontalLightBrush  	DarkBrush  PressedBrush  DisabledForegroundBrush
 �� Color  DisabledBackgroundBrush  WindowBackgroundBrush 	 SelectedBackgroundBrush 
 NormalBorderBrush  HorizontalNormalBorderBrush  DefaultedBorderBrush  PressedBorderBrush  DisabledBorderBrush  SolidBorderBrush  LightBorderBrush  
GlyphBrush  LightColorBrush  ScrollBarLineButton �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Path �� RelativeSource �� Triggers P� Property P� Value �� 
TargetName  ScrollBarPageButton  ScrollBarThumb  VerticalScrollBar � RowDefinitions ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton  HorizontalScrollBar � ColumnDefinitions �� Triggers*  $System.Windows.Controls.DataGridCell)  #System.Windows.Controls.DataGridRow �� 	Resources!   BOBY_Shulack.LootCollection  PersonCollection"   BOBY_Shulack.LootCollection2  PersonCollection2  PersonCollectionViewSource �� Source  PersonCollectionViewSource2��%�  &	        &	 V     &	 �     &	     &	 b    &	 �    &	 B    &	 R    &	 b    &		 r    &	
 �    &	 �    &	 B    &	 �    &	 �    &	 �    &	     &	     &	 .    &	 >    7   7 
 7  7  7  &	 �    &	 v    &	 >    7  7  7  7  7  &	 0    7  7  7  7   7 
 7  7  '�� �	    7  7  '  +    '  c    &	 �    &	 �    &	 �    7  &	 �    7  �� $	 0,0&�$	 0,1&���� � $
 #FFF��$		 0.0Q�� $
 #FFF��$		 1.0Q��� $	 0,0&�$	 1,0&���� � $ 	#88FFFFFF��$		 0.0Q�� $ 	#88FFFFFF��$		 1.0Q��� $	 0,0&�$	 0,1&���� � $
 #FFF��$		 0.0Q�� $
 #EEE��$		 1.0Q��� $	 0,0&�$	 1,0&���� � $
 #FFF��$		 0.0Q�� $
 #EEE��$		 1.0Q��� $	 0,0&�$	 0,1&���� � $
 #FFF��$		 0.0Q�� $
 #AAA��$		 1.0Q��� $	 0,0&�$	 0,1&���� � $
 #BBB��$		 0.0Q�� $
 #EEE��$		 0.1Q�� $
 #EEE��$		 0.9Q�� $
 #FFF��$		 1.0Q��� $

 #888���� $

 #EEE���� $

 #000���� $

 #DDD���� $	 0,0&�$	 0,1&���� � $ 	#55FFFFFF��$		 0.0Q�� $ 	#55FFFFFF��$		 1.0Q��� $	 0,0&�$	 1,0&���� � $ 	#55FFFFFF��$		 0.0Q�� $ 	#55FFFFFF��$		 1.0Q��� $	 0,0&�$	 0,1&���� � $
 #777��$		 0.0Q�� $
 #FFF��$		 1.0Q��� $	 0,0&�$	 0,1&���� � $
 #444��$		 0.0Q�� $
 #888��$		 1.0Q��� $

 #000���� $

 #888���� $

 #AAA���� $

 #FFF���� $

 #DDD���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� " ��.R��� $��Border��$��1q�$ 2��8��  8�� $��1q�._�F� $��Center=�$��Center=�8�� ���� $ Content� �� *TemplatedParent+ P�  � �� . . ��� $ Border��8   � ��P�  � {� .  . ��� 8   � ���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� 
������ �� " n�.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " n�.R��� $ 4��#��z��#��z��$��1q��� " ��.R��  �� $��0.00001* �.+��� $��1��$ 0��
������ ]� $��
PART_Track��$��1����.  �� 8��  $��ScrollBar.PageUpCommand�� n� 8�� $��1,0,1,0q�8�� 8��  �� 8�� $��ScrollBar.PageDownCommand���� " ��.R��  �� $��18���� $��0.00001* ��� $��18��.+��� $��3��$ 2�����#F0F0F0�� $��0��8��  $��18��$��ScrollBar.LineLeftCommand��$��M 4 0 L 4 8 L 0 4 Z��]� $��
PART_Track��$��1����.   �� 8�� $��ScrollBar.PageLeftCommand�� n� 8�� $��0,1,0,1q�8�� 8��  �� 8�� $ ��ScrollBar.PageRightCommand���� $��3��8�� $��18��$ ��ScrollBar.LineRightCommand��$��M 0 0 L 4 4 L 0 8 Z���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle .  P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 18����  � ��8   P�  �@��Orientation$ Vertical=�. ���  � ��$ 10����  � ��$
 Auto����  � ��8  �� "  .���  � ��$ 0q���  � ��$ 19���� "   ���� $
 	#88BBFFFF��(��       * SystemColors.HighlightBrushKey+)�� $
 	#88BBFFFF��(��       *SystemColors.ControlBrushKey+)�� $
 Black��(��       *" SystemColors.ControlTextBrushKey+)�� $
 Black��(��       *$"SystemColors.HighlightTextBrushKey+).���  � �� �	#77000000��  � ��
 �������  �@=�ErrorTemplate Z�     �� 8   �� 8   .��� .+��� $$��Image_Windows\Win_Top_Left.png��$	��Top=�$
��Left=�$��24��$��24��$	��0,0q��� $%��Image_Windows\Win_Top_Right.png��$	��Top=�$��Right=�$��24��$��24��$	��0,0q��� $$��Image_Windows\Win_Top_Fill.png��$	��Top=�$��Right=�$
��Fill=�$��24��$
��24,0q��� $%��Image_Windows\Win_Left_Fill.png��$	��Top=�$
��Left=�$
��Fill=�$��24��$
��0,24q��� $'��!Image_Windows\Win_Center_Fill.png��$	��Top=�$
��Left=�$
��Fill=�$��24,24q��� $&�� Image_Windows\Win_Right_Fill.png��$	��Top=�$��Right=�$
��Fill=�$��24��$
��0,24q��� $'��!Image_Windows\Win_Bottom_Left.png��$��Bottom=�$
��Left=�$��24��$��24��$	��0,0q��� $(��"Image_Windows\Win_Bottom_Right.png��$��Bottom=�$��Right=�$��24��$��24��$	��0,0q��� $'��!Image_Windows\Win_Bottom_Fill.png��$��Bottom=�$��Right=�$
��Fill=�$��24��$
��24,0q��� -   $��label��$��BOBY Recycle Loot��$��16(�$��Microsoft Tai Le)�$
��Bold"�
�������$	��Top=�$
��Left=�$
��12,7q�$��28�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$ 0Q�    Color$  White��!   Opacity$	! 0.5Q�"   
BlurRadius$" 10Q��� -   $��label2��$��Recycle Loot Full��$��16(�$��Microsoft Tai Le)�$
��Bold"�
�������$	��Top=�$
��Left=�$��	242,7,0,0q�$��28��   $ 0Q�$  White��$	! 0.5Q�$" 10Q�&   System.Windows.Controls.DataGrid  -   $��dataGrid��$	��Top=�$
��Left=�$��	11,34,0,0q�
������ #   AutoGenerateColumns# .  $   CanUserReorderColumns$ .  %   CanUserResizeColumns% .  &   CanUserResizeRows& .  '   CanUserSortColumns' .  (   CanUserAddRows( .  )   CanUserDeleteRows) .  *   RowDetailsVisibilityMode$* 	Collapsed=�+   VerticalGridLinesBrush+ �	#88FFFFFF,   HorizontalGridLinesBrush, �	#88FFFFFF-   SelectionMode$- Single=�.   RowHeaderWidth$. 0Q�$	��271��$	��228��/   ColumnHeaderHeight$/ 0Q����	#88FFFFFF$��Disabled=�$��Visible=����� 0 �� Source#0 [ 1   Columns1 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn2   Header$
2 Name��3   Width5	  /System.Windows.Controls.DataGridLengthConverter$	3 216	 1
  +System.Windows.Controls.DataGridBoundColumn4 
  Binding4 �� *Name+�� $	��Top=�$
��Left=�$��
12,306,0,0q�5 �� Orientation$5 
Horizontal=�.+��� -   $
��Bold"�$��57��  Style_Aion_Button#��[ .��+�� -   $
��Bold"�$��57��#��[ .��X�� -   $��57��#��[ .��▲�� -   $��57��#��[ .��▼�� -   $��listview_Item��$��Disabled=�$��Visible=�$
��Left=�$	��Top=����	#88FFFFFF$��1q�6 �� SelectionMode$6 Single=�$��
11,334,0,0q�
�������
������ $	��228��$	��150��  -	   $��	dataGrid2��$	��Top=�$
��Left=�$��
241,34,0,0q�
������ # .  $ .  % .  & .  ' .  ( .  ) .  $* 	Collapsed=�+ �	#88FFFFFF, �	#88FFFFFF$- Single=�$. 0Q�$	��271��$	��228��$/ 0Q����	#88FFFFFF$��Disabled=�$��Visible=����� #0 [ 1   $
2 Name��$	3 216	 4 �� *Name+�� $	��Top=�$
��Left=�$��242,306,0,0q�$5 
Horizontal=�.+��� -
   $
��Bold"�$��57��#��[ .��+�� -   $
��Bold"�$��57��#��[ .��X�� -   $��57��#��[ .��▲�� -   $��57��#��[ .��▼�� -   $��listview_Item2��$��Disabled=�$��Visible=�$
��Left=�$	��Top=����	#88FFFFFF$��1q�$6 Single=�$��241,334,0,0q�
�������
������ $	��228��$	��150��