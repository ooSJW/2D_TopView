using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameController : MonoBehaviour // Data Field
{
    private bool isPause = false;
    public bool IsPause
    {
        get => isPause;
        private set
        {
            isPause = value;
            UIContoller controller = MainSystem.Instance.UIManager.UIContoller;
            if (controller)
                controller.IsPause = isPause;
            MainSystem.Instance.PlayerManager.Player.Moveable = !IsPause;
        }
    }

    private float survivalTime;
    public float SurvivalTime { get => survivalTime; private set => survivalTime = value; }

    public int GoldFromThisRaid { get; set; }

    private bool isClear = false;
    public bool IsClear
    {
        get => isClear;
        set
        {
            isClear = value;
            if (isClear)
            {
                StageIndex stageIndex = MainSystem.Instance.StageManager.Spawner.StageIndex;
                MainSystem.Instance.DataManager.SaveClearStage(stageIndex);
                StartCoroutine(DespawnAllEnemy());
            }
        }
    }
}

public partial class GameController : MonoBehaviour // Initialize
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
        GoldFromThisRaid = 0;
    }
}

public partial class GameController : MonoBehaviour // Main
{
    private void Update()
    {
        if (!IsClear && MainSystem.Instance.PlayerManager.Player.PlayerState != PlayerState.Death)
            survivalTime += Time.deltaTime;
    }
}
public partial class GameController : MonoBehaviour // Property
{
    public void SetIsPause()
    {
        if (Time.timeScale == 0)
            IsPause = true;
        else
            IsPause = false;
    }
    public void SaveGold()
    {
        MainSystem.Instance.GameManager.Gold += GoldFromThisRaid;
    }
}
public partial class GameController : MonoBehaviour // Coroutine
{
    private IEnumerator DespawnAllEnemy()
    {
        List<Enemy> enemyList = MainSystem.Instance.EnemyManager.AllFieldEnemyList;
        MainSystem.Instance.PoolManager.Spawn("StageClearEffect");
        MainSystem.Instance.SoundManager.SoundController.SoundEffect.PlaySfx(AudioClipName.Explosion);
        yield return new WaitForSeconds(2.2f);

        for (int i = enemyList.Count - 1; i > 0; i--)
        {
            if (enemyList[i])
                enemyList[i].ReceiveDamage(int.MaxValue);
        }
        yield return new WaitForSeconds(2f);

        MainSystem.Instance.SoundManager.SoundController.SoundEffect.PlaySfx(AudioClipName.StageClear);
        MainSystem.Instance.UIManager.UIContoller.GameOver();
        yield break;
    }
}
