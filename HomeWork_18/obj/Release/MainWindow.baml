   M S B A M L   `   `   `  ���� F  BHomeWork_17, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:HomeWork_17HomeWork_17  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem     HomeWork_17.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:HomeWork_17     9system&clr-namespace:System;assembly=mscorlib     Ec9http://schemas.microsoft.com/netfx/2009/xaml/presentation    -     � WindowStyle$
  None=� � 
ResizeMode$ NoResize=�$	��800��$��Rostov)�$	��950�� � ContextMenu �� .@�m� -   $��Закрыть окно������  �� ImageSource$ background.jpg�� � 	Resources ��    Key	Array   �� Type" ��.s���!Физические лица����#Юридические лица��.���  � ColumnDefinitions �� $	��300 ��� $��* � � RowDefinitions �� $	��100 ��� $��* ��� $	��200 �.+��� -    ��Name$ 	lvClients��$��0��$��1��$��10q����� 	 �� Opacity$	 0Q� �� .@�m� -   $%��Добавить клиента��m� -   $/��)Редактировать клиента��m� -   $#��Удалить клиента��m� -   $��Обновить������ .R��� $��20��$��Center=����� *
FullName+�� $��1��$��0��$��10q�.+��� $��5q�
 �� Orientation$
 
Horizontal=�$��20��.+��� $.��(Фамилия Имя Отчество: ���� -    ��Name$ tbName��$	��250���� $*��$Привилегированный: ���� -	   $ tbIsVip���� $��5q�$
 
Horizontal=�.+��� $��20��$<��6Количество расчётных счётов: ���� -
   $ tbCountBankAcc��$��20������  �� Path$ CountBankAccounts��� $��5q�$
 
Horizontal=�.+��� $��20��$.��(Количество кредитов:  ���� -   $ tbCountBankCredits��$��20������ $ CountBankCredits��� -   $ lvBankAccounts��$��1��$��1��$��10q�$	��240��$	��Top=����� $	 0Q� �� .@�m� -   $0��*Открыть расчётный счёт��m� -   $4��.Пополнить расчётный счёт��m� -   $F��@Перевести на другой расчётный счёт��m� -   $;��5Перевести на другому клиенту��m� -   $0��*Закрыть расчётный счёт�� �� View �� .P��� $0��*Номер расчётного счёта�� �� Width$	 250�� �� DisplayMemberBinding �� *Number+�� $��Дата открытия��$	 150�� �� $ DateOpen� �� StringFormat$ 
dd.MM.yyyy���� $��Баланс��$	 100�� �� *Sum+�� $ ��Капитализация��$	 100�� �� CellTemplate �� .R��� {�.   d� 	IsChecked �� *Capitalization+�� -   $ lvBankCredits��$��1��$��1��$��10q�$	��200��$��Bottom=����� $	 0Q� �� .@�m� -   $!��Открыть кредит�� �� .P��� $��Номер кредита��$	 250�� �� *Number+�� $��Дата открытия��$	 150�� �� $ DateOpen�$ 
dd.MM.yyyy���� $��Долг��$	 100�� �� *Credit+�� $��Выплачено��$	 100�� �� *	PaidOut+�� $��0��$��0��.+��� $
 
Horizontal=�$
��10 5q�.+��� -   $	��120��$
��Left=�$��25��$!��Следующий день���� -   $��Right=�$
��40 0q�$	��120��$��25��$��Закрыть окно���� -   $ tbDate��$
��30 5q��� -    ��Name$ 	cbClients�� 	 Array#��[ {�.  $��14(�$��
10 10 10 5q�$	��250��$��25���� -   $ lvLogs��$��2��$��2��$��
10 0 10 10q�$��Disabled=����� $	 0Q����� .R��� $��5q�$
��Wrap=����� *Msg+