   M S B A M L   `   `   `  ����B  >FPPaint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:FPPaintFPPaint  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       FPPaint.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:FPPaint     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  
mainWindow��5	   �    � Title$ FPPaint��6	   $��646.667��6   $��901.333��6*   $��Visible=�6:   ��.  � 	Resources 5
         	LightGray  MyButton �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property	 P� Value  toggleStyle
 �� BasedOn �� Triggers��%B  &	        &	      &	 �    7d���5   
   	Black��6)   �� 5   
   " ��6!   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   $��0q�6/   
���   �#��z��6W   ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver	 . . �5   "   ��  �@J�Opacity$	 0.6Q�6<   5      5      5      5      �� 5   
   8
   " 	�5      .�5       ��  �@�OverridesDefaultStyle . 66    5!      P� 5"       �@d�	IsChecked$
	 trueY�6/   . �5#      ��  �@J�Opacity$ 1Q�60   5$      P� 5%       �@J�IsMouseOver	 . . �5&      ��  �@J�Opacity$ 1Q�60   5'      5)      5*       J� CommandBindings 5+      )  #System.Windows.Input.CommandBinding  5,   
   -   5           Command$	 New��5,      6'     5-   
   -   5        $
 Open��5-      6(     5.   
   -   5        $
 Save��5.      6(     50   
   -   5           FPPaint.MainWindow   SelectedPencil# Z 50      6N     51   
   -   5           SelectedLine# Z 51      6L     52   
   -   5           SelectedEllipse# Z 52      6O     53   
   -   5           SelectedRectangle# Z 53      6Q     54   
   -	   5           SelectedErasor# Z 54      6N     55   
   -
   5           SelectedFill# Z 55      6L   .��57      � -   5         �Name$ paintDockPanel��57   "   PaintDockPanel����58   
   �� 59       �� Color# � ��6   .+�5;   
   `� $	��Top=�6   $��30��6.   .�5=      c� -   5         c�Name$ toolBarFileOptions��5=      $	��100��62   ��5>      �� 5?      $ 	#FFF4F7FC��6&   .@�5A      �� -   5        $��25��5A   )   $��25��64   #��[ 6@   ��5B      �� 5C       �� ImageSource$ Assets/newFile.png��6%   5E      �� 5F      -   5        $��25��5F   *   $��25��65   #��[ 6A   ��5G      �� 5H      $ Assets/openFile.png��6%   5J      �� 5K      -   5        $��25��5K   ,   $��25��67   #��[ 6C   ��5L      �� 5M      $ Assets/saveFileAs.png��6%   5O      5P      c� 5Q      -   5        $ toolBarHistory��5Q      $��74��6.   $
��Left=�69   $��0,0,0,0q�6T   .@�5S      �� -   5        $��23��5S   ,   #��[ 67   $��0,0,0,2q�6Y   ��5T      �� 5U      $ Assets/undo.png��6%   5W      �� 5X      -   5        $��25��5X   ,   $��25��67   #��[ 6C   ��5Y      �� 5Z      $ Assets/redo.png��6%   5\      5]      c� 5^      -   5        $ toolBarShapes��5^      $	��174��6-   ��5_      �� 5`      # � ��6&   .@�5b      	� -   5         	�Name$ 	btnPencil��5b   �    d� 	IsChecked$
 trueY�6   #��Z 6/   #��[ 6d    	� 	GroupName$ groupShapes��6�   $��25��6�   $��25��6�   ��6�   Z�  J� OpacityMask 5c      �� 5d      $ Assets/pencil.png��6%    �� Opacity$	 0.5Q�6E   ��5f      �� 5g      $ Assets/pencil.png��6%   5i      	� 5j      -   5        $ btnLine��5j   �   $ groupShapes��6   #��Z 66   #��[ 6i   $��25��6�   $��25��6�   ��6�   Z�  5k      �� 5l      $ Assets/line.png��6%   ��5n      �� 5o      $ Assets/line.png��6%   5q      	� 5r      -   5        $ 
