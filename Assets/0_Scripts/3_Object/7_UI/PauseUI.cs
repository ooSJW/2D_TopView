using project02;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public partial class PauseUI : MonoBehaviour // Data Field
{
    [SerializeField] private List<ItemInformationSlot> itemInformationSlotList;
}
public partial class PauseUI : MonoBehaviour // Initialize
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
        gameObject.SetActive(false);
        for (int i = 0; i < itemInformationSlotList.Count; i++)
            itemInformationSlotList[i].Initialize();
    }
}
public partial class PauseUI : MonoBehaviour // Property
{
    public void ReFreshPauseUI()
    {
        List<SelectItem> itemList = MainSystem.Instance.ItemManager.ItemController.ValidSelectItemList;
        if (itemList != null && itemList.Count > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemInformationSlotList[i] != null)
                {
                    SelectItem item = itemList[i];
                    itemInformationSlotList[i].SetItemInformation(item);
                    itemInformationSlotList[i].RefreshItemInfoUI();
                }
                else
                    return;
            }
        }
    }
}
