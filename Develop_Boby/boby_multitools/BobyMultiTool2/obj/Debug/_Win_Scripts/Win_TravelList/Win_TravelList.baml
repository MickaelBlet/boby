   M S B A M L   `   `   `  ����Q  Mboby_multitools, Version=2.0.5794.10237, Culture=neutral, PublicKeyToken=null%clr-namespace:MyControl	MyControl  5   	   /clr-namespace:BobyMultitoolsBobyMultitools  5   	   #    BobyMultitools.Win_TravelList   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,Controlsclr-namespace:MyControl     5   	   .localclr-namespace:BobyMultitools     5   	     � Title$  Setting��5      $	��350��5      $	��422��5       � WindowStartupLocation$ CenterScreen=�5	       � Topmost .  � WindowStyle$
 None=�5      
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5       � ShowInTaskbar .   � 	Resources 5      *  $System.Windows.Controls.DataGridCell �� 
TargetType	 �� Property
 �� Value)  #System.Windows.Controls.DataGridRow �� 
TargetType ��Name J� SnapsToDevicePixels?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid � ColumnDefinitions � RowDefinitions?  9System.Windows.Controls.Primitives.DataGridCellsPresenter   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter   DetailsVisibility$   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical �� ConverterParameter&   System.Windows.Controls.DataGrid#   RowDetailsScrollingConverter �� 	Converter �� RelativeSource �� AncestorType:  4System.Windows.Controls.Primitives.DataGridRowHeader7	  1System.Windows.Controls.DataGridHeadersVisibility
 	  Row!   HeadersVisibilityConverter �� Triggers P� Property P� Value �� 
TargetName=
  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Padding  �Name! �� RadiusX" �� RadiusY# �� EndPoint$ �� 
StartPoint% � Color& � Offset' ��Name%   BobyMultitools.TravelCollection   TravelCollection  TravelCollectionViewSource( �� Source��%W
  '        '  �     '
  /    '�     &	  �	    &	 �	    7   �� 5   
   "  6   .�5      �� 	 � ��$
 0q�60   �� 5      	 � ��

 ���� 6+   �� 5      	 � ��$
 20��6*   �� 5      	 � ��$
 26��6'   �� 5      	 � ��

 �����6+   5      �� 5   
   "  6   .�5      �� 	 � ��$
 0q�60   �� 5      	 � ��

 ���� 6+   �� 5      	 � ��

 �����6+   �� 5      	 � ��$
 26��6'   �� 5      	 � ��
 5      �� 5       "  6&   .R�5!      �� $ 
DGR_Border��6!   
������ #��z��5"      #��z��5#       . ._�5%        $��bg��65    5&   "   �� 5'   &   $
��Auto �67   �� 5(   &   $��* �67    5*   "   �� 5+   &   $��* �64   �� 5,   &   $
��Auto �64   .+�5.   "     $��1��69   #��z 5/      # z 50        51   "   $��1��6;   $��1��52      #y�z 58       53      �� *AreRowDetailsFrozen+# Z # Z  �� "  58        59   "   $��2��64   $ Vertical=�5:      y�5;      �� *HeadersVisibility+# Z # Z  �� "  5?      5@       5A      P� 5B       �@J�IsMouseOver . . �5C   "   �� $ 
DGR_Border��6?   	 � ��
 �	#2255FFFF6W   5D      P� 5E       �@ 
IsSelected . . �5F   "   �� $ 
DGR_Border��6?   	 � ��
 �	#4455FFFF6W   5G      5I      5K      5L      �� 5N   
   " 
 5O      .�5P      �� 	 � ��
 �	#000000006+   �� 5Q      	 � ��
 �	#AAFFFFFF6+   �� 5R      	 � ��$
 26��6'   �� 5S      	 � ��
 5T      �� 5U      " 
 6&   .R�5V      � .+�5W      �� $��HeaderBorder��6%   $��0q�5X      $ 2,0,2,0q�5Y      ._�5Z   "   �� $��HeaderContent��63   
�������$��Center=�5\   3   $��Center=�5]   3   $��0,0,0,1q�5^   3   5_      5`      5a      5c      5d      �� 5e   
   " �6   .�5f      �� 	 � ��
 5g      �� 5h      " �6&   .R�5i      � $  
