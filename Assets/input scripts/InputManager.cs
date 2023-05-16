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

        _plyInput.Gameplay.Movement.performed += OnMovePerformed;
        _plyInput.Gameplay.Movement.canceled += OnMoveCanceled;

        _plyInput.Gameplay.Attack.started += OnAttackStart;

    }

    void OnAttackStart(InputAction.CallbackContext context)
    {
        bool attackInput;
        attackInput = context.ReadValue<float>() == 1 ? true:false;
        if(attackInput)
        {
            OnAttackPressed?.Invoke();

        }

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
