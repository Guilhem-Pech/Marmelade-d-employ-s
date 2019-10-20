using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWithChoice : Dialogue
{
    [SerializeField] protected Dialogue nextDialogue = null;
    [SerializeField] protected string[] choices;
    [SerializeField] protected int notPossibleChoice = 2;

    public override void TriggerDialogue()
    {
        if (!triggered)
        {
            DialogueManager thisBubble = GameObject.Instantiate(bubble, transform.position + heightBubble * Vector3.up, Quaternion.identity, transform).GetComponent<DialogueManager>();
            thisBubble.GiveDialogues(sentences, this, voice, false, nextDialogue, choices, notPossibleChoice);
            triggered = true;
        }
    }
}
