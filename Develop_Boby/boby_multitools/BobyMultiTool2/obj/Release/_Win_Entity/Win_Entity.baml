   M S B A M L   `   `   `  ���� P  Lboby_multitools, Version=2.0.5796.7710, Culture=neutral, PublicKeyToken=null%clr-namespace:MyControl	MyControl      BobyMultitools.Win_Entity   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,Controlsclr-namespace:MyControl     -     � Title$  BOBY_Entity��$	��298��$	��309��$	��358��$	��509��$	��109�� � WindowStartupLocation$ Manual=� � Topmost .  � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � 	Resources    myHeaderStyle �� 
TargetType	 �� Property
 �� Value  ItemContStyle �� 
TargetType �� 
StartPoint �� EndPoint � Offset � Color �� Content  ResizeHandle  ResizeHandleFull��%[  &	        &	 *     &	 �    &	 �    �� " ��.��� 	 � y�$
 	Collapsed=��� " ��.��� 	 � ��$
 Stretch=�-   �� 	 � ��$
 16���� 	 � ��$
 0q��� 	 � ��$
 0q��� 	 � ��
 �� *graph_forground+�� 	 � ��$
 0q��� 	 � ��
 �� " ��.R�� -   .+��� $
��Left=�$��0,0,0,0q�$��16������ *graph_HP_Meter+���� *graph_background+�� $��Right=�$��0,0,0,0q�$��16�����	#FF000000���� *graph_HP_Meter_inv+�� $	��Top=�$��0,0,0,0q�$��1������ $	 0,0&�$	 1,1&�.��� $ 0Q�$ Transparent��� $	 0.5Q�$ 	#FFFFFFFF��� $ 1Q�$ Transparent���� $	��bor��$��0,1,0,0q�$��15��$��1q����� *graph_border+� $
��left=�$��3,0,0,0q�$��16��$��16��.+��� $��image��$��center=�$��16��$��16��$
��None=�$��0,0,0,0q����� *	graph_img+�� $��image_class��$��right=�$��18��$��18��$��
0,-1,144,0q����� *graph_class+�� $��Stretch=�#��z��$
��Auto��$��0q�# z���� " �.��� 	 �@�Cursor$
 SizeNS��-   �� " �.��� 	 �@�Cursor$
 SizeNWSE��-   .��� .+�   MyControl.Windows     Title$ entity��� $��0,0,5,0q�$��Right=�$	��Top=�.+���  �� Orientation$ 
Horizontal=�.+��� -   $��bt_where_target��$��-1,0,0,0q�  	bt_Target#��[ �� -   $��tb_where��$��-1,1,0,0q�$	��128���� -   $��bt_clear_target��$��	-23,0,0,0q�  bt_Clear#��[ �� -	   $��
bt_Setting����.    WindowButton.Setting#��[ $��Right=�$	��Top=�$��27��$��2,0,0,0q� � ContextMenu �� $	��100��.@�m� -
   $��	Check_NPC�� m� IsCheckable . $	��NPC��m� -   $��
Check_Ally�� . $
��Ally��m� -   $��Check_Ennemy�� . $��Ennemy��m� -   $��Check_Gather�� . $��Gather���� -   $��bt_Minimise����.    WindowButton.Minimise#��[ $��27��$��-1,0,0,0q��� -   $��bt_Close����.    WindowButton.Close#��[ $��35��$��-1,0,0,0q��� $��	1,28,1,18q�.+��� $��0,0,0,0q�$ 
Horizontal=�.+��� -   $��bt_Rnk�� 	 List_Header#��[	 $��25��.��Rnk�� -   $��bt_Name��#��[	 $	��123��.��Name�� -   $��bt_Guild��#��[	 $��0��.��Guild�� -   $��bt_Lvl��#��[	 $��22��.��Lvl�� -   $��bt_Class��#��[	 $��87��.��Class�� -   $��bt_Hp��#��[	 $��35��.��HP%�� -   $��bt_Dst��#��[	 $��37��.��Dst�� -   $��bt_up��#��[	 $��10��.��^�� -   $��	lv_Entity��$��Disabled=�$��Visible=�$
��Left=�$	��Top=�$��2,1,0,0q�$	��337��$
��Auto��
������ ���	#00FFFFFF$
��11.3(�$��0q� �� SelectionMode$ Single=�#��[  �� View �� ! �� ColumnHeaderContainerStyle# [   �� AllowsColumnReorder .  .P���  �� Width$ 22��$
��Rank�� �� CellTemplate �� .R��� $��-3,0,0,0q�$��Center=����� *Rank+�� $	 121��$
��Name�� �� .R��� $��-3,0,0,0q�$��CharacterEllipsis=����� *Name+�� $ 0��$��Guild�� �� .R��� $��0,0,0,0q�$��CharacterEllipsis=����� *Guild+�� $ 33��$	��Lvl�� �� .R��� $��	-22,0,0,0q�$��Center=����� *Lvl+�� $ 76��$��Class�� �� .R��� $��-14,0,-10,0q�$��Center=�$��CharacterEllipsis=����� *Class+�� $ 41��$��HP�� �� .R��� $��Center=�$��
-15,0,-1,0q����� *HP+�� $ 40��$��Distance�� �� .R��� $��
-14,0,-6,0q�$
��Left=����� *
Distance+� $��1,0,1,1q�$��Bottom=�.+��� -   $��BG_Count_Ally��#��[ $��15��$	��155��$��10,0,0,0q�$��Bottom=�$
��Left=����� $	 0,0&�$	 1,1&�.��� $ 0Q�$ Transparent��� $ 1Q�$ 	#9900AA00���� -   $��BG_Count_Ennemy��#��[ $��15��$	��155��$��0,0,10,0q�$��Bottom=�$��Right=����� $	 0,0&�$	 1,1&�.��� $ 0Q�$ 	#99FF0000��� $ 1Q�$ Transparent���� -   $��Label_Count_Ally��#��[ $��12(�$
��Bold"����	#FFFFFFFF$��50��$	��Top=�$��Center=�$��-25,0,25,-5q�$��Center=�$��Right=��� -   $��Label_Count_Ennemy��#��[ $��12(�$
��Bold"����	#FFFFFFFF$��50��$	��Top=�$��Center=�$��25,0,-25,-5q�$��Center=�$
��Left=��� -   $��
Resize_all��#��[ $��15��$��10��$��0,0,0,0q�$��Bottom=�$��Right=����� $	 0,0&�$	 1,1&�.��� $ 0Q�$ 	#99FFFFFF��� $ 1Q�$ Transparent��