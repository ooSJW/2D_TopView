using project02;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static project02.PlayerStatData;

public partial class ItemData // Data Field
{
    [System.Serializable]
    public class ItemInformation : BaseInformation
    {
        public string itemType;
        public string uiName;
        public string lootType;
        public string itemName;
        public string itemDescription;
        public string itemIcon;
        public string iconPath;
        public string itemLevelText;

        public int maxLevel;
        public float itemValue;
    }
}
public partial class ItemData // Data Field
{
    private Dictionary<string, ItemInformation> itemInfoDict = default;
}
public partial class ItemData // Initialize
{
    private void Allocate()
    {
        itemInfoDict = new Dictionary<string, ItemInformation>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        MainSystem.Instance.DataManager.SetUpData<ItemInformation>(itemInfoDict, "ItemData");
    }
}
public partial class ItemData // Property
{
    public ItemInformation GetData(string index)
    {
        return itemInfoDict[index];
    }
    public ItemInformation GetData(ItemName itemName)
    {
        ItemInformation itemInformation = itemInfoDict.Values.
            FirstOrDefault(itemInfo => itemInfo.itemName == itemName.ToString());

        return itemInformation;
    }
}
