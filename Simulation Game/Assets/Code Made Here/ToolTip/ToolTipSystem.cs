using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    public GameObject ToolTipBG;
    public GameObject ToolTipText;
    
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
        current.ToolTipBG.SetActive(true);
        current.ToolTipText.SetActive(true);
        current.ToolTipScreenSpaceUI.SetText(content);
    }
    public static void Hide()
    {
        if (current.ToolTipBG)
        {
            current.ToolTipBG.SetActive(false);
            current.ToolTipText.SetActive(false);
        }
    }
    
}
