using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBarking : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    private void OnTriggerEnter2D(Collider2D c)
    {
        dialogue.TriggerDialogue();
    }
}
