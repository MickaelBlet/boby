   M S B A M L   `   `   `  ����N  Jboby_shulack, Version=1.0.5693.15142, Culture=neutral, PublicKeyToken=null+clr-namespace:BOBY_ShulackBOBY_Shulack  5   	       BOBY_Shulack.Win_Loot   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,localclr-namespace:BOBY_Shulack     5   	     � Title$  	BOBY_Loot��5   	    � Topmost .  � WindowStyle$
 None=�5   	   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5
   	   $	��495��5   	   $	��480��6    � 	Resources 5         NormalBrush �� 
StartPoint �� EndPoint � Color	 � Offset  HorizontalNormalBrush  
LightBrush  HorizontalLightBrush  	DarkBrush  PressedBrush  DisabledForegroundBrush
 �� Color  DisabledBackgroundBrush  WindowBackgroundBrush 	 SelectedBackgroundBrush 
 NormalBorderBrush  HorizontalNormalBorderBrush  DefaultedBorderBrush  PressedBorderBrush  DisabledBorderBrush  SolidBorderBrush  LightBorderBrush  
GlyphBrush  LightColorBrush  ScrollBarLineButton �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Path �� RelativeSource �� Triggers P� Property P� Value �� 
TargetName  ScrollBarPageButton  ScrollBarThumb  VerticalScrollBar � RowDefinitions ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton  HorizontalScrollBar � ColumnDefinitions �� Triggers*  $System.Windows.Controls.DataGridCell)  #System.Windows.Controls.DataGridRow �� 	Resources!   BOBY_Shulack.LootCollection  PersonCollection"   BOBY_Shulack.LootCollection2  PersonCollection2  PersonCollectionViewSource �� Source  PersonCollectionViewSource2��%�  &	        &	 �     &	 ^    &	     &	 �    &	 \    &	 `    &	 ~    &	 �    &		 �    &	
 �    &	 �    &	 @    &	 �    &	 �    &	 �    &	 �    &	 �    &	     &	 *    7   7 
 7  7  7  &	 �
    &	     &	 �    7  7  7  7  7  &	 V    7  7  7  7   7 
 7  7  '�� n    7  7  '  g    '  �    &	 �    &	     &	 +    7  &	 G    7  �� 5      $	 0,0&�6,   $	 0,1&�6=   ��5      � � 5      $
 #FFF��6   $		 0.0Q�6!   � 5      $
 #FFF��6   $		 1.0Q�6!   5      5      �� 5      $	 0,0&�66   $	 1,0&�6G   ��5      � � 5      $ 	#88FFFFFF��6   $		 0.0Q�6&   � 5      $ 	#88FFFFFF��6   $		 1.0Q�6&   5      5      �� 5!      $	 0,0&�6+   $	 0,1&�6<   ��5#      � � 5$      $
 #FFF��6   $		 0.0Q�6!   � 5%      $
 #EEE��6   $		 1.0Q�6!   5&      5(      �� 5*      $	 0,0&�65   $	 1,0&�6F   ��5,      � � 5-      $
 #FFF��6   $		 0.0Q�6!   � 5.      $
 #EEE��6   $		 1.0Q�6!   5/      51      �� 53      $	 0,0&�6*   $	 0,1&�6;   ��55      � � 56      $
 #FFF��6   $		 0.0Q�6!   � 57      $
 #AAA��6   $		 1.0Q�6!   58      5:      �� 5<      $	 0,0&�6-   $	 0,1&�6>   ��5>      � � 5?      $
 #BBB��6   $		 0.0Q�6!   � 5@      $
 #EEE��6   $		 0.1Q�6!   � 5A      $
 #EEE��6   $		 0.9Q�6!   � 5B      $
 #FFF��6   $		 1.0Q�6!   5C      5E      �� 5G      $

 #888��64   �� 5I      $

 #EEE��64   �� 5K      $

 #000��62   �� 5M      $

 #DDD��64   �� 5Q      $	 0,0&�62   $	 0,1&�6C   ��5S      � � 5T      $ 	#55FFFFFF��6   $		 0.0Q�6&   � 5U      $ 	#55FFFFFF��6   $		 1.0Q�6&   5V      5X      �� 5Z      $	 0,0&�6<   $	 1,0&�6M   ��5\      � � 5]      $ 	#55FFFFFF��6   $		 0.0Q�6&   � 5^      $ 	#55FFFFFF��6   $		 1.0Q�6&   5_      5a      �� 5c      $	 0,0&�65   $	 0,1&�6F   ��5e      � � 5f      $
 #777��6   $		 0.0Q�6!   � 5g      $
 #FFF��6   $		 1.0Q�6!   5h      5j      �� 5l      $	 0,0&�63   $	 0,1&�6D   ��5n      � � 5o      $
 #444��6   $		 0.0Q�6!   � 5p      $
 #888��6   $		 1.0Q�6!   5q      5s      �� 5u      $

 #000��60   �� 5w      $

 #888��6-   �� 5y      $

 #AAA��6-   �� 5|      $

 #FFF��6'   �� 5~      $

 #DDD��6,   �� 5�      " ��6&   .�5�      ��  �@J�SnapsToDevicePixels . 6+   �� 5�       �@�OverridesDefaultStyle . 6-   �� 5�       � |� .  6!   �� 5�       � �� 5�      �� 5�      " ��6   .R�5�      �� $��Border��5�      $��1q�5�      $ 2��5�      8��  8�� $��1q�5�      ._�5�   	   F� $��Center=�5�   	   $��Center=�5�   	   8�� ��5�   	   �� $ Content� �� *TemplatedParent+5�   	    5�      P� 5�   	    � �� . . �5�   
   �� $ Border��6   8   � ��6;   5�   
   P� 5�   	    � {� .  . �5�   
   �� 8   � ��6'   5�   
   5�      5�      5�      �� 5�      " ��6&   .�5�      ��  �@J�SnapsToDevicePixels . 6+   �� 5�       �@�OverridesDefaultStyle . 6-   �� 5�       � �� .  6!   �� 5�       � |� .  6!   �� 5�       � �� 5�      �� 5�      " ��6   .R�5�      �� 
������ 6   5�      5�      5�      �� 5�      " n�6!   .�5�      ��  �@J�SnapsToDevicePixels . 6+   �� 5�       �@�OverridesDefaultStyle . 6-   �� 5�       � �� .  6!   �� 5�       � |� .  6!   �� 5�       � �� 5�      �� 5�      " n�6   .R�5�      �� $ 4��5�      #��z��5�      #��z��5�      $��1q�5�      5�      5�      5�      �� 5�      " ��6.   .R�5�      �  5�      �� 5�      $��0.00001* �6   .+�5�      �� $��1��5�      $ 0��5�      
������ 5�      ]� 5�      $��
PART_Track��5�      $��1��5�      ��.  5�      �� 5�      8��  $��ScrollBar.PageUpCommand��5�       5�      n� 5�      8�� $��1,0,1,0q�5�      8�� 8�� 5�       5�      �� 5�      8�� $��ScrollBar.PageDownCommand��5�      5�      5�      5�      �� 5�      " ��60   .R�5�      �  5�      �� 5�      $��18��6   �� 5�      $��0.00001* �6   �� 5�      $��18��6   .+�5�      �� $��3��5�      $ 2��5�      ���#F0F0F05�      �� 5�      $��0��5�      8��  $��18��5�      $��ScrollBar.LineLeftCommand��5�      $��M 4 0 L 4 8 L 0 4 Z��5�      ]� 5�      $��
PART_Track��5�      $��1��5�      ��.   5�      �� 5�      8�� $��ScrollBar.PageLeftCommand��5�       5�      n� 5�      8�� $��0,1,0,1q�5      8�� 8�� 5      5     �� 5     8�� $ ��ScrollBar.PageRightCommand��5     5	     �� 5
     $��3��5     8�� $��18��5     $ ��ScrollBar.LineRightCommand��5     $��M 0 0 L 4 4 L 0 8 Z��5     5     5     �� 5     " ��6%   .�5     ��  �@J�SnapsToDevicePixels . 6+   �� 5      �@�OverridesDefaultStyle . 6-    5     P� 5      �@��Orientation$ 
