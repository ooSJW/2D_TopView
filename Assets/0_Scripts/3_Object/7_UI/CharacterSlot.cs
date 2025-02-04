using project02;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static project02.PlayerStatData;
using static project02.StageData;

public partial class CharacterSlot : MonoBehaviour // Data Field
{
    public PlayerStatInformation playerStatInformation;
    private PlayerName playerName;

    [SerializeField] private Button selectButton;
    [SerializeField] private Image characterImage;
    [SerializeField] private Image rockImage;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterDescription;
    [SerializeField] private TextMeshProUGUI unrockCostText;
}
public partial class CharacterSlot : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        playerName = Enum.Parse<PlayerName>(playerStatInformation.name);

        characterImage.sprite = Resources.Load<Sprite>(playerStatInformation.iconPath);
        characterName.text = playerStatInformation.name;
        characterDescription.text = playerStatInformation.characterDescription;
        GetComponent<RectTransform>().localScale = Vector3.one;
        rockImage.enabled = false;
    }
    public void Initialize(PlayerStatInformation playerStatInformationValue)
    {
        playerStatInformation = playerStatInformationValue;
        Allocate();
        Setup();
    }
    private void Setup()
    {
        RefreshCharacterUI();
    }
    private void OnEnable()
    {
        if (playerStatInformation != null)
            RefreshCharacterUI();
    }
}
public partial class CharacterSlot : MonoBehaviour // Property
{
    public void RefreshCharacterUI()
    {
        selectButton.onClick.RemoveListener(SelectCharacter);
        selectButton.onClick.RemoveListener(UnrockCharacter);


        switch (playerName)
        {
            // basicCharacter
            case PlayerName.Knight:
                selectButton.onClick.AddListener(SelectCharacter);
                unrockCostText.text = string.Empty;
                break;

            // unrock character
            default:
                bool isUnrock = MainSystem.Instance.DataManager.IsCharacterUnrock(playerName);
                if (isUnrock)
                {
                    selectButton.onClick.AddListener(SelectCharacter);
                    unrockCostText.text = string.Empty;
                    rockImage.enabled = false;
                }
                else
                {
                    selectButton.onClick.AddListener(UnrockCharacter);
                    unrockCostText.text = $"잠금 해제 : {playerStatInformation.cost.ToString()}Coin";
                    rockImage.enabled = true;
                }

                break;
        }
        gameObject.SetActive(true);
    }
    public void SelectCharacter()
    {
        MainSystem.Instance.PlayerManager.SignUpPlayerName(playerName);
        MainSystem.Instance.UIManager.UIContoller.OnOffCharacterSelectUI();
        MainSystem.Instance.UIManager.UIContoller.LobbyUI.SetCurrentCharacterInfo();
    }
    public void UnrockCharacter()
    {
        MainSystem.Instance.UIManager.UIContoller.SelectCharacterUI.SelectRockedCharacter(playerStatInformation);
    }
}
