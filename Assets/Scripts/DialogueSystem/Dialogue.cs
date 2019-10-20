using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] [TextArea(1, 3)] protected string[] sentences;
    [SerializeField] protected GameObject bubble;
    [SerializeField] protected float heightBubble = 1f;
    [SerializeField] protected int voice;
    protected bool triggered = false;
    

    public virtual void TriggerDialogue()
    {
        if (!triggered)
        {
            DialogueManager thisBubble = GameObject.Instantiate(bubble, transform.position + heightBubble * Vector3.up, Quaternion.identity, transform).GetComponent<DialogueManager>();
            thisBubble.GiveDialogues(sentences, voice);
            triggered = true;
        }
    }
}
