using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class SoundManager : MonoBehaviour // Data Field
{
	public SoundController SoundController { get; private set; }
}
public partial class SoundManager : MonoBehaviour // Initialize
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
public partial class SoundManager : MonoBehaviour // Sign
{
	public void SignUpSoundController(SoundController soundControllerValue)
	{
		SoundController = soundControllerValue;
		SoundController.Initialize();
	}
	public void SignOutSoundController()
	{
		SoundController = null;
	}
}
