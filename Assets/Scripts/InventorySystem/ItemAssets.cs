using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if(sceneName == "Tutorial")
        {
            ItemWorld.SpawnItemWorld(new Vector3(95, 20), new Item { itemType = Item.ItemType.ChaacMask, weaponChangeNum = 1 });
            ItemWorld.SpawnItemWorld(new Vector3(135, 20), new Item { itemType = Item.ItemType.JaguarMask, weaponChangeNum = 2 });
            ItemWorld.SpawnItemWorld(new Vector3(155, 20), new Item { itemType = Item.ItemType.JaguarTalisman, weaponChangeNum = 1 });
            ItemWorld.SpawnItemWorld(new Vector3(175, 20), new Item { itemType = Item.ItemType.AguilaTalisman, weaponChangeNum = 2 });
        } else
        {
            ItemWorld.SpawnItemWorld(new Vector3(20, 20), new Item { itemType = Item.ItemType.ChaacMask, weaponChangeNum = 1 });
            ItemWorld.SpawnItemWorld(new Vector3(-20, 20), new Item { itemType = Item.ItemType.JaguarMask, weaponChangeNum = 2 });
            ItemWorld.SpawnItemWorld(new Vector3(-10, 10), new Item { itemType = Item.ItemType.JaguarTalisman, weaponChangeNum = 1 });
            ItemWorld.SpawnItemWorld(new Vector3(10, 10), new Item { itemType = Item.ItemType.AguilaTalisman, weaponChangeNum = 2 });
        }
    }


    public Transform pfItemWorld;

    public Sprite chaacMaskSprite;
    public Sprite jaguarMaskSprite;
    public Sprite unequipedMask;
    public Sprite unequipedTalisman;
    public Sprite jaguarTalismanSprite;
    public Sprite aguilaTalismanSprite;

}
