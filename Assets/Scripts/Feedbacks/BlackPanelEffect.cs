using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPanelEffect : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
       
    }

    private void Start()
    {
        //StartCoroutine(Fade());
    }

    private void OnEnable()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        Color color = new Color(0,0,0,1);
        _spriteRenderer.color = color;
        yield return new WaitForSeconds(0.3f);
        while (_spriteRenderer.color.a > 0)
        {
            color.a -= 0.1f; 
            _spriteRenderer.color = color;
            yield return new WaitForSeconds(0.03f);
        }
        enabled = false;
    }
    
}
