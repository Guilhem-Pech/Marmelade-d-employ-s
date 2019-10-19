using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DialogueStyle {basic, fast};

public class DialogueManager : MonoBehaviour
{
    private DialogueStyle style = DialogueStyle.basic;

    private Queue<string> sentences = new Queue<string>();

    private string currentSentence;
    private int currentPosition;
    private float counter;
    private TextMeshProUGUI text;
    private bool active = false;
    private bool autoPass = true;

    [SerializeField] private float slowLetterDuration = 0.05f;
    [SerializeField] private float fastLetterDuration = 0.2f;
    private float textDuration = 2f;



    private void Start()
    {
        counter = 0.0f;
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void PassDialogue()
    {
        if (sentences.Count > 0 && currentPosition == sentences.Peek().Length)
        {
            if (sentences.Count > 0)
            {
                currentSentence = "";
                sentences.Dequeue();
                currentPosition = 0;
                counter = 0.0f;
            }
        }
        else if (sentences.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    public void GiveDialogues(string[] theseSentences, bool auto = true)
    {
        if (theseSentences == null)
        {
            return;
        }
        for(int i = 0; i < theseSentences.Length; ++i)
        {
            sentences.Enqueue(theseSentences[i]);
        }
        active = true;
        currentPosition = 0;
        currentSentence = "";
        counter = 0.0f;
        autoPass = auto;
    }
    
    private void Update()
    {
        if (active && sentences.Count > 0)
        {
            counter += Time.deltaTime;
            if ((currentPosition < sentences.Peek().Length) && ((style != DialogueStyle.fast && counter > slowLetterDuration) || (style == DialogueStyle.fast && counter > fastLetterDuration)))
            {
                counter = 0.0f;
                if (sentences.Peek().Substring(currentPosition, 1).Equals("*"))
                {
                    ++currentPosition;
                    style = style == DialogueStyle.fast ? DialogueStyle.basic : DialogueStyle.fast;
                }
                else
                {
                    currentSentence = currentSentence + sentences.Peek().Substring(currentPosition, 1);
                    ++currentPosition;
                    text.text = currentSentence;
                }
            }
            else if(counter > textDuration && autoPass)
            {
                PassDialogue();
            }
        } else if(sentences.Count == 0 && active)
        {
            PassDialogue();
        }
    }
}
