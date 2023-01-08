using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public string content;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(WaitToShowToolTip(0.3f));
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        ToolTipSystem.Hide();
    }

    IEnumerator WaitToShowToolTip(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ToolTipSystem.Show(content);
    }
    private void OnDisable()
    {
        ToolTipSystem.Hide();
    }
}
