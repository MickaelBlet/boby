   M S B A M L   `   `   `  ����Q  Mboby_multitools, Version=2.0.5794.10237, Culture=neutral, PublicKeyToken=null%clr-namespace:MyControl	MyControl  5   	   /clr-namespace:BobyMultitoolsBobyMultitools  5   	       BobyMultitools.Win_Script   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,Controlsclr-namespace:MyControl     5   	   .localclr-namespace:BobyMultitools     5   	   -   5          � Title$  Script��5      $	��297��5      $	��275��5       � WindowStartupLocation$ Manual=�5	       � Topmost .  � WindowStyle$
 None=�5      
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5       � ShowInTaskbar .   � 	Resources 5      %   BobyMultitools.ScriptCollection   ScriptCollection  ScriptViewSource �� Source��%K   &	        &	      7     5   
   5      �� 5   
   8   5      5      .��5      � .+�5   
      MyControl.Windows  	   Title$	 scripts��6   � 5   
   $��0,0,5,0q�6   $��Right=�5      $	��Top=�5      .+�5      �� 
 �� Orientation$
 
Horizontal=�6   .+�5       �� -   5        $��	bt_Travel��5!   	   ��.    WindowButton.Setting#��[ 5#   	   $��Right=�5$   	   $	��Top=�5%   	   $��27��5&      $��2,0,0,0q�5'   	   5(   	   �� 5)      -   5        $��
bt_Ability��5*   	   ��.  #��[ 5,   	   $��Right=�5-   	   $	��Top=�5.   	   $��27��5/      $��2,0,0,0q�50   	   51   	   �� 52      -   5        $��
bt_Setting��53   	   ��.  #��[ 55   	   $��Right=�56   	   $	��Top=�57   	   $��27��58      $��2,0,0,0q�59   	   5:   	   �� 5;      -   5        $��bt_Close��5<   	   ��.    WindowButton.Close#��[ 5>   	   $��35��5?      $��-1,0,0,0q�5@   	   5B      5C      5D      � 5E   
   $��1,28,1,1q�6   .+�5F      �� .+�5G      �� -   5        $��console��5G       �� TextWrapping$ WrapWithOverflow=�6)   $	��150��6I   $��2q�6V    �� AcceptsReturn .  �� 
IsReadOnly . $��11(�6�   $��Visible=�6�   6�   �� 5H      -   5        $��lb_Buff��5H      $��0,0,0,0q�5I      
������ ���	#00FFFFFF$��Stretch=�5L      $��Disabled=�5M      $��Visible=�5N      $��85��5P      ��5O      ��  �� Source# [ ��5Q      �� 5R      .R�5S      �  � Orientation$ Vertical=�6(   5T       5V       �� 
TargetType �� Property �� Value �� 
TargetType �� Text  Style_Buff_Play_Button��%�  '��       7  7  �� 5W      " ��6    .�5X      ��  � ��$ Stretch=�6K   �� 5Y       � ��$ 24��67   �� 5Z       � ��$ 1q�68   �� 5[       � ��$ 0q�67   �� 5\       � ��$ 0q�6@   �� 5]       � �� 5^   "   �� 5_   &   " ��5`      .R�5a   *   �� $
 
Horizontal=�65   .+�5b   .   �� -   5        $	��Top=�5b   7   $��Right=�5c   1   $	��214��5e   7   $��22��5f   1   $��0,0,0,0q�5g   1   
�������{�5d   7   �� *COMBO_ENABLE+��5i   7   �� *COMBO+ 5j   7   �� *FILE+5m   7   �� 5n   .   $��bt_Edit��65   -	   5        ��.  8��  $
��Left=�5q   )   $	��Top=�5r   )   $��22��5s   5   $��22��5t   5   $��0,0,0,0q�5u   )   5v   )   �� 5w   .   $��bt_Play��65   -
   5        ��.  8�� $
��Left=�5z   )   $	��Top=�5{   )   $��22��5|   5   $��22��5}   5   $��0,0,0,0q�5~   )   5   )   5�   +   5�   '   5�      5�      5�      5�      A� 5�       A� Columns$ 2��6   $��0,2,0,2q�6*   .+�5�      �� -   5        $��
add_script��5�      $��1,0,1,0q�6/   .��6Y   +6\   �� 5�      -   5        $��remove_script��5�      $��1,0,1,0q�62   .��6_   -6b   5�      5�      5�      5�      5�      