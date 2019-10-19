using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWithChoice : Dialogue
{
    [SerializeField] private Dialogue nextDialogue = null;
    [SerializeField] private string[] choices;

    public override void TriggerDialogue()
    {
        if (!triggered)
        {
            DialogueManager thisBubble = GameObject.Instantiate(bubble, transform.position + heightBubble * Vector3.up, Quaternion.identity, transform).GetComponent<DialogueManager>();
            thisBubble.GiveDialogues(sentences, false, nextDialogue, choices);
            triggered = true;
        }
    }
}
