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
    private PlayerName playerCharacterName;

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
        playerCharacterName = Enum.Parse<PlayerName>(playerStatInformation.name);

        characterImage.sprite = Resources.Load<Sprite>(playerStatInformation.iconPath);
        characterName.text = playerStatInformation.name;
        characterDescription.text = playerStatInformation.characterDescription;
        GetComponent<RectTransform>().localScale = Vector3.one;
        rockImage.enabled = false;
    }
    public void Initialize(PlayerStatInformation playerStatInformationValue)
    {
        // 빈 슬롯 UI를 풀링 후 생성 시 캐릭터 정보를 받은 후 IconPath, 이름, 설명 등의 정보를 바탕으로 슬롯UI 생성 
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


        switch (playerCharacterName)
        {
            // basicCharacter
            case PlayerName.Knight:
                selectButton.onClick.AddListener(SelectCharacter);
                unrockCostText.text = string.Empty;
                break;

            // unrock character
            default:
                bool isUnrock = MainSystem.Instance.DataManager.IsCharacterUnrock(playerCharacterName);
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
        MainSystem.Instance.PlayerManager.SignUpPlayerName(playerCharacterName);
        MainSystem.Instance.UIManager.UIContoller.OnOffCharacterSelectUI();
        MainSystem.Instance.UIManager.UIContoller.LobbyUI.SetCurrentCharacterInfo();
    }
    public void UnrockCharacter()
    {
        MainSystem.Instance.UIManager.UIContoller.SelectCharacterUI.SelectRockedCharacter(playerStatInformation);
    }
}
