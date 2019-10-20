﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 mousePos;
    private RectTransform _rectTransform;
    private Image img;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        img = GetComponent<Image>();
    }

    public void OnMouseChangePos(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        Cursor.visible = false;
        img.enabled = true;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
        img.enabled = false;
    }

    private void FixedUpdate()
    {
        _rectTransform.position = mousePos;
    }
}