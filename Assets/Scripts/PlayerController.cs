using System;
using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speedModifier = 1;
    private float XAxis = 0f;
    private Vector2 mousePos = Vector2.zero;
    private Rigidbody2D _rigidbody2D;
    private bool isAutoMoving = false;
    private Coroutine automove;
    
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    public void OnMouseChangePos(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(!context.performed || Math.Abs(XAxis) > 0.2f) return;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
 
        if(hit.collider != null && hit.transform.parent && hit.transform.parent.gameObject.CompareTag("Usable") )
        {
            hit.transform.parent.gameObject.GetComponent<Usable>().Use(this);
        }

    }

    public void StartAutoMove(Talkable caller, float distance = 2f)
    {
        if (!isAutoMoving)
            automove = StartCoroutine(AutoMove(caller, distance));
    }

    private IEnumerator AutoMove(Talkable caller, float distance)
    {
        float XPos = caller.gameObject.transform.position.x;
        
        isAutoMoving = true;
        for (; Math.Abs(transform.position.x - XPos) > distance ; )
        {
            XAxis = transform.position.x - XPos > 0 ? -1 : 1;
            yield return null;
        }
        XAxis = 0;
        isAutoMoving = false;
        caller.Use(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        float key = context.ReadValue<float>();
        
        if (isAutoMoving)
        {
            StopCoroutine(automove);
            isAutoMoving = false;
        }
            
        XAxis = key;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = speedModifier * XAxis * Vector2.right;
    }
}
