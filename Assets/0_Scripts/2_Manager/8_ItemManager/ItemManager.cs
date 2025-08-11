using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ItemManager : MonoBehaviour // Data Field
{
	public ItemController ItemController { get; private set; }
}
public partial class ItemManager : MonoBehaviour // Initialize
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

	}
}
public partial class ItemManager : MonoBehaviour // Sign
{
	public void SignUpItemController(ItemController itemControllerValue)
	{
		ItemController = itemControllerValue;
		ItemController.Initialize();
	}
	public void SignOutItgemController()
	{
		ItemController = null;
	}
}
