using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueChoice : MonoBehaviour
{
    private int id;
    private DialogueManager parent = null;

    public void Init(string text, DialogueManager thisParent, int thisId)
    {
        id = thisId;
        parent = thisParent;
        TextMeshProUGUI textToWrite = GetComponentInChildren<TextMeshProUGUI>();
        textToWrite.text = text;
    }

    public void OnTrigger()
    {
        parent.TriggerChoice(id);
    }
}
