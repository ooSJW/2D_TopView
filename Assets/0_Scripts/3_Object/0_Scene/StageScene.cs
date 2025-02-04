using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StageScene : BaseScene // Data Field
{

}
public partial class StageScene : BaseScene // Initialize
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
        MainSystem.Instance.GameManager.SignUpGameController(GameController);
        MainSystem.Instance.ItemManager.SignUpItemController(ItemController);
        MainSystem.Instance.UIManager.SignUpUIController(UIContoller);

        PlayerName playerName = MainSystem.Instance.PlayerManager.PlayerName;
        Player = MainSystem.Instance.PoolManager.Spawn(playerName.ToString()).GetComponent<Player>();
        MainSystem.Instance.PlayerManager.SignUpPlayer(Player);

        MainSystem.Instance.StageManager.SignupSpawner(Spawner);
        MainSystem.Instance.TileMapManager.SignUpTileMap(TileMaps);

        MainSystem.Instance.WeaponManager.SignUpWeaponController(WeaponController);
        WeaponRoot = Player.transform.Find("WeaponRoot").GetComponent<WeaponRoot>();

        MainSystem.Instance.WeaponManager.WeaponController.SignUpWeaponRoot(WeaponRoot);
        MainSystem.Instance.SoundManager.SignUpSoundController(SoundController);
        VirtualCamara.Initialize();
    }
}
