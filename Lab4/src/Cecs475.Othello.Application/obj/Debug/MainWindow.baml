   M S B A M L   `   `   `  ����V  RCecs475.Othello.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Cecs475.Othello.ApplicationCecs475.Othello.Application  5   	   ,    &Cecs475.Othello.Application.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:Cecs475.Othello.Application     5   	     � Title$  Othello!��5   	   $	��500��6    � 	Resources 5	      ;   5Cecs475.Othello.Application.OthelloAdvantageConverter   	Advantage8   2Cecs475.Othello.Application.OthelloPlayerConverter 
 Player��%:   &	        &	        5
   
   6*     5   
   6'   5      .��5      ]� .+�5   
   c� $��30��6   $	��Top=�6   $��10(�63    � DataContext 6A   ��  �� ElementName$ OthelloView��.@�5      �� -   5        $��
undoButton��5   w   $
��Undo��6   
�������{�6S   �� *Model.CanUndo+6w   5      �� 5   
   $��30��6   $��Bottom=�6    $��10(�68    6F   �� $ OthelloView��.@�5      � .+�5      �� .��6   Score:6    �� 5      ��6   �� *Model.CurrentAdvantage+ �� 	Converter# [  �� 5      ��6   �� *Model.CurrentPlayer+# [ 5      5      -   'Cecs475.Othello.Application.OthelloView  5   
   -   5          Name$ OthelloView��5      5      5      