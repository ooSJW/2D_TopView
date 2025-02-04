using Cinemachine;
using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class VirtualCamara : MonoBehaviour // Data Field
{
    [SerializeField] private Vector3 offset;
    private Transform playerTransform;
}
public partial class VirtualCamara : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        playerTransform = MainSystem.Instance.PlayerManager.Player.transform;
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
public partial class VirtualCamara : MonoBehaviour // Main
{
    private void FixedUpdate()
    {
        transform.position = playerTransform.position + offset;
    }
}
