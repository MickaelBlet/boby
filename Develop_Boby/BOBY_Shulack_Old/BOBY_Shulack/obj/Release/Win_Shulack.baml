   M S B A M L   `   `   `  ����   boby_shulack    BOBY_Shulack.Win_Shulack   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � WindowStartupLocation$  Manual=� � Topmost .  � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=� � Title$ BOBY_Shulack��$	��175��$	��200��.��� .+��� $$��Image_Windows\Win_Top_Left.png��$	��Top=�$
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
��24,0q��� -   $��label��$��BOBY Shulack��$��16(�$��Microsoft Tai Le)�$
��Bold"�
�������$	��Top=�$
��Left=�$
��12,7q�$��28�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$ 0Q�   Color$ White��	   Opacity$		 0.5Q�
   
BlurRadius$
 10Q��� -   $��
rectangle1��$��24�� �� Stretch$ UniformToFill=�$	��9,9q�$	��Top=�$
��Left=� � Cursor$ SizeAll������  �� Color$ Transparent���� -   $��Button_Minimise��   Style_Minimise_Button#��[  $��Right=�$	��Top=�$
��43,9q��� -   $��Button_Close��  Style_Close_Button#��[ $��Right=�$	��Top=�$��0,9,13,0q��� -   $��tbLog1��$ Arrow��$��Status�� �� TextWrapping$ WrapWithOverflow=�$��11(� �� 
IsReadOnly .  �� TextAlignment$ Center=����	#FFFFFFFF$��0q�$��0��$��0��$
��Left=�$	��Top=�$��	10,34,0,0q�$��90��$��18��.������ $ 	#55000000��.���� -   $��tbLog2��$ Arrow��$��Status��$ WrapWithOverflow=�$��11(� . $ Center=����	#FFFFFFFF$��0q�$��0��$��0��$��Right=�$	��Top=�$��	0,34,10,0q�$��90��$��18��.������ $ 	#55000000��.���� -   $��Player_Name��$��
Name * lvl��$ Arrow��$ WrapWithOverflow=�$��13(� . $ Center=����	#FFFFFFFF$��0q�$��0��$��0��$��Arial)�$��Center=�$	��Top=�$��0,53,0,0q�$	��180��$��18��.������ $ 	#55000000��.���� -	   $��Shulack_Count��$��0��$ Arrow��$ WrapWithOverflow=�$��13(� . $ Center=����	#FFFFFFFF$��0q�$��0��$��0��$��Arial)�$��Center=�$	��Top=�$��0,73,0,0q�$	��180��$��18��.������ $ 	#55000000��.���� -
   $��checkBox_deloot�� d� 	IsChecked$
 TrueY�$��Auto Recycle Loot��$��Arial)�$��13(�$��8,92,0,0q�$	��Top=�$
��Left=��� -   $��Button_Recycle_List��  Style_Left_Arrow_Button#��[ $��Right=�$	��Top=�$��	0,92,10,0q��� -   $��Button_Start��{�.  $
��Left=�$��Bottom=�$��	10,0,0,35q�$��90��  Style_Aion_Button#��[ .��Start�� -   $��Button_Skills_List��$��Right=�$��Bottom=�$��	0,0,10,35q�$��90��#��[ .��Skills List�� -   $��Button_Hide��$
��Left=�$��Bottom=�$��	10,0,0,10q�$��90��#��[ .��Hide/Unhide�� -   $��Button_Viewer��{�.  $��Right=�$��Bottom=�$��	0,0,10,10q�$��90��#��[ .��Boby Viewer