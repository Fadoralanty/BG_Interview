using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    public GameObject ToolTip;
    public ToolTipScreenSpaceUI ToolTipScreenSpaceUI;
    private static ToolTipSystem current;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        Hide();
    }

    public static void Show(string content)
    {
        current.ToolTip.SetActive(true);
        current.ToolTipScreenSpaceUI.SetText(content);
    }
    public static void Hide()
    {
        current.ToolTip.SetActive(false);
    }
}
