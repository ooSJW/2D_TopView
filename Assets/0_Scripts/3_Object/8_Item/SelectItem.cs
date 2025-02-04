using project02;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class SelectItem : ItemBase // Data Field
{
    [SerializeField] private Button button;
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemLevelText;

    private int level = 0;
    public int Level
    {
        get => level;
        private set
        {
            if (value <= ItemInformation.maxLevel)
            {
                level = value;
                LevelUp();
            }
        }
    }
}
public partial class SelectItem : ItemBase // Initialize
{
    private void Allocate()
    {
        ItemInformation = MainSystem.Instance.DataManager.ItemData.GetData(ItemName);
        ItemType = Enum.Parse<ItemType>(ItemInformation.itemType);
    }
    public void Initialize(ItemName itemNameValue)
    {
        ItemName = itemNameValue;
        base.Initialize();
        Allocate();
        Setup();
        RefreshSelectItemUI();
    }
    private void Setup()
    {
        button.onClick.AddListener(ButtonClick);
    }
}
public partial class SelectItem : ItemBase // Property
{
    public void RefreshSelectItemUI()
    {
        string iconPath = $"{ItemInformation.iconPath}{ItemInformation.itemIcon}";
        string itemDescription = ItemInformation.itemDescription;
        string itemLevel = ItemInformation.itemLevelText;

        itemDescription = itemDescription.Replace("@itemValue", (itemInformation.itemValue * 100).ToString("F0"));
        itemLevel = itemLevel.Replace("@Level", Level.ToString());
        itemLevel = itemLevel.Replace("@MaxLevel", ItemInformation.maxLevel.ToString());

        itemIconImage.sprite = Resources.Load<Sprite>(iconPath);
        itemNameText.text = ItemInformation.uiName;
        itemDescriptionText.text = itemDescription;
        itemLevelText.text = itemLevel;
    }

    private void LevelUp()
    {
        float increasePercent = ItemInformation.itemValue;
        switch (ItemType)
        {
            case ItemType.Weapon:
                WeaponPrefabName weaponPrefabName = Enum.Parse<WeaponPrefabName>(ItemName.ToString());
                MainSystem.Instance.WeaponManager.WeaponController.AddWeapon(weaponPrefabName);
                break;

            case ItemType.Shoes:
                MainSystem.Instance.PlayerManager.Player.GetShoes(increasePercent);
                break;

            case ItemType.Armor:
                MainSystem.Instance.PlayerManager.Player.GetArmor(increasePercent);
                break;

            case ItemType.Glove:
                MainSystem.Instance.PlayerManager.Player.GetGlove(increasePercent);
                break;
        }
    }

}
public partial class SelectItem : ItemBase // ButtonEvent
{
    private void ButtonClick()
    {
        Level++;

        List<SelectItem> itemList = MainSystem.Instance.ItemManager.ItemController.ValidSelectItemList;
        if (Level > 0 && !itemList.Contains(this))
            itemList.Add(this);

        MainSystem.Instance.UIManager.UIContoller.LevelUpUI.CloseLevelUpUI();
    }
}
