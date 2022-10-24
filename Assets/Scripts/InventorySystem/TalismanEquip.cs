using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalismanEquip : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] PlayerMovement player;
    public Inventory inventory;
    public List<Item> equipedItems;
    private int boostcount = 0;
    private int equipedTalisman;
    // Start is called before the first frame update
    void Start()
    {
        inventory = uiInventory.GetInventory();
    }

    // Update is called once per frame
    void Update()
    {
        equipedItems = inventory.GetEquipedList();
        equipedTalisman = equipedItems[1].weaponChangeNum;
        ChangeTalisman();
    }

    public void ChangeTalisman()
    {
        switch (equipedTalisman)
        {
            default:
            case 0:
                break;
            case 1:
                if(boostcount < 1)
                {
                    player.AddBoost(0.2f);
                    boostcount++;
                }
                break;
            case 2:
                player.DoubleOn();
                break;
        }
    }

    public void ResetChanges()
    {
        equipedTalisman = 0;
        player.RemoveBoost();
        player.DoubleOff();
        boostcount = 0;
    }
}
