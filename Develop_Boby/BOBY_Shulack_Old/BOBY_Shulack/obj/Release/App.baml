   M S B A M L   `   `   `  ����   boby_shulack    BOBY_Shulack.App   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       �� 	Resources     Style_Close_Button �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value �� 
TargetName  Style_Minimise_Button  Style_Aion_Button	 �� TextAlignment  Style_Setting_Button_Anim
 J� Opacity N� EnterActions g� DesiredFrameRate g� RepeatBehavior �� TargetProperty �� 
TargetName W� From	 W� To g� AutoReverse g� Duration  Style_Setting_Button  Style_Show_Button  Style_Hide_Button  Style_Left_Arrow_Button  Style_Play_Button 	 Style_Stop_Button �� Orientation ��Name ��Name �� 
StartPoint �� EndPoint � Color � Offset K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation 
 Null �� Triggers J� IsHitTestVisible m� 	IsChecked  d� 	IsChecked! J� SnapsToDevicePixels�� %�  &	        &	 p    &	 �    &	 �    &	 �
    &	 y    &	 �    &	 c    &	 �    &		 
    '�� *    '�� 5    &	
 �    'm� �    (�� I    *MenuItem.SeparatorStyleKey+)�� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 33����  � ��$ 24����  � �� �� " ��.R��� $��image��$��Image_Control\Close.png�� P�  �@J�IsMouseOver . . ��� $ image�� � ��$# Image_Control\Close_Hover.png��P�  � �� . . ��� $ image�� � ��$% Image_Control\Close_Pressed.png���� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 28����  � ��$ 24����  � �� �� " ��.R��� $��image��$ ��Image_Control\Minimise.png�� P�  �@J�IsMouseOver . . ��� $ image�� � ��$&  Image_Control\Minimise_Hover.png��P�  � �� . . ��� $ image�� � ��$( "Image_Control\Minimise_Pressed.png���� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 26����  � �� �� " ��.R�� .+��� $
��left��$
��Left=�$	��Top=�$��0,0,0,0q�$#��Image_Control\Button\Left.png���� $��right��$��Right=�$	��Top=�$��0,0,0,0q�$$��Image_Control\Button\Right.png���� $��center��$
��Left=�$	��Top=�$	��5,0q�$
��Fill=�$%��Image_Control\Button\Center.png���� ���	#FF000000$
��Left=�$	��Top=�$��1,5,0,0q�#��z��$	 Center=�#��z��#��z���� $��content�����	#FFEFEDC4$
��Left=�$	��Top=�$��0,4,0,0q�#��z��$	 Center=�#��z��#��z�� P�  � {� .  . ��� $
 left�� � ��$+ %Image_Control\Button\Left_Disable.png���� $ right�� � ��$, &Image_Control\Button\Right_Disable.png���� $ center�� � ��$- 'Image_Control\Button\Center_Disable.png���� $ content�� � �� �	#FFAAAAAAP�  �@J�IsMouseOver . . ��� $
 left�� � ��$) #Image_Control\Button\Left_Hover.png���� $ right�� � ��$* $Image_Control\Button\Right_Hover.png���� $ center�� � ��$+ %Image_Control\Button\Center_Hover.png���� $ content�� � �� �	#FFCBA85BP�  � �� . . ��� $
 left�� � ��$+ %Image_Control\Button\Left_Pressed.png���� $ right�� � ��$, &Image_Control\Button\Right_Pressed.png���� $ center�� � ��$- 'Image_Control\Button\Center_Pressed.png���� $ content�� � �� �	#FFCBA85B�� $ content�� � ��$ 0,5,0,0q��� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 28����  � ��$ 24����  � �� �� " ��.R�� .+��� $��image��$��Image_Control\Setting.png���� $��image2��$
 0Q�$$��Image_Control\Setting_Anim.png�� P�  �@J�IsMouseOver .   �� .���� $ 8X�$	 10x��.��W� $ (Image.Opacity)�$ image2��$ 0X�$ 1X� . $ 0:0:0.4D�P�  �@J�IsMouseOver . . ��� $ image�� � ��$% Image_Control\Setting_Hover.png��P�  � �� . . ��� $ image�� � ��$' !Image_Control\Setting_Pressed.png���� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 28����  � ��$ 24����  � �� �� " ��.R�� .+��� $��image��$��Image_Control\Setting.png�� P�  �@J�IsMouseOver . . ��� $ image�� � ��$% Image_Control\Setting_Hover.png��P�  � �� . . ��� $ image�� � ��$' !Image_Control\Setting_Pressed.png���� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 28����  � ��$ 24����  � �� �� " ��.R�� .+��� $��image��$��Image_Control\Show.png�� P�  �@J�IsMouseOver . . ��� $ image�� � ��$" Image_Control\Show_Hover.png��P�  � �� . . ��� $ image�� � ��$$ Image_Control\Show_Pressed.png���� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 28����  � ��$ 24����  � �� �� " ��.R�� .+��� $��image��$��Image_Control\Hide.png�� P�  �@J�IsMouseOver . . ��� $ image�� � ��$" Image_Control\Hide_Hover.png��P�  � �� . . ��� $ image�� � ��$$ Image_Control\Hide_Pressed.png���� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 22����  � ��$ 22����  � �� �� " ��.R�� .+��� $��image��$"��Image_Control\Left_Arrow.png�� P�  �@J�IsMouseOver . . ��� $ image�� � ��$( "Image_Control\Left_Arrow_Hover.png��P�  � �� . . ��� $ image�� � ��$* $Image_Control\Left_Arrow_Pressed.png���� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 20����  � ��$ 20����  � �� �� " ��.R�� .+��� $��image��$��Image_Control\Play.png�� P�  �@J�IsMouseOver . . ��� $ image�� � ��$" Image_Control\Play_Hover.png���� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 20����  � ��$ 20����  � �� �� " ��.R�� .+��� $��image��$��Image_Control\Stop.png�� P�  �@J�IsMouseOver . . ��� $ image�� � ��$" Image_Control\Stop_Hover.png���� " ��.���  � �� �� " ��.R��� 
������ $ 
Horizontal=�.+��� $ imageCheckBox��$&�� Image_Control\CheckBox_Check.png��$��19��$��19��$	��7,2q�� $��Center=�.+��� #��z��$
��Left=�$	��Top=�$��1,1,0,0q�
���   ��� $��content��#��z��$
��Left=�$	��Top=�$��0,0,0,0q����	#FFD8D6AF P�  �@d�	IsChecked$ FalseY�. ��� $ imageCheckBox�� � ��$( "Image_Control\CheckBox_UnCheck.png��P�  �@J�IsMouseOver . . ��� $ content�� � �� �	#FFCBA85B�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  �@�IsSharedSizeScope . ��  �@��HasDropShadow . ��  � �� �� " ��.R��� $ Border��$��1q����� $	 0,0&�$	 1,1&�.��� $ 	#FF555555��$	 0.0Q�� $ 	#DD222222��$
 0.50Q�� $ 	#FF555555��$	 1.0Q����� $	 0,0&�$	 1,1&�.��� $ 	#88DDDDDD��$	 0.0Q�� $ 	#FFDDDDDD��$
 0.50Q�� $ 	#88DDDDDD��$	 1.0Q�._���  . $ Cycle=� P�  �@��HasDropShadow . . ��� $ Border�� �@��Padding$ 0,3,0,3q��� $ Border�� �@��CornerRadius$ 4���� " m��� " m� P�  �@J�IsMouseOver . . ���  � �� �	#55000000P�  �@J�IsKeyboardFocusWithin . . ���  � �� �	#55000000P�  �@m�IsCheckable . . ���  � �� �� " m�.R�� #��z��.+��� #��z�� .  #  z P�  �@m�IsCheckable .  . ���  � �� �� " m�.R�� #��z��.+��� ���	#FFD8D6AF$
��33,1q�$��19��#��z�� .  �� " ��.���  � ��$ 0,2,0,2q���  � |� .  ��  � �� �� " ��.R��� $��1q�$��1��! . ���� $	 0,0&�$	 1,1&�.��� $ Transparent��$	 0.0Q�� $ 	#FFDDDDDD��$
 0.50Q�� $ Transparent��$	 1.0Q