PART_Track��6   .+�5j      �� #��z��6(   #��z��6J   $
��Left=�6j   $! 2��6�   $" 2��6�   ��5k   "   �� 5l   &   $	# 0,1&�6:   $	$ 1,0&�6I   .��5m   *   � $% 	#44000000��67   $& 0Q�6I   � 5n   *   $% 	#00000000��67   $	& 0.5Q�6I   � 5o   *   $% 	#44000000��67   $& 1Q�6I   5p   '   5r      �� 5s      $' PART_Indicator��6(   $
��Left=�6@   $! 2��6[   $" 2��6g   ��5t   "   �� 5u   &   $# 0.5,1&�6:   $$ 0.5,0&�6K   .��5v   *   � $% 	#9900A599��67   $& 0Q�6I   � 5w   *   $% 	#9900A599��67   $& 1Q�6I   5x   '   5z      5{      5|      5~      5        5�   
   5�      �� 5�   
   8(   5�      5�      .��5�      � .+�5�   
      MyControl.Windows  )   Title$) Travel list��6   � 5�   
   $��0,0,5,0q�6   $��Right=�5�      $	��Top=�5�      .+�5�      �� * �� Orientation$* 
Horizontal=�6   .+�5�      �� -   5        $	��new��5�        Style_File_New_Button#��[ 6$   6g   �� 5�      -   5        $
��save��5�        Style_File_Save_Button#��[ 6%   6j   �� 5�      -   5        $��tFile��5�      $	��Top=�5�      $
��Left=�5�      $	��200��5�      $��24��5�      $��1,2,2,0q�5�      
�������5�   f   �� 5�      -   5        $��bt_Close��5�   	   ��.    WindowButton.Close.Blue#��[ 5�   	   $��35��5�      $��-1,0,0,0q�5�   	   5�      5�      5�      � 5�   
   $��1,28,1,1q�6   .+�5�      �� .+�5�      � $	��262��6   .+�5�        -   5        $��dgTravel��5�      $	��Top=�5�      $��Stretch=�5�      $	��262��5�      $��0,0,0,0q�5�      +   AutoGenerateColumns+ .  ,   CanUserReorderColumns, .  -   CanUserResizeColumns- .  .   CanUserResizeRows. .  /   CanUserSortColumns/ .  0   CanUserAddRows0 .  1   CanUserDeleteRows1 .  2   RowDetailsVisibilityMode$2 	Collapsed=�5�      3   VerticalGridLinesBrush3 �	#00FFFFFF4   HorizontalGridLinesBrush4 �	#00FFFFFF5   SelectionMode$5 Single=�5�      6   RowHeaderWidth$6 0Q�5�      ���	#00FFFFFF$��Disabled=�5�      $��Visible=�5�      ��5�      �� 7 �� Source#7 [ ��5�      L� 5�      8 j� TileMode$
