   M S B A M L   `   `   `  ����  	GlobalApp&     GlobalApp.Views.SoundDevicesView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �   � RowDefinitions  5	   
   �� 5
      $
��Auto �6   6,   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   $	��200��6,   6=   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   .+�5   
   �� $��5q�6   $��0��6   $��0��6-    �� DisplayMemberPath$
 Name��6Z   ��6:   �� *	Devices+ �� SelectedIndex 5      �� *SelectedDevice+ �� Mode$ TwoWay=�6J   �� 5   
   $��5q�6   $��1��6   $��22��6)   $
��Tune��65   ��6D   �� *
GoToTune+6c   � 5   
   $��1��6    � Orientation$ Vertical=�6&   $��2��6=   $��10q�6N   $��20��6Z    � Value 6e   �� *VolumeLevel+$ OneWay=�6�   �� 5   
   $��2��6   $��2��6!   $��10q�62    �� Orientation$ Vertical=�6>    � Maximum$	 100Q�6U    6c   �� *
InputLevel+$ TwoWay=�6�   5      5      