Horizontal=�6%   . �5     ��  � ��$
 Auto��6   �� 5      � ��$ 18��6    �� 5      � ��8   6"   5     P� 5      �@��Orientation$ Vertical=�6%   . �5     ��  � ��$ 10��6   �� 5      � ��$
 Auto��6    �� 5      � ��8  6"   5      5"     �� 5#     "  6
   .�5$     ��  � ��$ 0q�6'   �� 5%      � ��$ 19��6!   5'     �� 5(     "  6
    5)     ���� 5*     $
 	#88BBFFFF��6H   (��       6   * SystemColors.HighlightBrushKey+)6H   �� 5+     $
 	#88BBFFFF��6F   (��       6   *SystemColors.ControlBrushKey+)6F   �� 5,     $
 Black��6J   (��       6   *" SystemColors.ControlTextBrushKey+)6J   �� 5-     $
 Black��6L   (��       6   *$"SystemColors.HighlightTextBrushKey+)6L   5.     .�5/     ��  � �� �	#770000006"   �� 50      � ��
 �����6"   �� 51      �@=�ErrorTemplate 60   Z� 52       54     55       56     57     �� 58     8   5:     �� 5;     8   5=     5>     .��5?     � .+�5A     �� $$��Image_Windows\Win_Top_Left.png��5B     $	��Top=�5C     $
��Left=�5D     $��24��5E     $��24��5F     $	��0,0q�5G     �� 5H     $%��Image_Windows\Win_Top_Right.png��5I     $	��Top=�5J     $��Right=�5K     $��24��5L     $��24��5M     $	��0,0q�5N     �� 5O     $$��Image_Windows\Win_Top_Fill.png��5P     $	��Top=�5Q     $��Right=�5R     $
��Fill=�5S     $��24��5T     $
��24,0q�5U     �� 5V     $%��Image_Windows\Win_Left_Fill.png��5W     $	��Top=�5X     $
��Left=�5Y     $
��Fill=�5Z     $��24��5[     $
��0,24q�5\     �� 5]     $'��!Image_Windows\Win_Center_Fill.png��5^     $	��Top=�5_     $
��Left=�5`     $
��Fill=�5a     $��24,24q�5b     �� 5c     $&�� Image_Windows\Win_Right_Fill.png��5d     $	��Top=�5e     $��Right=�5f     $
��Fill=�5g     $��24��5h     $
��0,24q�5i     �� 5j     $'��!Image_Windows\Win_Bottom_Left.png��5k     $��Bottom=�5l     $
��Left=�5m     $��24��5n     $��24��5o     $	��0,0q�5p     �� 5q     $(��"Image_Windows\Win_Bottom_Right.png��5r     $��Bottom=�5s     $��Right=�5t     $��24��5u     $��24��5v     $	��0,0q�5w     �� 5x     $'��!Image_Windows\Win_Bottom_Fill.png��5y     $��Bottom=�5z     $��Right=�5{     $
��Fill=�5|     $��24��5}     $
��24,0q�5~     �� 5�     -   5        $��label��5�     $��BOBY Recycle Loot��5�     $��16(�5�     $��Microsoft Tai Le)�5�     $
��Bold"�5�     
�������$	��Top=�5�     $
��Left=�5�     $
��12,7q�5�     $��28��5�      J� Effect 5�     3  -System.Windows.Media.Effects.DropShadowEffect  5�        ShadowDepth$ 0Q�6       Color$  White��5�     !   Opacity$	! 0.5Q�5�     "   
BlurRadius$" 10Q�5�     5�     �� 5�     -   5        $��label2��5�     $��Recycle Loot Full��5�     $��16(�5�     $��Microsoft Tai Le)�5�     $
��Bold"�5�     
�������$	��Top=�5�     $
��Left=�5�     $��	242,7,0,0q�5�     $��28��5�      5�       5�     $ 0Q�6   $  White��5�     $	! 0.5Q�5�     $" 10Q�5�     5�     &   System.Windows.Controls.DataGrid  5�     -   5        $��dataGrid��5�     $	��Top=�5�     $
��Left=�5�     $��	11,34,0,0q�5�     
������ #   AutoGenerateColumns# .  $   CanUserReorderColumns$ .  %   CanUserResizeColumns% .  &   CanUserResizeRows& .  '   CanUserSortColumns' .  (   CanUserAddRows( .  )   CanUserDeleteRows) .  *   RowDetailsVisibilityMode$* 	Collapsed=�5�     +   VerticalGridLinesBrush+ �	#88FFFFFF,   HorizontalGridLinesBrush, �	#88FFFFFF-   SelectionMode$- Single=�5�     .   RowHeaderWidth$. 0Q�5�     $	��271��5�     $	��228��5�     /   ColumnHeaderHeight$/ 0Q�5�     ���	#88FFFFFF$��Disabled=�5�     $��Visible=�5�     ��5�     �� 0 �� Source#0 [ 1   Columns1 5�     0  *System.Windows.Controls.DataGridTextColumn  5�     ,  &System.Windows.Controls.DataGridColumn2   Header$
2 Name��5�     3   Width5	  /System.Windows.Controls.DataGridLengthConverter$	3 216	 5�     1
  +System.Windows.Controls.DataGridBoundColumn4 
  Binding4 5�     �� *Name+5�     5�     �� 5�     $	��Top=�6   $
��Left=�5�     $��
12,306,0,0q�5�     5 �� Orientation$5 
Horizontal=�6#   .+�5�     �� -   5        $
��Bold"�5�     $��57��6     Style_Aion_Button#��[ 5�     .��5�     +6"   �� 5�     -   5        $
��Bold"�5�     $��57��5�     #��[ 6   .��5�     X6"   �� 5�     -   5        $��57��5�     #��[ 6   .��5�     ▲6!   �� 5�     -   5        $��57��5�     #��[ 6   .��5�      ▼6#   5�     �� 5�     -   5        $��listview_Item��5�     $��Disabled=�5�     $��Visible=�5�     $
��Left=�5�     $	��Top=�5�     ���	#88FFFFFF$��1q�5�     6 �� SelectionMode$6 Single=�5�     $��
11,334,0,0q�5�     
�������
������ $	��228��5�     $	��150��5�     5�       5�     -	   5        $��	dataGrid2��5�     $	��Top=�5�     $
��Left=�5�     $��
241,34,0,0q�5�     
������ # .  $ .  % .  & .  ' .  ( .  ) .  $* 	Collapsed=�5�     + �	#88FFFFFF, �	#88FFFFFF$- Single=�5�     $. 0Q�5�     $	��271��5�     $	��228��5�     $/ 0Q�5�     ���	#88FFFFFF$��Disabled=�5�     $��Visible=�5�     ��5�     �� #0 [ 1 5�       5�     $
2 Name��5�     $	3 216	 5     4 5      �� *Name+5     5     �� 5     $	��Top=�6   $
��Left=�5     $��242,306,0,0q�5     $5 
Horizontal=�6$   .+�5     �� -
   5        $
��Bold"�5     $��57��6   #��[ 5	     .��5
      +6#   �� 5     -   5        $
��Bold"�5     $��57��5     #��[ 6   .��5      X6#   �� 5     -   5        $��57��5     #��[ 6   .��5     ▲6"   �� 5     -   5        $��57��5     #��[ 6   .��5  !   ▼6$   5     �� 5     -   5        $��listview_Item2��5     $��Disabled=�5     $��Visible=�5     $
��Left=�5     $	��Top=�5     ���	#88FFFFFF$��1q�5     $6 Single=�5     $��241,334,0,0q�5     
�������
������ $	��228��5     $	��150��5      5!     5"     5#     