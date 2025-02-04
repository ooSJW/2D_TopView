using project02;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static project02.PlayerStatData;
using static project02.StageData;

public partial class SelectCharacterUI : MonoBehaviour // Data Field
{
    [SerializeField] private Transform selectCharacterUIRoot;
    private CharacterSlot[] slotArray;
    [SerializeField] private GameObject UnrockUI;
    [SerializeField] private Button unrockButton;
    [SerializeField] private Button cancelButton;

    private PlayerStatInformation currentSelectedCharacter;
}
public partial class SelectCharacterUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        slotArray = new CharacterSlot[MainSystem.Instance.DataManager.PlayerStatData.GetPlayerStatInformationCount()];
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        for (int i = 0; i < slotArray.Length; i++)
        {
            slotArray[i] = MainSystem.Instance.PoolManager.Spawn("CharacterSlot", selectCharacterUIRoot).GetComponent<CharacterSlot>();
            PlayerStatInformation playerStatInformation = MainSystem.Instance.DataManager.PlayerStatData.GetData(i.ToString());
            slotArray[i].Initialize(playerStatInformation);
        }
        UnrockUI.SetActive(false);
        gameObject.SetActive(false);
    }
}
public partial class SelectCharacterUI : MonoBehaviour // Property
{
    public void SelectRockedCharacter(PlayerStatInformation playerStatInformationValue = null)
    {
        currentSelectedCharacter = playerStatInformationValue;

        if (currentSelectedCharacter != null &&
                MainSystem.Instance.GameManager.Gold >= currentSelectedCharacter.cost)
            unrockButton.interactable = true;
        else
            unrockButton.interactable = false;

        OnOffUnrockUI();
    }
    public void OnOffUnrockUI()
    {
        UnrockUI.transform.SetAsLastSibling();
        UnrockUI.SetActive(!UnrockUI.activeSelf);
    }
    public void UnrockCharacter()
    {
        MainSystem.Instance.GameManager.Gold -= currentSelectedCharacter.cost;
        PlayerName playerName = Enum.Parse<PlayerName>(currentSelectedCharacter.name);
        MainSystem.Instance.DataManager.SaveUnrockCharacter(playerName);
        foreach (CharacterSlot slot in slotArray)
            slot.RefreshCharacterUI();
        OnOffUnrockUI();
    }
}
