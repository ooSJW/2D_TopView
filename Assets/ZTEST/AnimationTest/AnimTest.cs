using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AnimTest : MonoBehaviour // Data Field
{
    [SerializeField] private Animator animator;
}
public partial class AnimTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            animator.SetTrigger("Run");
        else if (Input.GetKeyDown(KeyCode.A))
            animator.SetTrigger("Attack");
    }
}
