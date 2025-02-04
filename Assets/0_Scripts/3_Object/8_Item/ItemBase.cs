using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemData;

public partial class ItemBase : MonoBehaviour // Data Field
{

}
public partial class ItemBase : MonoBehaviour // Data Property
{
    protected ItemInformation itemInformation;
    public ItemInformation ItemInformation
    {
        get => itemInformation;
        protected set
        {
            itemInformation = new ItemInformation()
            {
                index = value.index,
                itemType = value.itemType,
                uiName = value.uiName,
                lootType = value.lootType,
                itemName = value.itemName,
                itemDescription = value.itemDescription,
                itemIcon = value.itemIcon,
                iconPath = value.iconPath,
                maxLevel = value.maxLevel,
                itemValue = value.itemValue,
                itemLevelText = value.itemLevelText,
            };
        }
    }

    protected ItemType itemType;
    public ItemType ItemType { get => itemType; protected set => itemType = value; }

    protected ItemName itemName;
    public ItemName ItemName { get => itemName; protected set => itemName = value; }
}

public partial class ItemBase : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public virtual void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
