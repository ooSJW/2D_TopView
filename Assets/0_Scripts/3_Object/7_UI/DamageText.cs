using DG.Tweening;
using project02;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public partial class DamageText : MonoBehaviour // Data Field
{
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Color criticalColor;
    [SerializeField] private Color originColor;

    private Transform targetTransform;
    private Vector3 offset;
    private float moveSpeed;
    private float intervalTime;
    private float displayTime;
}
public partial class DamageText : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        rectTransform.localScale = Vector3.one;
    }
    public void Initialize(Transform targetTransformValue, int damage, bool isCritical = false)
    {
        transform.SetAsFirstSibling();
        targetTransform = targetTransformValue;
        damageText.text = damage.ToString();

        Allocate();
        Setup();

        if (isCritical)
            damageText.color = criticalColor;
        else
            damageText.color = originColor;

        rectTransform.position = Camera.main.WorldToScreenPoint(targetTransform.position + offset);
    }
    private void Setup()
    {
        intervalTime = 0;
        displayTime = 1f;
        damageText.alpha = 1f;
        offset = new Vector3(0, 1f, 0);
        moveSpeed = 125;
    }
}
public partial class DamageText : MonoBehaviour // Main
{
    private void Update()
    {
        if (intervalTime < displayTime)
        {
            intervalTime += Time.deltaTime;
            damageText.alpha = Mathf.Lerp(1, 0, intervalTime / displayTime);

            Vector3 targetScreenPosition = Camera.main.WorldToScreenPoint(targetTransform.position + offset);
            targetScreenPosition.y += moveSpeed * intervalTime;
            rectTransform.position = targetScreenPosition;
        }
        else
            MainSystem.Instance.PoolManager.Despawn(gameObject);
    }
}
