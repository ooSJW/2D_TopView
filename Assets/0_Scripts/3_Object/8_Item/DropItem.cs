using project02;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DropItem : ItemBase // Data Field
{
    [SerializeField] private MagnetMove magnetMove;
}
public partial class DropItem : ItemBase // Initialize
{
    private void Allocate()
    {
        ItemName = Enum.Parse<ItemName>(name);
        ItemInformation = MainSystem.Instance.DataManager.ItemData.GetData(ItemName);
        ItemType = Enum.Parse<ItemType>(ItemInformation.itemType);
    }
    public override void Initialize()
    {
        base.Initialize();

        Allocate();
        Setup();
    }
    private void Setup()
    {
        magnetMove.enabled = false;

    }
}
public partial class DropItem : ItemBase // Property
{
    public void Magnet()
    {
        if (ItemType == ItemType.Exp)
        {
            magnetMove.Initialize();
            magnetMove.enabled = true;
        }
    }
}

public partial class DropItem : ItemBase // Trigger Event
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            AudioClipName clipName = AudioClipName.GetExp;
            switch (ItemType)
            {
                case ItemType.Exp:
                    player.Exp += (int)ItemInformation.itemValue;
                    break;
                case ItemType.Potion:
                    player.HP += (int)ItemInformation.itemValue;
                    clipName = AudioClipName.GetPotion;
                    break;
                case ItemType.Magnet:
                    MainSystem.Instance.ItemManager.ItemController.Magnet();
                    clipName = AudioClipName.GetMagnet;
                    break;
            }
            MainSystem.Instance.SoundManager.SoundController.SoundEffect.PlaySfx(clipName);
            MainSystem.Instance.ItemManager.ItemController.DespawnDropItem(this);
        }
    }
}
