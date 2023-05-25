using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{

    static PlayersInput _plyInput;

    public static event Action<Vector2> OnMovePressed;
    public static event Action<Vector2> OnMoveHeld;
    public static event Action OnAttackPressed;
    public static event Action OnWeaponDrawPressed;
    public static event Action OnDashPressed;

    
    public static event Action OnRestartHeld;
    public static event Action OnRestartReleased;

    public static event Action OnGameStart;
    public static event Action OnDeathPressed;
    public static event Action OnBlockPressed;
    public static event Action OnBlockCanceled;

    public static event Action OnSprintPressed;
    public static event Action OnSprintCanceled;

    public static Vector2 movementInput;

    bool _MoveHeld;

    Vector2 lastMoveInput;


    // Start is called before the first frame update
    void Awake()
    {
        _plyInput = new PlayersInput();

    }


    void OnEnable()
    {
        _plyInput.Enable();


        _plyInput.Gameplay.ForceRestart.started += OnForceRestart;

        _plyInput.Gameplay.ForceRestart.canceled += OnForceRestartCheck;

        _plyInput.Gameplay.MenuStart.started += OnMenuIsPressed;

        _plyInput.Gameplay.DeathRestart.started += OnDeathIsPressed;

        _plyInput.Gameplay.Movement.performed += OnMovePerformed;
        _plyInput.Gameplay.Movement.canceled += OnMoveCanceled;

        _plyInput.Gameplay.Sprint.started += OnSprintStart;
        _plyInput.Gameplay.Sprint.canceled += OnSprintEnd;

        _plyInput.Gameplay.Block.started += OnBlockStart;
        _plyInput.Gameplay.Block.canceled += OnBlockEnd;

        _plyInput.Gameplay.Attack.started += OnAttackStart;
        _plyInput.Gameplay.Dash.started += OnDashStart;
        _plyInput.Gameplay.WeaponDraw.started += OnWeaponDrawStart;

    }


    void OnForceRestart(InputAction.CallbackContext context)
    {
        bool attackInput;
        attackInput = context.ReadValue<float>() > 0 ? true:false;
        if(attackInput)
        {
            OnRestartHeld?.Invoke();
        }
    }

    
    void OnForceRestartCheck(InputAction.CallbackContext context)
    {
        bool attackInput;
        attackInput = context.ReadValue<float>() > 0 ? true:false;
        if(!attackInput)
        {
            OnRestartReleased?.Invoke();
        }
    }


    void OnAttackStart(InputAction.CallbackContext context)
    {
        bool attackInput;
        attackInput = context.ReadValue<float>() > 0 ? true:false;
        if(attackInput)
        {
            OnAttackPressed?.Invoke();
        }
    }

    void OnWeaponDrawStart(InputAction.CallbackContext context)
    {
        bool WeaponDraw;
        WeaponDraw = context.ReadValue<float>() > 0 ? true : false;
        if (WeaponDraw)
        {
            OnWeaponDrawPressed?.Invoke();
        }
    }


    void OnDeathIsPressed(InputAction.CallbackContext context)
    {
        bool WeaponDraw;
        WeaponDraw = context.ReadValue<float>() > 0 ? true : false;
        if (WeaponDraw)
        {
            OnDeathPressed?.Invoke();
        }
    }
        void OnMenuIsPressed(InputAction.CallbackContext context)
    {
        bool WeaponDraw;
        WeaponDraw = context.ReadValue<float>() > 0 ? true : false;
        if (WeaponDraw)
        {
            OnGameStart?.Invoke();
        }
    }

    void OnDashStart(InputAction.CallbackContext context)
    {
        bool dash;
        dash = context.ReadValue<float>() > 0 ? true : false;
        if (dash)
        {
            OnDashPressed?.Invoke();
        }
    }

    void OnBlockStart(InputAction.CallbackContext context)
    {
        OnBlockPressed?.Invoke();      
    }
    void OnBlockEnd(InputAction.CallbackContext context)
    {
        OnBlockCanceled?.Invoke();    
    }

    void OnSprintStart(InputAction.CallbackContext context)
    {
        OnSprintPressed?.Invoke();
    }
    void OnSprintEnd(InputAction.CallbackContext context)
    {
        OnSprintCanceled?.Invoke();    
    }

    void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 moveInput;
        moveInput = context.ReadValue<Vector2>();
        OnMovePressed?.Invoke(moveInput);
        _MoveHeld = true;
        lastMoveInput = moveInput;
        movementInput = moveInput;

    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _MoveHeld = false;
        lastMoveInput = Vector2.zero;
        movementInput = Vector2.zero;
    }


    void OnDisable()
    {
        _plyInput.Disable();

        
        _plyInput.Gameplay.ForceRestart.started -= OnForceRestart;

        _plyInput.Gameplay.ForceRestart.canceled -= OnForceRestartCheck;

        _plyInput.Gameplay.MenuStart.started -= OnMenuIsPressed;

        _plyInput.Gameplay.DeathRestart.started -= OnDeathIsPressed;

        _plyInput.Gameplay.Movement.performed -= OnMovePerformed;
        _plyInput.Gameplay.Movement.canceled -= OnMoveCanceled;

        _plyInput.Gameplay.Sprint.started -= OnSprintStart;
        _plyInput.Gameplay.Sprint.canceled -= OnSprintEnd;

        _plyInput.Gameplay.Block.started -= OnBlockStart;
        _plyInput.Gameplay.Block.canceled -= OnBlockEnd;

        _plyInput.Gameplay.Attack.started -= OnAttackStart;
        _plyInput.Gameplay.Dash.started -= OnDashStart;
        _plyInput.Gameplay.WeaponDraw.started -= OnWeaponDrawStart;

    }

    //
    // Update is called once per frame
    void FixedUpdate()
    {
        if(_MoveHeld)
        {
            OnMoveHeld?.Invoke(lastMoveInput);
        }
    }
}
