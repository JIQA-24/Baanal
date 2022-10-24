using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite chaacMaskSprite;
    public Sprite jaguarMaskSprite;
    public Sprite unequipedMask;
    public Sprite unequipedTalisman;
    public Sprite jaguarTalismanSprite;
    public Sprite aguilaTalismanSprite;

}
