using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charles : DialogueWithChoice
{
    [SerializeField] private string nameSound;
    private uint currentSound;

    public override void TriggerDialogue()
    {
        if (!triggered)
        {
            DialogueManager thisBubble = GameObject.Instantiate(bubble, transform.position + heightBubble * Vector3.up, Quaternion.identity, transform).GetComponent<DialogueManager>();
            thisBubble.GiveDialogues(sentences, this, voice, false, nextDialogue, choices, notPossibleChoice);
            triggered = true;
            currentSound = AkSoundEngine.PostEvent(nameSound, gameObject);
        }
    }

    public override void TriggerAtEndOfDialogue()
    {
        AkSoundEngine.StopPlayingID(currentSound);
    }
}