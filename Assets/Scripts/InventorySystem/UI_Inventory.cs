using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.EventSystems;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] Shooter shooter;
    [SerializeField] TalismanEquip talisman;
    public List<Button> itemSlotsButtons;
    public List<Button> itemEquipSlotsButtons;
    private List<Item> itemList;
    private List<Item> equipList;


    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChange += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        itemList = inventory.GetItemList();
        equipList = inventory.GetEquipedList();
        int itemListLength = itemList.Count;
        int equipListLength = equipList.Count;

        for (int i = 0; i < 2; i++)
        {
            Item item = null;
            if(i < equipListLength)
            {
                item = equipList[i];
            }
            Image image = itemEquipSlotsButtons[i].GetComponent<Image>();
            if (item.itemType != Item.ItemType.UnequipedTalisman && item.itemType != Item.ItemType.UnequipedMask)
            {
                image.sprite = item.GetSprite();
                image.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                image.sprite = item.GetSprite();
                image.color = new Color32(0, 0, 0, 200);
            }
        }

        for (int i = 0; i < 8; i++)
        {
            Item item = null;
            if(i < itemListLength)
            {
                item = itemList[i];
            }
            Image image = itemSlotsButtons[i].GetComponent<Image>();
            if (item != null)
            {
                image.sprite = item.GetSprite();
                image.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                image.sprite = null;
            }

        }
        

    }

    public void InventoryButtonClicked(int buttonNum)
    {
        Item item = null;
        if (buttonNum < itemList.Count)
        {
            item = itemList[buttonNum];
        }
        if (Input.GetAxis("Submit") > 0 && item != null)
        {
            inventory.EquipItem(item, item.GetEquipPos());
            if (item.GetEquipPos() == 0)
            {
                shooter.fireArm = 0;
                shooter.changeOfInventory();
            }
            if(item.GetEquipPos() == 1)
            {
                talisman.ResetChanges();
                talisman.ChangeTalisman();
            }
        }else if (Input.GetAxis("Submit") < 0 && item != null)
        {
            inventory.RemoveItem(item);
            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            ItemWorld.DropItem(playerPos, item);
        }
    }

    public void EquipButtonClicked(int buttonNum)
    {
        Item item = null;
        if (buttonNum < equipList.Count)
        {
            item = equipList[buttonNum];
        }
        if (Input.GetAxis("Submit") < 0 && item != null)
        {
            inventory.UnequipItem(item, item.GetEquipPos());
            if (item.GetEquipPos() == 0)
            {
                shooter.fireArm = 0;
                shooter.changeOfInventory();
            }
            if (item.GetEquipPos() == 1)
            {
                talisman.ResetChanges();
                talisman.ChangeTalisman();
            }
        }
    }
}
