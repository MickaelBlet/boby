   M S B A M L   `   `   `  ����  Boby_Shulack    Boby_Shulack.Win_Main   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5          � Title$  BOBY_Bot��5      $	��198��5      $	��318��5       � WindowStartupLocation$ CenterScreen=�5       � Topmost .  � WindowStyle$
 None=�5
      
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5       +� FocusedElement 5      ��  �� ElementName$ lb_Game�� � 	Resources 5      	 �� 
TargetType
 �� Property �� Value �� 
TargetType �Name �� RadiusX �� RadiusY �� EndPoint �� 
StartPoint � Color � Offset ��Name��%�  '�       �� 5   
   "	 �6   .�5      �� 
 � �� 5      �� 5      " �6&   .R�5      � $ 
PART_Track��6   .+�5      �� #��z��6(   #��z��6J   $
��Left=�6j   $ 2��6�   $ 2��6�   ��5   "   �� 5   &   $	 0,1&�6:   $	 1,0&�6I   .��5   *   � $ 	#44000000��67   $ 0Q�6I   � 5   *   $ 	#00000000��67   $	 0.5Q�6I   � 5   *   $ 	#44000000��67   $ 1Q�6I   5   '   5      �� 5      $ PART_Indicator��6(   $
��Left=�6@   $ 2��6[   $ 2��6g   ��5    "   �� 5!   &   $ 0.5,1&�6:   $ 0.5,0&�6K   .��5"   *   � $ 	#9900A599��67   $ 0Q�6I   � 5#   *   $ 	#9900A599��67   $ 1Q�6I   5$   '   5&      5'      5(      5*      5+      5,      .��5-      � .+�5.   
   �� ��.     Style_Aion_Grid#��[  6+   � 5/   
   $��9,9,9,9q�6   .+�50      �� $��2,0,0,0q�51      $��BOBY Bot v1.0��52      $��16(�53      $
��Bold"�54      ���	#FFEFEDC4$	��Top=�56      $
��Left=�57      $��28��58      $��Center=�59      $	��Top=�5:       J� Effect 5;      3  -System.Windows.Media.Effects.DropShadowEffect  5<         ShadowDepth$ 0Q�5=         Color$ 	#FFEFEDC4��5>         Opacity$	 0.5Q�5?         
BlurRadius$ 10Q�5@      5B      �� 5C      -   5        $��rt_Title��5J      $��24��5E       �� Stretch$ UniformToFill=�5F      $��0,0,0,0q�5G      $	��Top=�5H      $
��Left=�5I       � Cursor$ SizeAll��5K      ��5L      �� 5M       �� Color$ Transparent��5N      5P      �� 5Q      -   5        $��bt_Close��5R      ��.    Style_Close_Button#��[ 5T      $��Right=�5U      $	��Top=�5V      $��0,0,4,0q�5W      5X      �� 5Y      -   5        $��bt_Minimise��5Z      ��.    Style_Minimise_Button#��[ 5\      $��Right=�5]      $	��Top=�5^      $��0,0,34,0q�5_      5`      5a      �� 5b   
   $��9,32,9,9q�6   .+�5c      ��  �� Orientation$ 
Horizontal=�6   .+�5d      � $	��294��6   $	��Top=�6#   $
��Left=�5e      $��3,0,3,0q�6    .+�5f      �� $	��Top=�6%   $
��Left=�5g      $��0,5,0,0q�6    ��.  $	��150��6C     BG1#��[ 6P   �� 5h      $	��Top=�6!   $
��Left=�5i      $��0,0,0,0q�6#   .+�5j      �� $ 
Horizontal=�6%   .+�5k      �� -   5        $��bt_Start��5k   %   $	��147��6P   $��0,7,0,0q�6\   .��6m   Start6t   �� 5l      -   5        $��
bt_Refresh��5l   %   $	��147��6Q   $��0,7,0,0q�6]   .��6n   	Refresh6w   5m      � 5n      .+�5o      � -   5        $��pg_ShugoLoading��5o   *   $��Center=�5q   *   $	��Top=�5r   *    J� Opacity$ 0Q�5s   *   $��0,90,0,0q�5t   *   $	��280��5u   *   $��10��5v   *   68   �� 5w      -   5        $��img_ShugoLoading��5x   !   $��Center=�5y      $��Center=�5z      $ 1Q�5{   !   $	��120��5|   !    5}   "     5~   &   $ 0Q�5      $ 	#FFFFFFFF��5�      $ 1Q�5�      $ 10Q�5�      5�      �� 5�      -	   5        $��lb_Game��5�      $
��Left=�5�      $	��Top=�5�      $��0,0,0,0q�5�      $	��294��5�      $	��100��5�      
������ ���	#00FFFFFF$��Hidden=�5�   !   $��13(�5�      5�      �� 5�      -
   5        $��l_Users��5�   !   $
��Left=�5�   !   $	��Top=�5�      $��	0,100,0,0q�5�      $	��300��5�      .��6   Online Users: 06)   5�      5�      5�      5�      5�      5�      5�      