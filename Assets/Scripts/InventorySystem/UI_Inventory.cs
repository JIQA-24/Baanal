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
    private PlayerMovement player;

    private void Awake()
    {
        //itemSlotContainer = transform.Find("ItemSlotContainer");
        //itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetPlayer(PlayerMovement player)
    {
        this.player = player;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChange += Inventory_OnItemListChanged;

        RefreshInventoryItems();
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
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 30f;
        RectTransform itemSlotRectTransform;
        List<Item> itemList = inventory.GetItemList();
        int itemListLength = itemList.Count;
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
                    inventory.UseItem(item);
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
