   M S B A M L   `   `   `  ����  BOBY_Shulack    BOBY_Shulack.Win_Viewer   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � WindowStartupLocation$  Manual=�5   	    � Topmost .  � WindowStyle$
 None=�5   	   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5	   	    � Title$ BOBY_Viewer��5
   	   $��74��6   $	��200��6)    � 	Resources 5       
  HP_Bar �� 
TargetType �� Property	 �� Value
 �� 
TargetType �Name �� RadiusX �� RadiusY �� Color ��Name �� EndPoint �� 
StartPoint � Color � Offset 
 MP_Bar��%�  &	        &	 k    �� 5      " �6   .�5      ��  � ��	 5      �� 5      "
 �6   .R�5      � $ 
PART_Track��6   .+�5   	   �� #��z��6   #��z��65   $
��Left=�6U   $ 5��6p   $ 10��6|   ��5   
   �� 5      $ Transparent��5      5   
   �� 5   	   $ PART_Indicator��6   $
��Left=�6+   $ 5��6F   $ 10��6R   ��5   
   �� 5      $ 0.5,1&�6   $ 0.5,0&�60   .��5      � $ 	#FFCD7563��6   $ 0Q�6+   � 5      $ 	#FFAE1615��6   $	 0.5Q�6+   � 5      $ 	#FFCD7563��6   $ 1Q�6+   5      5    
   5!   	   5"      5$      5%      �� 5&      " �6   .�5'      ��  � ��	 5(      �� 5)      "
 �6   .R�5*      � $ 
PART_Track��6   .+�5+   	   �� #��z��6   #��z��65   $
��Left=�6U   $ 5��6p   $ 10��6|   ��5,   
   �� 5-      $ Transparent��5.      50   
   �� 51   	   $ PART_Indicator��6   $
��Left=�6+   $ 5��6F   $ 10��6R   ��52   
   �� 53      $ 0.5,1&�6   $ 0.5,0&�60   .��54      � $ 	#FF3C6FD9��6   $ 0Q�6+   � 55      $ 	#FF2149AE��6   $	 0.5Q�6+   � 56      $ 	#FF3C6FD9��6   $ 1Q�6+   57      59   
   5:   	   5;      5=      5>      5?      .��5@      � .+�5A      �� $
��Left=�5B      $	��Top=�5C      ��5D      �� 5E       �� 	UriSource$! Image_Windows\BG_Viewer.png?�5F      5H      �� 5I      -   5        $��
Player_Lvl��5J      $��00��5K       � Cursor$ Arrow��5L       �� TextWrapping$ WrapWithOverflow=�5M      $��13(�5N       �� 
IsReadOnly .  �� TextAlignment$ Center=�5P      ���	#FFFFFFFF$��0q�5R      $��0��5S      $��0��5T      $��Arial)�5U      $
��Left=�5V      $	��Top=�5W      $��4,29,0,0q�5X      $��18��5Y      $��18��5Z      .��6   ��5[      �� 5\      $ Transparent��5]      .��5^      5_      �� 5`      -   5        $��Player_Name��5a      $
��Name��5b      $ Arrow��5c      $ WrapWithOverflow=�5d      $��13(�5e       . $
 Left=�5g      ���	#FFFFFFFF$��0q�5i      $��0��5j      $��0��5k      $��Arial)�5l      $
��Left=�5m      $	��Top=�5n      $��	70,18,0,0q�5o      $	��117��5p      $��18��5q      .��6   ��5r      �� 5s      $ Transparent��5t      .��5u      5v      �� 5w      -   5        $��Player_Cube��5x      $
��Name��5y      $ Arrow��5z      $ WrapWithOverflow=�5{      $��23(�5|       . $ Center=�5~      ���	#FFFFFFFF$��0q�5�      $��0��5�      $��0��5�      $��Arial)�5�      $
��Left=�5�      $	��Top=�5�      $��	30,22,0,0q�5�      $��30��5�      $��30��5�      .��6   ��5�      �� 5�      $ Transparent��5�      .��5�      5�      � 5�      -   5        $��HP_Bar��5�      #��[  5�      $��0��5�      $��0��5�      $
��Left=�5�      $	��Top=�5�      $��	73,36,0,0q�5�      $	��117��5�      $��9��5�      6   � 5�      -   5        $��MP_Bar��5�      #��[ 5�      $��0��5�      $��0��5�      $
��Left=�5�      $	��Top=�5�      $��	73,48,0,0q�5�      $	��117��5�      $��9��5�      6   �� 5�      -   5        $��
rectangle1��5�       �� Stretch$ UniformToFill=�5�      $	��9,9q�5�      $	��Top=�5�      $
��Left=�5�      $ SizeAll��5�      ��5�      �� 5�      $ Transparent��5�      5�      �� 5�      -   5        $��Button_Minimise��5�        Style_Minimise_Button#��[ 5�      $��Right=�5�      $	��Top=�5�      $
��9,11q�5�      5�      5�      