btnEllipse��5r   �   $ groupShapes��6   #��Z 66   #��[ 6l   $��25��6�   $��25��6�   ��6�   Z�  5s      �� 5t      $ Assets/ellipse.png��6%   ��5v      �� 5w      $ Assets/ellipse.png��6%   5y      	� 5z      -   5        $ btnRectangle��5z   �   $ groupShapes��6   #��Z 66   #��[ 6n   $��25��6�   $��25��6�   ��6�   Z�  5{      �� 5|      $ Assets/rectangle.png��6%   ��5~      �� 5      $ Assets/rectangle.png��6%   5�      	� 5�      -   5        $ 	btnErasor��5�   �   $ groupShapes��6   #��Z 66   #��[ 6k   $��25��6�   $��25��6�    �� 	ClickMode$ Press=�6�   ��6�   Z�  5�      �� 5�      $ Assets/erasor.png��6%   ��5�      �� 5�      $ Assets/erasor.png��6%   5�      	� 5�      -   5        $ btnFill��5�   �   $ groupShapes��6   #��Z 66   #��[ 6i   $��25��6�   $��25��6�   ��6�   Z�  5�      �� 5�      $ Assets/fill.png��6%   ��5�      �� 5�      $ Assets/fill.png��6%   5�      5�      c� 5�      -   5        $ toolBarCommands��5�      $	��149��6/   .@�5�      �� -   5         ��Name$ btnFlipHorizontally��5�      $��25��6X   $��25��6c   #��[ 6o   ��5�      �� 5�      $! Assets/flipHorizontally.png��6%   5�      �� 5�      -   5        $ btnFlipVertically��5�      $��25��6T   $��25��6_   #��[ 6k   ��5�      �� 5�      $ Assets/flipVertically.png��6%   5�      �� 5�      -   5        $ btnRotateCw90��5�      $��25��6L   $��25��6W   #��[ 6c   ��5�      �� 5�      $ Assets/rotateCw90.png��6%   5�      �� 5�      -   5        $ btnRotateCcw90��5�      $��25��6N   $��27��6Y   $��0,0,0,-1q�6e   #��[ 6w   ��5�      �� 5�      $ Assets/rotateCcw90.png��6%   5�      �� 5�      -   5        $ btnRotate180��5�      $��25��6J   $��25��6U   #��[ 6a   ��5�      �� 5�      $ Assets/rotate180.png��6%   5�      5�      c� 5�      -    5        $ toolBarColors��5�      $	��148��6-   .@�5�      �� -!   5         ��Name$ labelBrushColor��5�      $��25��61   .��6=   
BrushColor6I   �� 5�      -"   5        $ btnBrushColor��5�      $�� ��60     �� Left$  10��6W   $��50��6h   $��25��6s   #��[ 6   5�      5�      � 5�   
   $��	4,34,0,21q�6   .+�5�      �� $��1��6   $
��Auto=�6(   $
��Auto=�6M   .��5�      �� $
��Auto=�6   ��. ! �� CanVerticallyScroll! . " �� CanHorizontallyScroll" . $
��Auto=�6�   .+�5�      �� -#   5        # ��Name$# canvasDrawingSurface��5�      $	��499��6;   $	��Top=�6H   $	��813��6`   $
��Left=�6l   
�������$ J� Effect$ 5�      3  -System.Windows.Media.Effects.DropShadowEffect  5�      %   	Direction$	% 313Q�6/   &   Opacity$	& 0.2Q�6?   '   
BlurRadius$' 4Q�6M   5�      5�      5�      5�      �� 5�   
   -$   5        ( ��Name$( 
_StatusBar��5�   &   	StatusBar��$��Bottom=�6:   .@�5�      �� StatusBarItem_1��$��0q�64   .��5�      � 
Grid_1��.+�5�      �� -%   5        ) ��Name$) statusBarTextCurrentPx��5�      TextBlock_1��$��0 5 0 0q�5�      $��Center=�5�      $
��Left=�5�      $��1��5�      �� 5�      $�X,Y: ��6   6+   5�      5�      5�      5�      5�      