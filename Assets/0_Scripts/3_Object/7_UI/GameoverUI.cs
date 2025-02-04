using project02;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public partial class GameoverUI : MonoBehaviour // DataField
{
    [SerializeField] private TextMeshProUGUI gameStateText;
    [SerializeField] private TextMeshProUGUI gameTimeText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI goldFromThisRaidText;
}
public partial class GameoverUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
        gameObject.SetActive(false);
    }
    private void Setup()
    {

    }
}
public partial class GameoverUI : MonoBehaviour // Property
{
    public void RefreshGameoverUI()
    {
        Player player = MainSystem.Instance.PlayerManager.Player;
        float playerSurvivalTime = MainSystem.Instance.GameManager.GameController.SurvivalTime;
        int minute = Mathf.FloorToInt(playerSurvivalTime / 60);
        int second = Mathf.FloorToInt(playerSurvivalTime % 60);
        string timeText = string.Format("{0:00}:{1:00}", minute, second);
        int goldFromThisRaid = MainSystem.Instance.GameManager.GameController.GoldFromThisRaid;
        bool isClear = MainSystem.Instance.GameManager.GameController.IsClear;
        string resultMessage = string.Empty;

        if (isClear)
        {
            resultMessage = "Clear!";
            MainSystem.Instance.SoundManager.SoundController.SoundEffect.PlaySfx(AudioClipName.StageClear);
        }
        else
        {
            resultMessage = "Failed..";
            MainSystem.Instance.SoundManager.SoundController.SoundEffect.PlaySfx(AudioClipName.Gameover);
        }
        gameStateText.text = isClear ? "Clear!" : "Failed";
        levelText.text = $"���� : {player.Level}";
        gameTimeText.text = $"���� �ð� : {timeText}";
        killCountText.text = $"óġ �� : {player.KillCount}";
        goldFromThisRaidText.text = $"ȹ���� ��ȭ : {goldFromThisRaid}";
        transform.SetAsLastSibling();
        gameObject.SetActive(true);
    }
}
