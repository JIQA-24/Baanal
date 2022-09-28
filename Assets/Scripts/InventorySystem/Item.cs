using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        ChaacMask,
        JaguarMask,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.ChaacMask: return ItemAssets.Instance.chaacMaskSprite;
            case ItemType.JaguarMask: return ItemAssets.Instance.jaguarMaskSprite;
        }
    }
}
