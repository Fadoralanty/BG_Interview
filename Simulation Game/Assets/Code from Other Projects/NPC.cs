using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] Dialogue Dialogue;
    [SerializeField] private float interactRange=2;
    [SerializeField] private PlayerController player;
    [SerializeField] private Sprite characterPortrait;
    [SerializeField]private bool _isInDialogue;
    private float _distanceToPlayer;
    private void Start()
    {
        _isInDialogue = false;
        player.OnInteractPressed += OnInteractListener;
        Dialogue_Manager.instance.OnEndDialogue += OnEndDialogueListener;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.blue;
        Gizmos.DrawWireSphere(transform.position,interactRange);
    }

    private void OnInteractListener() //Todo Wait between interact inputs
    {
        if (_isInDialogue)
        {
            Dialogue_Manager.instance.DisplayNextSentence();
        }
        if (!_isInDialogue && _distanceToPlayer <= interactRange)
        {
            _isInDialogue = true;
            Dialogue_Manager.instance.StartDialogue(Dialogue, characterPortrait);
        }
    }

    private void OnEndDialogueListener()
    {
        StopAllCoroutines();
        StartCoroutine(WaitAfterEndOfDialogue(0.5f));
    }

    private IEnumerator WaitAfterEndOfDialogue(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _isInDialogue = false;
    }
    private void Update()
    {
        Vector2 diff = player.transform.position - transform.position;
        _distanceToPlayer= diff.magnitude;
    }

    private void OnDestroy()
    {
        player.OnInteractPressed -= OnInteractListener;
    }
}
