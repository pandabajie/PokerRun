   M S B A M L   `   `   `  ����C  ?PokerRun, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PokerRunPokerRun  5   	       PokerRun.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:PokerRun     5   	     � 
ResizeMode$  CanMinimize=�5       � Icon$ images/fav.ico��63    � Title$: 4湖南跑得快扑克牌游戏 开发者：熊跃辉��5   	   $	��800��6$   $
��1000��61    � 	Resources 5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $ BureauBlack.xaml?�6%   %    5      .��5      � .+�5   
   �� $��images\background.jpg��6   $
��Fill=�6/   6@   �� 5   
   -   5         ��Name$ 	herCanvas��5      $	��500��6(   $	��190��64   $��230,0,264,581q�6A   �� 5   
   -   5        $ 	hisCanvas��5      $	��190��6'   $	��500��63   $��794,268,10,3q�6@   �� 5   
   -   5        $ myCanvas��5      $	��500��6&   $	��190��62   $��3,571,491,10q�6?   �� 5   
   -   5        $ bottomCanvas��5      $��195,269,352,312q�6'   $	��190��6@   �� 5   
   $��262,481,335,230q�6    �� Orientation$ 
Horizontal=�6.   .+�5      �� -   5         ��Name$ labInfo��5      $��请您出牌��6%   $
��Left=�64   $	��Top=�6Q   $	��198��6i   �� 5      -   5        	 ��Name$	 
btnRestart��5      $��重新洗牌��6*   $
��Left=�69   $	��Top=�6T   $��95��6l   $��50��6�   �� 5      -   5        $	 btnPlay��5      $��我方出牌��6&   $
��Left=�65   $	��Top=�6R   $��95��6j   $��50��6u   6�   5      5      5      