8 Tile=�6+   9 j� Stretch$
9 Fill=�6<   : j� Viewport$: 0,0,1,52�6K   ; j� ViewportUnits$; Absolute=�6_   < L� Drawing< 5�   "   I� 5�   &   .��5�   *   � = � Brush= �	#55000000> � Geometry> 5�   .   �� 5�   2   ? �� Rect$? 0,0,1,1�6D   5�   +   � 5�   *   = �	#55555555> 5�   .   �� 5�   2   $? 0,1,1,1�6D   5�   +   5�   '   5�      @   Columns@ 5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      ,  &System.Windows.Controls.DataGridColumnA   Header$
A Name��5�   !   B   Width5  /System.Windows.Controls.DataGridLengthConverter$B 50 6/   C   CellTemplateC 5�   "   �� 5�   &   .R�5�   *   �� $��50��62   D �� TextAlignment$
D Left=�6=   ��6R   �� *NAME+E �� UpdateSourceTrigger$E PropertyChanged=�5�   '   5�        5�      $A X��5�   !   $B 60 6,   C 5�   "   �� 5�   &   .R�5�   *   �� -   5        $��60��5�   2   $
D Left=�6=   ��6R   �� *X+F �� StringFormat$F F2��6�   5�   '   5�        5�      $A Y��5�   !   $B 60 6,   C 5�   "   �� 5�   &   .R�5�   *   �� -   5        $��60��5�   2   $
D Left=�6=   ��6R   �� *Y+$F F2��6�   5�   '   5�        5�      $A Z��5�   !   $B 60 6,   C 5�   "   �� 5�   &   .R�5�   *   �� -   5        $��60��5�   2   $
D Left=�6=   ��6R   �� *Z+$F F2��6�   5�   '   5�        5�      $	A Fly��5�   %   $B 30 5�   %   C 5�   "   �� 5�   &   .R�5�   *   �� $��30��63   $��	-9,0,-5,0q�6>   G d� 	IsCheckedG 6R   �� *FLY+$E PropertyChanged=�5�   '   5�        5�      $
A Type��5�   %   $B 60 5�   %   C 5�   "   �� 5�   &   .R�5�   *   �� $��60��63   $��	-9,0,-5,0q�6>   H �� TextH 6Q   �� *TYPE+$E PropertyChanged=�.@�5�   .   �� $
��None��6;   �� 5�   .   $��Action��6;   �� 5�   .   $
��Rest��6;   5   +   5  '   5       5     $A Comment��5  %   $B 87 5  %   C 5  "   �� 5  &   .R�5	  *   �� $* 
Horizontal=�65   .+�5
  .   �� $��87��66   $
D Left=�6A   ��6V   �� *	COMMENT+$E PropertyChanged=�5  +   5  '   5     5     �� 5     -	   5        $��img_ShugoLoading��5     $��Center=�5  
   $��Center=�5  
   I J� Opacity$I 1Q�5     $	��115��5     J J� EffectJ 5     3  -System.Windows.Media.Effects.DropShadowEffect  5     K   ShadowDepth$K 0Q�5     L   Color$L 	#FFFFFFFF��5     M   Opacity$M 1Q�5     N   
BlurRadius$N 10Q�5     5     � 5     -
   5        $��	pgLoading��5  "   $��Bottom=�63   $��2q�6N   $��5��6Y   6f   5      A� 5!     O A� Columns$O 5��6   $��0,2,0,1q�6*   .+�5"     �� -   5        $��btRecord��5"     $��1,0,1,0q�6-   .��6U   Record6]   �� 5#     $��1,0,1,0q�6   ���#EEE$��Right=�6?   .��6b   Default Type :6r   �� 5$     -   5        $��cDefaultType��5$  T   $	��Top=�6   $��26��67   $��1,0,1,0q�6C   
�������$
H None��6{   .@�5%     �� $
��None��6'   �� 5&     $��Action��6'   �� 5'     $
��Rest��6'   5(     A� 5)     $O 2��6"   $��0,0,0,0q�6.   .+�5*     �� -   5        $��btAdd��5*  !   $��1,0,1,0q�6.   $��18(�6S   .��6a   +6d   �� 5+     -   5        $��btRemove��5+  !   $��1,0,1,0q�61   $��18(�6Y   .��6g   -6j   5,     A� 5-     $O 2��6"   $��0,0,0,0q�6.   .+�5.     �� -   5        $
��btUp��5.  !   $��1,0,1,0q�6-   .��6Q   ▲6T   �� 5/     -   5        $��btDown��5/  !   $��1,0,1,0q�6/   .��6U   ▼6X   50     51     A� 52     $O 4��6   $��0,1,0,2q�6*   .+�53     �� -   5        $��btRun��53     $��1,0,1,0q�6*   .��6O   
Test Run6Y   �� 54     -   5        $��	btFullRun��54     $��1,0,1,0q�6.   .��6W   Test Full Run6f   �� 55     $��1,0,1,0q�6   ���#EEE$��Right=�6?   .��6b   Travel Path :6q   �� 56     -   5        $��cTravelPath��56  T   $	��Top=�6   $��26��67   $��1,0,1,0q�6C   
�������$H Circular��6z   .@�57     �� $��Circular��6'   �� 58     $��Reverse��6'   59     5:     5;     5<     5=     5>     