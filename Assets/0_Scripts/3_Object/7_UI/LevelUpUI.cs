using project02;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public partial class LevelUpUI : MonoBehaviour // Data Field
{
    const int maxSpawnCount = 3;

    [SerializeField] private List<ItemName> spawnableItemNameList;
    private Queue<Action> openLevelupUIQueue;
    private List<GameObject> emptySlotList;
}
public partial class LevelUpUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        emptySlotList = new List<GameObject>();
        openLevelupUIQueue = new Queue<Action>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        SpawnAllSelectItem();
        gameObject.SetActive(false);
    }
}
public partial class LevelUpUI : MonoBehaviour // Initialize
{
    private void SpawnAllSelectItem()
    {
        for (int i = 0; i < spawnableItemNameList.Count; i++)
        {
            SelectItem item = MainSystem.Instance.PoolManager.Spawn("SelectItem", transform).GetComponent<SelectItem>();
            RectTransform itemRectTransform = item.GetComponent<RectTransform>();
            itemRectTransform.localScale = Vector3.one;
            MainSystem.Instance.ItemManager.ItemController.SelectItemList.Add(item);
            item.Initialize(spawnableItemNameList[i]);
            item.gameObject.SetActive(false);
        }
        for (int i = 0; i < maxSpawnCount; i++)
        {
            GameObject slot = MainSystem.Instance.PoolManager.Spawn("EmptySlot", transform);
            RectTransform slotRectTransform = slot.GetComponent<RectTransform>();
            slotRectTransform.localScale = Vector3.one;
            emptySlotList.Add(slot);
            slot.gameObject.SetActive(false);
        }
    }
    public void PlayerLevelUp()
    {
        if (!gameObject.activeSelf)
            OpenLevelUpUI();
        else
            openLevelupUIQueue.Enqueue(OpenLevelUpUI);
    }
    public void OpenLevelUpUI()
    {
        List<SelectItem> selectItemList = MainSystem.Instance.ItemManager.ItemController.SelectItemList;
        List<SelectItem> validItemList = selectItemList.Where(item => item.Level < item.ItemInformation.maxLevel).ToList();

        if (validItemList.Count == 0)
            return;
        else if (IsFirstSelect())
            FirstSelect();
        else
        {
            int spawnableCount = validItemList.Count <= maxSpawnCount ? validItemList.Count : maxSpawnCount;

            List<SelectItem> randomItemList = validItemList.OrderBy(elem => UnityEngine.Random.value).Take(spawnableCount).ToList();

            for (int i = 0; i < spawnableCount; i++)
            {
                randomItemList[i].RefreshSelectItemUI();
                randomItemList[i].gameObject.SetActive(true);
            }
            if (spawnableCount < maxSpawnCount)
            {
                for (int i = 0; i < maxSpawnCount - spawnableCount; i++)
                {
                    emptySlotList[i].gameObject.SetActive(true);
                }
            }
        }
        MainSystem.Instance.GameManager.PauseGame();
        gameObject.SetActive(true);
        transform.SetAsLastSibling();

    }
    public void CloseLevelUpUI()
    {
        foreach (Transform child in transform)
        {
            if (child != null && child.gameObject.activeSelf)
                child.gameObject.SetActive(false);
        }
        MainSystem.Instance.GameManager.ResumeGame();
        gameObject.SetActive(false);

        if (openLevelupUIQueue.Count > 0)
            openLevelupUIQueue.Dequeue().Invoke();

    }
    private bool IsFirstSelect()
    {
        List<SelectItem> selectItemList = MainSystem.Instance.ItemManager.ItemController.SelectItemList;
        List<SelectItem> validItemList = selectItemList.Where(item => item.Level > 0).ToList();
        if (validItemList == null || validItemList.Count == 0)
            return true;

        return false;
    }
    private void FirstSelect()
    {
        List<SelectItem> selectItemList = MainSystem.Instance.ItemManager.ItemController.SelectItemList;
        List<SelectItem> weaponList = selectItemList.Where(elem => elem.ItemType == ItemType.Weapon).ToList();
        if (weaponList.Count > maxSpawnCount)
        {
            List<SelectItem> randomItemList = weaponList.OrderBy(elem => UnityEngine.Random.value).Take(maxSpawnCount).ToList();
            for (int i = 0; i < randomItemList.Count; i++)
                randomItemList[i].gameObject.SetActive(true);
        }
        else
        {
            int emptySlotCount = maxSpawnCount - weaponList.Count;
            for (int i = 0; i < weaponList.Count; i++)
                weaponList[i].gameObject.SetActive(true);
            for (int i = 0; i < emptySlotCount; i++)
                emptySlotList[i].gameObject.SetActive(true);
        }
    }
}
