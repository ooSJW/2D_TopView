using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearEffect : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = MainSystem.Instance.PlayerManager.Player;
    }

    private void FixedUpdate()
    {
        transform.position = player.transform.position;
    }
}
