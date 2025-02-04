using project02;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayerHpBar : MonoBehaviour // Data Field
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Slider playerHpSlider;
    [SerializeField] private Vector3 baseOffset;

    private Vector3 newOffset;
    private float baseWidth;
    private float baseHeight;
    private float widthRatio;
    private float heightRatio;

}
public partial class PlayerHpBar : MonoBehaviour // Data Field
{
    private void Allocate()
    {
        baseWidth = 1080;
        baseHeight = 1920;
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        float scrrnWidth = Screen.width;
        float scrrnHeight = Screen.height;
        widthRatio = scrrnWidth / baseWidth;
        heightRatio = scrrnHeight / baseHeight;
        newOffset = new Vector3(baseOffset.x * widthRatio, baseOffset.y * heightRatio, baseOffset.z);
    }
}
public partial class PlayerHpBar : MonoBehaviour // Property
{
    public void RefreshHp(int currentHp, int maxHp)
    {
        playerHpSlider.value = (float)currentHp / maxHp;
    }
    public void RepositionHpBar(Transform targetTransform)
    {
        rectTransform.position = Camera.main.WorldToScreenPoint(targetTransform.position) + newOffset;
    }
}
