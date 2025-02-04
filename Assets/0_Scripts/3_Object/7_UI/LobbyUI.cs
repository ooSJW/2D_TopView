using project02;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static project02.PlayerStatData;

public partial class LobbyUI : MonoBehaviour // Data Field
{

    [SerializeField] private GameObject quitUI;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private GameObject warningMessage;
    [SerializeField] private Image selectedCharacterImage;
    [SerializeField] private TextMeshProUGUI selectedCharacterName;
}
public partial class LobbyUI : MonoBehaviour // Initialize
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
        quitUI.SetActive(false);
        RefreshPlayerGold();
        warningMessage.SetActive(false);

        SetCurrentCharacterInfo();
    }
}
public partial class LobbyUI : MonoBehaviour // Property
{

    public void SetCurrentCharacterInfo()
    {
        PlayerName currentPlayerName = MainSystem.Instance.PlayerManager.PlayerName;
        PlayerStatInformation playerStatInformation = MainSystem.Instance.DataManager.PlayerStatData.GetData(currentPlayerName);
        selectedCharacterImage.sprite = Resources.Load<Sprite>(playerStatInformation.iconPath);
        selectedCharacterName.text = playerStatInformation.name;
    }

    public void OnOffQuitUI()
    {
        quitUI.SetActive(!quitUI.activeSelf);
    }
    public void RefreshPlayerGold()
    {
        goldText.text = MainSystem.Instance.DataManager.LoadPlayerGold().ToString();
    }
    public void OnOffWarningMsg()
    {
        warningMessage.SetActive(!warningMessage.activeSelf);
    }
    public void DeleteData()
    {
        MainSystem.Instance.DataManager.DeleteData();
        QuitGame();
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
