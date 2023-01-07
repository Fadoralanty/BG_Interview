using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Action OnInteractPressed; 
    [SerializeField] private Vector2 _lookDir;
    private Movement _movement;
    private Vector2 _moveDir;
    private PlayerInput _controls;
    private PlayerActions _playerActions;


    private void Start()
    {
        
        _movement = GetComponent<Movement>();
        _controls = GetComponent<PlayerInput>();
        _playerActions = new PlayerActions();
        _playerActions.PlayerControls.Enable();
        _playerActions.PlayerControls.Interact.performed += Interact;
    }

    private void Update()
    {
        _moveDir = _playerActions.PlayerControls.Movement.ReadValue<Vector2>();
         if (_moveDir != Vector2.zero) _lookDir = _moveDir;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (context.performed) //When the interact button is pressed
        {
            OnInteractPressed?.Invoke();
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
