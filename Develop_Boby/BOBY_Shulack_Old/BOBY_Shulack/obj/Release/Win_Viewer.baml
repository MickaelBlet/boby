   M S B A M L   `   `   `  ����   boby_shulack    BOBY_Shulack.Win_Viewer   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � WindowStartupLocation$  Manual=� � Topmost .  � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=� � Title$ BOBY_Viewer��$��74��$	��200�� � 	Resources  
  HP_Bar �� 
TargetType �� Property	 �� Value
 �� 
TargetType �Name �� RadiusX �� RadiusY �� Color ��Name �� EndPoint �� 
StartPoint � Color � Offset 
 MP_Bar��%�  &	        &	 A    �� " �.���  � ��	 �� "
 �.R�� $ 
PART_Track��.+��� #��z��#��z��$
��Left=�$ 5��$ 10������ $ Transparent���� $ PART_Indicator��$
��Left=�$ 5��$ 10������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFCD7563��$ 0Q�� $ 	#FFAE1615��$	 0.5Q�� $ 	#FFCD7563��$ 1Q��� " �.���  � ��	 �� "
 �.R�� $ 
PART_Track��.+��� #��z��#��z��$
��Left=�$ 5��$ 10������ $ Transparent���� $ PART_Indicator��$
��Left=�$ 5��$ 10������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF3C6FD9��$ 0Q�� $ 	#FF2149AE��$	 0.5Q�� $ 	#FF3C6FD9��$ 1Q�.��� .+��� $
��Left=�$	��Top=�����  �� 	UriSource$! Image_Windows\BG_Viewer.png?��� -   $��
Player_Lvl��$��00�� � Cursor$ Arrow�� �� TextWrapping$ WrapWithOverflow=�$��13(� �� 
IsReadOnly .  �� TextAlignment$ Center=����	#FFFFFFFF$��0q�$��0��$��0��$��Arial)�$
��Left=�$	��Top=�$��4,29,0,0q�$��18��$��18��.������ $ Transparent��.���� -   $��Player_Name��$
��Name��$ Arrow��$ WrapWithOverflow=�$��13(� . $
 Left=����	#FFFFFFFF$��0q�$��0��$��0��$��Arial)�$
��Left=�$	��Top=�$��	70,18,0,0q�$	��117��$��18��.������ $ Transparent��.���� -   $��Player_Cube��$
��Name��$ Arrow��$ WrapWithOverflow=�$��23(� . $ Center=����	#FFFFFFFF$��0q�$��0��$��0��$��Arial)�$
��Left=�$	��Top=�$��	30,22,0,0q�$��30��$��30��.������ $ Transparent��.��� -   $��HP_Bar��#��[  $��0��$��0��$
��Left=�$	��Top=�$��	73,36,0,0q�$	��117��$��9��� -   $��MP_Bar��#��[ $��0��$��0��$
��Left=�$	��Top=�$��	73,48,0,0q�$	��117��$��9���� -   $��
rectangle1�� �� Stretch$ UniformToFill=�$	��9,9q�$	��Top=�$
��Left=�$ SizeAll������ $ Transparent���� -   $��Button_Minimise��  Style_Minimise_Button#��[ $��Right=�$	��Top=�$
��9,11q