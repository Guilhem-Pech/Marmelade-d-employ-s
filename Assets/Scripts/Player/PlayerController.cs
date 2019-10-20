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
    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private bool facingRight = true;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
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
        print("PISS");
        if(hit.collider != null  && hit.transform.gameObject.CompareTag("Usable") )
        {
            print("USABLE");
            if(!GameManager.bersekModActivated)
                hit.transform.gameObject.GetComponent<Talkable>().Use(this);
            else
                hit.transform.gameObject.GetComponent<Killable>().Use(this);
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
    public void StartAutoMove(float position, float distance = 0.1f)
    {
        if (!isAutoMoving)
            automove = StartCoroutine(AutoMove(position, distance));
    }

    private IEnumerator AutoMove(float position, float distance)
    {
        float XPos = position;
        
        isAutoMoving = true;
        for (; Math.Abs(transform.position.x - XPos) > distance ; )
        {
            XAxis = transform.position.x - XPos > 0 ? -1 : 1;
            yield return null;
        }
        XAxis = 0;
        isAutoMoving = false;
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

    void Flip ()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = speedModifier * XAxis * Vector2.right;
        _animator.SetFloat(Speed,_rigidbody2D.velocity.magnitude);
        
        if(XAxis > 0 && !facingRight)
            Flip();
        else if(XAxis < 0 && facingRight)
            Flip();
    }
}
