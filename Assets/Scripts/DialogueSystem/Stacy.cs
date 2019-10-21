using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacy : Dialogue
{
    public Gate gate;
    public override void TriggerDialogue()
    {
        if (!triggered)
        {
            DialogueManager thisBubble = GameObject.Instantiate(bubble, transform.position + heightBubble * Vector3.up, Quaternion.identity, transform).GetComponent<DialogueManager>();
            thisBubble.GiveDialogues(sentences, this, voice);
            triggered = true;
            gate.Open();
        }
    }
}
