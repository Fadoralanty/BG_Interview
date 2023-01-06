using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 _lookDir;
    private Movement _movement;
    private Vector2 _moveDir;

    private void Start()
    {
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector2(hor, ver);
        if (_moveDir != Vector2.zero)
        {
            _lookDir = _moveDir;
        }
    }

    private void FixedUpdate()
    {
        if (_moveDir != Vector2.zero)
        {
            _movement.Move(_moveDir.normalized);
        }
    }
}
