using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;
using EnemyManager;
using UnityEngine.Rendering.HighDefinition;

public class EnemyMediumBT : BT_Tree
{
    public bool _InCombat;

    public Transform _CurrentEnemyTransform;
    public GameObject plyRefrence;
    public static GameObject _Player;

    public float _Dir_Change_Timer;
    public float _PlayerDistance;
    public NavMeshAgent _NavMesh;
    public CharacterController _charControl;
    public static Transform _EnemyRestPos;
    public Transform enemyRestPos;

    float _AttackChance;
    float _RollChance;
    public bool _CanAttack = false;
    public float rollCheckTick = 0.4f;
    public float attackCheckTick = 0.4f;

    Vector3 rootMotion;
    Animator _EnemAnim;
    public float x = 0f, xmot = 0f;
    public float z = 0f, zmot = 0f;

    EnemyHealthManager _HealthMan;

    public float attackDistance = 8f, hoverDistance = 10f, backawayDistance = 6f, returnDistance = 35f, blockDistance = 7f;

    void Awake()// cant use start since Tree node uses it
    {
        _EnemyRestPos = enemyRestPos;
        _Player = plyRefrence;


        _NavMesh = gameObject.GetComponent<NavMeshAgent>();
        _charControl = gameObject.GetComponent<CharacterController>();
        _HealthMan = gameObject.GetComponent<EnemyHealthManager>();
        _EnemAnim = gameObject.GetComponent<Animator>();
        _EnemAnim.enabled = false;
        _EnemAnim.enabled = true;

        //_NavMesh.updatePosition = false;
        _NavMesh.updateRotation = false;
    }

    void OnAnimatorMove()
    {

        //_NavMesh.velocity = _EnemAnim.deltaPosition / Time.deltaTime;
    }


    void AttackChance(float chance, float minCool)// generates a chance for attack
    {

        if (attackCheckTick >= 0f)
        {
            attackCheckTick -= Time.deltaTime;
        }

        if (_AttackChance < chance)
        {
            _CanAttack = true;
            _AttackChance = 1f;
            attackCheckTick = 1f;
        }
        else
        {
            _CanAttack = false;
            if(attackCheckTick <= 0)
            {
                _AttackChance = Random.Range(0f, 1f);
                attackCheckTick = Random.Range(minCool, 2.3f);
            }
        }
    }
    void RollChance(float chance, float minCool, float maxCool)// generates a chance for Roll
    {

        if (rollCheckTick >= 0f)
        {
            rollCheckTick -= Time.deltaTime;
        }

        if (_RollChance < chance)
        {
            _HealthMan.dashing = true;
            _RollChance = 1f;
        }
        else
        {
            if (rollCheckTick <= 0)
            {
                _RollChance = Random.Range(0f, 1f);
                rollCheckTick = Random.Range(minCool, maxCool);

            }
        }
    }

    public void MovementAnim()
    {

        xmot = Vector3.Dot(_NavMesh.velocity, transform.right);
        zmot = Vector3.Dot(_NavMesh.velocity, transform.forward);

        if (zmot <= 0.5f)
        {
            _EnemAnim.SetFloat("Ydir", z = Mathf.MoveTowards(z, 1, 3f * Time.deltaTime));
        }
        if (zmot > 0.5f)
        {
            _EnemAnim.SetFloat("Ydir", z = Mathf.MoveTowards(z, -1, 3f * Time.deltaTime));
        }

        if (xmot <= 0.5f)
        {
            _EnemAnim.SetFloat("Xdir", x = Mathf.MoveTowards(x, 1, 3f * Time.deltaTime));
        }
        if (xmot > 0.5f)
        {
            _EnemAnim.SetFloat("Xdir", x = Mathf.MoveTowards(x, -1, 5f * Time.deltaTime));
        }


    }

    bool BlockChanceCheck()
    {
        if (_EnemAnim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            return false;
        }
        if (_EnemAnim.GetCurrentAnimatorStateInfo(0).IsTag("Block Reaction"))
        {
            return false;
        }
        return true;
    }

    void FixedUpdate()//use this spparingly
    {

        _PlayerDistance = Vector3.Distance(_Player.transform.position, _CurrentEnemyTransform.position);

        if(BlockChanceCheck() == true)
        {
            if (_PlayerDistance <= attackDistance && _PlayerDistance > 3f)
                AttackChance(0.65f, 0.5f);
            if (_PlayerDistance <= 3f)
                AttackChance(0.86f, 0.4f);
            if (_PlayerDistance <= 4f && _PlayerDistance >= 1.5f && _EnemAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") == false)
                RollChance(0.2f, 0.5f, 2f);
            if (_PlayerDistance <= 1.5f && _EnemAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") == false)
                RollChance(0.4f, 0.8f, 3.5f);
        }
        else
        {
            _HealthMan.dashing = false;

        }


        if (_PlayerDistance > attackDistance)
        {
            _CanAttack = false;
        }
            
    
    }

    protected override Node SetupTree()// this is the behaviour tree
    {
        Node root = new Selector(new List<Node>
        {

            new Sequence(new List<Node>
            {
                new CheckEnemyBeingDamaged(_CurrentEnemyTransform),
                new TaskMediumHurt(_CurrentEnemyTransform),

            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyRoll(_CurrentEnemyTransform),
                new TaskRoll(_CurrentEnemyTransform),

            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyBlocking(_CurrentEnemyTransform),
               

                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new CheckEnemyBeingHit(_CurrentEnemyTransform),
                        new TaskEnemyBlockReaction(_CurrentEnemyTransform),

                    }),

                     new TaskEnemyBlock(_CurrentEnemyTransform),
                }),
                
            }),
            new Sequence(new List<Node>
            {
                new CheckAttack(_CurrentEnemyTransform),
                new TaskEnemyLightAttack(_CurrentEnemyTransform),
                new TaskEnemyAttacking(_CurrentEnemyTransform),

            }),
            new Sequence(new List<Node>
            {
                new CheckIsAttacking(_CurrentEnemyTransform),
                new TaskEnemyAttacking(_CurrentEnemyTransform),

            }),
            new Sequence(new List<Node>
            {
                new CheckBackAway(_CurrentEnemyTransform, _PlayerDistance),
                new TaskBackAway(_CurrentEnemyTransform, _Dir_Change_Timer),

            }),
            new Sequence(new List<Node>
            {
                new CheckMoveToPlayer(_CurrentEnemyTransform, _PlayerDistance),
                new TaskMoveToPlayer(_CurrentEnemyTransform,_Dir_Change_Timer),            

            }),
            new Sequence(new List<Node>
            {
                new CheckReturnRest(_CurrentEnemyTransform,_NavMesh, _PlayerDistance),
                new TaskReturnRest(_CurrentEnemyTransform),

            }),
            new TaskHoverPlayer(_CurrentEnemyTransform,_PlayerDistance),

        });

        return root;
    }


}