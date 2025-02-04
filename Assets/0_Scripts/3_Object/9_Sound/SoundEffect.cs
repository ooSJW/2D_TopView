using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class SoundEffect : MonoBehaviour // Data Field
{
    [SerializeField] private AudioClip[] sfxClips;
    [SerializeField] private Button onOffButton;
    [SerializeField] private Sprite onImage;
    [SerializeField] private Sprite offImage;

    private AudioSource[] sfxPlayers;
    private float sfxVolume;

    private bool isSfxPlay = true;
    public bool IsSfxPlay
    {
        get => isSfxPlay;
        private set
        {
            if (isSfxPlay != value)
            {
                isSfxPlay = value;
                int intValue = isSfxPlay ? 1 : 0;
                PlayerPrefs.SetInt(PlayerPrefsName.IsSfxPlay.ToString(), intValue);

                if (isSfxPlay)
                    onOffButton.image.sprite = onImage;
                else
                    onOffButton.image.sprite = offImage;
            }
        }
    }
}
public partial class SoundEffect : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        sfxPlayers = new AudioSource[sfxClips.Length];
        sfxVolume = 0.5f;
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        AddBtnClickEvent();
    }
    private void Setup()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsName.IsSfxPlay.ToString()))
            isSfxPlay = PlayerPrefs.GetInt(PlayerPrefsName.IsSfxPlay.ToString()) == 0 ? false : true;

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            // 소리 동시 재생을 위해 오디오소스를 여러개 사용
            sfxPlayers[i] = gameObject.AddComponent<AudioSource>();
            sfxPlayers[i].clip = sfxClips[i];
            sfxPlayers[i].volume = sfxVolume;
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
        }

        if (isSfxPlay)
            onOffButton.image.sprite = onImage;
        else
            onOffButton.image.sprite = offImage;
    }
}
public partial class SoundEffect : MonoBehaviour // Property
{
    private void AddBtnClickEvent()
    {
        Button[] allBtn = FindObjectsOfType<Button>(true);
        Toggle[] allToggle = FindObjectsOfType<Toggle>(true);
        foreach (Button button in allBtn)
        {
            button.onClick.RemoveListener(PlayButtonClickSound);
            button.onClick.AddListener(PlayButtonClickSound);
        }
        foreach (Toggle toggle in allToggle)
        {
            toggle.onValueChanged.RemoveListener(PlayButtonClickSound);
            toggle.onValueChanged.AddListener(PlayButtonClickSound);
        }
    }

    public void OnOffSfx()
    {
        IsSfxPlay = !isSfxPlay;
    }

    public void PlayButtonClickSound()
    {
        if (IsSfxPlay)
            sfxPlayers[(int)AudioClipName.Click].Play();
    }
    public void PlayButtonClickSound(bool value)
    {
        if (IsSfxPlay)
            sfxPlayers[(int)AudioClipName.Click].Play();
    }
    public void PlaySfx(AudioClipName clipName)
    {
        if (IsSfxPlay)
            sfxPlayers[(int)clipName].Play();
    }
}
