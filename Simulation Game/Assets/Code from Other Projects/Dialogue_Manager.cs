using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Dialogue_Manager : MonoBehaviour
{
    public static Dialogue_Manager instance;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private Image characterPortrait;
    [SerializeField, Range(0,0.10f)] private float writeSentenceSpeed = 0.05f;
    private Queue<string> _sentences;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        _sentences = new Queue<string>();
        dialogueCanvas.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue,Sprite charPortrait)
    {
        dialogueCanvas.SetActive(true);
        nameText.text = dialogue._name;
        characterPortrait.sprite = charPortrait;
        _sentences.Clear();
        foreach (string sentence in dialogue._sentences)
        {
            _sentences.Enqueue(sentence);
        }
        string firstSentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(firstSentence));
        
        
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(writeSentenceSpeed);
        }
    }
    public void DisplayNextSentence()
    {
        if (_sentences.Count==0)
        {
             EndDialogue();
             return;
        }

        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void EndDialogue()
    {
        dialogueCanvas.SetActive(false);
    }

    public void CloseDialogue()
    {
        dialogueCanvas.SetActive(false);
    }
}
