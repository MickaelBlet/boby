   M S B A M L   `   `   `  ����   boby_multitools$    BobyMultitools.Win_Radar_Popup   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Popup��$	��162��$
��Auto�� � SizeToContent$ Height=� � Topmost .  � WindowStyle$
 None=� � ShowInTaskbar .   � AllowsTransparency .  � 
ResizeMode$ NoResize=�����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.��� 	 � Color$	 	#DD555555��
 � Offset$
 0Q�� $	 	#DD000000��$
 1Q� � 	Resources    ProgressBarStyle �� 
TargetType �� Property �� Value �� 
TargetType �Name �� RadiusX �� RadiusY ��Name��%o  &	        �� " �.���  � �� �� " �.R�� $ 
PART_Track��.+��� #��z��#��z��$
��Left=�$ 5��$ 10������ $ 0.5,1&�$ 0.5,0&�.��� $	 	#FF000000��$
 0Q�� $	 	#FF000000��$
 1Q��� $ PART_Indicator��$
��Left=�$ 5��$ 10������ $ 0.5,1&�$ 0.5,0&�.��� $	 	#FFFF5666��$
 0Q�� $	 	#FFFF0000��$
 1Q�.��� .+��� -   $��	border_bg��$��1q�
�������._��� .+��� -   $��tbName��$
��Left=�$	��Top=�
�������$��5,3,0,0q�$��12(�$��WrapWithOverflow=�$	��150��$
��Bold"��� -   $��tbGuild��$
��Left=�$	��Top=����	#FFCCCCCC$	��5,0q�$��10(�$��WrapWithOverflow=�$	��150��$
��Bold"��� -   $��tbLvl��$
��Left=�$	��Top=����	#FFD1B371$	��5,0q�$��11(�$��WrapWithOverflow=�$	��150��$
��Bold"���  �� Orientation$ 
Horizontal=�.+��� $��	Distance ��$
��Left=�$	��Top=����	#FFCCCCCC$	��5,0q�$��11(�$��WrapWithOverflow=�$
��Bold"��� -   $��Altitude��$	��Top=�$
��Left=�$��14��$��13��$	��0,0q��� -   $��
tbDistance��$
��Left=�$	��Top=����	#FFCCCCCC$	��5,0q�$��10(�$��WrapWithOverflow=�$	��100��$
��Bold"�� $��0,0,0,3q�.+�� -   $��prgHP��#��[  $	��0,0q�$	��150�� � Value$	 100Q�$��7���� -   $��tbHPbg��$
��Left=�$	��Top=����	#FF000000$��0,-1,0,0q�$��10(�$��Arial)� �� TextAlignment$ Center=�$	��150��$
��Bold"� J� Effect -  'System.Windows.Media.Effects.BlurEffect     Radius$	 1.0Q�   
KernelType$	 Box=��� -	   $
��tbHP��$
��Left=�$	��Top=����	#FFCCCCCC$��0,-1,0,0q�$��10(�$��Arial)�$ Center=�$	��150��$
��Bold"