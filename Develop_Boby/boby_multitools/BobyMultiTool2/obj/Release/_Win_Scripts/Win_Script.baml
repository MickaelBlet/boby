   M S B A M L   `   `   `  ���� P  Lboby_multitools, Version=2.0.5796.7710, Culture=neutral, PublicKeyToken=null%clr-namespace:MyControl	MyControl  /clr-namespace:BobyMultitoolsBobyMultitools      BobyMultitools.Win_Script   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,Controlsclr-namespace:MyControl     .localclr-namespace:BobyMultitools     -     � Title$  Script��$	��297��$	��275�� � WindowStartupLocation$ Manual=� � Topmost .  � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � 	Resources %   BobyMultitools.ScriptCollection   ScriptCollection  ScriptViewSource �� Source��%'   &	        &	      7     �� 8   .��� .+�   MyControl.Windows  	   Title$	 scripts��� $��0,0,5,0q�$��Right=�$	��Top=�.+��� 
 �� Orientation$
 
Horizontal=�.+��� -   $��	bt_Travel����.    WindowButton.Setting#��[ $��Right=�$	��Top=�$��27��$��2,0,0,0q��� -   $��
bt_Ability����.  #��[ $��Right=�$	��Top=�$��27��$��2,0,0,0q��� -   $��
bt_Setting����.  #��[ $��Right=�$	��Top=�$��27��$��2,0,0,0q��� -   $��bt_Close����.    WindowButton.Close#��[ $��35��$��-1,0,0,0q�� $��1,28,1,1q�.+��� .+�� $��2,2,2,0q�.+��� 
�������$��22��$��2q�.��Travel File :�� $
��Left=�$��70,0,0,0q�$	��170���� -   $��bt_Travel_Edit����.    Style_Buff_Play_Button#��[ $��Right=�$	��Top=�$��22��$��22��$��0,0,2,0q��� -   $��console�� �� TextWrapping$ WrapWithOverflow=�$	��150��$��2q� �� AcceptsReturn .  �� 
IsReadOnly . $��11(�$��Visible=��� -   $��lb_Buff��$��0,0,0,0q�
������ ���	#00FFFFFF$��Stretch=�$��Disabled=�$��Visible=�$��60������  �� Source# [ ���� .R��  � Orientation$ Vertical=�  �� 
TargetType �� Property �� Value �� 
TargetType �� Text��%  '��       7  7  �� " ��.���  � ��$ Stretch=���  � ��$ 24����  � ��$ 1q���  � ��$ 0q���  � ��$ 0q���  � �� �� " ��.R��� $
 
Horizontal=�.+��� -	   $	��Top=�$��Right=�$	��214��$��22��$��0,0,0,0q�
�������{��� *COMBO_ENABLE+���� *COMBO+ �� *FILE+�� $��bt_Edit��-
   ��.  8��  $
��Left=�$	��Top=�$��22��$��22��$��0,0,0,0q��� $��bt_Play��-   ��.  8�� $
��Left=�$	��Top=�$��22��$��22��$��0,0,0,0q�A�  A� Columns$ 2��$��0,2,0,2q�.+��� -   $��
add_script��$��1,0,1,0q�.��+�� -   $��remove_script��$��1,0,1,0q�.��-