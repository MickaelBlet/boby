   M S B A M L   `   `   `  ���� I  Eboby_be, Version=1.0.5787.29683, Culture=neutral, PublicKeyToken=null+clr-namespace:BOBY_ShulackBOBY_Shulack      BOBY_Shulack.Win_Skills   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,localclr-namespace:BOBY_Shulack       � Title$  BOBY_Skills�� � Topmost .  � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�$	��400��$	��400�� � 	Resources    NormalBrush �� 
StartPoint �� EndPoint � Color	 � Offset  HorizontalNormalBrush  
LightBrush  HorizontalLightBrush  	DarkBrush  PressedBrush  DisabledForegroundBrush
 �� Color  DisabledBackgroundBrush  WindowBackgroundBrush 	 SelectedBackgroundBrush 
 NormalBorderBrush  HorizontalNormalBorderBrush  DefaultedBorderBrush  PressedBorderBrush  DisabledBorderBrush  SolidBorderBrush  LightBorderBrush  
GlyphBrush  LightColorBrush  ScrollBarLineButton �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Path �� RelativeSource �� Triggers P� Property P� Value �� 
TargetName  ScrollBarPageButton  ScrollBarThumb  VerticalScrollBar � RowDefinitions ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton  HorizontalScrollBar � ColumnDefinitions �� Triggers*  $System.Windows.Controls.DataGridCell)  #System.Windows.Controls.DataGridRow �� 	Resources �� Binding �� Value   ComboBoxToggleButtonTemplate  ComboBoxTextBoxTemplate  ComboBoxTemplateData �� 	ClickMode  d� 	IsChecked! �� Mode" �� SelectionBoxItemTemplate# �� SelectionBoxItem$ J� IsHitTestVisible2  ,System.Windows.Markup.XmlAttributeProperties%  XmlSpace& �� 
IsReadOnly' � AllowsTransparency( �� IsDropDownOpen) � ActualWidth* �� MaxDropDownHeight+ J� SnapsToDevicePixels-  'System.Windows.Input.KeyboardNavigation,   DirectionalNavigation- P� 
SourceName  NewFocusGlow. :� RoutedEvent/ g� 	BeginTime0 �� TargetProperty1 �� 
TargetName2 �� KeyTime3 �� Value  ColumnHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader#   BOBY_Shulack.SkillsCollection  PersonCollection  PersonCollectionViewSource4 �� Source��%5"  &	        &	 V     &	 �     &	     &	 b    &	 �    &	 B    &	 R    &	 b    &		 r    &	
 �    &	 �    &	 B    &	 �    &	 �    &	 �    &	     &	     &	 .    &	 >    7   7 
 7  7  7  &	 �    &	 v    &	 >    7  7  7  7  7  &	 0    7  7  7  7   7 
 7  7  '�� �	    7  7  '  +    '  c    &	     &	 �    &	 �    7  7  '�� �    7  7  &	 *    &	 �    &	 h     &	 m     7  �� $	 0,0&�$	 0,1&���� � $
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
 	#88FFFFFF��(��       * SystemColors.HighlightBrushKey+)�� $
 Black��(��       *$"SystemColors.HighlightTextBrushKey+)�� $
 	#88FFFFFF��(��       *SystemColors.ControlBrushKey+)�� $
 Black��(��       *" SystemColors.ControlTextBrushKey+) ��  �� *Type+ Item.a���  � �� �	#FFFFFF00��  �� *Type+ OnFight.a���  � �� �	#FFFF7777��  �� *Type+ OffFight.a���  � �� �	#FF00FFFF��  �� *Type+ AllTime.a���  � �� �	#FFFF77FFP�  �@J�IsMouseOver . . ���  � �� �	#11FFFFFF.���  � �� �	#77000000��  �@=�ErrorTemplate Z� �� " d�.R��  �� �� $��16 �.+��� $��Border�����	#88FFFFFF$��1,1,1,1q�$ 3��
������ $��2���� $��1,1,1,1q����	#88FFFFFF
������ $��0��F� $��Arrow��$��0,0,3,0q�"����        � 	=  	= � z     	$��Center=�
�������$��Center=�$��8��$��1�� P�  �@J�IsMouseOver . . ��� $ Border�� � �� �	#66FFFFFFP�  �@d�	IsChecked$
 TrueY�. ��� $ Border�� � ��
 ���� �� $ Arrow�� � �� �	#FF8D979EP�  � {� .  . ��� $ Border�� � ��
 ������� $ Border�� � �� �	#66FFFFFF��  � ��
 ������� $ Arrow�� � �� �	#66FFFFFF�� " ��.R��� $��PART_ContentHost��
�������|�.  �� " �� ���� $
 	#55000000��(��       * SystemColors.HighlightBrushKey+)�� $
 	#55000000��(��       *SystemColors.ControlBrushKey+)�� $
 White��(��       *" SystemColors.ControlTextBrushKey+)�� $
 White��(��       *$"SystemColors.HighlightTextBrushKey+).���  � ��
 �   ���  � ��$ 17����  �@�OverridesDefaultStyle . ��  � �� �� " ��.R�� .+�d� $ Press=�
����� �|�.  8��    �� $ IsDropDownOpen� �� *TemplatedParent+$! TwoWay=��� $��ContentSite��$��3,0,23,3q�$
��Left=�$��Center=�#��z" #��z# $ .  �� % preserve$��PART_EditableTextBox��$��3,3,23,3q�$y�Hidden=�$
��Left=�
������ $��Center=�& .  |�. 8�� ��Z� � $��
PART_Popup��$��Bottom=�|�.  ' . #��z( $
��None=�.��� $��DropDown��#��z) #��z* + . .+��� $��DropDownBorder��$��1q�$ 4������ $	 0,0&�$	 1,1&�.��� $ 	#FFFFFFFF��$		 0.0Q�� $ 	#DDFFFFFF��$
	 0.50Q�� $ 	#FFFFFFFF��$		 1.0Q����� $	 0,0&�$	 1,1&�.��� $ 	#88000000��$		 0.0Q�� $ 	#FF000000��$
	 0.50Q�� $ 	#88000000��$		 1.0Q��� $��Disabled=�+ . $��1,2,1,1q�.���� $, 	Contained=� P�  �@��HasItems .  . ��� $ DropDownBorder�� � ��$ 95��P�  � {� .  . ���  � ��
 �   �P�  �@��
IsGrouping . . ���  � �� .  P� $- 
PART_Popup�� �@�AllowsTransparency . . ��� $ DropDownBorder�� �@��Padding$ 0,3,0,3q��� $ DropDownBorder�� � ��$ 0,2,0,0q�P�  �@��
IsEditable . . ���  �@ 	IsTabStop .  �� $ PART_EditableTextBox�� � y�$ Visible=��� $ ContentSite�� � y�$ Hidden=��� " �� ���� $
 	#55FFFFFF��(��       * SystemColors.HighlightBrushKey+)�� $
 	#55FFFFFF��(��       *SystemColors.ControlBrushKey+)�� $
 White��(��       *" SystemColors.ControlTextBrushKey+)�� $
 White��(��       *$"SystemColors.HighlightTextBrushKey+).���  �@�OverridesDefaultStyle . ��  � �� �� " ��.R�� .+�d� $ Press=�
����� �|�.  8��    �� $ IsDropDownOpen� �� *TemplatedParent+$! TwoWay=��� $��ContentSite��$��3,1,23,3q�$
��Left=�$��Center=�#��z" #��z# $ .  �� % preserve$��PART_EditableTextBox��$��3,3,23,3q�$y�Hidden=�$
��Left=�
������ $��Center=�& .  |�. 8�� ��Z� � $��
PART_Popup��$��Bottom=�|�.  ' . #��z( $
��Fade=�.��� $��DropDown��#��z) #��z* + . .+��� $��DropDownBorder��$��1q�$ 4������ $	 0,0&�$	 1,1&�.��� $ 	#FF555555��$		 0.0Q�� $ 	#DD222222��$
	 0.50Q�� $ 	#FF555555��$		 1.0Q����� $	 0,0&�$	 1,1&�.��� $ 	#88DDDDDD��$		 0.0Q�� $ 	#FFDDDDDD��$
	 0.50Q�� $ 	#88DDDDDD��$		 1.0Q��� $��Disabled=�+ . $��1,2,1,1q�.���� $, 	Contained=� P�  �@��HasItems .  . ��� $ DropDownBorder�� � ��$ 95��P�  � {� .  . ���  � ��
 �   �P�  �@��
IsGrouping . . ���  � �� .  P� $- 
PART_Popup�� �@�AllowsTransparency . . ��� $ DropDownBorder�� �@��Padding$ 0,3,0,3q��� $ DropDownBorder�� � ��$ 0,2,0,0q�P�  �@��
IsEditable . . ���  �@ 	IsTabStop .  �� $ PART_EditableTextBox�� � y�$ Visible=��� $ ContentSite�� � y�$ Hidden=��� " �� :� $. UIElement.MouseEnter��.[��� .���� .���� $/ 00:00:00X�$20 ,(Border.BorderBrush).(SolidColorBrush.Color)�1 Z� .g��� $2 
00:00:00.1��3 #CCCC00:� $. UIElement.MouseLeave��.[��� .���� .���� $/ 00:00:00X�$20 ,(Border.BorderBrush).(SolidColorBrush.Color)�1 Z� .g��� $2 
00:00:00.1��3 Transparent�� "  .���  � ��$ 15����  � �� �#000��  � �� �#FFF��  � ��$ 10(���  �@ SeparatorVisibility$ Visible=���  �@ SeparatorBrush �#FFF��  � ��$ Center=�  �� 84   .��� .+��� $$��Image_Windows\Win_Top_Left.png��$	��Top=�$
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
��24,0q��� -   $��label��$��BOBY Skills List��$��16(�$��Microsoft Tai Le)�$
��Bold"�
�������$	��Top=�$
��Left=�$
��12,7q�$��28��5 J� Effect5 3  -System.Windows.Media.Effects.DropShadowEffect  6   ShadowDepth$6 0Q�7   Color$7 White��8   Opacity$	8 0.5Q�9   
BlurRadius$9 10Q�&   System.Windows.Controls.DataGrid  -   $��dataGrid��$	��Top=�$
��Left=�$��	11,34,0,0q�
������ :   AutoGenerateColumns: .  ;   CanUserReorderColumns; .  <   CanUserResizeColumns< .  =   CanUserResizeRows= .  >   CanUserSortColumns> .  ?   CanUserAddRows? .  @   CanUserDeleteRows@ .  A   RowDetailsVisibilityMode$A 	Collapsed=�B   VerticalGridLinesBrushB �	#88FFFFFFC   HorizontalGridLinesBrushC �	#88FFFFFFD   SelectionMode$D Single=�E   RowHeaderWidth$E 0Q�$	��216��$	��378��F   ColumnHeaderStyle#F [ ���	#88FFFFFF���	#FFFFFFFF$��Disabled=�$��Visible=����� G �� Source#G [ H   ColumnsH 0	  *System.Windows.Controls.DataGridTextColumn	  ,
  &System.Windows.Controls.DataGridColumnI 
  Header$
I Name��J 
  Width5  /System.Windows.Controls.DataGridLengthConverter$	J 210 1  +System.Windows.Controls.DataGridBoundColumnK   BindingK �� *Name+	  $	I HP%��$J 50 K �� *HP+	  $	I MP%��$J 50 K �� *MP+4  .System.Windows.Controls.DataGridComboBoxColumn  $
I Type��-   comboboxColumn��$J 56 L   EditingElementStyle#L [ M   SelectedValueBindingM �� *Type+N   ElementStyleN �� " ��.���  � ��$ 2,0,0,0q���  � ��$ Center=��� $	��Top=�$
��Left=�$��
12,251,0,0q�O �� Orientation$O 
Horizontal=�.+��� -   $
��Bold"�$��89��  Style_Aion_Button#��[ .��+�� -   $
��Bold"�$��89��#��[ .��X�� -   $��89��#��[ .��▲�� -   $��89��#��[ .��▼�� -   $��listview_Skill��$��Disabled=�$��Visible=�$
��Left=�$	��Top=����	#88FFFFFF$��1q�P �� SelectionMode$P Single=�$��
11,278,0,0q�
�������
������ $	��188��$	��110���� -	   $��listview_Item��$��Disabled=�$��Visible=�$
��Left=�$	��Top=����	#88FFFFFF$��1q�$P Single=�$��201,278,0,0q�
�������
������ $	��188��$	��110��