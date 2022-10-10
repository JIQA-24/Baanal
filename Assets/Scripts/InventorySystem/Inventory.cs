using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChange;
    private List<Item> itemList;
    public List<Item> equipedItems;
    private Action<Item> useItemAction;

    public Inventory()
    {
        itemList = new List<Item>();
        equipedItems = new List<Item>();

        StartEquipMenu(new Item { itemType = Item.ItemType.UnequipedMask, weaponChangeNum = 0});
        StartEquipMenu(new Item { itemType = Item.ItemType.UnequipedTalisman, weaponChangeNum = 0});
        //Debug.Log(itemList.Count);
    }

    public void EquipItem(Item item, int pos)
    {
        if(equipedItems[pos].itemType != Item.ItemType.UnequipedTalisman && equipedItems[pos].itemType != Item.ItemType.UnequipedMask)
        {
            itemList.Add(equipedItems[pos]);
            equipedItems[pos] = item;
            itemList.Remove(item);
        } else
        {
            equipedItems[pos] = item;
            itemList.Remove(item);
        }
        
        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }

    public void StartEquipMenu(Item item)
    {
        equipedItems.Add(item);
        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }

    public void UnequipItem(Item item, int pos)
    {
        if(item.itemType != Item.ItemType.UnequipedTalisman && item.itemType != Item.ItemType.UnequipedMask)
        {
            switch (pos)
            {
                default:
                case 0:
                    equipedItems[pos] = new Item { itemType = Item.ItemType.UnequipedMask, weaponChangeNum = 0 };
                    itemList.Add(item);
                    break;
                case 1:
                    equipedItems[pos] = new Item { itemType = Item.ItemType.UnequipedTalisman, weaponChangeNum = 0 };
                    itemList.Add(item);
                    break;
            }
            OnItemListChange?.Invoke(this, EventArgs.Empty);
        }
        
    }

    //public void UseItem(Item item)
    //{
    //    useItemAction(item);
    //}

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public List<Item> GetEquipedList()
    {
        return equipedItems;
    }
}
