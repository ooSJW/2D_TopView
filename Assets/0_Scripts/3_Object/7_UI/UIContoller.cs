using project02;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class UIContoller : MonoBehaviour // Data Field
{
    [field: SerializeField] public PlayerUI PlayerUI { get; private set; }
    [field: SerializeField] public LevelUpUI LevelUpUI { get; private set; }
    [field: SerializeField] public PauseUI PauseUI { get; private set; }
    [field: SerializeField] public LobbyUI LobbyUI { get; private set; }
    [field: SerializeField] public GameoverUI GameoverUI { get; private set; }
    [field: SerializeField] public SelectStageUI SelectStageUI { get; private set; }
    [field: SerializeField] public SelectCharacterUI SelectCharacterUI { get; private set; }

    [SerializeField] private GameObject optionUI;
    [SerializeField] private Toggle damageToggle;

    private bool isPause = false;
    public bool IsPause
    {
        get => isPause;
        set
        {
            isPause = value;
            pauseButton.interactable = !isPause;
        }
    }

    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI resumeTimerText;

    private int playerGold;
    public int PlayerGold
    {
        get => playerGold;
        set
        {
            playerGold = value;
            if (MainSystem.Instance.SceneManager.ActiveScene.name == SceneName.LobbyScene.ToString())
                LobbyUI.RefreshPlayerGold();
        }
    }
}
public partial class UIContoller : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
        if (MainSystem.Instance.SceneManager.ActiveScene.name != SceneName.LobbyScene.ToString())
        {
            PlayerUI.Initialize();
            LevelUpUI.Initialize();
            PauseUI.Initialize();
            GameoverUI.Initialize();
            resumeTimerText.gameObject.SetActive(false);
        }
        else
        {
            LobbyUI.Initialize();
            SelectStageUI.Initialize();
            SelectCharacterUI.Initialize();
        }

    }
    private void Setup()
    {
        optionUI.SetActive(false);
        damageToggle.onValueChanged.RemoveListener(ActiveDeactiveDamageText);
        damageToggle.isOn = MainSystem.Instance.UIManager.DamageTextActive;
        damageToggle.onValueChanged.AddListener(ActiveDeactiveDamageText);
    }
}
public partial class UIContoller : MonoBehaviour // Property
{
    public void OnOffOptionUI()
    {
        optionUI.SetActive(!optionUI.activeSelf);
    }
    public void ActiveDeactiveDamageText(bool value)
    {
        MainSystem.Instance.UIManager.SetDamageTextActive(value);
    }
    public void SetActiveLevelUpUI()
    {
        LevelUpUI.PlayerLevelUp();
    }
    public void OnOffStageSelectUI()
    {
        SelectStageUI.gameObject.SetActive(!SelectStageUI.gameObject.activeSelf);
    }
    public void OnOffCharacterSelectUI()
    {
        SelectCharacterUI.gameObject.SetActive(!SelectCharacterUI.gameObject.activeSelf);
    }


    public void OpenPauseUI()
    {
        MainSystem.Instance.GameManager.PauseGame();
        MainSystem.Instance.SoundManager.SoundController.BackgroundMusic.SetBackgroundMusicState(!IsPause);
        PauseUI.ReFreshPauseUI();
        PauseUI.gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        if (IsPause)
        {
            StartCoroutine(ResumeTimer(3f));
        }
    }
    public void GameOver()
    {
        MainSystem.Instance.GameManager.GameController.SaveGold();
        GameoverUI.RefreshGameoverUI();
        MainSystem.Instance.GameManager.PauseGame();
    }
    public void ChangeScene(int sceneIndex)
    {
        SceneName name = (SceneName)sceneIndex;
        MainSystem.Instance.SceneManager.LoadScene((SceneName)sceneIndex);
    }
    public void SaveGold()
    {
        MainSystem.Instance.GameManager.GameController.SaveGold();
    }
}

public partial class UIContoller : MonoBehaviour // Coroutime
{
    public IEnumerator ResumeTimer(float delayTime)
    {
        float startTime = Time.realtimeSinceStartup;
        int leftTime = (int)delayTime;
        PauseUI.gameObject.SetActive(false);
        resumeTimerText.gameObject.SetActive(true);
        while (Time.realtimeSinceStartup <= startTime + delayTime)
        {
            leftTime = Mathf.CeilToInt(startTime + delayTime - Time.realtimeSinceStartup);
            resumeTimerText.text = leftTime.ToString();
            yield return null;
        }
        resumeTimerText.gameObject.SetActive(false);
        MainSystem.Instance.GameManager.ResumeGame();
        MainSystem.Instance.SoundManager.SoundController.BackgroundMusic.SetBackgroundMusicState(!IsPause);
        yield break;
    }
}