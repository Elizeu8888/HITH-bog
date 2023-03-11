using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

using PlayerManager;

public class PlayerBT : BT_Tree
{

    public static float speed = 2f;

    public bool _InCombat = false;

    public string[] _AnimationNames;

    public Transform _Camera;

    [Header("Script_OnPlayer_Refrences")]
    public static WeaponAttack _WeapAttack;
    public static PlayerHealthAndDamaged _HealthScript;

    Animator _AnimBase;

    public static Vector3 rootMotion;
    private CharacterController _CharacterController;

    void Awake()
    {
        _WeapAttack = gameObject.GetComponent<WeaponAttack>();
        _HealthScript = gameObject.GetComponent<PlayerHealthAndDamaged>();

        _AnimBase = gameObject.GetComponent<Animator>();
        _CharacterController = gameObject.GetComponent<CharacterController>();
    }

    void OnAnimatorMove()
    {
        rootMotion = _AnimBase.deltaPosition;
        _CharacterController.Move(rootMotion);
    }

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            //here the Hurt branch which always goes first
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
                    //here the player is checking and continuing the attack
                    new Sequence(new List<Node>
                    {  
                        
                        new CheckAttackPressed(transform),
                        new Selector(new List<Node>
                        {
                            
                            new Sequence(new List<Node>
                            {
                                new CheckComboAttack(transform, _AnimationNames),
                                new TaskSecondLightAttack(transform, _AnimationNames),
                            }),

                            new TaskLightAttack(transform, _AnimationNames),

                        }),


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

                    new Sequence(new List<Node>
                    {
                        new CheckMovementPressed(transform),
                        new TaskMovement(transform, _Camera),

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