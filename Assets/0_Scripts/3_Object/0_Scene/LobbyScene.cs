using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LobbyScene : BaseScene // Data Field
{

}
public partial class LobbyScene : BaseScene // Initialize
{
    private void Allocate()
    {

    }
    public override void Initialize()
    {
        base.Initialize();

        Allocate();
        Setup();
    }
    private void Setup()
    {
        MainSystem.Instance.PoolManager.Register();
        MainSystem.Instance.UIManager.SignUpUIController(UIContoller);
        MainSystem.Instance.SoundManager.SignUpSoundController(SoundController);
    }
}
