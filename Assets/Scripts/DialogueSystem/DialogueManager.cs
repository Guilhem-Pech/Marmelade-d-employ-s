using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DialogueStyle {basic, slow, ultraslow};

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
    private int notPossibleChoice;
    private uint currentSound;
    private int idVoice;
    private Dialogue dialogue;

    [SerializeField] private float ultraSlowLetterDuration = 0.25f;
    [SerializeField] private float slowLetterDuration = 0.1f;
    [SerializeField] private float fastLetterDuration = 0.04f;
    [SerializeField] private GameObject buttonPrefab;



    private void Start()
    {
        counter = 0.0f;
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void PassDialogue()
    {
        if (buttonExists)
        {
            return;
        }
        if (sentences.Count > 0 && currentPosition == sentences.Peek().Length)
        {
            if (sentences.Count > 0)
            {
                currentSentence = "";
                sentences.Dequeue();
                if (sentences.Count != 0)
                {
                    currentSound = AkSoundEngine.PostEvent(GetVoiceName(idVoice), gameObject);
                }
                currentPosition = 0;
                counter = 0.0f;
            }
        }
        if (sentences.Count == 0)
        {
            if (nextDialogue != null)
            {
                nextDialogue.TriggerDialogue();
            }
            dialogue.TriggerAtEndOfDialogue();
            Destroy(gameObject);
        }
    }

    public void TriggerChoice(int id)
    {
        if(nextDialogue != null)
        {
            nextDialogue.TriggerDialogue();
        }
        dialogue.TriggerAtEndOfDialogue();
        Destroy(gameObject);
    }

    public void GiveDialogues(string[] theseSentences, Dialogue parent, int thisIdVoice = 0, bool auto = true, Dialogue next = null, string[] theseChoices = null, int thisNotPossibleChoice = 2)
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
        notPossibleChoice = thisNotPossibleChoice;
        idVoice = thisIdVoice;
        currentSound = AkSoundEngine.PostEvent(GetVoiceName(idVoice),gameObject);
        dialogue = parent;
    }

    private string GetVoiceName(int id)
    {
        switch (id)
        {
            case 0:
                {
                    return "Play_Voice_Text_Male";
                }
            case 1:
                {
                    return "Play_Voice_Text_Female";
                }
            case 2:
                {
                    return "Play_Voice_Text_Boss";
                }
            default:
                {
                    return "Play_Voice_Text_Female";
                }
        }
    }
    
    private void Update()
    {
        if (active && sentences.Count > 0)
        {
            counter += Time.deltaTime;
            if ((currentPosition < sentences.Peek().Length) && ((style == DialogueStyle.slow && counter > slowLetterDuration) || (style == DialogueStyle.ultraslow && counter > ultraSlowLetterDuration) || (style == DialogueStyle.basic && counter > fastLetterDuration)))
            {
                counter = 0.0f;
                if (sentences.Peek().Substring(currentPosition, 1).Equals("*"))
                {
                    ++currentPosition;
                    style = style == DialogueStyle.slow ? DialogueStyle.basic : DialogueStyle.slow;
                }
                else if (sentences.Peek().Substring(currentPosition, 1).Equals("#"))
                {
                    ++currentPosition;
                    style = style == DialogueStyle.ultraslow ? DialogueStyle.basic : DialogueStyle.ultraslow;
                }
                else
                {
                    currentSentence = currentSentence + sentences.Peek().Substring(currentPosition, 1);
                    ++currentPosition;
                    text.text = currentSentence;
                }
                if(currentPosition >= sentences.Peek().Length)
                {
                    AkSoundEngine.StopPlayingID(currentSound);

                    if (sentences.Count == 1 && choices.Length > 0)
                    {
                        if (!buttonExists)
                        {
                            CreateButtons();
                            buttonExists = true;
                        }
                    }
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
    }

    private void CreateButtons()
    {
        for(int i = 0; i < choices.Length; ++i)
        {
            GameObject thisChoice = GameObject.Instantiate(buttonPrefab, transform.position - Vector3.up * 0.6f*(i+1), Quaternion.identity, transform.GetComponentInChildren<Canvas>().transform);
            thisChoice.GetComponent<DialogueChoice>().Init(choices[i],this,i, i >= notPossibleChoice);
        }
    }
}
