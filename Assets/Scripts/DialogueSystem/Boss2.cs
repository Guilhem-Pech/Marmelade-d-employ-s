using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Dialogue
{
    public override void TriggerDialogue()
    {
        GameManager.instance.SetBerserkMode();
    }
}
