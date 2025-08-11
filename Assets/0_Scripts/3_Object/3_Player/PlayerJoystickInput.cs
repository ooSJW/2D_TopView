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
    private float radius;
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
        radius = joystickBackground.rect.width * 0.5f;
    }
}

public partial class PlayerJoystickInput : MonoBehaviour, IDragHandler, IEndDragHandler // DragHandler
{
    public void OnDrag(PointerEventData eventData)
    {

        Vector2 localPoint = Vector2.zero;
        // 입력된 eventData 스크린 좌표를 조이스틱 로컬 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground, eventData.position, eventData.pressEventCamera, out localPoint);

        Vector2 clampedDirection = localPoint;

        // 조이스틱이 외부 테두리 영역을 벗어나지 않도록 예외처리.
        if (clampedDirection.magnitude > radius)
            clampedDirection = clampedDirection.normalized * radius;
        joystickHandler.localPosition = clampedDirection;


        if (player.PlayerState != PlayerState.Death && player.Moveable)
        {
            // 조이스틱 위치 이동에 따라 플레이어 속도 조절 가능
            // 최저, 최대 속도 제한
            float minMagnitude = 0.5f;
            Vector2 inputDirection = clampedDirection / radius;
            player.InputVector = inputDirection.magnitude < minMagnitude ? inputDirection.normalized * minMagnitude : inputDirection;
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
