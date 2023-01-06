using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool canMove;
    public float _Speed;
    Rigidbody2D _rb;

    private void Awake()
    {
        canMove = true;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 dir)
    {
        if (!canMove) return;
        //transform.position += (Vector3)( _Speed * Time.deltaTime * dir);
         _rb.MovePosition(new Vector2(
              transform.position.x + dir.x *_Speed * Time.deltaTime,
              transform.position.y + dir.y *_Speed *Time.deltaTime));
        //transform.position = Vector2.Lerp(transform.position, (Vector2)transform.position + dir* _Speed, Time.deltaTime );
    }
    
    
}
