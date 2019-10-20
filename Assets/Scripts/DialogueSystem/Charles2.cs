using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charles2 : DialogueWithChoice
{
    [SerializeField] private GameObject player;
    public override void TriggerAtEndOfDialogue()
    {
        player.gameObject.GetComponentInChildren<Animator>().SetTrigger("Write");
        player.gameObject.GetComponent<PlayerController>().blockMovement = false;
        Vector2 pos = player.transform.position;
        pos.y = -0.33f;
        player.transform.position = pos;
    }
}
