using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueChoice : MonoBehaviour
{
    private int id;
    private DialogueManager parent = null;
    private bool notPossible;
    [SerializeField] private Color notPossibleColor;

    public void Init(string text, DialogueManager thisParent, int thisId, bool cancelled = false)
    {
        id = thisId;
        parent = thisParent;
        TextMeshProUGUI textToWrite = GetComponentInChildren<TextMeshProUGUI>();
        textToWrite.text = text;
        notPossible = cancelled;
        if (notPossible)
        {
            GetComponent<Image>().color = notPossibleColor;
        }
    }

    public void OnTrigger()
    {
        if (notPossible)
        {
            Gamefeel.Instance.InitScreenshake(Camera.main, 0.1f, 0.1f);
        }
        else
        {
            parent.TriggerChoice(id);
        }
    }
}
