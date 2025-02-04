using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class BackgroundMusic : MonoBehaviour // Data Field
{
    [SerializeField] private AudioClip backGroundMusic;
    [SerializeField] private Button onOffButton;
    [SerializeField] private Sprite onImage;
    [SerializeField] private Sprite offImage;

    private AudioSource bgmPlayer;
    private float bgmVolume;

    private bool isBgmPlay = true;
    public bool IsBgmPlay
    {
        get => isBgmPlay;
        private set
        {
            if (isBgmPlay != value)
            {
                isBgmPlay = value;
                int intValue = isBgmPlay ? 1 : 0;
                PlayerPrefs.SetInt(PlayerPrefsName.IsBgmPlay.ToString(), intValue);
                if (isBgmPlay)
                {
                    onOffButton.image.sprite = onImage;
                    if (Time.timeScale != 0)
                        bgmPlayer.Play();
                }
                else
                {
                    bgmPlayer.Pause();
                    onOffButton.image.sprite = offImage;
                }
            }
        }
    }
}
public partial class BackgroundMusic : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        bgmVolume = 0.3f;
        bgmPlayer = gameObject.AddComponent<AudioSource>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsName.IsBgmPlay.ToString()))
            isBgmPlay = PlayerPrefs.GetInt(PlayerPrefsName.IsBgmPlay.ToString()) == 0 ? false : true;

        bgmPlayer.clip = backGroundMusic;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.loop = true;
        bgmPlayer.playOnAwake = isBgmPlay;

        if (isBgmPlay)
        {
            bgmPlayer.Play();
            onOffButton.image.sprite = onImage;
        }
        else
        {
            bgmPlayer.Pause();
            onOffButton.image.sprite = offImage;
        }
    }
}
public partial class BackgroundMusic : MonoBehaviour // Property
{
    public void OnOffBgm()
    {
        IsBgmPlay = !isBgmPlay;
    }
    public void SetBackgroundMusicState(bool value)
    {
        if (IsBgmPlay)
        {
            if (value)
                bgmPlayer.UnPause();
            else
                bgmPlayer.Pause();
        }
    }
}
