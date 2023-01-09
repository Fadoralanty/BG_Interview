using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Dialogue Dialogue;
    [SerializeField] private float interactRange=2;
    [SerializeField] private PlayerController player;
    [SerializeField] private Sprite characterPortrait;
    [SerializeField] private bool _isInDialogue;
    [SerializeField] private GameObject dialogueBoxImage;
    [SerializeField] private Animator dialogueBoxAnimator;
    private float _distanceToPlayer;
    private void Start()
    {
        _isInDialogue = false;
        player.OnInteractPressed += OnInteractListener;
        Dialogue_Manager.instance.OnEndDialogue += OnEndDialogueListener;
        dialogueBoxImage.SetActive(false);
    }

    private void OnInteractListener() //TODO Wait between interact inputs
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
        //calculate distance to player every frame
        Vector2 diff = player.transform.position - transform.position;
        _distanceToPlayer= diff.magnitude;
        if (_distanceToPlayer <= interactRange)
        {
            dialogueBoxImage.SetActive(true);
            dialogueBoxAnimator.Play("Dialogue Box Pop out");
        }
        else
        {
            dialogueBoxImage.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        player.OnInteractPressed -= OnInteractListener;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color=Color.blue;
        Gizmos.DrawWireSphere(transform.position,interactRange);
    }
}
