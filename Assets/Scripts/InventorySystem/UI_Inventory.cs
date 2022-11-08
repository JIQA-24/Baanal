using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
using CodeMonkey.Utils;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;
    [SerializeField] private Transform itemEquipContainer;
    [SerializeField] private Transform itemEquipTemplate;
    [SerializeField] Shooter shooter;
    [SerializeField] TalismanEquip talisman;

    private void Awake()
    {
        //itemSlotContainer = transform.Find("ItemSlotContainer");
        //itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }


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
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (Transform child in itemEquipContainer)
        {
            if (child == itemEquipTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 30f;
        RectTransform itemSlotRectTransform;
        RectTransform itemEquipRectTransform;
        List<Item> itemList = inventory.GetItemList();
        List<Item> equipList = inventory.GetEquipedList();
        int itemListLength = itemList.Count;
        int equipListLength = equipList.Count;
        for (int i = 0; i < 2; i++)
        {

            Item item = null;
            if (i < equipListLength)
            {
                item = equipList[i];
            }
            itemEquipRectTransform = Instantiate(itemEquipTemplate, itemEquipContainer).GetComponent<RectTransform>();

            if (item != null)
            {
                itemEquipRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
                {
                    ////Use item
                    //inventory.EquipItem(item, i);
                };
                itemEquipRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
                {
                    //Drop item
                    inventory.UnequipItem(item, item.GetEquipPos());
                    if(item.GetEquipPos() == 0)
                    {
                        shooter.fireArm = 0;
                        shooter.changeOfInventory();
                    }
                    if (item.GetEquipPos() == 1)
                    {
                        talisman.ResetChanges();
                        talisman.ChangeTalisman();
                    }
                };
            }

            itemEquipRectTransform.gameObject.SetActive(true);
            itemEquipRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemEquipRectTransform.Find("Image").GetComponent<Image>();

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

            y -= 5;

        }

        x = 0;
        y = 0;

        for(int i = 0; i < 8; i++)
        {
            Item item = null;
            if(i < itemListLength)
            {
                item = itemList[i];
            }
            itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();

            if (item != null)
            {
                itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
                {
                    //Use item
                    inventory.EquipItem(item, item.GetEquipPos());
                    if(item.GetEquipPos() == 0)
                    {
                        shooter.fireArm = 0;
                        shooter.changeOfInventory();
                    }
                    if(item.GetEquipPos() == 1)
                    {
                        talisman.ResetChanges();
                        talisman.ChangeTalisman();
                    }
                };
                itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
                {
                    //Drop item
                    inventory.RemoveItem(item);
                    Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                    ItemWorld.DropItem(playerPos, item);
                };
            }

            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();

            if(item != null)
            {
                image.sprite = item.GetSprite();
                image.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                GameObject gOImage = itemSlotRectTransform.Find("Image").gameObject;
                gOImage.SetActive(false);
                image.sprite = null;
            }

            x += 6;
            if(x >= 24)
            {
                x = 0;
                y -= 7;
            }
        }
    }
}
