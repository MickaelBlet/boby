   M S B A M L   `   `   `  ����Q  Mboby_multitools, Version=2.0.5787.35279, Culture=neutral, PublicKeyToken=null%clr-namespace:MyControl	MyControl  5   	   /clr-namespace:BobyMultitoolsBobyMultitools  5   	       BobyMultitools.Win_Script   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,Controlsclr-namespace:MyControl     5   	   .localclr-namespace:BobyMultitools     5   	     � Title$  Script��5      $	��287��5      $	��275��5       � WindowStartupLocation$ Manual=�5	       � Topmost .  � WindowStyle$
 None=�5      
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5       � ShowInTaskbar .   � 	Resources 5      %   BobyMultitools.ScriptCollection   ScriptCollection  ScriptViewSource �� Source��%K   &	        &	      7     5   
   5      �� 5   
   8   5      5      .��5      � .+�5   
      MyControl.Windows  	   Title$	 scripts��6   � 5   
   $��0,0,5,0q�6   $��Right=�5      $	��Top=�5      .+�5      �� 
 �� Orientation$
 
Horizontal=�6   .+�5      �� -   5        $��
bt_Setting��5       ��.    WindowButton.Setting#��[ 5"      $��Right=�5#      $	��Top=�5$      $��27��5%      $��2,0,0,0q�5&      5'      �� 5(      -   5        $��bt_Close��5)   	   ��.    WindowButton.Close#��[ 5+   	   $��35��5,      $��-1,0,0,0q�5-   	   5/      50      51      � 52   
   $��1,28,1,1q�6   .+�53      �� .+�54      �� -   5        $��console��54       �� TextWrapping$ WrapWithOverflow=�6)   $	��150��6I   $��2q�6V    �� AcceptsReturn .  �� 
IsReadOnly . $��11(�6�   $��Visible=�6�   6�   �� 55      -   5        $��lb_Buff��55      $��0,0,0,0q�56      
������ ���	#00FFFFFF$��Stretch=�59      $��Disabled=�5:      $��Visible=�5;      $	��105��5=      ��5<      ��  �� Source# [ ��5>      �� 5?      .R�5@      �  � Orientation$ Vertical=�6(   5A       5C       �� 
TargetType �� Property �� Value �� 
TargetType �� Text  Style_Buff_Play_Button��%�  '��       7  7  �� 5D      " ��6    .�5E      ��  � ��$ Stretch=�6K   �� 5F       � ��$ 24��67   �� 5G       � ��$ 1q�68   �� 5H       � ��$ 0q�67   �� 5I       � ��$ 0q�6@   �� 5J       � �� 5K   "   �� 5L   &   " ��5M      .R�5N   *   �� $
 
Horizontal=�65   .+�5O   .   �� -   5        $	��Top=�5O   7   $��Right=�5P   1   $	��214��5R   7   $��22��5S   1   $��0,0,0,0q�5T   1   
�������{�5Q   7   �� *COMBO_ENABLE+��5V   7   �� *COMBO+ 5W   7   �� *FILE+5X   1   �� 5Y   .   $��bt_Edit��65   -   5        ��.  8��  $
��Left=�5\   )   $	��Top=�5]   )   $��22��5^   5   $��22��5_   5   $��0,0,0,0q�5`   )   5a   )   �� 5b   .   $��bt_Play��65   -   5        ��.  8�� $
��Left=�5e   )   $	��Top=�5f   )   $��22��5g   5   $��22��5h   5   $��0,0,0,0q�5i   )   5j   )   5k   +   5l   '   5n      5o      5p      5q      5r      5s      5t      5u      