using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speedModifier = 1;
    private float XAxis = 0f;
    private Vector2 mousePos = Vector2.zero;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    public void OnMouseChangePos(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
        // TODO Do a feedback when we hover an NPC / Something we can interact
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // TODO Open dialogue or something
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        XAxis = context.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = speedModifier * XAxis * Vector2.right;
    }
}
