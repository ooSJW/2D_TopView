using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;
using static ItemData;

public partial class ItemInformationSlot : MonoBehaviour // Data Field
{
    private SelectItem selectItem;
    [SerializeField] private GameObject itemInfoRoot;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemLevelText;
}
public partial class ItemInformationSlot : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        itemInfoRoot.SetActive(false);
    }
}
public partial class ItemInformationSlot : MonoBehaviour // Property
{
    public void SetItemInformation(SelectItem itemValue = null)
    {
        selectItem = itemValue;
    }
    public void RefreshItemInfoUI()
    {
        if (selectItem == null)
        {
            itemInfoRoot.SetActive(false);
        }
        else
        {
            ItemInformation itemInfo = selectItem.ItemInformation;

            string iconPath = itemInfo.iconPath + itemInfo.itemIcon;
            string itemLevel = itemInfo.itemLevelText;
            itemLevel = itemLevel.Replace("@Level", selectItem.Level.ToString());
            itemLevel = itemLevel.Replace("@MaxLevel", itemInfo.maxLevel.ToString());

            itemIcon.sprite = Resources.Load<Sprite>(iconPath);
            itemLevelText.text = itemLevel;

            itemInfoRoot.SetActive(true);
        }
    }
}
