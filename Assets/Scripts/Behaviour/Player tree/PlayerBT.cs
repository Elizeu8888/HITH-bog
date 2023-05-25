using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

using PlayerManager;

public class PlayerBT : BT_Tree
{

    public static float speed = 2f;

    public GameObject _DeathScreen;
    public bool _InCombat = false;

    public string[] _AnimationNames;

    public Transform _Camera;

    public GameObject[] _VFX; 

    public bool inMenu;
    public static bool _ReadInMenuStatic = true;

   
    [Header("Script_OnPlayer_Refrences")]
    public static WeaponAttack _WeapAttack;
    public static PlayerHealthAndDamaged _HealthScript;
    public static EventSwitch _EventSwitch;

    Animator _AnimBase;

    public static Vector3 rootMotion;
    private CharacterController _CharacterController;


    public bool attackPressed = false, dashPressed = false, sprintPressed = false, weaponDrawPressed = false, blockPressed = false;

    public static bool menuPressed, _CanPressMenu;
    public static bool deathPressed, _CanPressDeath;
    void OnEnable()
    {

        PlayerCinematicHandler.OnLeaveMenu += LeftMenu;
        
        InputManager.OnDeathPressed += DeathPressed;
        InputManager.OnGameStart += MenuIsStarting;

        InputManager.OnAttackPressed += AttackPressed;
        InputManager.OnWeaponDrawPressed += WeaponDrawPressed;
        InputManager.OnDashPressed += DashPressed;

        InputManager.OnSprintPressed += SprintPressed;
        InputManager.OnSprintCanceled += SprintCanceled;

        InputManager.OnBlockPressed += BlockPressed;
        InputManager.OnBlockCanceled += BlockCanceled;
    }

    void OnDisable()
    {
        PlayerCinematicHandler.OnLeaveMenu -= LeftMenu;
        
        InputManager.OnDeathPressed -= DeathPressed;
        InputManager.OnGameStart -= MenuIsStarting;

        InputManager.OnAttackPressed -= AttackPressed;
        InputManager.OnWeaponDrawPressed -= WeaponDrawPressed;
        InputManager.OnDashPressed -= DashPressed;

        InputManager.OnSprintPressed -= SprintPressed;
        InputManager.OnSprintCanceled -= SprintCanceled;

        InputManager.OnBlockPressed -= BlockPressed;
        InputManager.OnBlockCanceled -= BlockCanceled;
    }
    

    void LeftMenu()
    {        
        inMenu = false;
        _ReadInMenuStatic = false;
    }

    void DeathPressed()
    {
        if(_CanPressDeath)
            deathPressed = true;
    }

    void MenuIsStarting()
    {
        StartCoroutine(MenuPressedCorou());
    }

    void AttackPressed()
    {        
        StartCoroutine(AttackPressedCorou());
    }
    void WeaponDrawPressed()
    {
        StartCoroutine(WeaponDrawPressedCorou());
    }
    void DashPressed()
    {        
        StartCoroutine(DashPressedCorou());
    }
//-----------------------------------------
    void SprintPressed()
    {
        sprintPressed = true;
    }
    void SprintCanceled()
    {
        sprintPressed = false;
    }
//-----------------------------------------
    void BlockPressed()
    {
        blockPressed = true;
    }
    void BlockCanceled()
    {
        blockPressed = false;
    }
//----------------------------------------------------
//---------------Coroutines for input-----------------
    public System.Collections.IEnumerator AttackPressedCorou()
    {
        attackPressed = true;
        yield return new WaitForSeconds(0.02f);
        attackPressed = false;
        yield return null;
    }
    public System.Collections.IEnumerator MenuPressedCorou()
    {
        menuPressed = true;
        Debug.Log("menupressed");
        yield return new WaitForSeconds(0.02f);
        menuPressed = false;
        yield return null;
    }
    public System.Collections.IEnumerator WeaponDrawPressedCorou()
    {
        weaponDrawPressed = true;
        yield return new WaitForSeconds(0.02f);
        weaponDrawPressed = false;
        yield return null;
    }
    public System.Collections.IEnumerator DashPressedCorou()
    {
        dashPressed = true;
        yield return new WaitForSeconds(0.01f);
        dashPressed = false;
        yield return null;
    }
//----------------------------------------------------
    public void SpawnVFX(GameObject item, GameObject prefab, Transform loc)// This is used in other scripts
    {
        Destroy(item);
        item = Instantiate(prefab, loc.position, Quaternion.identity);
        item.transform.parent = loc;
        item.transform.localPosition = new Vector3(0, 0, 0);
        item.transform.localRotation = Quaternion.identity;
        Destroy(item, 0.5f);
    }

