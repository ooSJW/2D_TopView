using project02;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayerUI : MonoBehaviour // Data Field
{
    private Player player;
    public Player Player { get => player; set => player = value; }

    [SerializeField] private Slider playerExpSlider;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI playerKillCountText;
    [SerializeField] private TextMeshProUGUI playerSurvivalTimeText;
    [SerializeField] private PlayerHpBar playerHpBar;
}
public partial class PlayerUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();

        playerHpBar.Initialize();
    }
    private void Setup()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }
}
public partial class PlayerUI : MonoBehaviour // Main
{
    private void Update()
    {
        RefreshSurvivalTime();
        playerHpBar.RepositionHpBar(player.transform);
    }
}
public partial class PlayerUI : MonoBehaviour // Property
{
    private void RefreshSurvivalTime()
    {
        float survivalTime = MainSystem.Instance.GameManager.GameController.SurvivalTime;
        int minute = Mathf.FloorToInt(survivalTime / 60);
        int second = Mathf.FloorToInt(survivalTime % 60);

        playerSurvivalTimeText.text = string.Format("{0:00}:{1:00}", minute, second);
    }
    public void RefreshPlayerUI()
    {
        RefreshExp();
        RefreshLevel();
        RefreshHp();
        RefreshKillCount();
    }
    public void RefreshExp()
    {
        playerExpSlider.value = (float)player.Exp / player.MaxExp;
    }
    public void RefreshLevel()
    {
        playerLevelText.text = player.Level.ToString();
    }
    public void RefreshHp()
    {
        playerHpBar.RefreshHp(player.HP, player.MaxHp);
    }
    public void RefreshKillCount()
    {
        playerKillCountText.text = player.KillCount.ToString();
    }
}
