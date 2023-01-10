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
    public bool isGamePaused;
    [SerializeField] private GameObject EquipablePrefab;
    [SerializeField] private Transform Head;
    [SerializeField] private Vector2 _lookDir;
    private bool _isHeadEquipped;
    private Animator _animator;
    private Movement _movement;
    private Vector2 _moveDir;
    private PlayerActions _playerActions;
    private PlayerInput _controls;
    private void Awake()
    {
        Inventory = new Inventory();
        _isHeadEquipped = false;
    }

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _controls = GetComponent<PlayerInput>();
        Wallet = GetComponent<Wallet>();
        _animator = GetComponentInChildren<Animator>();
        _playerActions = new PlayerActions();
        _playerActions.PlayerControls.Enable();
        _playerActions.PlayerControls.Interact.performed += Interact;
        _playerActions.PlayerControls.OpenInventory.performed += OpenInventory;
        _playerActions.PlayerControls.OpenPauseMenu.performed += OpenPauseMenu;
        GameManager.instance.OnPauseToggle += OnPauseToggleListener;
    }

    private void OpenPauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed && isGamePaused)
        {
            GameManager.instance.PauseGame(false);
        }
        else if (context.performed && !isGamePaused)
        {
            GameManager.instance.PauseGame(true);
        }
    }

    private void OnPauseToggleListener(bool obj)
    {
        isGamePaused = obj;
    }

    private void Update()
    {   
        if(isGamePaused) return;
        
        _moveDir = _playerActions.PlayerControls.Movement.ReadValue<Vector2>();
         if (_moveDir != Vector2.zero) _lookDir = _moveDir;
         if (_lookDir.x > 0 || _lookDir.y > 0) 
             transform.rotation = Quaternion.Euler(0, 180, 0);
         else 
             transform.rotation = Quaternion.Euler(0, 0, 0);
         
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if(isGamePaused) return;

        if (context.performed) //When the interact button is pressed
        {
            OnInteractPressed?.Invoke();
        } 
    }

    private void OpenInventory(InputAction.CallbackContext context)
    {
        if(isGamePaused) return;

        if (context.performed) //When the open inventory button is pressed
        {
            OnOpenInventory?.Invoke();
        } 
    }

    public void EquipItem(Item item)
    {
        if (!_isHeadEquipped)
        {
            _isHeadEquipped = true;
            GameObject obj = Instantiate(EquipablePrefab, Head);
            obj.GetComponent<SpriteRenderer>().sprite = item.Icon;
        }
        else
        {
            _isHeadEquipped = false;
            GameObject obj = Head.transform.GetChild(0).gameObject;
            Destroy(obj);
        }
    }
    private void FixedUpdate()
    {
        if(isGamePaused) return;
        
        if (_moveDir != Vector2.zero)
        {
            _movement.Move(_moveDir.normalized);
            _animator.SetBool("IsRunning",true);
        }
        else
        {
            _animator.SetBool("IsRunning",false);

        }
    }

    public void StopMovement(bool canMove)
    {
        _movement.canMove = canMove;
    }
    private void OnDisable()
    {
        GameManager.instance.OnPauseToggle -= OnPauseToggleListener;
        _playerActions.PlayerControls.Interact.performed -= Interact;
        _playerActions.PlayerControls.OpenInventory.performed -= OpenInventory;
        _playerActions.PlayerControls.OpenPauseMenu.performed -= OpenPauseMenu;
    }
}
