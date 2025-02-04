using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundController : MonoBehaviour // Data Field
{
    [field:SerializeField] public BackgroundMusic BackgroundMusic { get; set; }
    [field:SerializeField] public SoundEffect SoundEffect { get; set; }
}
public partial class SoundController : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
        BackgroundMusic.Initialize();
        SoundEffect.Initialize();
    }
    private void Setup()
    {

    }
}
