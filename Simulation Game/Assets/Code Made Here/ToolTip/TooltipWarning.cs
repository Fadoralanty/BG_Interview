using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TooltipWarning : MonoBehaviour
{
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private RectTransform parentRectTransform;
    [SerializeField] private RectTransform bgRectTransform;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Vector2 paddingSize=new Vector2(8,8);

    private void Awake()
    {
        //SetText("hello");
    }

    public void SetText(string tooltipText)
    {
        textMeshProUGUI.text = tooltipText;
        //To dynamically adjust background to text size
        textMeshProUGUI.ForceMeshUpdate();
        Vector2 textSize = textMeshProUGUI.GetRenderedValues(false);
        bgRectTransform.sizeDelta = textSize + paddingSize;
    }

    private void Update()
    {
        Vector2 anchoredPosition = Mouse.current.position.ReadValue() / canvasRectTransform.localScale;

        parentRectTransform.anchoredPosition = anchoredPosition;
        
        Vector2 mousePos = Mouse.current.position.ReadValue();
        float pivotX = mousePos.x / Screen.width;
        float pivotY = mousePos.y / Screen.height;

        bgRectTransform.pivot = new Vector2(pivotX, pivotY);

    }
}
