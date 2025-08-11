using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using static Cinemachine.DocumentationSortingAttribute;

public partial class ItemController : MonoBehaviour // Data Field
{
    private List<DropItem> allFieldItems;
    public List<SelectItem> SelectItemList { get; private set; }
    public List<SelectItem> ValidSelectItemList { get; private set; }
}
public partial class ItemController : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        allFieldItems = new List<DropItem>();
        SelectItemList = new List<SelectItem>();
        ValidSelectItemList = new List<SelectItem>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
public partial class ItemController : MonoBehaviour // Property
{
    public DropItem SpawnDropItem(string itemName, Transform parent = null, Vector2 position = default)
    {
        DropItem dropItem = MainSystem.Instance.PoolManager.Spawn(itemName, parent, position).GetComponent<DropItem>();
        dropItem.Initialize();
        allFieldItems.Add(dropItem);
        return dropItem;
    }
    public void DespawnDropItem(DropItem dropItem)
    {
        allFieldItems.Remove(dropItem);
        MainSystem.Instance.PoolManager.Despawn(dropItem.gameObject);
    }

    public void RegisterItem(SelectItem item)
    {
        if (item.Level > 0 && !ValidSelectItemList.Contains(item))
            ValidSelectItemList.Add(item);
    }

    public void Magnet()
    {
        for (int i = 0; i < allFieldItems.Count; i++)
        {
            allFieldItems[i].Magnet();
        }
    }
}
