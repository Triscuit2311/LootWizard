   M S B A M L   `   `   `  ���� E  ALootWizard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:LootWizard
LootWizard      LootWizard.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:LootWizard       � Title$  Loot Wizard��$	��640��$	��720�� � Icon$$ resources/icons/wizard-hat.png��   BackgroundColor#���     ForegroundColor#���   � WindowStyle$ ThreeDBorderWindow=� � 	Resources   FlatButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType J� Opacity	 �� Triggers
 P� Property P� Value��%  &	       �� " ��.���  � ��# �   ��  � ��
 ���� ��  � ��$ 0q���  �@J�Opacity$	 0.8Q���  � �� �� " ��.R��� #���   #��z��#��z��# z ._��� $��Center=�$��Center=�	 P� 
 �@J�IsMouseOver . . ���  �@J�Opacity$ 1Q�.��� .+���  �� TabStripPlacement$
 Left=�#���   #���    ��Name ��Name �� 
TargetName��%X  '��       �� " ��.���  � �� �� " ��.R�� .+��� $ TabItemBorder��#���   $��0,5,0,5q�._��� $ ContentSite��$��Center=�$��Center=�$��Header��$
��12,2q�	 P� 
 �@J�IsMouseOver . . ��� $ TabItemBorder�� � ��
 �""��P� 
 �@��
IsSelected . . ��� $ TabItemBorder�� � ��
 � d �.@��� $��Items��#���   #���  .���  � RowDefinitions �� $
��Auto ��� $��* �.+�� $��0��$��5q� �� $
��Auto ��� $
��Auto �.+�� $��0��$��5q� �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��* �.+��� -    ��Name$ ItemsSearchBoxLabel��$��0��$��0��$��Search: ��$��Center=�#���   #���  �� -    ��Name$ ItemsSearchBox��$��1��$��0��$��Stretch=�$��Center=�$��
10,10,10,0q�#���   #���  � $��1��$��5q�$	��Top=�$��Center=� �� $
��Auto � �� $��* ��� $
��Auto ��� $��* �.+�� $��5q�$	��Top=�$��Center=� �� $
��Auto ��� $
��Auto � �� $
��Auto ��� $
��Auto �.+��� -   $ PriceFilterBox��$��0��$��0��$��5,5,5,0q�#���   #���  �� -    ��Name$ PriceFilterType��$��0��$��1��$��5,5,5,0q�.@��� $��Filter by Price���� $��Filter by Price/Slot���� -    ��Name$ FavoritesFilterCheckBox��$��1��$��Filter by Favorites��$��0��$��5,5,5,0q�#���   #���  �� -   $ SelectedFilterCheckBox��$��1��$��Filter by Selected��$��1��$��5,5,5,0q�#���   #���  �� -    ��Name$ 	ItemsList��$��1��$��
10,5,10,10q�$��Stretch=�#���   #���  ���� " ��.���  � ��$ Stretch=�����  �� DataType   LootWizard.DisplayItem"  .R�� $��5q� �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto �.+��� -   $��0��$��50��$��50��$��Uniform=� �� BitmapScalingMode$ HighQuality=����� *	img_res+�� $��1�� �� Orientation$ Vertical=�$��10,0,0,0q�.+��� ���� *display_name+�� ���� *display_price+�� ���� *display_price_slot+d� $��2��$��10,0,0,0q�$��24��$��24��$��0q� d� 	IsChecked �� *itemData.favorite+���� " d�.���  � ��
 ���� ��  � �� �� " d�.R���  ��Name$ imgFavorite��$'��!resources/icons/star_inactive.png��$ HighQuality=�	 ��  �� Value$
 True�� �� Binding �� *itemData.favorite+.a��� $ imgFavorite�� � ��$% resources/icons/star_active.png��d� -	   $��3��$��10,0,0,0q�$��24��$��24��$��0q� �� *itemData.selected+���� " d�.���  � ��
 ���� ��  � �� �� " d�.R��� $ 
imgChecked��$&�� resources/icons/check_active.png��$ HighQuality=�	 �� $
 True�� �� *itemData.selected+.a��� $ 
imgChecked�� � ��$( "resources/icons/check_inactive.png���� $��Quests��#���   #���  .��� .+��� $��Stretch=�$��10q����� " ��.R��  �� $
��Auto ��� $��* �.+��� $��0��$��Center=� K� IsItemsHost . �� $��1��$��SelectedContent��.@��� ���� $&�� resources/eft/traders/Prapor.png��$��36��$��36��.���� -
   $ QuestsListPrapor��$��1��$��
10,5,10,10q�$��Stretch=�#���   #���  ���� " ��.���  � ��$ Stretch=�����    LootWizard.DisplayQuest"  .R�� $��5q� �� $��* ��� $
��Auto � �� $��15 ��� $��* ��� $��15 �.+��� $��1��$ Vertical=�$��10,0,0,0q�.+��� $
��Bold"����� *	questName+�� $��1��$��1��$��Stretch=�#���   #���  ���� *RequiredItems+���� .R�� $��Stretch=� �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��36��$��36������ *ImageResource+�� $��1��$ Vertical=�$��5q�.+��� ���� *Name+�� ���� *QuantityNeededStr+� $��2�� �� $��* ��� $
��Auto �.+��� $��Right=�$��0��$ 
Horizontal=�$��5q�.+���  ��Name$ QuestItemDec��-   #��[ $��25��$��25��.���� $$��resources/icons/down-arrow.png��$ HighQuality=��� $
��10,5q����� *QuantityFound+�� $ QuestItemInc��-   #��[ $��25��$��25��.���� $&�� resources/icons/upward-arrow.png��$ HighQuality=�d� -   $��Right=�$��1��$��10,0,0,0q�$��24��$��24��$��0q� �� *
Selected+���� " d�.���  � ��
 ���� ��  � �� �� " d�.R��� $ 
imgChecked��$&�� resources/icons/check_active.png��$ HighQuality=�	 �� $
 True�� �� *
Selected+.a��� $ 
imgChecked�� � ��$( "resources/icons/check_inactive.png���� ���� $)��#resources/eft/traders/Therapist.png��$��36��$��36��.���� -   $ QuestsListTherapist��$��1��$��
10,5,10,10q�$��Stretch=�#���   #���  ���� " ��.���  � ��$ Stretch=����� "  .R�� $��5q� �� $��* ��� $
��Auto � �� $��15 ��� $��* ��� $��15 �.+��� $��1��$ Vertical=�$��10,0,0,0q�.+��� $
��Bold"����� *	questName+�� $��1��$��1��$��Stretch=�#���   #���  ���� *RequiredItems+���� .R�� $��Stretch=� �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��36��$��36������ *ImageResource+�� $��1��$ Vertical=�$��5q�.+��� ���� *Name+�� ���� *QuantityNeededStr+� $��2�� �� $��* ��� $
��Auto �.+��� $��Right=�$��0��$ 
Horizontal=�$��5q�.+��� $ QuestItemDec��-   #��[ $��25��$��25��.���� $$��resources/icons/down-arrow.png��$ HighQuality=��� $
��10,5q����� *QuantityFound+�� $ QuestItemInc��-   #��[ $��25��$��25��.���� $&�� resources/icons/upward-arrow.png��$ HighQuality=�d� -   $��Right=�$��1��$��10,0,0,0q�$��24��$��24��$��0q� �� *
Selected+���� " d�.���  � ��
 ���� ��  � �� �� " d�.R��� $ 
imgChecked��$&�� resources/icons/check_active.png��$ HighQuality=�	 �� $
 True�� �� *
Selected+.a��� $ 
imgChecked�� � ��$( "resources/icons/check_inactive.png���� ���� $%��resources/eft/traders/Skier.png��$��36��$��36��.���� -   $ QuestsListSkier��$��1��$��
10,5,10,10q�$��Stretch=�#���   #���  ���� " ��.���  � ��$ Stretch=����� "  .R�� $��5q� �� $��* ��� $
��Auto � �� $��15 ��� $��* ��� $��15 �.+��� $��1��$ Vertical=�$��10,0,0,0q�.+��� $
��Bold"����� *	questName+�� $��1��$��1��$��Stretch=�#���   #���  ���� *RequiredItems+���� .R�� $��Stretch=� �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��36��$��36������ *ImageResource+�� $��1��$ Vertical=�$��5q�.+��� ���� *Name+�� ���� *QuantityNeededStr+� $��2�� �� $��* ��� $
��Auto �.+��� $��Right=�$��0��$ 
Horizontal=�$��5q�.+��� $ QuestItemDec��-   #��[ $��25��$��25��.���� $$��resources/icons/down-arrow.png��$ HighQuality=��� $
��10,5q����� *QuantityFound+�� $ QuestItemInc��-   #��[ $��25��$��25��.���� $&�� resources/icons/upward-arrow.png��$ HighQuality=�d� -   $��Right=�$��1��$��10,0,0,0q�$��24��$��24��$��0q� �� *
Selected+���� " d�.���  � ��
 ���� ��  � �� �� " d�.R��� $ 
imgChecked��$&�� resources/icons/check_active.png��$ HighQuality=�	 �� $
 True�� �� *
Selected+.a��� $ 
imgChecked�� � ��$( "resources/icons/check_inactive.png���� ���� $%��resources/eft/traders/Fence.png��$��36��$��36��.���� -   $ QuestsListFence��$��1��$��
10,5,10,10q�$��Stretch=�#���   #���  ���� " ��.���  � ��$ Stretch=����� "  .R�� $��5q� �� $��* ��� $
��Auto � �� $��15 ��� $��* ��� $��15 �.+��� $��1��$ Vertical=�$��10,0,0,0q�.+��� $
��Bold"����� *	questName+�� $��1��$��1��$��Stretch=�#���   #���  ���� *RequiredItems+���� .R�� $��Stretch=� �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��36��$��36������ *ImageResource+�� $��1��$ Vertical=�$��5q�.+��� ���� *Name+�� ���� *QuantityNeededStr+� $��2�� �� $��* ��� $
��Auto �.+��� $��Right=�$��0��$ 
Horizontal=�$��5q�.+��� $ QuestItemDec��-   #��[ $��25��$��25��.���� $$��resources/icons/down-arrow.png��$ HighQuality=��� $
��10,5q����� *QuantityFound+�� $ QuestItemInc��-   #��[ $��25��$��25��.���� $&�� resources/icons/upward-arrow.png��$ HighQuality=�d� -   $��Right=�$��1��$��10,0,0,0q�$��24��$��24��$��0q� �� *
Selected+���� " d�.���  � ��
 ���� ��  � �� �� " d�.R��� $ 
imgChecked��$&�� resources/icons/check_active.png��$ HighQuality=�	 �� $
 True�� �� *
Selected+.a��� $ 
imgChecked�� � ��$( "resources/icons/check_inactive.png���� ���� $+��%resources/eft/traders/Peacekeeper.png��$��36��$��36��.���� -   $ QuestsListPeacekeeper��$��1��$��
10,5,10,10q�$��Stretch=�#���   #���  ���� " ��.���  � ��$ Stretch=����� "  .R�� $��5q� �� $��* ��� $
��Auto � �� $��15 ��� $��* ��� $��15 �.+��� $��1��$ Vertical=�$��10,0,0,0q�.+��� $
��Bold"����� *	questName+�� $��1��$��1��$��Stretch=�#���   #���  ���� *RequiredItems+���� .R�� $��Stretch=� �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��36��$��36������ *ImageResource+�� $��1��$ Vertical=�$��5q�.+��� ���� *Name+�� ���� *QuantityNeededStr+� $��2�� �� $��* ��� $
��Auto �.+��� $��Right=�$��0��$ 
Horizontal=�$��5q�.+��� $ QuestItemDec��-   #��[ $��25��$��25��.���� $$��resources/icons/down-arrow.png��$ HighQuality=��� $
��10,5q����� *QuantityFound+�� $ QuestItemInc��-   #��[ $��25��$��25��.���� $&�� resources/icons/upward-arrow.png��$ HighQuality=�d� -   $��Right=�$��1��$��10,0,0,0q�$��24��$��24��$��0q� �� *
Selected+���� " d�.���  � ��
 ���� ��  � �� �� " d�.R��� $ 
imgChecked��$&�� resources/icons/check_active.png��$ HighQuality=�	 �� $
 True�� �� *
Selected+.a��� $ 
imgChecked�� � ��$( "resources/icons/check_inactive.png���� ���� $(��"resources/eft/traders/Mechanic.png��$��36��$��36��.���� -   $ QuestsListMechanic��$��1��$��
10,5,10,10q�$��Stretch=�#���   #���  ���� " ��.���  � ��$ Stretch=����� "  .R�� $��5q� �� $��* ��� $
��Auto � �� $��15 ��� $��* ��� $��15 �.+��� $��1��$ Vertical=�$��10,0,0,0q�.+��� $
��Bold"����� *	questName+�� $��1��$��1��$��Stretch=�#���   #���  ���� *RequiredItems+���� .R�� $��Stretch=� �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��36��$��36������ *ImageResource+�� $��1��$ Vertical=�$��5q�.+��� ���� *Name+�� ���� *QuantityNeededStr+� $��2�� �� $��* ��� $
��Auto �.+��� $��Right=�$��0��$ 
Horizontal=�$��5q�.+��� $ QuestItemDec��-   #��[ $��25��$��25��.���� $$��resources/icons/down-arrow.png��$ HighQuality=��� $
��10,5q����� *QuantityFound+�� $ QuestItemInc��-    #��[ $��25��$��25��.���� $&�� resources/icons/upward-arrow.png��$ HighQuality=�d� -!   $��Right=�$��1��$��10,0,0,0q�$��24��$��24��$��0q� �� *
Selected+���� " d�.���  � ��
 ���� ��  � �� �� " d�.R��� $ 
imgChecked��$&�� resources/icons/check_active.png��$ HighQuality=�	 �� $
 True�� �� *
Selected+.a��� $ 
imgChecked�� � ��$( "resources/icons/check_inactive.png���� ���� $&�� resources/eft/traders/Ragman.png��$��36��$��36��.���� -"   $ QuestsListRagman��$��1��$��
10,5,10,10q�$��Stretch=�#���   #���  ���� " ��.���  � ��$ Stretch=����� "  .R�� $��5q� �� $��* ��� $
��Auto � �� $��15 ��� $��* ��� $��15 �.+��� $��1��$ Vertical=�$��10,0,0,0q�.+��� $
��Bold"����� *	questName+�� $��1��$��1��$��Stretch=�#���   #���  ���� *RequiredItems+���� .R�� $��Stretch=� �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��36��$��36������ *ImageResource+�� $��1��$ Vertical=�$��5q�.+��� ���� *Name+�� ���� *QuantityNeededStr+� $��2�� �� $��* ��� $
��Auto �.+��� $��Right=�$��0��$ 
Horizontal=�$��5q�.+��� $ QuestItemDec��-#   #��[ $��25��$��25��.���� $$��resources/icons/down-arrow.png��$ HighQuality=��� $
��10,5q����� *QuantityFound+�� $ QuestItemInc��-$   #��[ $��25��$��25��.���� $&�� resources/icons/upward-arrow.png��$ HighQuality=�d� -%   $��Right=�$��1��$��10,0,0,0q�$��24��$��24��$��0q� �� *
Selected+���� " d�.���  � ��
 ���� ��  � �� �� " d�.R��� $ 
imgChecked��$&�� resources/icons/check_active.png��$ HighQuality=�	 �� $
 True�� �� *
Selected+.a��� $ 
imgChecked�� � ��$( "resources/icons/check_inactive.png���� ���� $&�� resources/eft/traders/Jaeger.png��$��36��$��36��.���� -&   $ QuestsListJaeger��$��1��$��
10,5,10,10q�$��Stretch=�#���   #���  ���� " ��.���  � ��$ Stretch=����� "  .R�� $��5q� �� $��* ��� $
��Auto � �� $��15 ��� $��* ��� $��15 �.+��� $��1��$ Vertical=�$��10,0,0,0q�.+��� $
��Bold"����� *	questName+�� $��1��$��1��$��Stretch=�#���   #���  ���� *RequiredItems+���� .R�� $��Stretch=� �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��36��$��36������ *ImageResource+�� $��1��$ Vertical=�$��5q�.+��� ���� *Name+�� ���� *QuantityNeededStr+� $��2�� �� $��* ��� $
��Auto �.+��� $��Right=�$��0��$ 
Horizontal=�$��5q�.+��� $ QuestItemDec��-'   #��[ $��25��$��25��.���� $$��resources/icons/down-arrow.png��$ HighQuality=��� $
��10,5q����� *QuantityFound+�� $ QuestItemInc��-(   #��[ $��25��$��25��.���� $&�� resources/icons/upward-arrow.png��$ HighQuality=�d� -)   $��Right=�$��1��$��10,0,0,0q�$��24��$��24��$��0q� �� *
Selected+���� " d�.���  � ��
 ���� ��  � �� �� " d�.R��� $ 
imgChecked��$&�� resources/icons/check_active.png��$ HighQuality=�	 �� $
 True�� �� *
Selected+.a��� $ 
imgChecked�� � ��$( "resources/icons/check_inactive.png���� ���� $+��%resources/eft/traders/Lightkeeper.png��$��36��$��36��.���� -*   $ QuestsListLightkeeper��$��1��$��
10,5,10,10q�$��Stretch=�#���   #���  ���� " ��.���  � ��$ Stretch=����� "  .R�� $��5q� �� $��* ��� $
��Auto � �� $��15 ��� $��* ��� $��15 �.+��� $��1��$ Vertical=�$��10,0,0,0q�.+��� $
��Bold"����� *	questName+�� $��1��$��1��$��Stretch=�#���   #���  ���� *RequiredItems+���� .R�� $��Stretch=� �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��36��$��36������ *ImageResource+�� $��1��$ Vertical=�$��5q�.+��� ���� *Name+�� ���� *QuantityNeededStr+� $��2�� �� $��* ��� $
��Auto �.+��� $��Right=�$��0��$ 
Horizontal=�$��5q�.+��� $ QuestItemDec��-+   #��[ $��25��$��25��.���� $$��resources/icons/down-arrow.png��$ HighQuality=��� $
��10,5q����� *QuantityFound+�� $ QuestItemInc��-,   #��[ $��25��$��25��.���� $&�� resources/icons/upward-arrow.png��$ HighQuality=�d� --   $��Right=�$��1��$��10,0,0,0q�$��24��$��24��$��0q� �� *
Selected+���� " d�.���  � ��
 ���� ��  � �� �� " d�.R��� $ 
imgChecked��$&�� resources/icons/check_active.png��$ HighQuality=�	 �� $
 True�� �� *
Selected+.a��� $ 
imgChecked�� � ��$( "resources/icons/check_inactive.png���� ���� $#��resources/eft/traders/Ref.png��$��36��$��36��.���� -.   $ QuestsListRef��$��1��$��
10,5,10,10q�$��Stretch=�#���   #���  ���� " ��.���  � ��$ Stretch=����� "  .R�� $��5q� �� $��* ��� $
��Auto � �� $��15 ��� $��* ��� $��15 �.+��� $��1��$ Vertical=�$��10,0,0,0q�.+��� $
��Bold"����� *	questName+�� $��1��$��1��$��Stretch=�#���   #���  ���� *RequiredItems+���� .R�� $��Stretch=� �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��36��$��36������ *ImageResource+�� $��1��$ Vertical=�$��5q�.+��� ���� *Name+�� ���� *QuantityNeededStr+� $��2�� �� $��* ��� $
��Auto �.+��� $��Right=�$��0��$ 
Horizontal=�$��5q�.+��� $ QuestItemDec��-/   #��[ $��25��$��25��.���� $$��resources/icons/down-arrow.png��$ HighQuality=��� $
��10,5q����� *QuantityFound+�� $ QuestItemInc��-0   #��[ $��25��$��25��.���� $&�� resources/icons/upward-arrow.png��$ HighQuality=�d� -1   $��Right=�$��1��$��10,0,0,0q�$��24��$��24��$��0q� �� *
Selected+���� " d�.���  � ��
 ���� ��  � �� �� " d�.R��� $ 
imgChecked��$&�� resources/icons/check_active.png��$ HighQuality=�	 �� $
 True�� �� *
Selected+.a��� $ 
imgChecked�� � ��$( "resources/icons/check_inactive.png���� $��Settings��#���   #���  .��� $��10q� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� -2   $��Toggle Dark Mode��$��4��$��20��$��5q�#���   #���  �� $��Change Output Path:��$��0��$��5q��� -3   $ txtOutputPath��$��1��$��5q�#���   #���  �� -4   $��Change Path��$��2��$��5q�#���   #���  �� -5     ��Name$  txtFeedback��$��3��$��5q�
���  ��#���   