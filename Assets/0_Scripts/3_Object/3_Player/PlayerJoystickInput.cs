using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class PlayerJoystickInput : MonoBehaviour, IDragHandler, IEndDragHandler // Data Field
{
    private Player player;

    [SerializeField] private RectTransform joystickBackground;
    [SerializeField] private RectTransform joystickHandler;
    [SerializeField] private Vector3 offset;
}

public partial class PlayerJoystickInput : MonoBehaviour, IDragHandler, IEndDragHandler // Initialize
{
    private void Allocate()
    {
        joystickBackground.localScale = Vector3.one;
        joystickBackground.pivot = new Vector2(0.5f, 0.5f);
        joystickBackground.anchoredPosition = Vector3.zero + offset;
    }
    public void Initialize(Player playerValue)
    {
        player = playerValue;
        Allocate();
        Setup();
    }
    private void Setup()
    {
        transform.SetAsFirstSibling();
    }
}

public partial class PlayerJoystickInput : MonoBehaviour, IDragHandler, IEndDragHandler // DragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 inputDirection = eventData.position - (Vector2)joystickBackground.position;
        Vector2 joystickDirection = inputDirection; ;

        if (joystickDirection.magnitude > joystickBackground.rect.width * 0.5f)
            joystickDirection = joystickDirection.normalized * (joystickBackground.rect.width * 0.5f);
        joystickHandler.localPosition = joystickDirection;

        if (inputDirection.magnitude > 1)
            inputDirection.Normalize();

        if (player.PlayerState != PlayerState.Death && player.CanMove)
        {
            player.InputVector = inputDirection;
            player.PlayerState = PlayerState.Move;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        joystickHandler.localPosition = Vector2.zero;
        player.InputVector = Vector2.zero;
        if (player.PlayerState != PlayerState.Death)
            player.PlayerState = PlayerState.Idle;
    }
}
