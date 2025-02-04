using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public partial class MagnetMove : MonoBehaviour // Data Field
{
    [SerializeField] private float moveSpeed;
    private Transform playerTransform;
}
public partial class MagnetMove : MonoBehaviour // Initialize
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
public partial class MagnetMove : MonoBehaviour // Main
{
    private void Update()
    {
        MoveItem();
    }
}
public partial class MagnetMove : MonoBehaviour // Property
{
    private void MoveItem()
    {
        Vector2 playerPosition = playerTransform.position;
        if (playerTransform == null || Vector2.Distance(transform.position, playerPosition) <= 0.1f)
            enabled = false;
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }
}