    void Awake()// cant use start since Tree node uses it
    {
        
        _WeapAttack = gameObject.GetComponent<WeaponAttack>();
        _AnimationNames = _WeapAttack._AttackLightAnimNames;
        _HealthScript = gameObject.GetComponent<PlayerHealthAndDamaged>();
        _EventSwitch = gameObject.GetComponent<EventSwitch>();

        _AnimBase = gameObject.GetComponent<Animator>();
        _CharacterController = gameObject.GetComponent<CharacterController>();
    }

    void OnAnimatorMove()// this allows root motion to work
    {
        if(_CharacterController.enabled)
        {
            rootMotion = _AnimBase.deltaPosition;
            _CharacterController.Move(rootMotion);
        }

    }

    protected override Node SetupTree()// this is the behaviour tree
    {
        Node root = new Selector(new List<Node>
        {
            //here the Cut Scene branch which always goes first
            new Sequence(new List<Node>
            {
                new CheckPlayerDying(transform),
                new TaskPlayerDying(transform),
            }),



            //here the Cut Scene branch which always goes first
            new Sequence(new List<Node>
            {
                new CheckEnteredCutScene(transform),
                new TaskCutScene(transform, _Camera),
            }),

            new Sequence(new List<Node>
            {
                new CheckMoveToCutScene(transform),
                new TaskMoveToCutSceneStart(transform, _Camera),
            }),

            new Sequence(new List<Node>
            {
                new CheckInMenu(transform),
                new TaskIdle(transform),
            }),


            new Sequence(new List<Node>
            {
                new CheckGuardBroken(transform),
                new TaskGuardBroken(transform),
            }),

            new Sequence(new List<Node>
            {
                new CheckBeingDamaged(transform),
                new TaskHurt(transform),
            }),

            //here checking if the player has weapon out
            new Sequence(new List<Node>
            {
                new CheckInCombat(transform, _InCombat),
                new TaskEnteringCombat(transform),


                new Selector(new List<Node>
                {

                    new CheckDashPressed(transform),



                    new Sequence(new List<Node>
                    {
                        new CheckDashing(transform),
                        new CheckDashAttacking(transform),
                        new TaskDashAttack(transform),
                    }),
                    new Sequence(new List<Node>
                    {
                        new CheckDashing(transform),
                        new CheckRollPressed(transform),
                        new TaskPlayerRoll(transform,_Camera),

                    }),
                    new Sequence(new List<Node>
                    {
                        new CheckDashing(transform),
                        new CheckAttackPressed(transform),
                        new TaskDashAttack(transform),
                    }),
                    new Sequence(new List<Node>
                    {
                        new CheckDashing(transform),
                        new TaskDashing(transform, _Camera),

                    }),

                    //here the player is checking if block is being pressed
                    new Sequence(new List<Node>
                    {

                        new CheckBlockPressed(transform),

                        new Selector(new List<Node>
                        {
                            new Sequence(new List<Node>
                            {
                                new CheckHitByAttack(transform),
                                new Sequence(new List<Node>
                                {
                                    new TaskBlockReaction(transform),
                                }),


                            }),
                            new Sequence(new List<Node>
                            {
                                new CheckMovementPressed(transform),
                                new TaskBlockMovement(transform, _Camera),

                            }),

                        }),

                    }),

                    //here the player is checking and continuing the attack
                    new Sequence(new List<Node>
                    {  
                        
                        new CheckAttackPressed(transform),
                        new TaskLightAttack(transform, _AnimationNames),                

                    }),

                    // here the player moves when in combat
                    new Sequence(new List<Node>
                    {

                        new Selector(new List<Node>
                        {
                            new Sequence(new List<Node>
                            {
                                new CheckSprintPressed(transform),
                                new TaskSprinting(transform, _Camera),

                            }),
                            new Sequence(new List<Node>
                            {
                                new CheckMovementPressed(transform),
                                new TaskMovement(transform, _Camera),

                            }),

                        }),

                    }),

                    new TaskCombatIdle(transform,_Camera),

                }),
              
            }),


            //here is the rest of the tree when the weapon is NOT out
            new Sequence(new List<Node>
            {

                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new CheckSprintPressed(transform),
                        new TaskSprinting(transform, _Camera),

                    }),
                    new Sequence(new List<Node>
                    {
                        new CheckMovementPressed(transform),
                        new TaskMovement(transform, _Camera),

                    }),

                }),

            }),
            new TaskIdle(transform),

        });

        return root;
    }


}