   M S B A M L   `   `   `  ���� P  Lboby_multitools, Version=2.0.5796.7710, Culture=neutral, PublicKeyToken=null%clr-namespace:MyControl	MyControl  /clr-namespace:BobyMultitoolsBobyMultitools  #    BobyMultitools.Win_DialogList   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,Controlsclr-namespace:MyControl     .localclr-namespace:BobyMultitools       � Title$  Setting��$	��375��$	��322�� � WindowStartupLocation$ CenterScreen=� � Topmost .  � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � 	Resources  �� 
TargetType	 �� Property
 �� Value �� 
TargetType �Name �� RadiusX �� RadiusY �� EndPoint �� 
StartPoint � Color � Offset ��Name   TreeViewItemFocusVisual  ExpandCollapseToggleStyle (� Stretch F�Name �� Triggers P� Property P� Value �� 
TargetName � RowDefinitions J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   ShadowDepth   Color   Opacity   
BlurRadius  d�Name! �� 	ClickMode" d� 	IsChecked# �� Path$ �� RelativeSource% �� TextAlignment& ��Name' P� 
SourceName��%<  '�       &	  ~    &	 �    'R� �    'Q�     7   7  �� " �.��� 	 � ��
 �� " �.R�� $ 
PART_Track��.+��� #��z��#��z��$
��Left=�$ 2��$ 2������ $	 0,1&�$	 1,0&�.��� $ 	#44000000��$ 0Q�� $ 	#00000000��$	 0.5Q�� $ 	#44000000��$ 1Q��� $ PART_Indicator��$
��Left=�$ 2��$ 2������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#9900A599��$ 0Q�� $ 	#9900A599��$ 1Q��� .��� 	 � ��
 �� .R��� �� " d�.��� 	 � |�
 .  �� 	 � ��$
 19���� 	 � ��$
 19���� 	 � ��
 �� " d�.R��� $��19��$��19��
������ ._��� $��11��$��11�����	#01000000$��0q�._�(� $ UniformToFill=�.��F� $ 
ExpandPath��$��1,1,1,1q�
�������$��5��$��5��s����    �� �    ��- р� ��- р� @KL ���- @KL ���- ��- �@KL ��- �@KL �� ���- �� ���-     р�     р� �� 	 P�  �@d�	IsChecked$
 TrueY�. ��� $ 
ExpandPath��	 � ��+
 ��    �� �    ��- �@KL ��- �@KL �� 	�� " R�.��� 	 � ��

 ������� 	 � ��
 �	#55000000�� 	 � ��$
 0q��� " Q�.��� 	 � ��

 ������� 	 �@�FocusVisualStyle8
   �� 	 � ��
 �� " Q�.R�� $
��g_st��
������  �� $
��Auto ��� .+��� $��r_top_select��$y�Hidden=�$	��Top=�$��0,0,0,0q�$��1������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ 	#FF84AF7E��� $	 0.5Q�$ 	#FF84AF7E��� $ 1Q�$ 	#8884AF7E��   $ 4Q�$ Black��$ 1Q�$ 3Q��� $��r_bottom_select��$y�Hidden=�$��Bottom=�$��0,0,0,0q�$��1������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ 	#8884AF7E��� $	 0.5Q�$ 	#FF84AF7E��� $ 1Q�$ 	#FF84AF7E���� $��r_left_select��$y�Hidden=�$
��Left=�$��0,0,0,0q�$��1��$��20������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ 	#FF84AF7E��� $	 0.5Q�$ 	#FF84AF7E��� $ 1Q�$ Transparent��   $ 4Q�$ Black��$ 1Q�$ 3Q��� $��r_right_select��$y�Hidden=�$��Right=�$��0,0,0,0q�$��1��$��20������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ Transparent��� $	 0.5Q�$ 	#FF84AF7E��� $ 1Q�$ 	#FF84AF7E���� $��r_top��$y�Hidden=�$	��Top=�$��0,0,0,0q�$��1������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ Transparent��� $	 0.5Q�$ 	#FFA4A75E��� $ 1Q�$ Transparent���� $��r_bottom��$y�Hidden=�$��Bottom=�$��0,0,0,0q�$��1������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ Transparent��� $	 0.5Q�$ 	#FFA4A75E��� $ 1Q�$ Transparent��� $��	container��$��Stretch=����	#01000000.+�d� $  Expander��$
��Left=�8�� $! Press=�" �� $# 
IsExpanded�$ �� *TemplatedParent+�� $��content_back�����	#FF000000$
��Left=�$	��Top=�$��17,2,0,0q�#��z��$
% Left=�#��z��#��z���� $��content�����	#FFEFEDC4$
��Left=�$	��Top=�$��16,1,0,0q�#��z��$
% Left=�#��z��#��z���� $& 	ItemsHost��$��10,0,0,0q�$��1�� P� $' 	container�� �@J�IsMouseOver . . ��� $ r_top��	 � y�$
 Visible=��� $ r_bottom��	 � y�$
 Visible=��� $ content��	 � ��
 �	#FFCBA85BP�  �@Q�
IsSelected . . ��� $ r_top��	 � y�$
 Hidden=��� $ r_bottom��	 � y�$
 Hidden=��� $ r_top_select��	 � y�$
 Visible=��� $ r_bottom_select��	 � y�$
 Visible=��� $ r_left_select��	 � y�$
 Visible=��� $ r_right_select��	 � y�$
 Visible=��� $ content��	 � ��
 �	#FFCBA85B�� $ 	container��	 � ��
 �	#2255FFFFP�  �@Q�
IsExpanded .  . ��� $ 	ItemsHost��	 � y�$
 	Collapsed=�P�  �@��HasItems .  . ��� $ Expander��	 � y�$
 Hidden=�.��� .+�   MyControl.Windows  (   Title$( dialog list��� $��0,0,5,0q�$��Right=�$	��Top=�.+��� ) �� Orientation$) 
Horizontal=�.+��� -   $��
bt_Refresh����.    WindowButton.Refresh#��[ $��28��$��-1,0,0,0q��� -   $��bt_Close����.    WindowButton.Close.Blue#��[ $��35��$��-1,0,0,0q�� $��0,28,0,0q�.+��� .+�� $	��292��.+�R� -   $��tvDialog��$��1q�.@�Q� $
��test��.@�Q� $
��test���� -   $��img_ShugoLoading��$��Center=�$��Center=�* J� Opacity$* 1Q�$	��115��   $ 0Q�$ 	#FFFFFFFF��$ 1Q�$ 10Q�� -   $��	pgLoading��$��Bottom=�$��2q�$��5���� -   $��ariane��$��1q�+ �� 
IsReadOnly+ . A� , A� Columns$, 4��$��0,2,0,2q�.+��� -   $��btClick��$��1,0,1,0q�.��Click�� -   $��btShow��$��1,0,1,0q�.��Show�� -	   $��btEnable��$��1,0,1,0q�.��Enable�� -
   $��btClipboard��$��1,0,1,0q�.��Copy