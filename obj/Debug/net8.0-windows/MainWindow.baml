   M S B A M L   `   `   `  ����J  FDuplicateFinder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DuplicateFinderDuplicateFinder  5   	        DuplicateFinder.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:DuplicateFinder     5   	     � SizeToContent$  WidthAndHeight=�5   	    � 
ResizeMode$ NoResize=�5	   	    � Title$( "Duplicate Finder By Ronja Sjögren��5
   	   .��5      � $��20q�6    � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5      $	��200 �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0��6    5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� -   5        $��
lblFolders��5   3   $��0��6   $��Folders��6!   63   �� 5      -   5        $��
FolderList��5   0   $��1��6   $	��200��6#    �� SelectionMode$ Multiple=�6b   ��6B   �� *	Folders+6b   �� 5       $��2��6    �� Orientation$ 
Horizontal=�6&   $��Center=�6?   $��Center=�6Z   $
��0,10q�6w   .+�5!      �� -   5        $��
Add Folder��5!      $	��100��6H   $��3q�6T   $��0, 0, 20, 0q�6`   �� 5"      -   5        $��Remove Folder��5"      $	��100��6N   $��3q�6Z   5#      5$      � 5&   
   $��1��6   $��0��6   $��20, 0, 0, 0q�6,    5'      �� 5(      $
��Auto �6    �� 5)      $
��Auto �6    �� 5*      $
��Auto �6    �� 5+      $
��Auto �6    �� 5,      $
��Auto �6    �� 5-      $
��Auto �6    �� 5.      $
��Auto �6    �� 5/      $
��Auto �6    .+�51      �� -   5        $��lblAttributes��51   A   $��0��6   $��Attributes to compare��6!   6A   �� 52      -   5        $��chkName��52   3   $��1��6   $
��Name��6$   63   �� 53      -   5        $��chkDateCreated��53   ;   $��2��6   $��Date Created��6$   6;   �� 54      -   5        $��chkDateModified��54   <   $��3��6   $��Date Modified��6$   6<   �� 55      -	   5        $��chkSize��55   3   $��4��6   $
��Size��6$   63   �� 56      -
   5        $��lblSelectFileTypes��56   6   $��5��6   $��
File types��6!   66   �� 57      -   5        $��FileTypeList��57   0   $��6��6   $	��100��6#   $ Multiple=�6f   ��6D   �� *	FileTypes+6f   �� 58      -   5        $��7��58      $��Find duplicates��6"   $	��100��6[   $��5q�6g   $��0, 10, 0, 0q�6s   59      � 5;   
   $��0��6   $��2��6   $��1��63    5<      �� 5=      $
��Auto �6    �� 5>      $
��Auto �6    �� 5?      $
��Auto �6    .+�5A      �� -   5        $��
lblResults��5A   3   $��0��6   $��Results��6!   63   �� 5C      -   5        $��
ResultList��5C   5   $��1��6   $	��200��6(   $
��Auto=�6G   $
��Auto=�6j   .��5D      �� -   5        $��	lvResults��5D      5F      5G      �� 5H      -   5        $��2��5H      $��Delete file��6"   $	��100��6S   $��5q�6_   $��0, 10, 0, 0q�6k   5I      5K      5L      