using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static project02.StageData;

public partial class SelectStageUI : MonoBehaviour // DataField
{
    [SerializeField] private Transform selectStageUIRoot;
    private StageSlot[] slotArray;
}
public partial class SelectStageUI : MonoBehaviour // Inialize
{
    private void Allocate()
    {
        slotArray = new StageSlot[MainSystem.Instance.DataManager.StageData.GetStageInformationCount()];
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        for (int i = 0; i < slotArray.Length; i++)
        {
            slotArray[i] = MainSystem.Instance.PoolManager.Spawn("StageSlot", selectStageUIRoot).GetComponent<StageSlot>();
            StageInformation stageInfo = MainSystem.Instance.DataManager.StageData.GetData(i.ToString());
            slotArray[i].Initialize(stageInfo);
        }
        gameObject.SetActive(false);
    }
}
