using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public Action OnInteractPressed;
    public Action OnOpenInventory;
    public Wallet Wallet;
    public Inventory Inventory;
    
    [SerializeField] private Vector2 _lookDir;
    private Movement _movement;
    private Vector2 _moveDir;
    private PlayerActions _playerActions;
    private PlayerInput _controls;

    private void Awake()
    {
        Inventory = new Inventory();
    }

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _controls = GetComponent<PlayerInput>();
        Wallet = GetComponent<Wallet>();
        _playerActions = new PlayerActions();
        _playerActions.PlayerControls.Enable();
        _playerActions.PlayerControls.Interact.performed += Interact;
        _playerActions.PlayerControls.OpenInventory.performed += OpenInventory;
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

    private void OpenInventory(InputAction.CallbackContext context)
    {
        if (context.performed) //When the open inventory button is pressed
        {
            OnOpenInventory?.Invoke();
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
