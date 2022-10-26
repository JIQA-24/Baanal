using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        ChaacMask,
        JaguarMask,
        UnequipedMask,
        UnequipedTalisman,
        JaguarTalisman,
        AguilaTalisman,
    }

    public ItemType itemType;
    public int weaponChangeNum;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.ChaacMask: return ItemAssets.Instance.chaacMaskSprite;
            case ItemType.JaguarMask: return ItemAssets.Instance.jaguarMaskSprite;
            case ItemType.UnequipedMask: return ItemAssets.Instance.unequipedMask;
            case ItemType.UnequipedTalisman: return ItemAssets.Instance.unequipedTalisman;
            case ItemType.JaguarTalisman: return ItemAssets.Instance.jaguarTalismanSprite;
            case ItemType.AguilaTalisman: return ItemAssets.Instance.aguilaTalismanSprite;
        }
    }

    public int GetEquipPos()
    {
        switch (itemType)
        {
            default:
            case ItemType.ChaacMask: return 0;
            case ItemType.JaguarMask: return 0;
            case ItemType.AguilaTalisman: return 1;
            case ItemType.JaguarTalisman: return 1;
        }
    }
}
