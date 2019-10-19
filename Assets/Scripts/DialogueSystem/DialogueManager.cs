using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueStyle {basic, fast};

public class DialogueManager : MonoBehaviour
{
    private DialogueStyle style = DialogueStyle.basic;
    private Queue<string> sentences;
    private string currentSentence;
    private int currentPosition;
    private float counter;

    [SerializeField] private float slowLetterDuration;
    [SerializeField] private float fastLetterDuration;



    private void Start()
    {
        counter = 0.0f;
    }

    public void PassDialogue()
    {
        if (sentences.Count > 0 && currentPosition == currentSentence.Length - 1)
        {
            currentSentence = "";
            sentences.Dequeue();
            currentPosition = 0;
            counter = 0.0f;
        }
    }
    
    private void Update()
    {
        counter += Time.deltaTime;
        if ((style != DialogueStyle.fast && counter > slowLetterDuration) || (style == DialogueStyle.fast && counter > fastLetterDuration))
        {
            counter = 0.0f;
            ++currentPosition;
            currentSentence = sentences.Peek().Substring(0,currentPosition);
        }
    }
}
