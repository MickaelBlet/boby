   M S B A M L   `   `   `  ���� P  Lboby_multitools, Version=2.0.5796.7710, Culture=neutral, PublicKeyToken=null%clr-namespace:MyControl	MyControl      MyControl.Windows   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:MyControl       � 	Resources     titleBar �� 
TargetType �� Property �� Value 
� GradientOrigin 
� Center � Color � Offset �� Triggers	 P� Property
 P� Value��%  &	        �� " ��.���  � �� 
� $	 0,0&�$ 0.5,0.5&���� $ #777777��$ 0Q�� $ #333333��$	 1.0Q� P� 	 �@J�IsMouseOver
 . . ���  � �� 
� $	 0,0&�$ 0.5,0.5&���� $ #73BCDE��$ 0Q�� $ #333333��$	 1.0Q�.��� .+��� ���#555$��1q� �� CornerRadius$ 5��$��0q��� -   $��	title_bar�����#FFF J� Opacity$
 0.70Q�$��28��$��0,0,0,0q�$	��Top=�$ 5,5,0,0��$��1q�#��[  �� ���#FFF$
 0.70Q�$��1,29,1,1q�$��0,0,0,0q�$ 0,0,5,5����
� $ 0.5,1&�$ 0.5,0.8&���� $ #777777��$ 0Q�� $ #333333��$	 1.0Q��� $	��Top=����#FFF$
 0.70Q�$��1,28,1,1q�$��16��$��0,0,0,0q�$ 0,0,5,5������  �� 
StartPoint$ 0.5,0&� �� EndPoint$ 0.5,1&���� $ 	#55000000��$ 0Q�� $ 	#00000000��$	 1.0Q��� -   $��title�� J� IsHitTestVisible .  $��?????????????��$	��Top=�$
��Left=�$��28��$��5,1,0,0q����	#FF000000$��18(�$��Center=�$	��Top=�$
��Bold"� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$ 0Q�   Color$ 	#FF000000��   Opacity$ 1Q�   
BlurRadius$ 10Q��� -   $��title_double_shadow�� .  $��?????????????��$	��Top=�$
��Left=�$��28��$��5,1,0,0q����	#FFFFFFFF$��18(�$��Center=�$	��Top=�$
��Bold"�   $ 0Q�$ 	#FF000000��$ 1Q�$ 10Q