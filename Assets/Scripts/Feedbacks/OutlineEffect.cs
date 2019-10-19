using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class OutlineEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer _sprite;
    private Color _originalColor;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _originalColor = _sprite.color;
    }

    private void OnMouseEnter()
    {
        _sprite.color = Color.grey;
    }

    private void OnMouseExit()
    {
        _sprite.color = _originalColor;
    }

    private void OnDisable()
    {
        _sprite.color = _originalColor;
    }
}
