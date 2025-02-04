using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StageManager : MonoBehaviour // Data Field
{
    public Spawner Spawner { get; private set; }
}
public partial class StageManager : MonoBehaviour // Initialzie
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

    }
}
public partial class StageManager : MonoBehaviour // Sign
{
    public void SignupSpawner(Spawner spawnerValue)
    {
        Spawner = spawnerValue;
        Spawner.Initialize();
    }
    public void SigndownSpawner()
    {
        Spawner = null;
    }
}
