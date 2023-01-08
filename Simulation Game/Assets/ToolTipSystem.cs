using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    public GameObject ToolTip;
    private static ToolTipSystem current;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        Hide();
    }

    public static void Show()
    {
        current.ToolTip.SetActive(true);
    }
    public static void Hide()
    {
        current.ToolTip.SetActive(false);
    }
}
