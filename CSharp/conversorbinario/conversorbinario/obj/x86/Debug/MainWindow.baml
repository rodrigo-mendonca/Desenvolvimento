   M S B A M L   `   `   `  ����  conversorbinario!    conversorbinario.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Converte Binario��5   	   $	��489��6"   $	��676��6/    � 
ResizeMode$ NoResize=�6;   .��5      � ��6   Z�  � ColumnDefinitions 5   
   �� 5      $��1112* �6   �� 5      $��9* �6   .+�5
   
   �� -   5        $��
cmdDECIMAL��5
   K   $��Binary to Decimal��6   $��23��6-   $��0,41,5,0q�69   $	��Top=�6]   $��Right=�6�   $	��165��6�   �� 5   
   -   5        $��	txtRESULT��5   N   $	��177��6   $
��Left=�6   $��
12,261,0,0q�6:   $	��Top=�6_   $	��320��6w   {�.  �� AcceptsReturn .  �� 
AcceptsTab .  �� TextWrapping$
 Wrap=�6�    �� 
IsReadOnly .  � UseLayoutRounding .  $ �� HorizontalScrollBarVisibility$ Hidden=�6�   "	 �� VerticalScrollBarVisibility$
	 Auto=�6"  6G  �� 5   
   -   5        $��
cmdBINARIO��5   g   $��Decimal to Binary��6   $��23��6-   $��Right=�69   $��0,12,4,0q�6U   $	��Top=�6y   $	��166��6�   6�   �� 5   
   -   5        $��txtTEXT��5   4   $	��231��6   $��12,12,178,0q�6   $	��Top=�6C    .  . $
 Wrap=�6�   �� 5   
   -   5        $��cmdDESTRING��5   f   $��String to Binary��6   $��23��6,   $��Right=�68   $��0,70,7,0q�6T   $	��Top=�6y   $	��165��6�   6�   �� 5   
   -   5        $��cmdPARASTRING��5   J   $��Binary to String��6   $��23��6,   $��0,99,7,0q�68   $	��Top=�6_   $��Right=�6�   $	��165��6�   �� 5   
   -   5        $��txtLOG��5   [    .  . $	��177��69   $��338,261,7,0q�6F   $
 Wrap=�6i   $	��Top=�6}    . {�. $
	 Auto=�6�   �� 5   
   -   5        $��cmdDECIMALTOHEX��5   I   $��Decimal to Hex��6   $��23��6*   $��	0,128,6,0q�66   $��Right=�6`   $	��165��6|   $	��Top=�6�   �� 5   
   -	   5        $��cmdHEXTODECIMAL��5   e   $��Hex to Decimal��6   $��23��6*   $��Right=�66   $��	0,157,7,0q�6R   $	��Top=�6|   $	��165��6�   �� 5   
   -
   5        $��cmdSTRINGTOHEX��5   d   $��String to Hex��6   $��23��6)   $��Right=�65   $��	0,186,5,0q�6Q   $	��Top=�6z   $	��165��6�   �� 5   
   -   5        $��cmdHEXTOSTRING��5   X   $��Hex to String��6   $��Right=�6)   $��	0,215,5,0q�6E   $	��165��6n   $��23��6z   $	��Top=�6�   �� 5   
   -   5        $��label1��5   _   $��	Resultado��6   $��28��6$   $
��Left=�60   $��
12,238,0,0q�6K   $	��Top=�6m   �� 5   
   -   5        $��label2��5   ^   $��Calculo��6   $��28��6"   $
��Left=�6.   $��338,238,0,0q�6I   $	��Top=�6l   5      5      