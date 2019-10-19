using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] [TextArea(1, 3)] private string[] sentences;
    [SerializeField] private GameObject bubble;
    [SerializeField] private float heightBubble;
    private bool triggered = false;

    public void TriggerDialogue()
    {
        if (!triggered)
        {
            DialogueManager thisBubble = GameObject.Instantiate(bubble, transform.position + heightBubble * Vector3.up, Quaternion.identity, transform).GetComponent<DialogueManager>();
            thisBubble.GiveDialogues(sentences);
            triggered = true;
        }
    }
}
