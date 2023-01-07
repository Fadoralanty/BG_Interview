using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] Dialogue Dialogue;
    [SerializeField] private float interactRange=2;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Sprite characterPortrait;
    
    private void OnDrawGizmos()
    {
        Gizmos.color=Color.blue;
        Gizmos.DrawWireSphere(transform.position,interactRange);
    }

    private void Update()
    {
        Vector2 diff = playerTransform.position - transform.position;
        float distance = diff.magnitude;
        if (distance < interactRange)
        {
            Dialogue_Manager.instance.StartDialogue(Dialogue,characterPortrait);
        }
        //TODO change this to new input unity
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Dialogue_Manager.instance.StartDialogue(Dialogue,characterPortrait);
        }     
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Dialogue_Manager.instance.DisplayNextSentence();
        }
    }
}
