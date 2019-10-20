using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEndGame : MonoBehaviour
{
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.bersekModActivated)
            sprite.color = Color.black;
    }
}
