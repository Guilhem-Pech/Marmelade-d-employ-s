using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DialogueStyle {basic, fast};

public class DialogueManager : MonoBehaviour
{
    private DialogueStyle style = DialogueStyle.basic;
    
    private Queue<string> sentences = new Queue<string>();
    private Dialogue nextDialogue = null;
    private string[] choices = null;

    private string currentSentence;
    private int currentPosition;
    private float counter;
    private TextMeshProUGUI text;
    private bool active = false;
    private bool autoPass = true;
    private float textDuration = 2f;
    private bool buttonExists = false;

    [SerializeField] private float slowLetterDuration = 0.06f;
    [SerializeField] private float fastLetterDuration = 0.006f;
    [SerializeField] private GameObject buttonPrefab;



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

    public void TriggerChoice(int id)
    {
        if(nextDialogue != null)
        {
            nextDialogue.TriggerDialogue();
        }
        Destroy(gameObject);
    }

    public void GiveDialogues(string[] theseSentences, bool auto = true, Dialogue next = null, string[] theseChoices = null)
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
        nextDialogue = next;
        choices = theseChoices;
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
        } else if(sentences.Count == 0 && active && autoPass)
        {
            PassDialogue();
        }
        else if(sentences.Count == 0 && active && !autoPass)
        {
            if (!buttonExists)
            {
                CreateButtons();
                buttonExists = true;
            }
        }
    }

    private void CreateButtons()
    {
        for(int i = 0; i < choices.Length; ++i)
        {
            GameObject thisChoice = GameObject.Instantiate(buttonPrefab, transform.position - Vector3.up * 0.5f * (i+1), Quaternion.identity, transform.GetComponentInChildren<Canvas>().transform);
            thisChoice.GetComponent<DialogueChoice>().Init(choices[i],this,i);
        }
    }
}
