using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour // Data Field
{
    public GameController GameController { get; private set; }

    private int gold;
    public int Gold
    {
        get => gold;
        set
        {
            if (gold != value)
            {
                gold = value;
                MainSystem.Instance.DataManager.SaveData();
                MainSystem.Instance.UIManager.UIContoller.PlayerGold = gold;
            }
        }
    }
}

public partial class GameManager : MonoBehaviour // Initialize
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
        gold = MainSystem.Instance.DataManager.LoadPlayerGold();
    }
}
public partial class GameManager : MonoBehaviour // Sign
{
    public void SignUpGameController(GameController gameController)
    {
        GameController = gameController;
        GameController.Initialize();
    }
    public void SignDownGameController()
    {
        GameController = null;
    }
}

public partial class GameManager : MonoBehaviour // Property
{
    public void PauseGame()
    {
        Time.timeScale = 0;
        GameController.SetIsPause();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameController.SetIsPause();
    }
}
