   M S B A M L   `   `   `  ����   boby_shulack    BOBY_Shulack.Win_Choose   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  BOBY_Shulack��$	��220��$	��320�� � WindowStartupLocation$ CenterScreen=� � Topmost .  � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=� +� FocusedElement ��  �� ElementName$ listBox_Game�� � 	Resources    	myLBStyle	 �� 
TargetType
 �� 	Resources �� 
StartPoint �� EndPoint � Color � Offset �� Color��%�  &	        �� "	 ��
 ���� $	 0,0&�$	 1,0&�(��       * SystemColors.HighlightBrushKey+).��� $ 	#8800AAAA��$	 0.0Q�� $ 	#8800AAAA��$
 0.50Q�� $ Transparent��$	 1.0Q��� $	 0,0&�$	 1,0&�(��       *SystemColors.ControlBrushKey+).��� $ 	#8800AAAA��$	 0.0Q�� $ 	#8800AAAA��$
 0.50Q�� $ Transparent��$	 1.0Q��� $ White��(��       *" SystemColors.ControlTextBrushKey+).��� .+��� $!��Image_Windows\BG_Choose.png��$	��Top=�$
��Left=�$	��300��$	��170��$��	10,38,0,0q��� $$��Image_Windows\Win_Top_Left.png��$	��Top=�$
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
��24,0q��� $��BOBY Shulack��$��16(�$��Microsoft Tai Le)�$
��Bold"����	#FFFFFFFF$��0��$��0��$	��Top=�$
��Left=�$��10,9,0,0q�$	��300��$��28��$��Center=�$	��Top=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$ 0Q�   Color$ White��   Opacity$	 0.5Q�   
BlurRadius$ 10Q��� -   $��
rectangle1��$��24�� �� Stretch$ UniformToFill=�$	��9,9q�$	��Top=�$
��Left=� � Cursor$ SizeAll������ $ Transparent���� -   $��Button_Close��  Style_Close_Button#��[ $��Right=�$	��Top=�$��0,9,13,0q��� -   $��listBox_Game��$��0��$��0��$
��Left=�$	��Top=�$��
10,138,0,0q�$	��300��$��70�����	#00FFFFFF���	#FFFFFFFF#��[  $��Arial)�$��13(����� $	 0,0&�$	 1,0&�.��� $ Transparent��$	 0.0Q�� $ 	#55FFFFFF��$
 0.50Q�� $ Transparent��$	 1.0Q��� -   $��Center=�$	��Top=�$��	0,112,0,0q�$��80��  Style_Aion_Button#��[ .��Start�� -   $��tbLog��$�� �� �� TextWrapping$ WrapWithOverflow=�$��11(� �� 
IsReadOnly . ���	#FFFFFFFF$��0q�$��0��$��0��$
��Left=�$	��Top=�$��10,38.96,0,0q�$	��300��$��72��.������ $	 0,0&�$	 1,0&�.��� $ Transparent��$	 0.0Q�� $ 	#55FFFFFF��$
 0.50Q�� $ Transparent��$	 1.0Q�.��