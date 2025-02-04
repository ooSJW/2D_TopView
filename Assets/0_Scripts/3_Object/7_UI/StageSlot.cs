using project02;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static project02.StageData;

public partial class StageSlot : MonoBehaviour // DataField
{
    private StageInformation stageInformation;

    [SerializeField] private Button startButton;
    [SerializeField] private Image stageImage;
    [SerializeField] private Image rockImage;
    [SerializeField] private TextMeshProUGUI stageNameText;

    private StageIndex stageIndex;
}
public partial class StageSlot : MonoBehaviour // Initalzie
{
    private void Allocate()
    {
        stageIndex = Enum.Parse<StageIndex>(stageInformation.stageName);

        stageImage.sprite = Resources.Load<Sprite>(stageInformation.imagePath);
        stageNameText.text = stageInformation.stageName;
        GetComponent<RectTransform>().localScale = Vector3.one;
        rockImage.enabled = false;
    }
    public void Initialize(StageInformation stageInformationValue)
    {
        stageInformation = stageInformationValue;
        Allocate();
        Setup();
        startButton.onClick.AddListener(ChangeScene);
    }
    private void Setup()
    {
        switch (stageIndex)
        {
            case StageIndex.Stage01:
                startButton.interactable = true;
                break;

            default:
                StageIndex previousStageIndex = (StageIndex)((int)stageIndex - 1);
                bool isPreviousStageClear = MainSystem.Instance.DataManager.IsClearStage(previousStageIndex);
                if (isPreviousStageClear)
                    startButton.interactable = true;
                else
                {
                    startButton.interactable = false;
                    rockImage.enabled = true;
                }
                break;
        }

        gameObject.SetActive(true);
    }
}
public partial class StageSlot : MonoBehaviour // Initalzie
{
    public void ChangeScene()
    {
        string sceneNameString = stageIndex.ToString() + "Scene";
        if (Enum.TryParse(sceneNameString, out SceneName sceneName))
            MainSystem.Instance.SceneManager.LoadScene(sceneName);
    }
}